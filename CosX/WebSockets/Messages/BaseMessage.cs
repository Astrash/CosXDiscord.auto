using CosX.Discord.WebSocket.Messages.Enums;
using CosX.Discord.WebSocket.Messages.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosX.Discord.WebSocket.Messages {
    public class BaseMessage {
        /// <summary>
        /// Gets or sets the OP code of the payload.
        /// </summary>
        [JsonProperty("op")]
        public GatewayOpCode OpCode { get; set; }

        /// <summary>
        /// Gets or sets the data of the payload.
        /// </summary>
        [JsonProperty("d")]
        public object Data { get; set; }

        /// <summary>
        /// Gets or sets the sequence number of the payload. Only present for OP 0.
        /// </summary>
        [JsonProperty("s", NullValueHandling = NullValueHandling.Ignore)]
        public int? Sequence { get; set; }

        /// <summary>
        /// Gets or sets the event name of the payload. Only present for OP 0.
        /// </summary>
        [JsonProperty("t", NullValueHandling = NullValueHandling.Ignore)]
        public string EventName { get; set; }
    }
}
