// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CssFrameworkAttribute.cs" company="Allors bvba">
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

namespace Allors.Web.Mvc.Views
{
    using System.Web.Mvc;

    public class CssFrameworkAttribute : FilterAttribute, IResultFilter
    {
        private const string Key = "Allors.CssFramework";

        private readonly string name;

        public CssFrameworkAttribute(string name)
        {
            this.name = name;
        }

        void IResultFilter.OnResultExecuted(ResultExecutedContext filterContext)
        {
        }

        void IResultFilter.OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.Controller.ViewData[Key] = this.name;
        }

        public static string GetValue(ViewDataDictionary viewData)
        {
            return (string)viewData[Key];
        }
    }
}