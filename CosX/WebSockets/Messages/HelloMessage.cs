using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosX.Discord.WebSocket.Messages {
    public class HelloMessage : BaseMessage {
        public const int Id = 10;

        public int heartbeat_interval {
            get;
            set;
        }

        public object _trace {
            get;
            set;
        }
    }
}
