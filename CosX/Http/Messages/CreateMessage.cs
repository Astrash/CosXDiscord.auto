using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosX.Discord.Http.Messages {
    public class CreateMessage : DefaultMessage {
        public long ChannelId { get; set; }

        public override string URL => string.Format("channels/{0}/messages", ChannelId);
        public override string Method => "POST";

        [HttpField("content")]
        public string Content { get; set; }

        [HttpField("tts")]
        public bool? TTS { get; set; }
    }
}
