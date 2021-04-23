namespace WhoAreYou_Xamarin.Models
{
    public static class Foreground
    {
        public static string title = "알림";
        public static string content 
        {
            get 
            {
                if(count > 0)
                {
                    return count + "개의 알림이 있습니다";
                }

                else
                {
                    return "장치가 작동하면 알려드릴게요";
                }
                
            }
        }
        public static int count = 0;
    }
}
