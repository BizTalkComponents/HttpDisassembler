using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Winterdom.BizTalk.PipelineTesting;
using BizTalkComponents.Utils.ContextPropertyHelpers;
using System.Runtime.InteropServices;

namespace BizTalkComponents.HttpDisassembler.Tests.UnitTests
{
    [TestClass]
    public class HttpDisassemblerTest
    {
        [TestMethod]
        public void CreateMessageTest()
        {
            var pipeline = PipelineFactory.CreateEmptyReceivePipeline();

            pipeline.AddDocSpec(typeof(TestSchema));

            var disassembler = new PipelineComponents.HttpDisassembler.HttpDisassembler
            {
                DocumentSpecName = "BizTalkComponents.HttpDisassembler.Tests.UnitTests.TestSchema"
            };

            var message = MessageHelper.CreateFromString(string.Empty);
            message.Context.Promote(new ContextProperty("http://BiztalkComponents.PropertySchema#TestProperty1"), "value1");
            message.Context.Promote(new ContextProperty("http://BiztalkComponents.PropertySchema#TestProperty2"), "value2");

            pipeline.AddComponent(disassembler, PipelineStage.Disassemble);

            var result = pipeline.Execute(message);

            Assert.AreEqual(1, result.Count);

            var doc = XDocument.Load(result[0].BodyPart.GetOriginalDataStream());

            Assert.AreEqual("value1", doc.Descendants("TestElement1").Single().Value);
            Assert.AreEqual("value2", doc.Descendants("TestElement2").Single().Value);
        }

        [TestMethod]
        [ExpectedException(typeof(COMException))]
        public void UnknownDocType()
        {
            var pipeline = PipelineFactory.CreateEmptyReceivePipeline();

            pipeline.AddDocSpec(typeof(TestSchema));

            var disassembler = new PipelineComponents.HttpDisassembler.HttpDisassembler
            {
                DocumentSpecName = "Schema"
            };

            var message = MessageHelper.CreateFromString(string.Empty);
            message.Context.Promote(new ContextProperty("http://BiztalkComponents.PropertySchema#TestProperty1"), "value1");
            message.Context.Promote(new ContextProperty("http://BiztalkComponents.PropertySchema#TestProperty2"), "value2");

            pipeline.AddComponent(disassembler, PipelineStage.Disassemble);

            var result = pipeline.Execute(message);
        }
    }
}