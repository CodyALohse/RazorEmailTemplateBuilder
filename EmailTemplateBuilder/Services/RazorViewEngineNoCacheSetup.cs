using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.Extensions.Options;

namespace EmailTemplateBuilder.Services
{
    public class RazorViewEngineNoCacheSetup : IConfigureOptions<MvcViewOptions>
    {
        private readonly IRazorViewEngine _razorViewEngine;

        /// <summary>
        /// Initializes a new instance of <see cref="MvcRazorMvcViewOptionsSetup"/>.
        /// </summary>
        /// <param name="razorViewEngine">The <see cref="IRazorViewEngine"/>.</param>
        public RazorViewEngineNoCacheSetup(IRazorViewEngine razorViewEngine)
        {
            if (razorViewEngine == null)
            {
                throw new ArgumentNullException(nameof(razorViewEngine));
            }

            _razorViewEngine = razorViewEngine;
        }

        /// <summary>
        /// Configures <paramref name="options"/> to use <see cref="RazorViewEngine"/>.
        /// </summary>
        /// <param name="options">The <see cref="MvcViewOptions"/> to configure.</param>
        public void Configure(MvcViewOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            options.ViewEngines.Add(_razorViewEngine);
        }
    }
}
