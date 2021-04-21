using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using WhoAreYou_Xamarin.Models;
using WhoAreYou_Xamarin.Models.Property;
using WhoAreYou_Xamarin.Models.Response;
using WhoAreYou_Xamarin.Models.Url;
using WhoAreYou_Xamarin.Services;

namespace WhoAreYou_Xamarin.ViewModels
{
    class LogViewModel : BaseViewModel
    {
        private string deviceName;

        public ObservableCollection<Log> logCollection { get; set; } =  new ObservableCollection<Log>();
        private WebService webService = new WebService();
        private PropertyService propertyService = new PropertyService();
        private JsonService jsonService = new JsonService();

        public string DeviceName
        {
            get { return deviceName; }
            set
            {
                deviceName = value;
                OnPropertyUpdate("DeviceName");
            }
        }

        public LogViewModel()
        {
            Task.Run(() => DequeueLog());
        }

        /// <summary>
        /// 장치명 등록되면 기록 인출
        /// </summary>
        private void DequeueLog()
        {
            try
            {
                while (true)
                {
                    while (logQueue.Count > 0)
                    {
                        logCollection.Clear();
                        DeviceName = logQueue.Dequeue();
                        Init(DeviceName);
                    }

                    controller.Reset();
                    controller.WaitOne(Timeout.Infinite);
                }
            }

            catch { }
        }


        private async void Init(string deviceName)
        {
            string result = await webService.SendGetWithToken(Urls.LOG, propertyService.Read(Property.User.token).ToString(), deviceName);

            if (jsonService.ReadJson(result, Response.code) == Response.Code.success.ToString())
            {
                result = jsonService.ReadJson(result, Response.result);
                
                var stateList = jsonService.ReadJArray(result, Property.Log.state);
                var timeList = jsonService.ReadJArray(result, Property.Log.time);
                DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

                for (int i = 0; i < stateList.Count; i++)
                {
                    logCollection.Add(new Log()
                    {
                        state = bool.Parse(stateList[i]),
                        time = dt.AddMilliseconds(double.Parse(timeList[i]))
                                 .ToLocalTime()
                    });
                }



            }
        }
    }
}
