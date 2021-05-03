using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace WhoAreYou_Xamarin.ViewModels
{
    class BaseViewModel : INotifyPropertyChanged
    {
        protected static Queue<string> logQueue = new Queue<string>();
        protected static ManualResetEvent logController = new ManualResetEvent(false);

        protected static Queue<string> deviceQueue = new Queue<string>();
        protected static ManualResetEvent deviceController = new ManualResetEvent(false);

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyUpdate(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

        }

        /// <summary>
        /// 새로운 기기 등록
        /// </summary>
        /// <param name="deviceName"></param>
        protected void EnqueueDevice(string deviceName)
        {
            deviceQueue.Enqueue(deviceName);
            deviceController.Set();
        }


        /// <summary>
        /// 조회할 기기 이름 등록
        /// </summary>
        /// <param name="deviceName"></param>
        protected void EnqueueLog(string deviceName)
        {
            logQueue.Enqueue(deviceName);
            logController.Set();
        }
    }
}
