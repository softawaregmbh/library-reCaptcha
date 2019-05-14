﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using softaware.reCaptcha.Exceptions;

namespace softaware.reCaptcha.AspNetCore
{
    public class ReCaptchaVerificationActionFilter : IAsyncActionFilter
    {
        private const string DefaultHeaderKey = "re-captcha-token";

        private readonly IVerifyCaptcha verifyCaptcha;

        public string HeaderKey { get; set; } = DefaultHeaderKey;

        public bool UseRemoteIpVerification { get; set; } = false;

        public ReCaptchaVerificationActionFilter(IVerifyCaptcha verifyCaptcha)
        {
            this.verifyCaptcha = verifyCaptcha ?? throw new System.ArgumentNullException(nameof(verifyCaptcha));
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.Request.Headers.TryGetValue(this.HeaderKey, out var token))
            {
                string ipAddress = null;
                if (context.HttpContext?.Connection?.RemoteIpAddress != null && this.UseRemoteIpVerification)
                {
                    ipAddress = context.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
                }

                try
                {
                    await this.verifyCaptcha.VerifyAsync(token, ipAddress);
                    await next();
                }
                catch (GoogleServerNotAvailableException)
                {
                    // do nothing - set status code result to 419
                }
                catch (NotVerifiedException)
                {
                    // do nothing - set status code result to 419
                }
            }

            context.Result = new StatusCodeResult(419);
        }
    }
}
