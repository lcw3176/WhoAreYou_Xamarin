namespace WhoAreYou_Xamarin.Models.Response
{
    public static class Response
    {
        public static string code = "code";
        public static string result = "result";

        public static class Code
        {
            public static int notFound = 404;
            public static int success = 200;
            public static int notAllowed = 406;
            public static int wrongRequest = 400;
        }

        public static class Result
        {
            public static string notMember = "notMember";
            public static string existMember = "existMember";
            public static string wrongValue = "wrongValue";
            public static string wrongRequest = "wrongRequest";
            public static string notAllowed = "notAllowed";
        }
    }
}
