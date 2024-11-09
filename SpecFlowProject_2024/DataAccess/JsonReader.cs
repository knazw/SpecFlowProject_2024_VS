using Newtonsoft.Json;
using SpecFlowProject_2024.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProject_2024.DataAccess
{
    public class JsonReader
    {
        public IList<UserX> UserXList { get; private set; }

        public void readFile(string fileName)
        {
            using (var sr = new StreamReader(fileName))
            {
                var json = sr.ReadToEnd();
                UserXList = JsonConvert.DeserializeObject<IList<UserX>>(json);
            }

        }

        public T? readFileGeneric<T>(string fileName)
        {
            using (var sr = new StreamReader(fileName))
            {
                var json = sr.ReadToEnd();
                T? deserializedClass = JsonConvert.DeserializeObject<T>(json);
                return deserializedClass;
            }

        }
    }
}
