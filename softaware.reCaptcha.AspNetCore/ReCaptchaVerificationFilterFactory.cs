using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace softaware.reCaptcha.AspNetCore
{
    public class ReCaptchaVerificationFilterFactory : Attribute, IFilterFactory
    {
        public string HeaderKey { get; set; }

        public bool IsReusable => false;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            if (!(serviceProvider.GetService(typeof(ReCaptchaVerificationActionFilter)) is ReCaptchaVerificationActionFilter filter))
            {
                if (!(serviceProvider.GetService(typeof(IVerifyCaptcha)) is IVerifyCaptcha verifyCaptcha))
                {
                    throw new ArgumentNullException($"Missing services - register either {nameof(IVerifyCaptcha)} or {nameof(ReCaptchaVerificationActionFilter)}");
                }

                filter = new ReCaptchaVerificationActionFilter(verifyCaptcha);
            }

            if (!string.IsNullOrWhiteSpace(this.HeaderKey))
            {
                filter.HeaderKey = this.HeaderKey;
            }

            return filter;
        }
    }
}
