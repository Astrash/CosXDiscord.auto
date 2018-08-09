using CosX.Discord;
using System;
using InfinityScript;
using CosX.Discord.WebSocket.Messages.Types;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace CosX.Discord
{
    public partial class CosXDiscord : BaseScript
    {
        private static string ConsoleName = "Console: ";
        private static string BotToken = "NDc3MTAzODEwNzU4ODM2MjM0.Dk3TyQ.P2jlck0u6HRYW3dXMiyl02SQqlw";
        private static Int64 ChannelID = 477109056423723028;
        private static string Discord_Prefix = "^0[^5Discord^0]^7 ";
        public CosXDiscord()
        {
            AstroPrint("Loading Discord Bridge");
            try
            {
                DiscordClient.Instance.Token = BotToken;
                DiscordClient.Instance.Initialize();
                DiscordClient.MessageReceived += DiscordClient_MessageReceived;
                string servername = Call<string>("getdvar", "sv_hostname");
                servername = servername.Replace("^0", "").Replace("^1", "").Replace("^2", "").Replace("^3", "").Replace("^4", "").Replace("^5", "").Replace("^6", "").Replace("^7", "").Replace("^8", "").Replace("^9", "").Replace("^;", "").Replace("^:", "");
                AstroPrint("Discord v0.0.1 has been loaded");
                AstroPrint("Discord Token: " + BotToken);
                AstroPrint("Discord ChannelID: " + ChannelID);
                AstroPrint("Discord Bridge by astro and Alpa");
                PlayerConnecting += Astro_PlayerConnecting;
                PlayerConnected += Astro_PlayerConnected;
                PlayerDisconnected += Astro_PlayerDisconnected;
                DiscordClient.SetGame("Mw3: " + servername);
            }
            catch (Exception ex)
            {
                AstroPrint("Error: Discord");
                AstroPrint(ex.Message);
            }
        }

        private void Astro_PlayerDisconnected(Entity obj)
        {

            DiscordClient.SendMessage(ChannelID, $"**[Disconnected]** {obj.Name}");
        }

        private void Astro_PlayerConnected(Entity obj)
        {
            DiscordClient.SendMessage(ChannelID, $"**[Connected]** {obj.Name}");
        }

        private void Astro_PlayerConnecting(Entity obj)
        {
            DiscordClient.SendMessage(ChannelID, $"**[Connecting]** {obj.Name}");

        }
        private static void AstroPrint(string v)
        {
            Log.Info(v);
        }
        private void DiscordClient_MessageReceived(DiscordMessage obj)
        {
            if (obj.Author.Id == 238621376238452736 && obj.ChannelId == ChannelID || obj.Author.Id == 389852393095036959 && obj.ChannelId == ChannelID)
            {
                if (!obj.Content.StartsWith("!"))
                {
                    string name = obj.Author.Username.Replace("^0", "").Replace("^1", "").Replace("^2", "").Replace("^3", "").Replace("^4", "").Replace("^5", "").Replace("^6", "").Replace("^7", "").Replace("^8", "").Replace("^9", "").Replace("^;", "").Replace("^:", "");
                    string message = obj.Content.Replace("^0", "").Replace("^1", "").Replace("^2", "").Replace("^3", "").Replace("^4", "").Replace("^5", "").Replace("^6", "").Replace("^7", "").Replace("^8", "").Replace("^9", "").Replace("^;", "").Replace("^:", "");
                    AstroPrint($"**[Discord]** {name}: {message}");
                    SayDiscordAll($"{obj.Author.Username}: {obj.Content}");
                    DiscordClient.SendMessage(ChannelID, $"[Discord-Chat] {name}: {message}");
                }
                else
                {
                    AstroPrint($"**[Discord]** {obj.Author.Username}: {obj.Content}");
                    LoadCommands(obj);
                }
                return;

            }
        }
        public override EventEat OnSay2(Entity player, string name, string message)
        {
            if(!message.StartsWith("!"))
            {
                string messagenocolor = message.Replace("^0", "").Replace("^1", "").Replace("^2", "").Replace("^3", "").Replace("^4", "").Replace("^5", "").Replace("^6", "").Replace("^7", "").Replace("^8", "").Replace("^9", "").Replace("^;", "").Replace("^:", "");
                string namenocolor = player.Name.Replace("^0", "").Replace("^1", "").Replace("^2", "").Replace("^3", "").Replace("^4", "").Replace("^5", "").Replace("^6", "").Replace("^7", "").Replace("^8", "").Replace("^9", "").Replace("^;", "").Replace("^:", "");
                DiscordClient.SendMessage(ChannelID, namenocolor + ": " + messagenocolor);
            }
            return base.OnSay2(player, name, message);
        }
        private void LoadCommands(DiscordMessage obj)
        {
            StartUseCommand(obj);
        }

        private void StartUseCommand(DiscordMessage obj)
        {
            string[] separator = obj.Content.Split(' ');
            string console = ConsoleName.Replace("^0", "").Replace("^1", "").Replace("^2", "").Replace("^3", "").Replace("^4", "").Replace("^5", "").Replace("^6", "").Replace("^7", "").Replace("^8", "").Replace("^9", "").Replace("^;", "").Replace("^:", "");
            if (obj.Content.StartsWith("!version"))
            {
                DiscordClient.SendMessage(ChannelID, $"**[Version]** Discord Bridge v0.1 Beta by Astro and Alpa");
                return;
            }
            if (obj.Content.StartsWith("!help"))
            {
                DiscordClient.SendMessage(Convert.ToInt64(ChannelID), $"**[Help]** Discord Command list: *!help, !say, !scream, !status*");
                return;
            }
            if (obj.Content.StartsWith("!say "))
            {
                obj.Content = obj.Content.Replace("!say ", "");
                SayAll(obj.Content);
                DiscordClient.SendMessage(ChannelID, $"**[Command]** {console} *{obj.Author.Username}: {obj.Content}*");
                return;
            }
            if (obj.Content.StartsWith("!scream "))
            {
                obj.Content = obj.Content.Replace("!scream ", "");
                string[] multicolor = { "^0" + obj.Content, "^1" + obj.Content, "^2" + obj.Content, "^3" + obj.Content, "^4" + obj.Content, "^5" + obj.Content, "^6" + obj.Content, "^7" + obj.Content, "^8" + obj.Content, "^9" + obj.Content, "^:" + obj.Content, "^;" + obj.Content };
                SayAllMulti(500, multicolor);
                DiscordClient.SendMessage(ChannelID, $"**[Command]** {console} *{obj.Author.Username}: {obj.Content}*");
                return;
            }
            if (obj.Content.StartsWith("!status"))
            {
                StatusCommand_Discord();
                return;
            }
            /*if (obj.Content.StartsWith("!kick"))
            {
                if (obj.Content.Contains(" "))
                {
                    KickCommandDicord(obj, separator[1], "Discord " + obj.Author.Username, separator[2]);
                    return;
                }
                else
                {
                    DiscordClient.SendMessage(Convert.ToInt64(ChannelID), $"**[Kick]** Usage: !kick <target> [reason]");
                    return;
                }
            }
            if (obj.Content.StartsWith("!ban"))
            {
                if (obj.Content.Contains(" "))
                {
                    BanCommandDicord(obj, separator[1], obj.Author.Username, separator[2]);
                    return;
                }
                else
                {
                    DiscordClient.SendMessage(Convert.ToInt64(ChannelID), $"**[Ban]** Usage: !ban <target> [reason]");
                    return;
                }
            }
            if (obj.Content.StartsWith("!tmpban"))
            { 
                if (obj.Content.Contains(" "))
                {
                    TempBanCommandDiscord(obj, separator[1], obj.Author.Username, separator[2]);
                    return;
                }
                else
                {
                    DiscordClient.SendMessage(Convert.ToInt64(ChannelID), $"**[TempBan]** Usage: !tempban <target> [reason]");
                    return;
                }
            }*/
            DiscordClient.SendMessage(ChannelID, $"**[Command]** Command not found");
        }

        private void TempBanCommandDiscord(DiscordMessage senderdc, string targetname, string sender, string reason)
        {
            Entity target = GetEntityDiscord(senderdc, targetname);
            if (target == null)
            {
                return;
            }
            Utilities.ExecuteCommand("tempbanclient " + target.EntRef + " ^1You have been tmpbanned by " + sender + " Reason: " + reason);
            DiscordClient.SendMessage(ChannelID, $"**[TempBan]** {target.Name} has been tempbanned by {sender} for {reason}");
        }
        private void StatusCommand_Discord()
        {
            List<Entity> tmp = Players.OrderBy((Func<Entity, int>)(e => e.EntRef)).ToList<Entity>();
            int i = 0;
            if (i >= Players.Count)
            {
                DiscordClient.SendMessage(ChannelID, "**[Status]** There are no online players");
            }
            OnInterval(1500, () =>
            {
                if (i >= Players.Count)
                {
                    return false;
                }
                DiscordClient.SendMessage(ChannelID, "**[Status]** [" + tmp[i].EntRef.ToString() + "] " + tmp[i].Name);
                ++i;
                return true;
            });
        }
        private void KickCommandDicord(DiscordMessage senderdc, string targetname, string sender, string reason)
        {
            Entity target = GetEntityDiscord(senderdc, targetname);
            if (target == null)
            {
                return;
            }
            Utilities.ExecuteCommand("dropclient " + target.EntRef + " ^1You have been kicked by " + sender + " Reason: " + reason);
            DiscordClient.SendMessage(ChannelID, $"**[Kick]** {target.Name} has been kicked by {sender} for {reason}");
        }
        private void BanCommandDicord(DiscordMessage senderdc, string targetname, string sender, string reason)
        {
            Entity target = GetEntityDiscord(senderdc, targetname);
            if (target == null)
            {
                return;
            }
            Utilities.ExecuteCommand("banclient " + target.EntRef + " ^1You have been banned by " + sender + " Reason: " + reason);
            DiscordClient.SendMessage(Convert.ToInt64(ChannelID), $"**[Banned]** {target.Name} has been banned by {sender} for {reason}");
        }
        private Entity GetEntityDiscord(DiscordMessage Admin, string Name)
        {
            bool f = false;
            Entity ent = null;
            foreach (Entity e in Players)
            {
                if (e.Name.ToLower().Contains(Name.ToLower()))
                {
                    if (f)
                    {
                        DiscordClient.SendMessage(ChannelID, $"**[Error]** More players were found");
                        return null;
                    }
                    else
                    {
                        ent = e;
                        f = true;
                    }
                }
            }
            if (ent == null)
            {
                DiscordClient.SendMessage(ChannelID, $"**[Error]** Player not found");
                return null;
            }
            return ent;
        }
        private static void SayDiscordAll(string v)
        {
            Utilities.RawSayAll(Discord_Prefix + v);
        }
        private static void SayAll(string content)
        {
            Utilities.RawSayAll(ConsoleName + content);
        }
        private void SayAllMulti(int time, string[] v)
        {
            int num = 0;
            foreach (string str in v)
            {
                string message = str;
                AfterDelay(num * time, (() => SayAll(message)));
                ++num;
            }
        }
    }
}
