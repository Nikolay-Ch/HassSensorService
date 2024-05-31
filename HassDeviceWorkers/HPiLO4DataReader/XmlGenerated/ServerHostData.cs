#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace HassDeviceWorkers.HPiLO4DataReader.XmlGenerated
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(ElementName = "RIBCL", Namespace = "", IsNullable = false)]
    public partial class ServerHostData
    {

        private RIBCLRESPONSE rESPONSEField;

        private RIBCLSMBIOS_RECORD[] gET_HOST_DATAField;

        private decimal vERSIONField;

        /// <remarks/>
        public RIBCLRESPONSE RESPONSE
        {
            get
            {
                return this.rESPONSEField;
            }
            set
            {
                this.rESPONSEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("SMBIOS_RECORD", IsNullable = false)]
        public RIBCLSMBIOS_RECORD[] GET_HOST_DATA
        {
            get
            {
                return this.gET_HOST_DATAField;
            }
            set
            {
                this.gET_HOST_DATAField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal VERSION
        {
            get
            {
                return this.vERSIONField;
            }
            set
            {
                this.vERSIONField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class RIBCLSMBIOS_RECORD
    {

        private RIBCLSMBIOS_RECORDFIELD[] fIELDField;

        private byte tYPEField;

        private string b64_DATAField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("FIELD")]
        public RIBCLSMBIOS_RECORDFIELD[] FIELD
        {
            get
            {
                return this.fIELDField;
            }
            set
            {
                this.fIELDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte TYPE
        {
            get
            {
                return this.tYPEField;
            }
            set
            {
                this.tYPEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string B64_DATA
        {
            get
            {
                return this.b64_DATAField;
            }
            set
            {
                this.b64_DATAField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class RIBCLSMBIOS_RECORDFIELD
    {

        private string nAMEField;

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NAME
        {
            get
            {
                return this.nAMEField;
            }
            set
            {
                this.nAMEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string VALUE
        {
            get
            {
                return this.vALUEField;
            }
            set
            {
                this.vALUEField = value;
            }
        }
    }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
