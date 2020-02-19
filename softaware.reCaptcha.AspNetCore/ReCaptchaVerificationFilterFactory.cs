using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace softaware.reCaptcha.AspNetCore
{
    public class ReCaptchaVerificationFilterFactory : Attribute, IFilterFactory
    {
        public string HeaderKey { get; set; }

        public bool IsEnabled { get; set; } = true;

        public bool IsReusable => false;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            var verifyCaptcha = serviceProvider.GetService(typeof(IVerifyCaptcha)) as IVerifyCaptcha;
            var filter = new ReCaptchaVerificationActionFilter(verifyCaptcha);

            if (!string.IsNullOrWhiteSpace(this.HeaderKey))
            {
                filter.HeaderKey = this.HeaderKey;
            }

            filter.IsEnabled = this.IsEnabled;

            return filter;
        }
    }
}
