// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultHtmlHelper.cs" company="Allors bvba">
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
    using System.Web.Mvc.Html;

    public partial class DefaultHtmlHelper<TModel> : IHtmlHelper<TModel>
    {
        private readonly HtmlHelper<TModel> html;

        internal DefaultHtmlHelper(HtmlHelper<TModel> html)
        {
            this.html = html;
        }

        public MvcHtmlString DisplayForModel()
        {
            return this.html.DisplayForModel();
        }

        public MvcHtmlString Display(string expression)
        {
            var modelMetaData = ModelMetadata.FromStringExpression(expression, this.html.ViewData);
            return this.OnDisplay(modelMetaData);
        }

        public MvcHtmlString DisplayFor<TValue>(Expression<Func<TModel, TValue>> expression)
        {
            var modelMetaData = ModelMetadata.FromLambdaExpression(expression, this.html.ViewData);
            return this.OnDisplay(modelMetaData);
        }

        public virtual MvcHtmlString EditorForModel()
        {
            return this.html.EditorForModel();
        }

        public MvcHtmlString Label(string expression)
        {
            var modelMetaData = ModelMetadata.FromStringExpression(expression, this.html.ViewData);
            return this.OnLabel(modelMetaData);
        }

        public MvcHtmlString LabelFor<TValue>(Expression<Func<TModel, TValue>> expression)
        {
            var modelMetaData = ModelMetadata.FromLambdaExpression(expression, this.html.ViewData);
            return this.OnLabel(modelMetaData);
        }

        public MvcHtmlString Editor(string expression)
        {
            var modelMetaData = ModelMetadata.FromStringExpression(expression, this.html.ViewData);
            return this.OnEditor(modelMetaData);
        }

        public MvcHtmlString EditorFor<TValue>(Expression<Func<TModel, TValue>> expression)
        {
            var modelMetaData = ModelMetadata.FromLambdaExpression(expression, this.html.ViewData);
            return this.OnEditor(modelMetaData);
        }

        public MvcHtmlString ValidationMessage(string expression)
        {
            var modelMetaData = ModelMetadata.FromStringExpression(expression, this.html.ViewData);
            return this.OnValidationMessage(modelMetaData);
        }

        public MvcHtmlString ValidationMessageFor<TValue>(Expression<Func<TModel, TValue>> expression)
        {
            var modelMetaData = ModelMetadata.FromLambdaExpression(expression, this.html.ViewData);
            return this.OnValidationMessage(modelMetaData);
        }

        protected virtual MvcHtmlString OnDisplay(ModelMetadata modelMetadata)
        {
            return this.html.Display(modelMetadata.PropertyName);
        }

        protected virtual MvcHtmlString OnLabel(ModelMetadata modelMetadata)
        {
            return this.html.Label(modelMetadata.PropertyName);
        }

        protected virtual MvcHtmlString OnEditor(ModelMetadata modelMetadata)
        {
            return this.html.Editor(modelMetadata.PropertyName);
        }

        protected virtual MvcHtmlString OnValidationMessage(ModelMetadata modelMetadata)
        {
            return this.html.ValidationMessage(modelMetadata.PropertyName);
        }
    }
}