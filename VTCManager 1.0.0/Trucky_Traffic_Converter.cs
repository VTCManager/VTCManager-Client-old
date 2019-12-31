namespace VTCManager_1._0._0
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class TruckyTopTraffic
    {
        [JsonProperty("response")]
        public List<Response> Response { get; set; }
    }

    public partial class Response
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("players")]
        public long Players { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("severity")]
        public string Severity { get; set; }

        [JsonProperty("averageSpeed")]
        public long AverageSpeed { get; set; }

        [JsonProperty("newSeverity")]
        public string NewSeverity { get; set; }

        [JsonProperty("trafficJams")]
        public long TrafficJams { get; set; }

        [JsonProperty("playersInvolvedInTrafficJams")]
        public long PlayersInvolvedInTrafficJams { get; set; }

        [JsonProperty("layerID")]
        public Guid LayerId { get; set; }
    }

    public partial class TruckyTopTraffic
    {
        public static TruckyTopTraffic FromJson(string json) => JsonConvert.DeserializeObject<TruckyTopTraffic>(json, VTCManager_1._0._0.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this TruckyTopTraffic self) => JsonConvert.SerializeObject(self, VTCManager_1._0._0.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
