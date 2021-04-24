using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WhoAreYou_Xamarin.Models.Property;
using WhoAreYou_Xamarin.Models.Response;
using WhoAreYou_Xamarin.Models.Url;
using WhoAreYou_Xamarin.Services.Dependencies;
using Xamarin.Forms;

namespace WhoAreYou_Xamarin.Services
{
    static class SocketService
    {
        private static ClientWebSocket socket = new ClientWebSocket();
        private static JsonService jsonService = new JsonService();

        public static bool IsConnect()
        { 
            if(socket.State != WebSocketState.Connecting)
            {
                return false;
            }

            return true;
        }

        public async static Task<bool> Connect(string token)
        {

            try
            {
                if(socket.State != WebSocketState.Connecting)
                {
                    Uri uri = new Uri(Urls.SOCKET);
                    socket.Options.SetRequestHeader("X-AUTH-TOKEN", token);
                    await socket.ConnectAsync(uri, CancellationToken.None);
                }
                

                return true;
            }

            catch
            {
                return false;
            }
            
        }

        public async static void StartReceive()
        {
            try
            {
                while (socket.State == WebSocketState.Open)
                {
                    ArraySegment<byte> bytes = new ArraySegment<byte>(new byte[1024]);

                    WebSocketReceiveResult result = await socket.ReceiveAsync(bytes, CancellationToken.None);
                    string values = Encoding.UTF8.GetString(bytes.Array, 0, result.Count);

                    if(jsonService.ReadJson(values, Response.code) == Response.Code.success.ToString())
                    {
                        string resultSet = jsonService.ReadJson(values, Response.result);
                        string deviceName = jsonService.ReadJson(resultSet, Property.Device.name);
                        bool state = bool.Parse(jsonService.ReadJson(resultSet, Property.Log.state));

                        
                        DependencyService.Get<IForegroundManager>().Update(deviceName, state);
                    }
                }
            }

            catch
            {
                DependencyService.Get<IToastMessage>().Alert("알람 에러");
            }

        }

        static bool testValue = true;

        public async static void Test()
        {
            string token = new PropertyService().Read(Property.User.token).ToString();
            ArraySegment<byte> bytes = new ArraySegment<byte>(Encoding.UTF8.GetBytes(token + ",무야호," + testValue.ToString()));
            await socket.SendAsync(bytes, WebSocketMessageType.Text, true, CancellationToken.None);

            testValue = !testValue;
        }
    }
}
