using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CosX.Discord.Http.Messages {
    public abstract class DefaultMessage {
        public virtual string URL { get; set; }
        public virtual string Method { get; set; }

        public virtual void Send() {
            var type = this.GetType();
            var properties = type.GetProperties();

            var request = (HttpWebRequest)WebRequest.Create(HttpManager.BASE_URL + this.URL);

            var postData = "";

            foreach (var prop in properties) {
                var attr = prop.CustomAttributes.Any(x => x.AttributeType == typeof(HttpFieldAttribute));

                if (attr) {
                    var attrObj = prop.GetCustomAttribute<HttpFieldAttribute>();
                    string key = attrObj.Key;
                    string value = (string)prop.GetValue(this).ToString();

                    postData += $"{key}={value}&";
                }
            }

            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = this.Method;
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            request.Headers.Add("Authorization", "Bot " + DiscordClient.Instance.Token);

            using (var stream = request.GetRequestStream()) {
                stream.Write(data, 0, data.Length);
            }

            try {
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            }catch(WebException ex) {
                if (DiscordClient.Debug)
                    Console.WriteLine(ex);
                switch (((HttpWebResponse)ex.Response).StatusCode) {
                    case HttpStatusCode.NotFound:
                        Console.WriteLine(((HttpWebResponse)ex.Response).StatusCode);
                        break;
                }
            }catch(Exception ex) {
                if (DiscordClient.Debug)
                    Console.WriteLine(ex);
            }
        }
    }
}
