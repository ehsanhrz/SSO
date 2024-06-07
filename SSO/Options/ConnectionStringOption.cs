namespace SSO.Options
{
    public class ConnectionStringOption
    {
        public string OptionName { get; set; } = "ConnectionStrings";

        public string DefaultConnection { get; set; } = string.Empty;

        public string RedisHost { get; set; } = string.Empty;

        public string RedisPassword { get; set; } = string.Empty;

        public string RedisPort { get; set; } = string.Empty;
    }
}
