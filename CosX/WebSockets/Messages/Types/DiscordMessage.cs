using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosX.Discord.WebSocket.Messages.Types {
    public class DiscordMessage {
        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("channel_id")]
        public long ChannelId { get; set; }

        [JsonProperty("author")]
        public DiscordUser Author { get; set; }
    }
}
