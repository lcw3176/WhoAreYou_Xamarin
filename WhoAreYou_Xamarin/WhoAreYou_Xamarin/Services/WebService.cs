using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WhoAreYou_Xamarin.Services
{
    class WebService
    {

        public object Request(object obj)
        {
            return null;
        }

        public async Task<string> SendToGet(string address, params string[] values)
        {
            try
            {
                StringBuilder url = new StringBuilder();
                url.Append(address.ToString());

                foreach (var i in values)
                {
                    url.Append("/");
                    url.Append(i);
                }

                using (HttpClient http = new HttpClient())
                {
                    using (HttpResponseMessage response = await http.GetAsync(url.ToString()))
                    {
                        using (HttpContent content = response.Content)
                        {
                            return await content.ReadAsStringAsync();
                        }
                    }
                }
            }

            catch
            {
                return null;
            }
            

        }

        public async Task<string> SendToPost(string url, Dictionary<string, string> values)
        {
            try
            {
                using (HttpClient http = new HttpClient())
                {
                    var encodedContent = new FormUrlEncodedContent(values);

                    using (HttpResponseMessage response = await http.PostAsync(url, encodedContent))
                    {
                        using (HttpContent content = response.Content)
                        {
                            return await content.ReadAsStringAsync();
                        }
                    }
                }
            }

            catch
            {
                return null;
            }

        }
    }
}
