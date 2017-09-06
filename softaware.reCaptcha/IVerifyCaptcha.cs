using System.Threading.Tasks;

namespace softaware.reCaptcha
{
    public interface IVerifyCaptcha
    {
        Task VerifyAsync(string captchaResponse, string remoteIP);
    }
}
