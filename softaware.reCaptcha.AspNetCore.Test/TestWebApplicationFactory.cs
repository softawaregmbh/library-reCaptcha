using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace softaware.reCaptcha.AspNetCore.Test
{
    public class TestWebApplicationFactory : WebApplicationFactory<TestStartup>
    {
        private readonly IVerifyCaptcha verifyCaptcha;

        public TestWebApplicationFactory(IVerifyCaptcha verifyCaptcha)
        {
            this.verifyCaptcha = verifyCaptcha ?? throw new System.ArgumentNullException(nameof(verifyCaptcha));
        }

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return new WebHostBuilder().UseStartup<TestStartup>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddTransient<IVerifyCaptcha>(s => this.verifyCaptcha);
            });
        }
    }
}
