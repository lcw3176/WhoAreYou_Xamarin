using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace WhoAreYou_Xamarin.Services
{
    class JsonService
    {

        public string MakeJson(Dictionary<string, object> value)
        {
            JObject json = new JObject();

            foreach(var i in value)
            {
                json.Add(new JProperty(i.Key, i.Value));
            }

            return json.ToString();
        }


        public object ReadJson(object json)
        {
            return null;
        }

        public string ReadJson(object json, string keyName)
        {
            foreach(var i in JObject.Parse(json.ToString()))
            {
                if(i.Key == keyName)
                {
                    return i.Value.ToString();
                }
            }

            return null;
        }
    }
}
