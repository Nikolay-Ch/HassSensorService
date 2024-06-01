#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace HassDeviceWorkers.HPiLO4DataReader.XmlGenerated
{
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(ElementName = "RIBCL", Namespace = "", IsNullable = false)]
    public partial class ServerNameData
    {

        private RIBCLRESPONSE rESPONSEField;

        private RIBCLSERVER_NAME sERVER_NAMEField;

        private RIBCLSERVER_OSNAME sERVER_OSNAMEField;

        private RIBCLSERVER_OSVERSION sERVER_OSVERSIONField;

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
        public RIBCLSERVER_NAME SERVER_NAME
        {
            get
            {
                return this.sERVER_NAMEField;
            }
            set
            {
                this.sERVER_NAMEField = value;
            }
        }

        /// <remarks/>
        public RIBCLSERVER_OSNAME SERVER_OSNAME
        {
            get
            {
                return this.sERVER_OSNAMEField;
            }
            set
            {
                this.sERVER_OSNAMEField = value;
            }
        }

        /// <remarks/>
        public RIBCLSERVER_OSVERSION SERVER_OSVERSION
        {
            get
            {
                return this.sERVER_OSVERSIONField;
            }
            set
            {
                this.sERVER_OSVERSIONField = value;
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
    public partial class RIBCLSERVER_NAME
    {

        private string vALUEField;

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

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class RIBCLSERVER_OSNAME
    {

        private string vALUEField;

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

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class RIBCLSERVER_OSVERSION
    {

        private string vALUEField;

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
