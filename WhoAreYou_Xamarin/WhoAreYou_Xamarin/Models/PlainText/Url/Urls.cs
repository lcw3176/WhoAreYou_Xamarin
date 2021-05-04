namespace WhoAreYou_Xamarin.Models.Url
{
    public static class Urls
    {
        public static string ip = "172.30.1.40";

        public static string SOCKET = "ws://" + ip + ":8080/auth/socket";

        public static string SIGNIN = "http://" + ip + ":8080/users";
        public static string SIGNUP = "http://" + ip + ":8080/users";

        public static string AUTHCHECK = "http://" + ip + ":8080/validation";

        public static string LOG = "http://" + ip + ":8080/auth/logs";
        public static string DEVICE = "http://" + ip + ":8080/auth/devices";
    }
}
