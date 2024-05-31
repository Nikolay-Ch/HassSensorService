#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace HassDeviceWorkers.HPiLO4DataReader.XmlGenerated
{
    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLRESPONSE
    {

        private string sTATUSField;

        private string mESSAGEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string STATUS
        {
            get
            {
                return sTATUSField;
            }
            set
            {
                sTATUSField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string MESSAGE
        {
            get
            {
                return mESSAGEField;
            }
            set
            {
                mESSAGEField = value;
            }
        }
    }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
