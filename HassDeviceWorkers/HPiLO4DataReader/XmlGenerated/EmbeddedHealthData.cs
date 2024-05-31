#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace HassDeviceWorkers.HPiLO4DataReader.XmlGenerated
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    [System.Xml.Serialization.XmlRoot(ElementName = "RIBCL", Namespace = "", IsNullable = false)]
    public partial class EmbeddedHealthData
    {

        private RIBCLRESPONSE rESPONSEField;

        private RIBCLGET_EMBEDDED_HEALTH_DATA gET_EMBEDDED_HEALTH_DATAField;

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
        public RIBCLGET_EMBEDDED_HEALTH_DATA GET_EMBEDDED_HEALTH_DATA
        {
            get
            {
                return gET_EMBEDDED_HEALTH_DATAField;
            }
            set
            {
                gET_EMBEDDED_HEALTH_DATAField = value;
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
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATA
    {

        private RIBCLGET_EMBEDDED_HEALTH_DATAFAN[] fANSField;

        private RIBCLGET_EMBEDDED_HEALTH_DATATEMP[] tEMPERATUREField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIES pOWER_SUPPLIESField;

        private object vRMField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAPROCESSORS pROCESSORSField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAMEMORY mEMORYField;

        private RIBCLGET_EMBEDDED_HEALTH_DATANIC[] nIC_INFORMATIONField;

        private RIBCLGET_EMBEDDED_HEALTH_DATASTORAGE sTORAGEField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATION fIRMWARE_INFORMATIONField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAHEALTH_AT_A_GLANCE hEALTH_AT_A_GLANCEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItem("FAN", IsNullable = false)]
        public RIBCLGET_EMBEDDED_HEALTH_DATAFAN[] FANS
        {
            get
            {
                return fANSField;
            }
            set
            {
                fANSField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItem("TEMP", IsNullable = false)]
        public RIBCLGET_EMBEDDED_HEALTH_DATATEMP[] TEMPERATURE
        {
            get
            {
                return tEMPERATUREField;
            }
            set
            {
                tEMPERATUREField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIES POWER_SUPPLIES
        {
            get
            {
                return pOWER_SUPPLIESField;
            }
            set
            {
                pOWER_SUPPLIESField = value;
            }
        }

        /// <remarks/>
        public object VRM
        {
            get
            {
                return vRMField;
            }
            set
            {
                vRMField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAPROCESSORS PROCESSORS
        {
            get
            {
                return pROCESSORSField;
            }
            set
            {
                pROCESSORSField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAMEMORY MEMORY
        {
            get
            {
                return mEMORYField;
            }
            set
            {
                mEMORYField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItem("NIC", IsNullable = false)]
        public RIBCLGET_EMBEDDED_HEALTH_DATANIC[] NIC_INFORMATION
        {
            get
            {
                return nIC_INFORMATIONField;
            }
            set
            {
                nIC_INFORMATIONField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATASTORAGE STORAGE
        {
            get
            {
                return sTORAGEField;
            }
            set
            {
                sTORAGEField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATION FIRMWARE_INFORMATION
        {
            get
            {
                return fIRMWARE_INFORMATIONField;
            }
            set
            {
                fIRMWARE_INFORMATIONField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAHEALTH_AT_A_GLANCE HEALTH_AT_A_GLANCE
        {
            get
            {
                return hEALTH_AT_A_GLANCEField;
            }
            set
            {
                hEALTH_AT_A_GLANCEField = value;
            }
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAFAN
    {

        private RIBCLGET_EMBEDDED_HEALTH_DATAFANZONE zONEField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAFANLABEL lABELField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAFANSTATUS sTATUSField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAFANSPEED sPEEDField;

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAFANZONE ZONE
        {
            get
            {
                return zONEField;
            }
            set
            {
                zONEField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAFANLABEL LABEL
        {
            get
            {
                return lABELField;
            }
            set
            {
                lABELField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAFANSTATUS STATUS
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
        public RIBCLGET_EMBEDDED_HEALTH_DATAFANSPEED SPEED
        {
            get
            {
                return sPEEDField;
            }
            set
            {
                sPEEDField = value;
            }
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAFANZONE
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAFANLABEL
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAFANSTATUS
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAFANSPEED
    {

        private byte vALUEField;

        private string uNITField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public byte VALUE
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string UNIT
        {
            get
            {
                return uNITField;
            }
            set
            {
                uNITField = value;
            }
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATATEMP
    {

        private RIBCLGET_EMBEDDED_HEALTH_DATATEMPLABEL lABELField;

        private RIBCLGET_EMBEDDED_HEALTH_DATATEMPLOCATION lOCATIONField;

        private RIBCLGET_EMBEDDED_HEALTH_DATATEMPSTATUS sTATUSField;

        private RIBCLGET_EMBEDDED_HEALTH_DATATEMPCURRENTREADING cURRENTREADINGField;

        private RIBCLGET_EMBEDDED_HEALTH_DATATEMPCAUTION cAUTIONField;

        private RIBCLGET_EMBEDDED_HEALTH_DATATEMPCRITICAL cRITICALField;

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATATEMPLABEL LABEL
        {
            get
            {
                return lABELField;
            }
            set
            {
                lABELField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATATEMPLOCATION LOCATION
        {
            get
            {
                return lOCATIONField;
            }
            set
            {
                lOCATIONField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATATEMPSTATUS STATUS
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
        public RIBCLGET_EMBEDDED_HEALTH_DATATEMPCURRENTREADING CURRENTREADING
        {
            get
            {
                return cURRENTREADINGField;
            }
            set
            {
                cURRENTREADINGField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATATEMPCAUTION CAUTION
        {
            get
            {
                return cAUTIONField;
            }
            set
            {
                cAUTIONField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATATEMPCRITICAL CRITICAL
        {
            get
            {
                return cRITICALField;
            }
            set
            {
                cRITICALField = value;
            }
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATATEMPLABEL
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATATEMPLOCATION
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATATEMPSTATUS
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATATEMPCURRENTREADING
    {

        private string vALUEField;

        private string uNITField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string UNIT
        {
            get
            {
                return uNITField;
            }
            set
            {
                uNITField = value;
            }
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATATEMPCAUTION
    {

        private string vALUEField;

        private string uNITField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string UNIT
        {
            get
            {
                return uNITField;
            }
            set
            {
                uNITField = value;
            }
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATATEMPCRITICAL
    {

        private string vALUEField;

        private string uNITField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string UNIT
        {
            get
            {
                return uNITField;
            }
            set
            {
                uNITField = value;
            }
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIES
    {

        private RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESPOWER_SUPPLY_SUMMARY pOWER_SUPPLY_SUMMARYField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESSUPPLY[] sUPPLYField;

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESPOWER_SUPPLY_SUMMARY POWER_SUPPLY_SUMMARY
        {
            get
            {
                return pOWER_SUPPLY_SUMMARYField;
            }
            set
            {
                pOWER_SUPPLY_SUMMARYField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("SUPPLY")]
        public RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESSUPPLY[] SUPPLY
        {
            get
            {
                return sUPPLYField;
            }
            set
            {
                sUPPLYField = value;
            }
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESPOWER_SUPPLY_SUMMARY
    {

        private RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESPOWER_SUPPLY_SUMMARYPRESENT_POWER_READING pRESENT_POWER_READINGField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESPOWER_SUPPLY_SUMMARYPOWER_MANAGEMENT_CONTROLLER_FIRMWARE_VERSION pOWER_MANAGEMENT_CONTROLLER_FIRMWARE_VERSIONField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESPOWER_SUPPLY_SUMMARYHP_POWER_DISCOVERY_SERVICES_REDUNDANCY_STATUS hP_POWER_DISCOVERY_SERVICES_REDUNDANCY_STATUSField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESPOWER_SUPPLY_SUMMARYHIGH_EFFICIENCY_MODE hIGH_EFFICIENCY_MODEField;

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESPOWER_SUPPLY_SUMMARYPRESENT_POWER_READING PRESENT_POWER_READING
        {
            get
            {
                return pRESENT_POWER_READINGField;
            }
            set
            {
                pRESENT_POWER_READINGField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESPOWER_SUPPLY_SUMMARYPOWER_MANAGEMENT_CONTROLLER_FIRMWARE_VERSION POWER_MANAGEMENT_CONTROLLER_FIRMWARE_VERSION
        {
            get
            {
                return pOWER_MANAGEMENT_CONTROLLER_FIRMWARE_VERSIONField;
            }
            set
            {
                pOWER_MANAGEMENT_CONTROLLER_FIRMWARE_VERSIONField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESPOWER_SUPPLY_SUMMARYHP_POWER_DISCOVERY_SERVICES_REDUNDANCY_STATUS HP_POWER_DISCOVERY_SERVICES_REDUNDANCY_STATUS
        {
            get
            {
                return hP_POWER_DISCOVERY_SERVICES_REDUNDANCY_STATUSField;
            }
            set
            {
                hP_POWER_DISCOVERY_SERVICES_REDUNDANCY_STATUSField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESPOWER_SUPPLY_SUMMARYHIGH_EFFICIENCY_MODE HIGH_EFFICIENCY_MODE
        {
            get
            {
                return hIGH_EFFICIENCY_MODEField;
            }
            set
            {
                hIGH_EFFICIENCY_MODEField = value;
            }
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESPOWER_SUPPLY_SUMMARYPRESENT_POWER_READING
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESPOWER_SUPPLY_SUMMARYPOWER_MANAGEMENT_CONTROLLER_FIRMWARE_VERSION
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESPOWER_SUPPLY_SUMMARYHP_POWER_DISCOVERY_SERVICES_REDUNDANCY_STATUS
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESPOWER_SUPPLY_SUMMARYHIGH_EFFICIENCY_MODE
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESSUPPLY
    {

        private RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESSUPPLYLABEL lABELField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESSUPPLYPRESENT pRESENTField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESSUPPLYSTATUS sTATUSField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESSUPPLYPDS pDSField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESSUPPLYHOTPLUG_CAPABLE hOTPLUG_CAPABLEField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESSUPPLYMODEL mODELField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESSUPPLYSPARE sPAREField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESSUPPLYSERIAL_NUMBER sERIAL_NUMBERField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESSUPPLYCAPACITY cAPACITYField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESSUPPLYFIRMWARE_VERSION fIRMWARE_VERSIONField;

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESSUPPLYLABEL LABEL
        {
            get
            {
                return lABELField;
            }
            set
            {
                lABELField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESSUPPLYPRESENT PRESENT
        {
            get
            {
                return pRESENTField;
            }
            set
            {
                pRESENTField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESSUPPLYSTATUS STATUS
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
        public RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESSUPPLYPDS PDS
        {
            get
            {
                return pDSField;
            }
            set
            {
                pDSField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESSUPPLYHOTPLUG_CAPABLE HOTPLUG_CAPABLE
        {
            get
            {
                return hOTPLUG_CAPABLEField;
            }
            set
            {
                hOTPLUG_CAPABLEField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESSUPPLYMODEL MODEL
        {
            get
            {
                return mODELField;
            }
            set
            {
                mODELField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESSUPPLYSPARE SPARE
        {
            get
            {
                return sPAREField;
            }
            set
            {
                sPAREField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESSUPPLYSERIAL_NUMBER SERIAL_NUMBER
        {
            get
            {
                return sERIAL_NUMBERField;
            }
            set
            {
                sERIAL_NUMBERField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESSUPPLYCAPACITY CAPACITY
        {
            get
            {
                return cAPACITYField;
            }
            set
            {
                cAPACITYField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESSUPPLYFIRMWARE_VERSION FIRMWARE_VERSION
        {
            get
            {
                return fIRMWARE_VERSIONField;
            }
            set
            {
                fIRMWARE_VERSIONField = value;
            }
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESSUPPLYLABEL
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESSUPPLYPRESENT
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESSUPPLYSTATUS
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESSUPPLYPDS
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESSUPPLYHOTPLUG_CAPABLE
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESSUPPLYMODEL
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESSUPPLYSPARE
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESSUPPLYSERIAL_NUMBER
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESSUPPLYCAPACITY
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAPOWER_SUPPLIESSUPPLYFIRMWARE_VERSION
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAPROCESSORS
    {

        private RIBCLGET_EMBEDDED_HEALTH_DATAPROCESSORSPROCESSOR pROCESSORField;

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAPROCESSORSPROCESSOR PROCESSOR
        {
            get
            {
                return pROCESSORField;
            }
            set
            {
                pROCESSORField = value;
            }
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAPROCESSORSPROCESSOR
    {

        private RIBCLGET_EMBEDDED_HEALTH_DATAPROCESSORSPROCESSORLABEL lABELField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAPROCESSORSPROCESSORNAME nAMEField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAPROCESSORSPROCESSORSTATUS sTATUSField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAPROCESSORSPROCESSORSPEED sPEEDField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAPROCESSORSPROCESSOREXECUTION_TECHNOLOGY eXECUTION_TECHNOLOGYField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAPROCESSORSPROCESSORMEMORY_TECHNOLOGY mEMORY_TECHNOLOGYField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAPROCESSORSPROCESSORINTERNAL_L1_CACHE iNTERNAL_L1_CACHEField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAPROCESSORSPROCESSORINTERNAL_L2_CACHE iNTERNAL_L2_CACHEField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAPROCESSORSPROCESSORINTERNAL_L3_CACHE iNTERNAL_L3_CACHEField;

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAPROCESSORSPROCESSORLABEL LABEL
        {
            get
            {
                return lABELField;
            }
            set
            {
                lABELField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAPROCESSORSPROCESSORNAME NAME
        {
            get
            {
                return nAMEField;
            }
            set
            {
                nAMEField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAPROCESSORSPROCESSORSTATUS STATUS
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
        public RIBCLGET_EMBEDDED_HEALTH_DATAPROCESSORSPROCESSORSPEED SPEED
        {
            get
            {
                return sPEEDField;
            }
            set
            {
                sPEEDField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAPROCESSORSPROCESSOREXECUTION_TECHNOLOGY EXECUTION_TECHNOLOGY
        {
            get
            {
                return eXECUTION_TECHNOLOGYField;
            }
            set
            {
                eXECUTION_TECHNOLOGYField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAPROCESSORSPROCESSORMEMORY_TECHNOLOGY MEMORY_TECHNOLOGY
        {
            get
            {
                return mEMORY_TECHNOLOGYField;
            }
            set
            {
                mEMORY_TECHNOLOGYField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAPROCESSORSPROCESSORINTERNAL_L1_CACHE INTERNAL_L1_CACHE
        {
            get
            {
                return iNTERNAL_L1_CACHEField;
            }
            set
            {
                iNTERNAL_L1_CACHEField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAPROCESSORSPROCESSORINTERNAL_L2_CACHE INTERNAL_L2_CACHE
        {
            get
            {
                return iNTERNAL_L2_CACHEField;
            }
            set
            {
                iNTERNAL_L2_CACHEField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAPROCESSORSPROCESSORINTERNAL_L3_CACHE INTERNAL_L3_CACHE
        {
            get
            {
                return iNTERNAL_L3_CACHEField;
            }
            set
            {
                iNTERNAL_L3_CACHEField = value;
            }
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAPROCESSORSPROCESSORLABEL
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAPROCESSORSPROCESSORNAME
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAPROCESSORSPROCESSORSTATUS
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAPROCESSORSPROCESSORSPEED
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAPROCESSORSPROCESSOREXECUTION_TECHNOLOGY
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAPROCESSORSPROCESSORMEMORY_TECHNOLOGY
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAPROCESSORSPROCESSORINTERNAL_L1_CACHE
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAPROCESSORSPROCESSORINTERNAL_L2_CACHE
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAPROCESSORSPROCESSORINTERNAL_L3_CACHE
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAMEMORY
    {

        private RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYADVANCED_MEMORY_PROTECTION aDVANCED_MEMORY_PROTECTIONField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYMEMORY_DETAILS_SUMMARY mEMORY_DETAILS_SUMMARYField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYCPU_1[] mEMORY_DETAILSField;

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYADVANCED_MEMORY_PROTECTION ADVANCED_MEMORY_PROTECTION
        {
            get
            {
                return aDVANCED_MEMORY_PROTECTIONField;
            }
            set
            {
                aDVANCED_MEMORY_PROTECTIONField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYMEMORY_DETAILS_SUMMARY MEMORY_DETAILS_SUMMARY
        {
            get
            {
                return mEMORY_DETAILS_SUMMARYField;
            }
            set
            {
                mEMORY_DETAILS_SUMMARYField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItem("CPU_1", IsNullable = false)]
        public RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYCPU_1[] MEMORY_DETAILS
        {
            get
            {
                return mEMORY_DETAILSField;
            }
            set
            {
                mEMORY_DETAILSField = value;
            }
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYADVANCED_MEMORY_PROTECTION
    {

        private RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYADVANCED_MEMORY_PROTECTIONAMP_MODE_STATUS aMP_MODE_STATUSField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYADVANCED_MEMORY_PROTECTIONCONFIGURED_AMP_MODE cONFIGURED_AMP_MODEField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYADVANCED_MEMORY_PROTECTIONAVAILABLE_AMP_MODES aVAILABLE_AMP_MODESField;

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYADVANCED_MEMORY_PROTECTIONAMP_MODE_STATUS AMP_MODE_STATUS
        {
            get
            {
                return aMP_MODE_STATUSField;
            }
            set
            {
                aMP_MODE_STATUSField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYADVANCED_MEMORY_PROTECTIONCONFIGURED_AMP_MODE CONFIGURED_AMP_MODE
        {
            get
            {
                return cONFIGURED_AMP_MODEField;
            }
            set
            {
                cONFIGURED_AMP_MODEField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYADVANCED_MEMORY_PROTECTIONAVAILABLE_AMP_MODES AVAILABLE_AMP_MODES
        {
            get
            {
                return aVAILABLE_AMP_MODESField;
            }
            set
            {
                aVAILABLE_AMP_MODESField = value;
            }
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYADVANCED_MEMORY_PROTECTIONAMP_MODE_STATUS
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYADVANCED_MEMORY_PROTECTIONCONFIGURED_AMP_MODE
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYADVANCED_MEMORY_PROTECTIONAVAILABLE_AMP_MODES
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYMEMORY_DETAILS_SUMMARY
    {

        private RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYMEMORY_DETAILS_SUMMARYCPU_1 cPU_1Field;

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYMEMORY_DETAILS_SUMMARYCPU_1 CPU_1
        {
            get
            {
                return cPU_1Field;
            }
            set
            {
                cPU_1Field = value;
            }
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYMEMORY_DETAILS_SUMMARYCPU_1
    {

        private RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYMEMORY_DETAILS_SUMMARYCPU_1NUMBER_OF_SOCKETS nUMBER_OF_SOCKETSField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYMEMORY_DETAILS_SUMMARYCPU_1TOTAL_MEMORY_SIZE tOTAL_MEMORY_SIZEField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYMEMORY_DETAILS_SUMMARYCPU_1OPERATING_FREQUENCY oPERATING_FREQUENCYField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYMEMORY_DETAILS_SUMMARYCPU_1OPERATING_VOLTAGE oPERATING_VOLTAGEField;

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYMEMORY_DETAILS_SUMMARYCPU_1NUMBER_OF_SOCKETS NUMBER_OF_SOCKETS
        {
            get
            {
                return nUMBER_OF_SOCKETSField;
            }
            set
            {
                nUMBER_OF_SOCKETSField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYMEMORY_DETAILS_SUMMARYCPU_1TOTAL_MEMORY_SIZE TOTAL_MEMORY_SIZE
        {
            get
            {
                return tOTAL_MEMORY_SIZEField;
            }
            set
            {
                tOTAL_MEMORY_SIZEField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYMEMORY_DETAILS_SUMMARYCPU_1OPERATING_FREQUENCY OPERATING_FREQUENCY
        {
            get
            {
                return oPERATING_FREQUENCYField;
            }
            set
            {
                oPERATING_FREQUENCYField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYMEMORY_DETAILS_SUMMARYCPU_1OPERATING_VOLTAGE OPERATING_VOLTAGE
        {
            get
            {
                return oPERATING_VOLTAGEField;
            }
            set
            {
                oPERATING_VOLTAGEField = value;
            }
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYMEMORY_DETAILS_SUMMARYCPU_1NUMBER_OF_SOCKETS
    {

        private byte vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public byte VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYMEMORY_DETAILS_SUMMARYCPU_1TOTAL_MEMORY_SIZE
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYMEMORY_DETAILS_SUMMARYCPU_1OPERATING_FREQUENCY
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYMEMORY_DETAILS_SUMMARYCPU_1OPERATING_VOLTAGE
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYCPU_1
    {

        private RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYCPU_1SOCKET sOCKETField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYCPU_1STATUS sTATUSField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYCPU_1HP_SMART_MEMORY hP_SMART_MEMORYField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYCPU_1PART pARTField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYCPU_1TYPE tYPEField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYCPU_1SIZE sIZEField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYCPU_1FREQUENCY fREQUENCYField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYCPU_1MINIMUM_VOLTAGE mINIMUM_VOLTAGEField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYCPU_1RANKS rANKSField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYCPU_1TECHNOLOGY tECHNOLOGYField;

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYCPU_1SOCKET SOCKET
        {
            get
            {
                return sOCKETField;
            }
            set
            {
                sOCKETField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYCPU_1STATUS STATUS
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
        public RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYCPU_1HP_SMART_MEMORY HP_SMART_MEMORY
        {
            get
            {
                return hP_SMART_MEMORYField;
            }
            set
            {
                hP_SMART_MEMORYField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYCPU_1PART PART
        {
            get
            {
                return pARTField;
            }
            set
            {
                pARTField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYCPU_1TYPE TYPE
        {
            get
            {
                return tYPEField;
            }
            set
            {
                tYPEField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYCPU_1SIZE SIZE
        {
            get
            {
                return sIZEField;
            }
            set
            {
                sIZEField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYCPU_1FREQUENCY FREQUENCY
        {
            get
            {
                return fREQUENCYField;
            }
            set
            {
                fREQUENCYField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYCPU_1MINIMUM_VOLTAGE MINIMUM_VOLTAGE
        {
            get
            {
                return mINIMUM_VOLTAGEField;
            }
            set
            {
                mINIMUM_VOLTAGEField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYCPU_1RANKS RANKS
        {
            get
            {
                return rANKSField;
            }
            set
            {
                rANKSField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYCPU_1TECHNOLOGY TECHNOLOGY
        {
            get
            {
                return tECHNOLOGYField;
            }
            set
            {
                tECHNOLOGYField = value;
            }
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYCPU_1SOCKET
    {

        private byte vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public byte VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYCPU_1STATUS
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYCPU_1HP_SMART_MEMORY
    {

        private string vALUEField;

        private string typeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string Type
        {
            get
            {
                return typeField;
            }
            set
            {
                typeField = value;
            }
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYCPU_1PART
    {

        private string nUMBERField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string NUMBER
        {
            get
            {
                return nUMBERField;
            }
            set
            {
                nUMBERField = value;
            }
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYCPU_1TYPE
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYCPU_1SIZE
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYCPU_1FREQUENCY
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYCPU_1MINIMUM_VOLTAGE
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYCPU_1RANKS
    {

        private byte vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public byte VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAMEMORYCPU_1TECHNOLOGY
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATANIC
    {

        private RIBCLGET_EMBEDDED_HEALTH_DATANICNETWORK_PORT nETWORK_PORTField;

        private RIBCLGET_EMBEDDED_HEALTH_DATANICPORT_DESCRIPTION pORT_DESCRIPTIONField;

        private RIBCLGET_EMBEDDED_HEALTH_DATANICLOCATION lOCATIONField;

        private RIBCLGET_EMBEDDED_HEALTH_DATANICMAC_ADDRESS mAC_ADDRESSField;

        private RIBCLGET_EMBEDDED_HEALTH_DATANICIP_ADDRESS iP_ADDRESSField;

        private RIBCLGET_EMBEDDED_HEALTH_DATANICSTATUS sTATUSField;

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATANICNETWORK_PORT NETWORK_PORT
        {
            get
            {
                return nETWORK_PORTField;
            }
            set
            {
                nETWORK_PORTField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATANICPORT_DESCRIPTION PORT_DESCRIPTION
        {
            get
            {
                return pORT_DESCRIPTIONField;
            }
            set
            {
                pORT_DESCRIPTIONField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATANICLOCATION LOCATION
        {
            get
            {
                return lOCATIONField;
            }
            set
            {
                lOCATIONField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATANICMAC_ADDRESS MAC_ADDRESS
        {
            get
            {
                return mAC_ADDRESSField;
            }
            set
            {
                mAC_ADDRESSField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATANICIP_ADDRESS IP_ADDRESS
        {
            get
            {
                return iP_ADDRESSField;
            }
            set
            {
                iP_ADDRESSField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATANICSTATUS STATUS
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
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATANICNETWORK_PORT
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATANICPORT_DESCRIPTION
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATANICLOCATION
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATANICMAC_ADDRESS
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATANICIP_ADDRESS
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATANICSTATUS
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATASTORAGE
    {

        private RIBCLGET_EMBEDDED_HEALTH_DATASTORAGEDISCOVERY_STATUS dISCOVERY_STATUSField;

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATASTORAGEDISCOVERY_STATUS DISCOVERY_STATUS
        {
            get
            {
                return dISCOVERY_STATUSField;
            }
            set
            {
                dISCOVERY_STATUSField = value;
            }
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATASTORAGEDISCOVERY_STATUS
    {

        private RIBCLGET_EMBEDDED_HEALTH_DATASTORAGEDISCOVERY_STATUSSTATUS sTATUSField;

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATASTORAGEDISCOVERY_STATUSSTATUS STATUS
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
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATASTORAGEDISCOVERY_STATUSSTATUS
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATION
    {

        private RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_1 iNDEX_1Field;

        private RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_2 iNDEX_2Field;

        private RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_3 iNDEX_3Field;

        private RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_4 iNDEX_4Field;

        private RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_5 iNDEX_5Field;

        private RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_6 iNDEX_6Field;

        private RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_7 iNDEX_7Field;

        private RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_8 iNDEX_8Field;

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_1 INDEX_1
        {
            get
            {
                return iNDEX_1Field;
            }
            set
            {
                iNDEX_1Field = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_2 INDEX_2
        {
            get
            {
                return iNDEX_2Field;
            }
            set
            {
                iNDEX_2Field = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_3 INDEX_3
        {
            get
            {
                return iNDEX_3Field;
            }
            set
            {
                iNDEX_3Field = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_4 INDEX_4
        {
            get
            {
                return iNDEX_4Field;
            }
            set
            {
                iNDEX_4Field = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_5 INDEX_5
        {
            get
            {
                return iNDEX_5Field;
            }
            set
            {
                iNDEX_5Field = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_6 INDEX_6
        {
            get
            {
                return iNDEX_6Field;
            }
            set
            {
                iNDEX_6Field = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_7 INDEX_7
        {
            get
            {
                return iNDEX_7Field;
            }
            set
            {
                iNDEX_7Field = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_8 INDEX_8
        {
            get
            {
                return iNDEX_8Field;
            }
            set
            {
                iNDEX_8Field = value;
            }
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_1
    {

        private RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_1FIRMWARE_NAME fIRMWARE_NAMEField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_1FIRMWARE_VERSION fIRMWARE_VERSIONField;

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_1FIRMWARE_NAME FIRMWARE_NAME
        {
            get
            {
                return fIRMWARE_NAMEField;
            }
            set
            {
                fIRMWARE_NAMEField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_1FIRMWARE_VERSION FIRMWARE_VERSION
        {
            get
            {
                return fIRMWARE_VERSIONField;
            }
            set
            {
                fIRMWARE_VERSIONField = value;
            }
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_1FIRMWARE_NAME
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_1FIRMWARE_VERSION
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_2
    {

        private RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_2FIRMWARE_NAME fIRMWARE_NAMEField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_2FIRMWARE_VERSION fIRMWARE_VERSIONField;

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_2FIRMWARE_NAME FIRMWARE_NAME
        {
            get
            {
                return fIRMWARE_NAMEField;
            }
            set
            {
                fIRMWARE_NAMEField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_2FIRMWARE_VERSION FIRMWARE_VERSION
        {
            get
            {
                return fIRMWARE_VERSIONField;
            }
            set
            {
                fIRMWARE_VERSIONField = value;
            }
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_2FIRMWARE_NAME
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_2FIRMWARE_VERSION
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_3
    {

        private RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_3FIRMWARE_NAME fIRMWARE_NAMEField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_3FIRMWARE_VERSION fIRMWARE_VERSIONField;

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_3FIRMWARE_NAME FIRMWARE_NAME
        {
            get
            {
                return fIRMWARE_NAMEField;
            }
            set
            {
                fIRMWARE_NAMEField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_3FIRMWARE_VERSION FIRMWARE_VERSION
        {
            get
            {
                return fIRMWARE_VERSIONField;
            }
            set
            {
                fIRMWARE_VERSIONField = value;
            }
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_3FIRMWARE_NAME
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_3FIRMWARE_VERSION
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_4
    {

        private RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_4FIRMWARE_NAME fIRMWARE_NAMEField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_4FIRMWARE_VERSION fIRMWARE_VERSIONField;

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_4FIRMWARE_NAME FIRMWARE_NAME
        {
            get
            {
                return fIRMWARE_NAMEField;
            }
            set
            {
                fIRMWARE_NAMEField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_4FIRMWARE_VERSION FIRMWARE_VERSION
        {
            get
            {
                return fIRMWARE_VERSIONField;
            }
            set
            {
                fIRMWARE_VERSIONField = value;
            }
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_4FIRMWARE_NAME
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_4FIRMWARE_VERSION
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_5
    {

        private RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_5FIRMWARE_NAME fIRMWARE_NAMEField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_5FIRMWARE_VERSION fIRMWARE_VERSIONField;

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_5FIRMWARE_NAME FIRMWARE_NAME
        {
            get
            {
                return fIRMWARE_NAMEField;
            }
            set
            {
                fIRMWARE_NAMEField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_5FIRMWARE_VERSION FIRMWARE_VERSION
        {
            get
            {
                return fIRMWARE_VERSIONField;
            }
            set
            {
                fIRMWARE_VERSIONField = value;
            }
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_5FIRMWARE_NAME
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_5FIRMWARE_VERSION
    {

        private decimal vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public decimal VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_6
    {

        private RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_6FIRMWARE_NAME fIRMWARE_NAMEField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_6FIRMWARE_VERSION fIRMWARE_VERSIONField;

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_6FIRMWARE_NAME FIRMWARE_NAME
        {
            get
            {
                return fIRMWARE_NAMEField;
            }
            set
            {
                fIRMWARE_NAMEField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_6FIRMWARE_VERSION FIRMWARE_VERSION
        {
            get
            {
                return fIRMWARE_VERSIONField;
            }
            set
            {
                fIRMWARE_VERSIONField = value;
            }
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_6FIRMWARE_NAME
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_6FIRMWARE_VERSION
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_7
    {

        private RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_7FIRMWARE_NAME fIRMWARE_NAMEField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_7FIRMWARE_VERSION fIRMWARE_VERSIONField;

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_7FIRMWARE_NAME FIRMWARE_NAME
        {
            get
            {
                return fIRMWARE_NAMEField;
            }
            set
            {
                fIRMWARE_NAMEField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_7FIRMWARE_VERSION FIRMWARE_VERSION
        {
            get
            {
                return fIRMWARE_VERSIONField;
            }
            set
            {
                fIRMWARE_VERSIONField = value;
            }
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_7FIRMWARE_NAME
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_7FIRMWARE_VERSION
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_8
    {

        private RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_8FIRMWARE_NAME fIRMWARE_NAMEField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_8FIRMWARE_VERSION fIRMWARE_VERSIONField;

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_8FIRMWARE_NAME FIRMWARE_NAME
        {
            get
            {
                return fIRMWARE_NAMEField;
            }
            set
            {
                fIRMWARE_NAMEField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_8FIRMWARE_VERSION FIRMWARE_VERSION
        {
            get
            {
                return fIRMWARE_VERSIONField;
            }
            set
            {
                fIRMWARE_VERSIONField = value;
            }
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_8FIRMWARE_NAME
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAFIRMWARE_INFORMATIONINDEX_8FIRMWARE_VERSION
    {

        private string vALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string VALUE
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

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAHEALTH_AT_A_GLANCE
    {

        private RIBCLGET_EMBEDDED_HEALTH_DATAHEALTH_AT_A_GLANCEBIOS_HARDWARE bIOS_HARDWAREField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAHEALTH_AT_A_GLANCEFANS fANSField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAHEALTH_AT_A_GLANCETEMPERATURE tEMPERATUREField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAHEALTH_AT_A_GLANCEPOWER_SUPPLIES pOWER_SUPPLIESField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAHEALTH_AT_A_GLANCEPROCESSOR pROCESSORField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAHEALTH_AT_A_GLANCEMEMORY mEMORYField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAHEALTH_AT_A_GLANCENETWORK nETWORKField;

        private RIBCLGET_EMBEDDED_HEALTH_DATAHEALTH_AT_A_GLANCESTORAGE sTORAGEField;

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAHEALTH_AT_A_GLANCEBIOS_HARDWARE BIOS_HARDWARE
        {
            get
            {
                return bIOS_HARDWAREField;
            }
            set
            {
                bIOS_HARDWAREField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAHEALTH_AT_A_GLANCEFANS FANS
        {
            get
            {
                return fANSField;
            }
            set
            {
                fANSField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAHEALTH_AT_A_GLANCETEMPERATURE TEMPERATURE
        {
            get
            {
                return tEMPERATUREField;
            }
            set
            {
                tEMPERATUREField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAHEALTH_AT_A_GLANCEPOWER_SUPPLIES POWER_SUPPLIES
        {
            get
            {
                return pOWER_SUPPLIESField;
            }
            set
            {
                pOWER_SUPPLIESField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAHEALTH_AT_A_GLANCEPROCESSOR PROCESSOR
        {
            get
            {
                return pROCESSORField;
            }
            set
            {
                pROCESSORField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAHEALTH_AT_A_GLANCEMEMORY MEMORY
        {
            get
            {
                return mEMORYField;
            }
            set
            {
                mEMORYField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAHEALTH_AT_A_GLANCENETWORK NETWORK
        {
            get
            {
                return nETWORKField;
            }
            set
            {
                nETWORKField = value;
            }
        }

        /// <remarks/>
        public RIBCLGET_EMBEDDED_HEALTH_DATAHEALTH_AT_A_GLANCESTORAGE STORAGE
        {
            get
            {
                return sTORAGEField;
            }
            set
            {
                sTORAGEField = value;
            }
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAHEALTH_AT_A_GLANCEBIOS_HARDWARE
    {

        private string sTATUSField;

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
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAHEALTH_AT_A_GLANCEFANS
    {

        private string sTATUSField;

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
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAHEALTH_AT_A_GLANCETEMPERATURE
    {

        private string sTATUSField;

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
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAHEALTH_AT_A_GLANCEPOWER_SUPPLIES
    {

        private string sTATUSField;

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
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAHEALTH_AT_A_GLANCEPROCESSOR
    {

        private string sTATUSField;

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
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAHEALTH_AT_A_GLANCEMEMORY
    {

        private string sTATUSField;

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
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAHEALTH_AT_A_GLANCENETWORK
    {

        private string sTATUSField;

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
    }

    /// <remarks/>
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class RIBCLGET_EMBEDDED_HEALTH_DATAHEALTH_AT_A_GLANCESTORAGE
    {

        private string sTATUSField;

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
    }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
