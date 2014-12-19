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

namespace BizTalkComponents.PipelineComponents.HttpDisassembler
{
    [System.Runtime.InteropServices.Guid("FE75A97A-EB7C-49AF-8778-136FA366A5F4")]
    [ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
    [ComponentCategory(CategoryTypes.CATID_DisassemblingParser)]
    public partial class HttpDisassembler : IBaseComponent, IPersistPropertyBag, IComponentUI, IDisassemblerComponent
    {
        private const string DocumentSpecNamePropertyName = "DocumentSpecName";
        private readonly Queue _outputQueue = new Queue();

        [RequiredRuntime]
        [DisplayName("DocumentSpecName")]
        [Description("DocumentSpec name of the schema to create an instance from.")]
        public string DocumentSpecName { get; set; }

        public void Disassemble(IPipelineContext pContext, IBaseMessage pInMsg)
        {
            string errorMessage;

            if (!Validate(out errorMessage))
            {
                throw new ArgumentException(errorMessage);
            }

            //Get a reference to the BizTalk schema.
            var documentSpec = (DocumentSpec)pContext.GetDocumentSpecByName(DocumentSpecName);

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
            outMsg.BodyPart.Data = ms;

            //Promote message type and SchemaStrongName
            outMsg.Context.Promote(new ContextProperty(SystemProperties.MessageType), documentSpec.DocType);
            outMsg.Context.Promote(new ContextProperty(SystemProperties.SchemaStrongName), documentSpec.DocSpecStrongName);

            _outputQueue.Enqueue(outMsg);

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
    }
}
