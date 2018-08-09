using CosX.Discord.WebSocket.Messages.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosX.Discord.WebSocket.Messages {
    public class IdentifyMessage {
        /// <summary>
        /// Gets or sets the token used to identify the client to Discord.
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the client's properties.
        /// </summary>
        [JsonProperty("properties")]
        public IdentifyConnection ClientProperties { get; set; }

        /// <summary>
        /// Gets or sets whether to encrypt websocket traffic.
        /// </summary>
        [JsonProperty("compress")]
        public bool Compress { get; set; }

        /// <summary>
        /// Gets or sets the member count at which the guild is to be considered large.
        /// </summary>
        [JsonProperty("large_threshold")]
        public int LargeThreshold { get; set; }

        /// <summary>
        /// Gets or sets the shard info for this connection.
        /// </summary>
        [JsonProperty("shard")]
        public ShardInfo ShardInfo { get; set; }

    }
}
