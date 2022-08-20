using SenseHome.Services.Configurations;

namespace SenseHome.API.Configuration.Model
{
    public class SenseHomeApiConfiguration
    {
        public AuthenticationConfiguration AuthenticationConfiguration { get; set; }
        public InternalCredentialConfiguration InternalCredentialConfiguration { get; set; }
    }
}
