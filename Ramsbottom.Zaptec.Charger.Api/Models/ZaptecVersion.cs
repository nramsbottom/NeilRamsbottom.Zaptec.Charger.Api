using System.Text.Json.Serialization;

namespace NeilRamsbottom.Zaptec.Charger.Api.Models
{
    public class ZaptecVersion
    {
        [JsonPropertyName("_Major")]
        public int Major { get; set; }
        [JsonPropertyName("_Minor")]
        public int Minor { get; set; }
        [JsonPropertyName("_Build")]
        public int Build { get; set; }
        [JsonPropertyName("_Revision")]
        public int Revision { get; set; }
    }
}
