using CosX.Discord.WebSocket.Messages.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosX.Discord.WebSocket.Messages.Types {
    public class Activity {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public ActivityType Type { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("timestamps")]
        public ActivityTimestamps Timestamps { get; set; }

        [JsonProperty("application_id")]
        public int ApplicationId { get; set; }

        [JsonProperty("details")]
        public string Details { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("party")]
        public ActivityParty Party { get; set; }

        [JsonProperty("assets")]
        public ActivityAssets Assets { get; set; }
    }
}
