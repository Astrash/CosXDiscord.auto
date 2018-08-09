using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosX.Discord.WebSocket.Messages.Types {
    public class ActivityParty {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("size")]
        public PartySizeInfo Size { get; set; }
    }
}
