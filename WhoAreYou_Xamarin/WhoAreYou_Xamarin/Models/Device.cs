namespace WhoAreYou_Xamarin.Models
{
    class Device
    {
        private string log;

        public int index { get; set; }
        public string name { get; set; }
        public string lastLog 
        {
            get { return log + "분 전"; }
            set { log = value; }
        }
    }
}
