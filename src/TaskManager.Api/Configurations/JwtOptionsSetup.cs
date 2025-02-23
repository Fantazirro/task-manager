using Microsoft.Extensions.Options;
using TaskManager.Infrastructure.Authentication;

namespace TaskManager.Api.Configurations
{
    public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
    {
        private readonly IConfiguration _configuration;

        public JwtOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(JwtOptions options)
        {
            _configuration.GetSection("JwtOptions").Bind(options);
        }
    }
}