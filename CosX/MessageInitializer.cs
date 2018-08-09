using CosX.Discord.Extensions;
using CosX.Discord.WebSocket.Messages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CosX.Discord {
    public static class MessageInitializer {
        private static readonly Dictionary<int, MessageDefinition> MessagesDefinitions = new Dictionary<int, MessageDefinition>();

        public static void Initialize() {
            Assembly asm = Assembly.GetAssembly(typeof(MessageInitializer));

            foreach (Type type in asm.GetTypes().Where(x => x.IsSubclassOf(typeof(BaseMessage)))) {
                var fieldId = type.GetField("Id");

                if (fieldId != null) {
                    var id = (int)fieldId.GetValue(type);

                    ConstructorInfo ctor = type.GetConstructor(Type.EmptyTypes);

                    if (ctor == null)
                        throw new Exception(
                            string.Format("'{0}' doesn't implemented a parameterless constructor",
                                            type));

                    var deleg = ctor.CreateDelegate<Func<BaseMessage>>();

                    MessageDefinition message;
                    if (!MessagesDefinitions.TryGetValue(id, out message)) {
                        MessagesDefinitions.Add(id, new MessageDefinition(id, type, deleg));
                    }
                }
            }
        }

        public static BaseMessage BuildMessage(int id, string json) {
            MessageDefinition definition;
            if (!MessagesDefinitions.TryGetValue(id, out definition))
                throw new Exception(string.Format("Message <id:{0}> doesn't exist", id));

            BaseMessage message = definition.MessageConstructor();
            
            return (BaseMessage)JsonConvert.DeserializeObject(json, definition.MessageType);
        }

        public static Type GetMessageType(int id) {
            MessageDefinition definition;
            if (!MessagesDefinitions.TryGetValue(id, out definition))
                throw new Exception(string.Format("Message <id:{0}> doesn't exist", id));

            return definition.MessageType;
        }
    }
}
