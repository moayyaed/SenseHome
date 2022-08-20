namespace SenseHome.Services.Configurations
{
    public class AuthenticationConfiguration
    {
        public string Secret { get; set; }
        public int TokenValidityInDays { get; set; }
    }
}
