namespace api.App
{
    public static class RouteParts
    {
        public const string Registration = "registration";
        public const string Verify = "verify";
        public const string Login = "login";
        public const string Refresh = "refresh";
        public const string Logout = "logout";
        public const string Profile = "profile";
        public const string Settings = "settings";
        public const string Your = "your";
        public const string ProfileSettings = Profile + "-" + Settings;
        public const string Bike = "bike";
        public const string Bikes = "bikes";
        public const string BikeId = "{bikeId:int:required}";
        public const string YourBikes = Your + "-" + Bikes;
        public const string DeleteYourBikes = YourBikes + "/" + BikeId;
        public const string BikeById = Bikes + "/" + Bike + "/" + BikeId;
    }
}
