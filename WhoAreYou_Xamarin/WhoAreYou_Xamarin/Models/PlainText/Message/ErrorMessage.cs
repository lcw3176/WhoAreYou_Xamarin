namespace WhoAreYou_Xamarin.Models
{
    static class ErrorMessage
    {
        public static string empty = "공백은 허용되지 않습니다";
        public static string existUser = "이미 존재하는 유저입니다";
        public static string network = "네트워크 에러입니다";
        public static string notMember = "가입된 유저가 아닙니다";

        public static class Bluetooth
        {
            public static string Disabled = "블루투스와 Gps를 켜고 다시 진행해주세요";
            public static string ConnectionFailed = "장치와 연결에 실패했습니다";
            public static string alreadyExist = "이미 존재하는 기기명입니다";
        }

        public static class Wifi
        {
            public static string Disabled = "와이파이를 켜고 다시 진행해주세요";
            public static string ConnectionFailed = "장치와 연결에 실패했습니다";
        }

    }
}
