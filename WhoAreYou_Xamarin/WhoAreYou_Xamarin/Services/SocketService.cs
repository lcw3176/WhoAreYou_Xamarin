﻿using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WhoAreYou_Xamarin.Models.Property;
using WhoAreYou_Xamarin.Models.Url;
using WhoAreYou_Xamarin.Services.Dependencies;
using Xamarin.Forms;

namespace WhoAreYou_Xamarin.Services
{
    public static class SocketService
    {
        private static ClientWebSocket socket = new ClientWebSocket();
        private static JsonService jsonService = new JsonService();
        private const string tokenHeaderName = "X-AUTH-TOKEN";

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
                    socket.Options.SetRequestHeader(tokenHeaderName, token);
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
                PropertyService property = new PropertyService();

                while (socket.State == WebSocketState.Open)
                {
                    ArraySegment<byte> bytes = new ArraySegment<byte>(new byte[1024]);

                    WebSocketReceiveResult result = await socket.ReceiveAsync(bytes, CancellationToken.None);
                    string values = Encoding.UTF8.GetString(bytes.Array, 0, result.Count);

                    string deviceName = jsonService.ReadJson(values, Property.Device.name);
                    bool isOpen = bool.Parse(jsonService.ReadJson(values, Property.Log.state));
                    bool userOpenAlert = bool.Parse(property.Read(Property.User.openAlert).ToString());
                    bool userCloseAlert = bool.Parse(property.Read(Property.User.closeAlert).ToString());

                    if ((isOpen && userOpenAlert) || (!isOpen && userCloseAlert))
                    {
                        DependencyService.Get<DIPushAlarm>().Update(deviceName, isOpen);
                    }

                    
                    
                }
            }

            catch
            {
                DependencyService.Get<DIForeground>().StopRun();
            }

        }

        static bool testValue = true;

        public async static void Test()
        {
            string email = new PropertyService().Read(Property.User.email).ToString();
            ArraySegment<byte> bytes = new ArraySegment<byte>(Encoding.UTF8.GetBytes(email + ",무야호," + testValue.ToString()));
            await socket.SendAsync(bytes, WebSocketMessageType.Text, true, CancellationToken.None);

            testValue = !testValue;
        }
    }
}
