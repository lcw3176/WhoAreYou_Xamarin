using System;

namespace WhoAreYou_Xamarin.Models
{
    class Log
    {

        public DateTime time { get; set; }
        public bool isClosed { get; set; }
        public string stateString 
        {
            get 
            {
                if(isClosed)
                {
                    return "닫힘";
                }

                else
                {
                    return "열림";
                }
            } 
        }
    }
}
