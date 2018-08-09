using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.ClientEngine;
using WebSocket4Net;
using Newtonsoft.Json;
using CosX.Discord.WebSocket.Messages;
using CosX.Discord.WebSocket.Handler;
using CosX.Discord.WebSocket.Messages.Types;
using System.Timers;
using CosX.Discord.WebSocket.Messages.Enums;
using CosX.Discord.Http.Messages;

namespace CosX.Discord
{
    public class DiscordClient
    {
        private Timer Timer { get; set; }

        public static event Action<DiscordMessage> MessageReceived;

        private static DiscordClient m_instance;

        public static DiscordClient Instance {
            get {
                if (m_instance == null)
                    m_instance = new DiscordClient();
                return m_instance;
            }
        }

        public WebSocket4Net.WebSocket WebSock { get; set; }

        public int Heartbeat { get; set; }

        public int? LastSequence { get; set; }

        public string Token { get; set; }

        public static bool Debug { get; set; }

        public void PreInitialize() {
            MessageInitializer.Initialize();
            HandlerManager.RegisterAll();
        }

        public void Initialize(bool reconnect = false) {
            if(!reconnect)
                PreInitialize();

            WebSock = new WebSocket4Net.WebSocket("wss://gateway.discord.gg/?v=6&encoding=json");

            WebSock.Opened += WebSock_Opened;
            WebSock.MessageReceived += WebSock_MessageReceived;
            WebSock.Closed += WebSock_Closed;
            WebSock.Error += WebSock_Error;

            MessageReceived += DiscordClient_MessageReceived;

            //Avoid Mono Security
            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                delegate (object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors) {
                return true;
            };

            WebSock.Open();
        }

        private void DiscordClient_MessageReceived(DiscordMessage obj) {
            Console.WriteLine($"{obj.Author.Username}: {obj.Content}");
            //SetGame(obj.Content);
        }

        private void WebSock_Error(object sender, ErrorEventArgs e) {
            if(e.Exception is System.Net.Sockets.SocketException) {
                Console.WriteLine("DiscordClient Reconnexion: " + e.Exception.ToString());
                Initialize(true);
            }

            if(Debug)
                Console.WriteLine("DiscordClient Error: " + e.Exception.ToString());

        }

        private void WebSock_Closed(object sender, EventArgs e) {
            Console.WriteLine("DiscordClient Closed");
        }

        private void WebSock_MessageReceived(object sender, MessageReceivedEventArgs e) {
            try {
                var message = JsonConvert.DeserializeObject<BaseMessage>(e.Message);
                LastSequence = message.Sequence;

                var newMessage = MessageInitializer.BuildMessage((int)message.OpCode, message.OpCode == GatewayOpCode.Dispatch ? e.Message : message.Data.ToString());
                HandlerManager.GetHandler((int)message.OpCode)?.Invoke(null, newMessage);
            } catch {

            }
        }

        private void WebSock_Opened(object sender, EventArgs e) {
        }

        public static void OnDiscordMessageReceived(DiscordMessage message) {
            MessageReceived.Invoke(message);
        }

        public static void SendMessage(long channelId, string content) {
            CreateMessage message = new CreateMessage() {
                ChannelId = channelId,
                Content = content,
                TTS = false,
                Method = "POST"
            };

            message.Send();

        }

        public static void Send(BaseMessage message) {
            var json = JsonConvert.SerializeObject(message);
            Instance.WebSock.Send(json);
        }

        public static void SetGame(string game) {
            var presence = new UpdatePresenceMessage() {
                Status = "online",
                Game = new Activity() {
                    Type = ActivityType.Game,
                    Name = game
                },
                Afk = false
            };

            var message = new BaseMessage() {
                OpCode = GatewayOpCode.StatusUpdate,
                Data = presence
            };

            Send(message);
        }

        public void SetTimer() {
            Timer = new Timer(Heartbeat);
            Timer.Elapsed += Timer_Elapsed;
            Timer.AutoReset = true;
            Timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e) {
            var message = new BaseMessage() {
                OpCode = GatewayOpCode.Heartbeat,
                Sequence = LastSequence
            };

            Send(message);
        }
    }
}
