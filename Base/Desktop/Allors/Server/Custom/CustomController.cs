namespace Website
{
    using System;
    using System.Globalization;
    using System.Threading;
    using System.Web.Configuration;

    public class CustomController : Allors.Web.Mvc.Controller
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

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            var cultureName = DefaultCultureName;
            if (this.AuthenticatedUser != null)
            {
                var locale = this.AuthenticatedUser.Locale;
                if (locale != null && !string.IsNullOrWhiteSpace(locale.Name))
                {
                    cultureName = locale.Name;
                }
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

            this.ViewBag.Locale = cultureName;
            this.ViewBag.Class = IsProduction ? "production" : "test";

            return base.BeginExecuteCore(callback, state);
        }

    }
}