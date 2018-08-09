using CosX.Discord.WebSocket.Messages;
using CosX.Discord.WebSocket.Messages.Types;
using CosX.Discord.WebSocket.Messages.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosX.Discord.WebSocket.Handler.Handlers {
    public class ConnectionHandler : DiscordHandlerContener {
        [DiscordHandler(10)]
        public void HelloMessageHandler(HelloMessage message) {
            var packedMessage = new IdentifyMessage() {
                Token = "Bot " + DiscordClient.Instance.Token,
                ClientProperties = new IdentifyConnection() {
                    browser = "AlpaBot",
                    device = "AlpaBot",
                    os = "windows"
                },
                LargeThreshold = 250,
                Compress = true,
                ShardInfo = new ShardInfo() {
                    ShardCount = 1,
                    ShardId = 0
                },
            };

            var msg = new BaseMessage() {
                Data = packedMessage,
                OpCode = GatewayOpCode.Identify
            };

            DiscordClient.Send(msg);

            DiscordClient.Instance.Heartbeat = message.heartbeat_interval;
            DiscordClient.Instance.SetTimer();
        }

        [DiscordHandler(9)]
        public void IncorrectSessionHandler(HelloMessage message) {

        }

        [DiscordHandler((int)GatewayOpCode.Dispatch)]
        public void Handler(BaseMessage message) {
            switch (message.EventName) {
                case "MESSAGE_CREATE":
                    var discordMessage = JsonConvert.DeserializeObject<DiscordMessage>(message.Data.ToString());
                    DiscordClient.OnDiscordMessageReceived(discordMessage);
                    break;
            }
        }
    }
}
