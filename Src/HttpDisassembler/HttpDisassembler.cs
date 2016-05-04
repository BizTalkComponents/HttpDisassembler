using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Message.Interop;
using Microsoft.XLANGs.RuntimeTypes;
using BizTalkComponents.Utils;
using BizTalkComponents.Utilities.ComponentInstrumentation;
using System.Runtime.InteropServices;
using Microsoft.BizTalk.Streaming;

namespace BizTalkComponents.PipelineComponents.HttpDisassembler
{

    [System.Runtime.InteropServices.Guid("FE75A97A-EB7C-49AF-8778-136FA366A5F4")]
    [ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
    [ComponentCategory(CategoryTypes.CATID_DisassemblingParser)]
    public partial class HttpDisassembler : IBaseComponent, IPersistPropertyBag, IComponentUI, IDisassemblerComponent
    {
        private const string DocumentSpecNamePropertyName = "DocumentSpecName";
        private readonly Queue _outputQueue = new Queue();
        private readonly ComponentInstrumentationHelper _instrumentationHelper;

#if tracking
        public HttpDisassembler()
        {
            _instrumentationHelper = new ComponentInstrumentationHelper(new AppInsightsComponentTracker("insert key here"), Name);
        }
#else
        public HttpDisassembler()
        {
            _instrumentationHelper = new ComponentInstrumentationHelper(new TraceComponentTracker(), Name);
        }
#endif
        [RequiredRuntime]
        [DisplayName("DocumentSpecName")]
        [Description("DocumentSpec name of the schema to create an instance from.")]
        public string DocumentSpecName { get; set; }

        public void Disassemble(IPipelineContext pContext, IBaseMessage pInMsg)
        {
            string errorMessage;

            if (!Validate(out errorMessage))
            {
                var ex = new ArgumentException(errorMessage);
                _instrumentationHelper.TrackComponentError(ex);
                throw ex;
            }

            var data = pInMsg.BodyPart.GetOriginalDataStream();

            //Determine of the request body has any data. GET request will not have any body.
            var hasData = HasData(data);

            //Get a reference to the BizTalk schema.
            DocumentSpec documentSpec;
            try
            {
                documentSpec = (DocumentSpec)pContext.GetDocumentSpecByName(DocumentSpecName);
            }
            catch (COMException cex)
            {
                _instrumentationHelper.TrackComponentError(cex);
                throw cex;
            }

            //Get a list of properties defined in the schema.
            var annotations = documentSpec.GetPropertyAnnotationEnumerator();
            var doc = new XmlDocument();
            var sw = new StringWriter(new StringBuilder());

            //Create a new instance of the schema.
            doc.Load(documentSpec.CreateXmlInstance(sw));
            sw.Dispose();

            //Write all properties to the message body.
            while (annotations.MoveNext())
            {
                var annotation = (IPropertyAnnotation)annotations.Current;
                var node = doc.SelectSingleNode(annotation.XPath);
                object propertyValue;

                if (pInMsg.Context.TryRead(new ContextProperty(annotation.Name, annotation.Namespace), out propertyValue))
                {
                    node.InnerText = propertyValue.ToString();
                }
            }

            var ms = new MemoryStream();
            doc.Save(ms);
            ms.Seek(0, SeekOrigin.Begin);

            var outMsg = pInMsg;

            //If the request has a body it should be preserved an the query parameters should be written to it's own message part.
            if (hasData)
            {
                outMsg = pInMsg;
                outMsg.BodyPart.Data = pInMsg.BodyPart.Data;
                outMsg.Context = PipelineUtil.CloneMessageContext(pInMsg.Context);
                var factory = pContext.GetMessageFactory();
                var queryPart = factory.CreateMessagePart();
                queryPart.Data = ms;

                outMsg.AddPart("querypart", queryPart, false);
            }
            else
            {
                outMsg.BodyPart.Data = ms;
                //Promote message type and SchemaStrongName
                outMsg.Context.Promote(new ContextProperty(SystemProperties.MessageType), documentSpec.DocType);
                outMsg.Context.Promote(new ContextProperty(SystemProperties.SchemaStrongName), documentSpec.DocSpecStrongName);
            }

            _outputQueue.Enqueue(outMsg);
            _instrumentationHelper.TrackComponentSuccess();

        }

        public void Load(IPropertyBag propertyBag, int errorLog)
        {
            DocumentSpecName = PropertyBagHelper.ToStringOrDefault(PropertyBagHelper.ReadPropertyBag(propertyBag, DocumentSpecNamePropertyName), string.Empty);
        }

        public void Save(IPropertyBag propertyBag, bool clearDirty, bool saveAllProperties)
        {
            PropertyBagHelper.WritePropertyBag(propertyBag, DocumentSpecNamePropertyName, DocumentSpecName);
        }

        public IBaseMessage GetNext(IPipelineContext pContext)
        {
            if (_outputQueue.Count > 0)
            {
                return (IBaseMessage)_outputQueue.Dequeue();
            }

            return null;
        }

        private bool HasData(Stream data)
        {
            byte[] buffer= new byte[10];
            const int bufferSize = 0x280;
            const int thresholdSize = 0x100000;

            if (!data.CanSeek || !data.CanRead)
            {
                data = new ReadOnlySeekableStream(data, new VirtualStream(bufferSize, thresholdSize), bufferSize);
            }

            int num = data.Read(buffer, 0, buffer.Length);
            data.Seek(0, SeekOrigin.Begin);
            data.Position = 0;

            return num > 0;
        }
    }
}
