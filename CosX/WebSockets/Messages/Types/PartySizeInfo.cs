﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosX.Discord.WebSocket.Messages.Types {
    [JsonConverter(typeof(PartySizeInfoConverter))]
    public class PartySizeInfo {
        /// <summary>
        /// Gets or sets this client's shard id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the total shard count for this token.
        /// </summary>
        public int Size { get; set; }
    }

    public class PartySizeInfoConverter : JsonConverter {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            var sinfo = value as ShardInfo;
            var obj = new object[] { sinfo.ShardId, sinfo.ShardCount };
            serializer.Serialize(writer, obj);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            var arr = ReadArrayObject(reader, serializer);
            return new PartySizeInfo {
                Id = (int)arr[0],
                Size = (int)arr[1],
            };
        }

        private JArray ReadArrayObject(JsonReader reader, JsonSerializer serializer) {
            var arr = serializer.Deserialize<JToken>(reader) as JArray;
            if (arr == null || arr.Count != 2)
                throw new JsonSerializationException("Expected array of length 2");
            return arr;
        }

        public override bool CanConvert(Type objectType) {
            return objectType == typeof(PartySizeInfo);
        }
    }
}
