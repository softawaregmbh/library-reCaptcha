using System.Threading.Tasks;
using softaware.reCaptcha.Exceptions;

namespace softaware.reCaptcha.AspNetCore.Test
{
    public class ErrorVerifyCaptcha : IVerifyCaptcha
    {
        public Task VerifyAsync(string captchaResponse, string remoteIP = null)
        {
            throw new NotVerifiedException();
        }
    }
}
