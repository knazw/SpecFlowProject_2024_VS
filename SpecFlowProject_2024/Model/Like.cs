using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProject_2024.Model
{
    internal class Like
    {
        [JsonProperty("id")]
        public String id { get; set; }
        [JsonProperty("uuid")]
        public String uuid { get; set; }
        [JsonProperty("userId")]
        public String userId { get; set; }
        [JsonProperty("transactionId")]
        public String transactionId { get; set; }
        [JsonProperty("createdAt")]
        public String createdAt { get; set; }
        [JsonProperty("modifiedAt")]
        public String modifiedAt { get; set; }
    }
}
