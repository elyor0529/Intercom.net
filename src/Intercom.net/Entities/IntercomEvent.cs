using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Intercom
{
    public class IntercomEvent
    {
        public IntercomEvent()
        {
            this.CreatedAt = DateTime.UtcNow;
        }

        [JsonProperty("event_name")]
        public string EventName { get; set; }

        [JsonProperty("created_at")]
        [JsonConverter(typeof(EpochDateTimeConverter))]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("metadata")]
        public Dictionary<string, string> Metadata { get; set; }
    }
}
