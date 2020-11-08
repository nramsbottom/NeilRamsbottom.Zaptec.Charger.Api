using System;
using System.Text.Json.Serialization;

namespace NeilRamsbottom.Zaptec.Charger.Api
{
    public class ZaptecApiToken
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName(".issued")]
        [JsonConverter(typeof(RFC1123DateTimeConverter))]
        public DateTime IssuedDate { get; set; }

        [JsonPropertyName(".expires")]
        [JsonConverter(typeof(RFC1123DateTimeConverter))]
        public DateTime ExpiresDate { get; set; }
    }
}
