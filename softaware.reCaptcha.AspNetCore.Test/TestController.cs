using Microsoft.AspNetCore.Mvc;

namespace softaware.reCaptcha.AspNetCore.Test
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        public const string ReCaptchaHeaderKey = "g-recaptcha-token";

        [Route("HeaderKeyAndEnabled")]
        [ReCaptchaVerificationFilterFactory(HeaderKey = ReCaptchaHeaderKey, IsEnabled = true)]
        public ActionResult HeaderKeyAndEnabled()
        {
            return this.Ok();
        }

        [Route("HeaderKeyAndDisabled")]
        [ReCaptchaVerificationFilterFactory(HeaderKey = ReCaptchaHeaderKey, IsEnabled = false)]
        public ActionResult HeaderKeyAndDisabled()
        {
            return this.Ok();
        }

        [Route("NoHeaderKeyAndEnabled")]
        [ReCaptchaVerificationFilterFactory(IsEnabled = true)]
        public ActionResult NoHeaderKeyAndEnabled()
        {
            return this.Ok();
        }

        [Route("NoHeaderKeyAndDisabled")]
        [ReCaptchaVerificationFilterFactory(IsEnabled = false)]
        public ActionResult NoHeaderKeyAndDisabled()
        {
            return this.Ok();
        }
    }
}
