using System.Threading.Tasks;

namespace softaware.reCaptcha.AspNetCore.Test
{
    public class SuccessVerifyCaptcha : IVerifyCaptcha
    {
        public Task VerifyAsync(string captchaResponse, string remoteIP = null)
        {
            return Task.CompletedTask;
        }
    }
}