using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WhoAreYou_Xamarin.Services
{
    class WebService
    {

        public async Task<string> SendGet(string address, params string[] values)
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
                        if(response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            using (HttpContent content = response.Content)
                            {
                                return await content.ReadAsStringAsync();
                            }
                        }

                        return string.Empty;
                    }
                }
            }

            catch
            {
                return string.Empty;
            }
            

        }

        public async Task<string> SendGetWithToken(string address, string token, params string[] values)
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
                    http.DefaultRequestHeaders.Add("X-AUTH-TOKEN", token);

                    using (HttpResponseMessage response = await http.GetAsync(url.ToString()))
                    {
                        if(response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            using (HttpContent content = response.Content)
                            {
                                return await content.ReadAsStringAsync();
                            }
                        }

                        return string.Empty;

                    }
                }
            }

            catch
            {
                return string.Empty;
            }


        }


        public async Task<string> SendPost(string url, Dictionary<string, string> values)
        {
            try
            {
                using (HttpClient http = new HttpClient())
                {
                    var encodedContent = new FormUrlEncodedContent(values);

                    using (HttpResponseMessage response = await http.PostAsync(url, encodedContent))
                    {
                        if(response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            using (HttpContent content = response.Content)
                            {
                                return await content.ReadAsStringAsync();
                            }
                        }

                        return string.Empty;

                    }
                }
            }

            catch
            {
                return string.Empty;
            }

        }

        public async Task<string> SendPostWithToken(string url, string token, Dictionary<string, string> values)
        {
            try
            {
                using (HttpClient http = new HttpClient())
                {
                    http.DefaultRequestHeaders.Add("X-AUTH-TOKEN", token);
                    var encodedContent = new FormUrlEncodedContent(values);

                    using (HttpResponseMessage response = await http.PostAsync(url, encodedContent))
                    {
                        if(response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            using (HttpContent content = response.Content)
                            {
                                return await content.ReadAsStringAsync();
                            }
                        }

                        return string.Empty;

                    }
                }
            }

            catch
            {
                return string.Empty;
            }

        }

        public async Task<bool> SendDeleteWithToken(string address, string token, params string[] values)
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
                    http.DefaultRequestHeaders.Add("X-AUTH-TOKEN", token);

                    using (HttpResponseMessage response = await http.DeleteAsync(url.ToString()))
                    {
                        if(response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            using (HttpContent content = response.Content)
                            {
                                return true;
                            }
                        }

                        return false;
                    }

                }
            }

            catch
            {
                return false;
            }

        }
    }
}
