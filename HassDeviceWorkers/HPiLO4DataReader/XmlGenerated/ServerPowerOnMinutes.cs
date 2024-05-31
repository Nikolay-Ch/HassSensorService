#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace HassDeviceWorkers.HPiLO4DataReader.XmlGenerated
{
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    [System.Xml.Serialization.XmlRoot(ElementName = "RIBCL", Namespace = "", IsNullable = false)]
    public partial class ServerPowerOnMinutes
    {

        private RIBCLRESPONSE rESPONSEField;

        private RIBCLSERVER_POWER_ON_MINUTES sERVER_POWER_ON_MINUTESField;

        private decimal vERSIONField;

        /// <remarks/>
        public RIBCLRESPONSE RESPONSE
        {
            get
            {
                return rESPONSEField;
            }
            set
            {
                rESPONSEField = value;
            }
        }

        /// <remarks/>
        public RIBCLSERVER_POWER_ON_MINUTES SERVER_POWER_ON_MINUTES
        {
            get
            {
                return sERVER_POWER_ON_MINUTESField;
            }
            set
            {
                sERVER_POWER_ON_MINUTESField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public decimal VERSION
        {
            get
            {
                return vERSIONField;
            }
            set
            {
                vERSIONField = value;
            }
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLSERVER_POWER_ON_MINUTES
    {

        private uint vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public uint VALUE
        {
            get
            {
                return vALUEField;
            }
            set
            {
                vALUEField = value;
            }
        }
    }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
