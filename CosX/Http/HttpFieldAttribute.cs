using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosX.Discord.Http {
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class HttpFieldAttribute : Attribute {
        public HttpFieldAttribute(string key) {
            Key = key;
        }

        public string Key {
            get;
            set;
        }
    }
}
