// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHtmlHelper.cs" company="Allors bvba">
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
    using System;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    public partial interface IHtmlHelper<TModel>
    {
        MvcHtmlString DisplayForModel();

        MvcHtmlString Display(string expression);

        MvcHtmlString DisplayFor<TValue>(Expression<Func<TModel, TValue>> expression);

        MvcHtmlString EditorForModel();

        MvcHtmlString Label(string expression);

        MvcHtmlString LabelFor<TValue>(Expression<Func<TModel, TValue>> expression);

        MvcHtmlString Editor(string expression);

        MvcHtmlString EditorFor<TValue>(Expression<Func<TModel, TValue>> expression);

        MvcHtmlString ValidationMessage(string expression);
    }
}