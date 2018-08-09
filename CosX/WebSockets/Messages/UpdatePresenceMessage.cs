using CosX.Discord.WebSocket.Messages.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosX.Discord.WebSocket.Messages {
    public class UpdatePresenceMessage : BaseMessage {
        [JsonProperty("since")]
        public int? Since { get; set; }

        [JsonProperty("game")]
        public Activity Game { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("afk")]
        public bool Afk { get; set; }
    }
}
