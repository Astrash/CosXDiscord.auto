using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosX.Discord.WebSocket.Messages.Types {
    public class IdentifyConnection {
        [JsonProperty("$os")]
        public string os {
            get;
            set;
        }

        [JsonProperty("$browser")]
        public string browser {
            get;
            set;
        }

        [JsonProperty("$device")]
        public string device {
            get;
            set;
        }
    }
}
