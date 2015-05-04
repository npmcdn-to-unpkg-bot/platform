// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BootstrapHtmlHelper.cs" company="Allors bvba">
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
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    using Allors.Meta;
    using Allors.Web.Mvc.Models;

    public partial class BootstrapHtmlHelper<TModel> : IHtmlHelper<TModel>
    {
        private readonly HtmlHelper<TModel> html;

        internal BootstrapHtmlHelper(HtmlHelper<TModel> html)
        {
            this.html = html;
        }

        public virtual MvcHtmlString EditorForModel()
        {
            return this.html.EditorForModel("Bootstrap/object");
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

        protected virtual MvcHtmlString OnLabel(ModelMetadata propertyModelMetadata)
        {
            return this.html.Label(propertyModelMetadata.PropertyName, new { @class = "col-md-2 control-label" });
        }

        protected virtual MvcHtmlString OnEditor(ModelMetadata propertyModelMetadata)
        {
            var modelType = Nullable.GetUnderlyingType(propertyModelMetadata.ModelType) ?? propertyModelMetadata.ModelType;

            if (modelType == typeof(IMetadataModel))
            {
                return this.html.Editor(propertyModelMetadata.PropertyName, "Bootstrap/object");
            }

            if (modelType == typeof(Select))
            {
                return this.html.Editor(propertyModelMetadata.PropertyName, "Bootstrap/select");
            }

            if (modelType == typeof(MultipleSelect))
            {
                return this.html.Editor(propertyModelMetadata.PropertyName, "Bootstrap/multipleSelect");
            }

            if (modelType == typeof(bool))
            {
                return this.html.Editor(propertyModelMetadata.PropertyName, "Bootstrap/bool");
            }

            if (modelType == typeof(DateTime))
            {
                return this.html.Editor(propertyModelMetadata.PropertyName, "Bootstrap/dateTime");
            }

            if (modelType == typeof(decimal))
            {
                return this.html.Editor(propertyModelMetadata.PropertyName, "Bootstrap/decimal");
            }

            if (modelType == typeof(double))
            {
                return this.html.Editor(propertyModelMetadata.PropertyName, "Bootstrap/double");
            }

            if (modelType == typeof(Guid))
            {
                return this.html.Editor(propertyModelMetadata.PropertyName, "Bootstrap/guid");
            }

            if (modelType == typeof(long))
            {
                return this.html.Editor(propertyModelMetadata.PropertyName, "Bootstrap/long");
            }

            if (modelType == typeof(string))
            {
                return this.html.Editor(propertyModelMetadata.PropertyName, "Bootstrap/string");
            }

            if (modelType == typeof(HttpPostedFileBase))
            {
                return this.html.Editor(propertyModelMetadata.PropertyName, "Bootstrap/file");
            }

            return this.html.Editor(propertyModelMetadata.PropertyName, new { htmlAttributes = new { @class = "form-control", placeholder = this.html.ViewData.ModelMetadata.Watermark } });
        }

        protected virtual MvcHtmlString OnValidationMessage(ModelMetadata propertyModelMetadata)
        {
            return this.html.ValidationMessage(propertyModelMetadata.PropertyName, new { @class = "col-md-2 control-label" });
        }
    }
}