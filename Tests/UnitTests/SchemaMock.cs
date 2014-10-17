namespace BizTalkComponents.HttpDisassembler.Tests.UnitTests
{
    using Microsoft.XLANGs.BaseTypes;


    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.BizTalk.Schema.Compiler", "3.0.1.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [SchemaType(SchemaTypeEnum.Document)]
    [Schema(@"http://BiztalkComponents.TestSchema", @"TestSchema")]
    [Microsoft.XLANGs.BaseTypes.PropertyAttribute(typeof(global::BizTalkComponents.HttpDisassembler.Tests.UnitTests.TestProperty1), XPath = @"/*[local-name()='TestSchema' and namespace-uri()='http://BiztalkComponents.TestSchema']/*[local-name()='TestElement1' and namespace-uri()='']", XsdType = @"string")]
    [Microsoft.XLANGs.BaseTypes.PropertyAttribute(typeof(global::BizTalkComponents.HttpDisassembler.Tests.UnitTests.TestProperty2), XPath = @"/*[local-name()='TestSchema' and namespace-uri()='http://BiztalkComponents.TestSchema']/*[local-name()='TestElement2' and namespace-uri()='']", XsdType = @"string")]
    [System.SerializableAttribute()]
    [SchemaRoots(new string[] { @"TestSchema" })]
    [Microsoft.XLANGs.BaseTypes.SchemaReference(@"BizTalkComponents.HttpDisassembler.Tests.UnitTests.PropertySchema", typeof(global::BizTalkComponents.HttpDisassembler.Tests.UnitTests.PropertySchema))]
    public sealed class TestSchema : Microsoft.BizTalk.TestTools.Schema.TestableSchemaBase
    {

        [System.NonSerializedAttribute()]
        private static object _rawSchema;

        [System.NonSerializedAttribute()]
        private const string _strSchema = @"<?xml version=""1.0"" encoding=""utf-16""?>
<xs:schema xmlns=""http://BiztalkComponents.TestSchema"" xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" xmlns:ns0=""http://BiztalkComponents.PropertySchema"" targetNamespace=""http://BiztalkComponents.TestSchema"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
  <xs:annotation>
    <xs:appinfo>
      <b:imports>
        <b:namespace prefix=""ns0"" uri=""http://BiztalkComponents.PropertySchema"" location=""BizTalkComponents.HttpDisassembler.Tests.UnitTests.PropertySchema"" />
      </b:imports>
    </xs:appinfo>
  </xs:annotation>
  <xs:element name=""TestSchema"">
    <xs:annotation>
      <xs:appinfo>
        <b:properties>
          <b:property name=""ns0:TestProperty1"" xpath=""/*[local-name()='TestSchema' and namespace-uri()='http://BiztalkComponents.TestSchema']/*[local-name()='TestElement1' and namespace-uri()='']"" />
          <b:property name=""ns0:TestProperty2"" xpath=""/*[local-name()='TestSchema' and namespace-uri()='http://BiztalkComponents.TestSchema']/*[local-name()='TestElement2' and namespace-uri()='']"" />
        </b:properties>
      </xs:appinfo>
    </xs:annotation>
    <xs:complexType>
      <xs:sequence>
        <xs:element name=""TestElement1"" type=""xs:string"" />
        <xs:element name=""TestElement2"" type=""xs:string"" />
      </xs:sequence>
    </xs:complexType>
  </xs:element>
</xs:schema>";

        public TestSchema()
        {
        }

        public override string XmlContent
        {
            get
            {
                return _strSchema;
            }
        }

        public override string[] RootNodes
        {
            get
            {
                string[] _RootElements = new string[1];
                _RootElements[0] = "TestSchema";
                return _RootElements;
            }
        }

        protected override object RawSchema
        {
            get
            {
                return _rawSchema;
            }
            set
            {
                _rawSchema = value;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.BizTalk.Schema.Compiler", "3.0.1.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [SchemaType(SchemaTypeEnum.Property)]
    [System.SerializableAttribute()]
    [SchemaRoots(new string[] { @"TestProperty1", @"TestProperty2" })]
    public sealed class PropertySchema : Microsoft.BizTalk.TestTools.Schema.TestableSchemaBase
    {

        [System.NonSerializedAttribute()]
        private static object _rawSchema;

        [System.NonSerializedAttribute()]
        private const string _strSchema = @"<?xml version=""1.0"" encoding=""utf-16""?>
<xs:schema xmlns=""http://BizTalk_Server_Project1.PropertySchema1"" xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" xmlns:ns0=""http://BiztalkComponents.PropertySchema"" targetNamespace=""http://BiztalkComponents.PropertySchema"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
  <xs:annotation>
    <xs:appinfo>
      <b:schemaInfo schema_type=""property"" xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" />
    </xs:appinfo>
  </xs:annotation>
  <xs:element name=""TestProperty1"" type=""xs:string"">
    <xs:annotation>
      <xs:appinfo>
        <b:fieldInfo propertyGuid=""b4541213-68f9-4142-8607-2cbf9c03d233"" propSchFieldBase=""MessageContextPropertyBase"" />
      </xs:appinfo>
    </xs:annotation>
  </xs:element>
  <xs:element name=""TestProperty2"" type=""xs:string"">
    <xs:annotation>
      <xs:appinfo>
        <b:fieldInfo propertyGuid=""230965b9-46c0-4c6d-aa37-b8970b5d9632"" propSchFieldBase=""MessageContextPropertyBase"" />
      </xs:appinfo>
    </xs:annotation>
  </xs:element>
</xs:schema>";

        public PropertySchema()
        {
        }

        public override string XmlContent
        {
            get
            {
                return _strSchema;
            }
        }

        public override string[] RootNodes
        {
            get
            {
                string[] _RootElements = new string[2];
                _RootElements[0] = "TestProperty1";
                _RootElements[1] = "TestProperty2";
                return _RootElements;
            }
        }

        protected override object RawSchema
        {
            get
            {
                return _rawSchema;
            }
            set
            {
                _rawSchema = value;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.BizTalk.Schema.Compiler", "3.0.1.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [System.SerializableAttribute()]
    [PropertyType(@"TestProperty1", @"http://BiztalkComponents.PropertySchema", "string", "System.String")]
    [PropertyGuidAttribute(@"b4541213-68f9-4142-8607-2cbf9c03d233")]
    public sealed class TestProperty1 : Microsoft.XLANGs.BaseTypes.MessageContextPropertyBase
    {

        [System.NonSerializedAttribute()]
        private static System.Xml.XmlQualifiedName _QName = new System.Xml.XmlQualifiedName(@"TestProperty1", @"http://BiztalkComponents.PropertySchema");

        private static string PropertyValueType
        {
            get
            {
                throw new System.NotSupportedException();
            }
        }

        public override System.Xml.XmlQualifiedName Name
        {
            get
            {
                return _QName;
            }
        }

        public override System.Type Type
        {
            get
            {
                return typeof(string);
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.BizTalk.Schema.Compiler", "3.0.1.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [System.SerializableAttribute()]
    [PropertyType(@"TestProperty2", @"http://BiztalkComponents.PropertySchema", "string", "System.String")]
    [PropertyGuidAttribute(@"230965b9-46c0-4c6d-aa37-b8970b5d9632")]
    public sealed class TestProperty2 : Microsoft.XLANGs.BaseTypes.MessageContextPropertyBase
    {

        [System.NonSerializedAttribute()]
        private static System.Xml.XmlQualifiedName _QName = new System.Xml.XmlQualifiedName(@"TestProperty2", @"http://BiztalkComponents.PropertySchema");

        private static string PropertyValueType
        {
            get
            {
                throw new System.NotSupportedException();
            }
        }

        public override System.Xml.XmlQualifiedName Name
        {
            get
            {
                return _QName;
            }
        }

        public override System.Type Type
        {
            get
            {
                return typeof(string);
            }
        }
    }
}
