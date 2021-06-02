using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace WhoAreYou_Xamarin.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected static Queue<string> logQueue = new Queue<string>();
        protected static ManualResetEvent logController = new ManualResetEvent(false);

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyUpdate(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

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
