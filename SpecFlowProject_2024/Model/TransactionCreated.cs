using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProject_2024.Model
{
    internal class TransactionCreated
    {
        [JsonProperty("id")]
        public String id { get; set; }
        [JsonProperty("uuid")]
        public String uuid { get; set; }
        [JsonProperty("amount")]
        public int amount { get; set; }
        [JsonProperty("description")]
        public String description { get; set; }
        [JsonProperty("receiverId")]
        public String receiverId { get; set; }
        [JsonProperty("senderId")]
        public String senderId { get; set; }
        [JsonProperty("status")]
        public String status { get; set; }
        [JsonProperty("requestStatus")]
        public String requestStatus { get; set; }
        [JsonProperty("createdAt")]
        public String createdAt { get; set; }
        [JsonProperty("modifiedAt")]
        public String modifiedAt { get; set; }

    }
}
