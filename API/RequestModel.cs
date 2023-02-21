using System.Text.Json.Serialization;

namespace API
{
    public class RequestModel
    {
        [JsonPropertyName("client")]
        public string client { get; set; }

        [JsonPropertyName("user")]
        public string user { get; set; }

        [JsonPropertyName("url")]
        public string url { get; set; }

        [JsonPropertyName("time")]
        public string time { get; set; }

        [JsonPropertyName("message")]
        public string msg { get; set; }

        [JsonPropertyName("id")]
        public int? id { get; set; }

    }
}
