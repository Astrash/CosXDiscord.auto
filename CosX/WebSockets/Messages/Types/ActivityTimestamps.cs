using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosX.Discord.WebSocket.Messages.Types {
    public class ActivityTimestamps {
        [JsonProperty("start")]
        public int? Start { get; set; }

        [JsonProperty("end")]
        public int? End { get; set; }
    }
}
