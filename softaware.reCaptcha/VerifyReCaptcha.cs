using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Polly;
using softaware.reCaptcha.Exceptions;
using softaware.reCaptcha.Models;

namespace softaware.reCaptcha
{
    public class VerifyReCaptcha : IVerifyCaptcha
    {
        private readonly HttpClient httpClient;
        private readonly string secret;
        private readonly string url;

        public VerifyReCaptcha(string secret, string url, HttpClient httpClient)
        {
            this.secret = secret ?? throw new ArgumentNullException(nameof(secret));
            this.url = url ?? throw new ArgumentNullException(nameof(url));
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task VerifyAsync(string captchaResponse, string remoteIP = null)
        {
            var keys = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("secret", this.secret),
                    new KeyValuePair<string, string>("response", captchaResponse)
                };

            if (remoteIP != null)
            {
                keys.Add(new KeyValuePair<string, string>("remoteip", remoteIP));
            }

            // retry for 3 times if google server is not available
            var response = await Policy
                                    .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                                    .RetryAsync(3)
                                    .ExecuteAsync(() => this.httpClient.PostAsync(this.url, new FormUrlEncodedContent(keys)));

            if (!response.IsSuccessStatusCode)
            {
                throw new GoogleServerNotAvailableException(this.url, response.StatusCode);
            }

            var result = await response.Content.ReadFromJsonAsync<ReCaptchaResult>();
            if (result == null || !result.Success)
            {
                throw new NotVerifiedException();
            }
        }
    }
}
