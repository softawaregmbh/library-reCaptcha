using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Polly;

namespace softaware.reCaptcha
{
    public class VerifyReCaptcha : IVerifyCaptcha
    {
        private readonly string secret;
        private readonly string url;

        public VerifyReCaptcha(string secret, string url)
        {
            this.secret = secret ?? throw new ArgumentNullException(nameof(secret));
            this.url = url ?? throw new ArgumentNullException(nameof(url));
        }

        public async Task VerifyAsync(string captchaResponse, string remoteIP)
        {
            using (var client = new HttpClient())
            {
                var keys = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("secret", this.secret),
                    new KeyValuePair<string, string>("response", captchaResponse),
                    new KeyValuePair<string, string>("remoteip", remoteIP)
                };

                // retry for 3 times if google server is not available
                var response = await Policy
                                        .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                                        .Retry(3)
                                        .ExecuteAsync(() => client.PostAsync(this.url, new FormUrlEncodedContent(keys)));

                if (!response.IsSuccessStatusCode)
                {
                    throw new GoogleServerNotAvailableException(this.url, response.StatusCode);
                }

                var content = await response.Content.ReadAsStringAsync();
                var reCaptchaResult = JsonConvert.DeserializeObject<ReCaptchaResult>(content);
                if (!reCaptchaResult.Success)
                {
                    throw new NotVerifiedException();
                }
            }
        }
    }
}
