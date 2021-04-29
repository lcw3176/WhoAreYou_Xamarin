using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace WhoAreYou_Xamarin.Services
{
    class JsonService
    {

        public List<string> ReadJArray(object json, string key)
        {
            JArray jArray = JArray.Parse(json.ToString());
            List<string> list = new List<string>();

            foreach(JObject i in jArray)
            {
                list.Add(i.GetValue(key).ToString());
                                
            }

            return list;
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
