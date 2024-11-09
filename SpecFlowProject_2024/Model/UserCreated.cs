using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProject_2024.Model
{
    internal class UserCreated
    {
        [JsonProperty("firstName")]
        public string firstName { get; set; }

        [JsonProperty("lastName")]
        public string lastName { get; set; }

        [JsonProperty("username")]
        public string username { get; set; }

        [JsonProperty("password")]
        public string password { get; set; }

        [JsonProperty("confirmPassword")]
        public string confirmPassword { get; set; }



        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("uuid")]
        public string uuid { get; set; }
        [JsonProperty("balance")]
        public int balance { get; set; }
        [JsonProperty("createdAt")]
        public string createdAt { get; set; }
        [JsonProperty("modifiedAt")]
        public string modifiedAt { get; set; }


        public override string ToString()
        {
            return "UserCreated{" +
                    "id='" + id + '\'' +
                    ", uuid='" + uuid + '\'' +
                    ", firstName='" + firstName + '\'' +
                    ", lastName='" + lastName + '\'' +
                    ", username='" + username + '\'' +
                    ", password='" + password + '\'' +
                    ", balance=" + balance +
                    ", createdAt='" + createdAt + '\'' +
                    ", modifiedAt='" + modifiedAt + '\'' +
                    '}';
        }


    }
}
