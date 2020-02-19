using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace softaware.reCaptcha.AspNetCore.Test
{
    public class TestWebApplicationFactory : WebApplicationFactory<TestStartup>
    {
        private readonly IVerifyCaptcha verifyCaptcha;
        private readonly bool isReCaptchaEnabled;

        public TestWebApplicationFactory(IVerifyCaptcha verifyCaptcha, bool isReCaptchaEnabled)
        {
            this.verifyCaptcha = verifyCaptcha ?? throw new System.ArgumentNullException(nameof(verifyCaptcha));
            this.isReCaptchaEnabled = isReCaptchaEnabled;
        }

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return new WebHostBuilder().UseStartup<TestStartup>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddTransient<ReCaptchaVerificationActionFilter>(s =>
                    new ReCaptchaVerificationActionFilter(this.verifyCaptcha, this.isReCaptchaEnabled));
            });
        }
    }
}
