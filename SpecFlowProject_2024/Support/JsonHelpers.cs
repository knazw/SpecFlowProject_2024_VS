using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProject_2024.Support
{
    internal class JsonHelpers
    {
        public static string getJson(string jsonData, string token)
        {
            var rss = JObject.Parse(jsonData);
            var valueJson = rss.SelectToken(token);

            var builder = new StringBuilder();
            using (var textWriter = new StringWriter(builder))
            using (var jsonWriter = new JsonTextWriter(textWriter))
                valueJson?.WriteTo(jsonWriter);

            return builder.ToString();
        }
    }
}
