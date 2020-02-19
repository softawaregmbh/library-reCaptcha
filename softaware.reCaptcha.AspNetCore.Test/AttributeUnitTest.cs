using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace softaware.reCaptcha.AspNetCore.Test
{
    public class AttributeUnitTest
    {
        [Fact]
        public async Task Request_WithHeaderKey_Enabled_Success()
        {
            await this.TestRequestAsync(
                new SuccessVerifyCaptcha(), TestController.ReCaptchaHeaderKey, "api/test/HeaderKey", 200);
        }

        [Fact]
        public async Task Request_WithHeaderKey_Enabled_Error()
        {
            await this.TestRequestAsync(
                new ErrorVerifyCaptcha(), TestController.ReCaptchaHeaderKey, "api/test/HeaderKey", 419);
        }

        [Fact]
        public async Task Request_WithHeaderKey_Disabled_Success()
        {
            await this.TestRequestAsync(
                new SuccessVerifyCaptcha(), TestController.ReCaptchaHeaderKey, "api/test/HeaderKey", 200, false);
        }

        [Fact]
        public async Task Request_WithHeaderKey_Disabled_Error()
        {
            await this.TestRequestAsync(
                new ErrorVerifyCaptcha(), TestController.ReCaptchaHeaderKey, "api/test/HeaderKey", 200, false);
        }

        [Fact]
        public async Task Request_NoHeaderKey_Enabled_Success()
        {
            await this.TestRequestAsync(
                new SuccessVerifyCaptcha(), "re-captcha-token", "api/test/NoHeaderKey", 200);
        }

        [Fact]
        public async Task Request_NoHeaderKey_Enabled_Error()
        {
            await this.TestRequestAsync(
                new ErrorVerifyCaptcha(), "re-captcha-token", "api/test/NoHeaderKey", 419);
        }

        [Fact]
        public async Task Request_NoHeaderKey_Disabled_Success()
        {
            await this.TestRequestAsync(
                new SuccessVerifyCaptcha(), "re-captcha-token", "api/test/NoHeaderKey", 200, false);
        }

        [Fact]
        public async Task Request_NoHeaderKey_Disabled_Error()
        {
            await this.TestRequestAsync(
                new ErrorVerifyCaptcha(), "re-captcha-token", "api/test/NoHeaderKey", 200, false);
        }

        private async Task<HttpResponseMessage> TestRequestAsync(IVerifyCaptcha verifyCaptcha, string header, string endpoint, int expectedStatusCode, bool isReCaptchaEnabled = true)
        {
            using var client = this.GetHttpClient(verifyCaptcha, isReCaptchaEnabled);
            if (header != null)
            {
                client.DefaultRequestHeaders.Add(header, "test");
            }

            var response = await client.GetAsync(endpoint);
            Assert.Equal(expectedStatusCode, (int)response.StatusCode);
            return response;
        }

        private HttpClient GetHttpClient(IVerifyCaptcha verifyCaptcha, bool isReCaptchaEnabled)
        {
            var factory = new TestWebApplicationFactory(verifyCaptcha, isReCaptchaEnabled);
            return factory.CreateDefaultClient();
        }
    }
}
