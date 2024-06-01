using HassDeviceWorkers.HPiLO4DataReader.DTO;
using HassDeviceWorkers.HPiLO4DataReader.XmlGenerated;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HassDeviceWorkers.HPiLO4DataReader
{
    /// <summary>
    /// Read Health and Uptime information from iLO4
    /// Based on information:
    /// https://techhub.hpe.com/eginfolib/servers/docs/HPRestfultool/iLo4/data_model_reference.html#Thermal
    /// https://hewlettpackard.github.io/ilo-rest-api-docs/ilo4/#navigating-the-data-model
    /// https://seveas.github.io/python-hpilo/health.html
    /// </summary>
    public class ILO4DataReader
    {
        private readonly string RequestStringTemplate = "<?xml version=\"1.0\"?>\r\n" +
            "<RIBCL VERSION=\"2.0\"><LOGIN USER_LOGIN=\"{0}\" PASSWORD=\"{1}\">" +
            "<SERVER_INFO MODE=\"read\">{2}</SERVER_INFO></LOGIN></RIBCL>";

        private HttpClient ApiClient { get; }
        private CookieContainer CookieContainer { get; }

        public ILO4DataReader(string baseAddress, string userName, string userPass)
        {
            CookieContainer = new();

            var handler = new HttpClientHandler
            {
                CookieContainer = CookieContainer,
                UseCookies = true,
                UseDefaultCredentials = false,
                ServerCertificateCustomValidationCallback = (o, c, ch, er) => true
            };

            ApiClient = new(handler)
            {
                BaseAddress = new Uri(baseAddress)
            };

            RequestStringTemplate = string.Format(RequestStringTemplate, userName, userPass, "{0}");
        }

        async protected Task<string> InnerGetData(string request)
        {
            var uri = new Uri(ApiClient.BaseAddress!, $"/ribcl");

            var content = new StringContent(string.Format(RequestStringTemplate, request),
                Encoding.UTF8, "text/xml");

            var response = await ApiClient.PostAsync(uri, content);
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException(response.ReasonPhrase);

            using var input = await response.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(input);

            return await streamReader.ReadToEndAsync();
        }

        async public Task<HostData> GetHostData()
        {
            var result = await InnerGetData("<GET_HOST_DATA/><GET_SERVER_NAME/>");

            // skip service empty statuses...
            var doc = result
                .Split("<?xml version=\"1.0\"?>")
                .Skip(5)
                .Take(2)
                .ToList();

            var serverHostData = DeserializeXml<ServerHostData>(doc[0]);
            var serverNameData = DeserializeXml<ServerNameData>(doc[1]);
            return new(serverNameData, serverHostData);
        }

        async public Task<HealthData> GetHealthAndUptime()
        {
            var result = await InnerGetData("<GET_EMBEDDED_HEALTH/><GET_SERVER_POWER_ON_TIME/>");

            // skip service empty statuses...
            var docs = result
                .Split("<?xml version=\"1.0\"?>")
                .Skip(5)
                .Take(3)
                .ToList();

            var embeddedHealthData = DeserializeXml<EmbeddedHealthData>(docs[0]);
            var serverPowerOnMinutes = DeserializeXml<ServerPowerOnMinutes>(docs[2]);

            return new HealthData
            {
                Fans = embeddedHealthData.GET_EMBEDDED_HEALTH_DATA.FANS.Select(e =>
                    new Fan
                    {
                        Label = e.LABEL.VALUE,
                        Zone = e.ZONE.VALUE,
                        Status = e.STATUS.VALUE.ParseToEnumWithDefault<HealthStatus>(),
                        SpeedInPercent = e.SPEED.VALUE,
                    }).ToList(),
                Temperatures = embeddedHealthData.GET_EMBEDDED_HEALTH_DATA.TEMPERATURE
                    .Where(e => e.STATUS.VALUE != "Not Installed")
                    .Select(e =>
                        new Temperature
                        {
                            Label = e.LABEL.VALUE,
                            Location = e.LOCATION.VALUE.ParseToEnumWithDefault<LocationArea>(),
                            Status = e.STATUS.VALUE.ParseToEnumWithDefault<HealthStatus>(),
                            CurrentTemperature = e.CURRENTREADING.VALUE.ParseToIntWithNA(),
                            CautionTemperature = e.CAUTION.VALUE.ParseToIntWithNA(),
                            CriticalTemperature = e.CRITICAL.VALUE.ParseToIntWithNA(),
                        }).ToList(),
                PowerSupplies = embeddedHealthData.GET_EMBEDDED_HEALTH_DATA.POWER_SUPPLIES.SUPPLY
                    .Select(e =>
                        new PowerSupply
                        {
                            Label = e.LABEL.VALUE,
                            Status = e.STATUS.VALUE
                        }).ToList(),
                HealthAtAGlance = new HealthAtAGlance
                {
                    BiosHardware = embeddedHealthData.GET_EMBEDDED_HEALTH_DATA.HEALTH_AT_A_GLANCE.
                        BIOS_HARDWARE.STATUS.ParseToEnumWithDefault<HealthStatus>(),
                    Fans = embeddedHealthData.GET_EMBEDDED_HEALTH_DATA.HEALTH_AT_A_GLANCE.
                        FANS.STATUS.ParseToEnumWithDefault<HealthStatus>(),
                    Temperature = embeddedHealthData.GET_EMBEDDED_HEALTH_DATA.HEALTH_AT_A_GLANCE.
                        TEMPERATURE.STATUS.ParseToEnumWithDefault<HealthStatus>(),
                    PowerSupplies = embeddedHealthData.GET_EMBEDDED_HEALTH_DATA.HEALTH_AT_A_GLANCE.
                        POWER_SUPPLIES.STATUS.ParseToEnumWithDefault<HealthStatus>(),
                    Processor = embeddedHealthData.GET_EMBEDDED_HEALTH_DATA.HEALTH_AT_A_GLANCE.
                        PROCESSOR.STATUS.ParseToEnumWithDefault<HealthStatus>(),
                    Memory = embeddedHealthData.GET_EMBEDDED_HEALTH_DATA.HEALTH_AT_A_GLANCE.
                        MEMORY.STATUS.ParseToEnumWithDefault<HealthStatus>(),
                    Network = embeddedHealthData.GET_EMBEDDED_HEALTH_DATA.HEALTH_AT_A_GLANCE.
                        NETWORK.STATUS.ParseToEnumWithDefault<HealthStatus>(),
                    Storage = embeddedHealthData.GET_EMBEDDED_HEALTH_DATA.HEALTH_AT_A_GLANCE.
                        STORAGE.STATUS.ParseToEnumWithDefault<HealthStatus>(),
                },
                ServerUptime = DateTime.Now.AddMinutes(-serverPowerOnMinutes.SERVER_POWER_ON_MINUTES.VALUE),
            };
        }

        private static T DeserializeXml<T>(string doc)
        {
            using var stream = new StringReader(doc);
            var ser = new XmlSerializer(typeof(T));
            return (T?)ser?.Deserialize(stream) ?? throw new SerializationException("Deserialization error... Object is empty");
        }
    }
}
