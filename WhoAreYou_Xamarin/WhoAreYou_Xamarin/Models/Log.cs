using System;

namespace WhoAreYou_Xamarin.Models
{
    class Log
    {

        public DateTime time { get; set; }
        public bool state { get; set; }
        public string stateString 
        {
            get 
            {
                if(state)
                {
                    return "열림";
                }

                else
                {
                    return "닫힘";
                }
            } 
        }
    }
}
