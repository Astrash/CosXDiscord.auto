using CosX.Discord.Extensions;
using CosX.Discord.WebSocket.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CosX.Discord.WebSocket.Handler {
    public static class HandlerManager {
        public static Dictionary<int, Action<object, BaseMessage>> m_handlers = new Dictionary<int, Action<object, BaseMessage>>();

        public static void RegisterAll() {
            Assembly asm = Assembly.GetAssembly(typeof(MessageInitializer));

            foreach (Type type in asm.GetTypes().Where(w => w.IsSubclassOf(typeof(DiscordHandlerContener)))) {
                var methods = type.GetMethods().Where(x => x.CustomAttributes.Any(w => w.AttributeType == typeof(DiscordHandlerAttribute)));

                foreach (var method in methods) {
                    var messageId = method.GetCustomAttribute<DiscordHandlerAttribute>().MessageId;
                    var handlerDelegate = DynamicExtension.CreateDelegate(method, typeof(BaseMessage)) as Action<object, BaseMessage>;

                    m_handlers.Add(messageId, handlerDelegate);
                }
            }
        }

        public static Action<object, BaseMessage> GetHandler(int key) {
            m_handlers.TryGetValue(key, out Action<object, BaseMessage> handler);
            return handler;
        }
    }
}
