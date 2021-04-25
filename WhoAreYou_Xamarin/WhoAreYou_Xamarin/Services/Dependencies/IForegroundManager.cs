using System;
using System.Collections.Generic;
using System.Text;

namespace WhoAreYou_Xamarin.Services.Dependencies
{
    public interface IForegroundManager
    {
        bool IsRunning();
        void StartService();
        void StopRun();
    }
}
