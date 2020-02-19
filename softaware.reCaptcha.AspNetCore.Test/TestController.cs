using Microsoft.AspNetCore.Mvc;

namespace softaware.reCaptcha.AspNetCore.Test
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        public const string ReCaptchaHeaderKey = "g-recaptcha-token";

        [Route("HeaderKey")]
        [ReCaptchaVerificationFilterFactory(HeaderKey = ReCaptchaHeaderKey)]
        public ActionResult HeaderKey()
        {
            return this.Ok();
        }

        [Route("NoHeaderKey")]
        [ReCaptchaVerificationFilterFactory()]
        public ActionResult NoHeaderKey()
        {
            return this.Ok();
        }
    }
}
