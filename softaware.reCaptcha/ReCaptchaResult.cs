using System;
using Newtonsoft.Json;

namespace softaware.reCaptcha
{
    public class ReCaptchaResult
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("challenge_ts")]
        public DateTime ChallengeDate { get; set; }

        [JsonProperty("hostname")]
        public string Hostname { get; set; }

        [JsonProperty("error-codes")]
        public string[] ErrorCodes { get; set; }
    }
}
