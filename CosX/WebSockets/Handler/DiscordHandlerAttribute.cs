using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosX.Discord.WebSocket.Handler {
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class DiscordHandlerAttribute : Attribute {
        public DiscordHandlerAttribute(int messageId) {
            MessageId = messageId;
        }

        public int MessageId {
            get;
            set;
        }

        public override string ToString() {
            return MessageId.ToString();
        }
    }
}
