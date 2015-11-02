namespace Website
{
    using System;
    using System.Globalization;
    using System.Threading;
    using System.Web.Configuration;
    using System.Web.Http.Controllers;

    using Allors.Api;

    public class CustomApiController : AllorsApiController
    {
        private const string DefaultCultureName = "en-US";
        
        public bool IsProduction
        {
            get
            {
                var production = WebConfigurationManager.AppSettings["production"];
                return string.IsNullOrWhiteSpace(production) || bool.Parse(production);
            }
        }

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            var cultureName = DefaultCultureName;
            var locale = this.AuthenticatedUser?.Locale;

            if (!string.IsNullOrWhiteSpace(locale?.Name))
            {
                cultureName = locale.Name;
            }

            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            }
            catch
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(DefaultCultureName);
                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            }
        }
    }
}