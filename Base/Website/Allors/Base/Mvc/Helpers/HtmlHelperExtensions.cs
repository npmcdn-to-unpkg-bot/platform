// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HtmlHelperExtensions.cs" company="Allors bvba">
//   Copyright 2002-2013 Allors bvba.
// 
// Dual Licensed under
//   a) the General Public Licence v3 (GPL)
//   b) the Allors License
// 
// The GPL License is included in the file gpl.txt.
// The Allors License is an addendum to your contract.
// 
// Allors Applications is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// For more information visit http://www.allors.com/legal
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Web.Mvc.Helpers
{
    using System.Web.Mvc;

    using Allors.Web.Mvc.Views;

    public static class HtmlHelperExtensions
    {
        private static IHtmlHelperFactory factory = new HtmlHelperFactory();

        public static IHtmlHelperFactory Factory
        {
            get
            {
                return factory;
            }

            set
            {
                factory = value;
            }
        }

        public static IHtmlHelper<TModel> Allors<TModel>(this HtmlHelper<TModel> html)
        {
            var rootViewContext = html.ViewContext.RootViewContext();
            var cssFramework = CssFrameworkAttribute.GetValue(rootViewContext.ViewData);
            return Factory.Create(html, cssFramework);
        }
    }
}