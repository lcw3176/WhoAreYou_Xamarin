using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace WhoAreYou_Xamarin.Services
{
    class JsonService
    {
        public object ReadJson(object json)
        {
            return null;
        }

        public object ReadJson(object json, string keyName)
        {
            foreach(var i in JObject.Parse(json.ToString()))
            {
                if(i.Key == keyName)
                {
                    return i.Value;
                }
            }

            return null;
        }
    }
}
