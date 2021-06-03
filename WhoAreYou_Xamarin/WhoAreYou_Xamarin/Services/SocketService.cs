using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WhoAreYou_Xamarin.Models.Property;
using WhoAreYou_Xamarin.Models.Url;
using WhoAreYou_Xamarin.Services.Dependencies;
using WhoAreYou_Xamarin.ViewModels;
using Xamarin.Forms;

namespace WhoAreYou_Xamarin.Services
{
    public class SocketService
    {
        private ClientWebSocket socket = new ClientWebSocket();
        private JsonService jsonService = new JsonService();
        private const string tokenHeaderName = "X-AUTH-TOKEN";

        private static SocketService instance;

        public static SocketService GetInstance()
        {
            if(instance == null)
            {
                instance = new SocketService();
            }

            return instance;
        }


        public bool IsConnect()
        { 
            if(socket == null)
            {
                socket = new ClientWebSocket();
            }

            if(socket.State != WebSocketState.Open)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> Connect(string token)
        {

            try
            {
                Uri uri = new Uri(Urls.SOCKET);
                socket.Options.SetRequestHeader(tokenHeaderName, token);
                await socket.ConnectAsync(uri, CancellationToken.None);

                return true;
            }

            catch
            {
                return false;
            }
            
        }

        public async void StartReceive()
        {
            try
            {
                PropertyService property = new PropertyService();
                await Connect(property.Read(Property.User.token).ToString());

                while (socket.State == WebSocketState.Open)
                {
                    ArraySegment<byte> bytes = new ArraySegment<byte>(new byte[1024]);

                    WebSocketReceiveResult result = await socket.ReceiveAsync(bytes, CancellationToken.None);
                    string values = Encoding.UTF8.GetString(bytes.Array, 0, result.Count);

                    string deviceName = jsonService.ReadJson(values, Property.Device.name);
                    string state = jsonService.ReadJson(values, Property.Log.state);

                    if(state == "create")
                    {
                        DependencyService.Get<DIPushAlarm>().Create(deviceName);
                        DevicesViewModel.GetInstance().Init();
                    }

                    else
                    {
                        bool isClosed = bool.Parse(jsonService.ReadJson(values, Property.Log.state));
                        bool userOpenAlert = bool.Parse(property.Read(Property.User.openAlert).ToString());
                        bool userCloseAlert = bool.Parse(property.Read(Property.User.closeAlert).ToString());

                        if ((!isClosed && userOpenAlert) || (isClosed && userCloseAlert))
                        {
                            DependencyService.Get<DIPushAlarm>().Update(deviceName, isClosed);
                        }
                    }
                   
                }
            }

            catch
            {
                
            }

            finally
            {
                socket.Dispose();
                DependencyService.Get<DIForeground>().StopRun();
            }

        }
    }
}
