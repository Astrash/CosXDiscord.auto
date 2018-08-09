using CosX.Discord.WebSocket.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosX.Discord {
    public class MessageDefinition {
        public MessageDefinition(int id, Type messageType, Func<BaseMessage> messageConstructor) {
            Id = id;
            MessageType = messageType;
            MessageConstructor = messageConstructor;
        }

        public int Id {
            get;
            set;
        }

        public Type MessageType {
            get;
            set;
        }

        public Func<BaseMessage> MessageConstructor {
            get;
            set;
        }

    }
}
