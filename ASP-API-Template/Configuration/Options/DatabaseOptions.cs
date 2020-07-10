namespace ASP_API_Template.Configuration.Options
{
    public class DatabaseOptions
    {
        public string Host { get; set; } = "DbDomain.com";
        public string Database { get; set; } = "AccountAPI";
        public string Username { get; set; } = "AddUserName";
        public string Password { get; set; } = "AddPassword";

        public static DatabaseOptions Default
            => new DatabaseOptions();

        public string BuildConnectionString()
            => $"Host={Host};Database={Database};Username={Username};Password={Password}";
    }
}
