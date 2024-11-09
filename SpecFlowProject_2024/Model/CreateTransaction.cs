using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProject_2024.Model
{
    internal class CreateTransaction
    {
        
        
        [JsonProperty("transactionType")]
        public String transactionType { get; set; }

        [JsonProperty("amount")]
        public int amount { get; set; }

        [JsonProperty("description")]
        public String description { get; set; }

        [JsonProperty("senderId")]
        public String senderId { get; set; }

        [JsonProperty("receiverId")]
        public String receiverId { get; set; }
    }
}
