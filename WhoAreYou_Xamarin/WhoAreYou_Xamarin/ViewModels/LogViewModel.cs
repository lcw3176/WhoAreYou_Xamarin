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
        private static LogViewModel instance;

        public string DeviceName
        {
            get { return deviceName; }
            set
            {
                deviceName = value;
                OnPropertyUpdate("DeviceName");
            }
        }

        private LogViewModel()
        {
            Task.Run(() => DequeueLog());
        }

        public static LogViewModel GetInstance()
        {
            if(instance == null)
            {
                instance = new LogViewModel();
            }

            return instance;
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
                        Init();
                    }

                    logController.Reset();
                    logController.WaitOne(Timeout.Infinite);
                }
            }

            catch { }
        }


        public async void Init()
        {
            string token = propertyService.Read(Property.User.token).ToString();
            string userId = propertyService.Read(Property.User.email).ToString();
            string result = await webService.SendGetWithToken(Urls.LOG, token, userId, DeviceName);

            if (jsonService.ReadJson(result, Response.code) == Response.Code.success.ToString())
            {
                result = jsonService.ReadJson(result, Response.result);
                
                var stateList = jsonService.ReadJArray(result, Property.Log.state);
                var timeList = jsonService.ReadJArray(result, Property.Log.time);
                DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);

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
