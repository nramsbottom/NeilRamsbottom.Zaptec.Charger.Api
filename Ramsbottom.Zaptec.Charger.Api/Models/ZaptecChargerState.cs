using System;
using System.Text.Json.Serialization;

namespace NeilRamsbottom.Zaptec.Charger.Api.Models
{
    public class ZaptecChargerState
    {
        public string ChargerId { get; set; }
        public int StateId { get; set; }
        public DateTimeOffset Timestamp { get; set; }

        [JsonPropertyName("ValueAsString")]
        public string Value { get; set; }

        public override string ToString()
        {
            return $"{StateId} {Value}";
        }
    }
}
