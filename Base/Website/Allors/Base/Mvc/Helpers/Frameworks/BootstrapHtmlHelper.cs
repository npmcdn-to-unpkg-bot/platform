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
            return this.html.EditorForModel("Bootstrap/AllorsObject");
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

            if (modelType == typeof(Select))
            {
                return this.html.Editor(propertyModelMetadata.PropertyName, "Bootstrap/Select");
            }

            if (modelType == typeof(MultipleSelect))
            {
                return this.html.Editor(propertyModelMetadata.PropertyName, "Bootstrap/MultipleSelect");
            }

            var path = propertyModelMetadata.GetPath();
            var propertyType = path != null ? path.End.PropertyType : null;
            var roleType = propertyType as IRoleType;

            if (roleType != null)
            {
                var unitType = roleType.ObjectType as IUnit;
                if (unitType != null)
                {
                    switch (unitType.UnitTag)
                    {
                        case UnitTags.AllorsBinary:
                            return this.html.Editor(propertyModelMetadata.PropertyName, "Bootstrap/AllorsBinary");

                        case UnitTags.AllorsBoolean:
                            return this.html.Editor(propertyModelMetadata.PropertyName, "Bootstrap/AllorsBoolean");

                        case UnitTags.AllorsDateTime:
                            return this.html.Editor(propertyModelMetadata.PropertyName, "Bootstrap/AllorsDateTime");

                        case UnitTags.AllorsDecimal:
                            return this.html.Editor(propertyModelMetadata.PropertyName, "Bootstrap/AllorsDecimal");

                        case UnitTags.AllorsFloat:
                            return this.html.Editor(propertyModelMetadata.PropertyName, "Bootstrap/AllorsFloat");

                        case UnitTags.AllorsInteger:
                            return this.html.Editor(propertyModelMetadata.PropertyName, "Bootstrap/AllorsInteger");

                        case UnitTags.AllorsString:
                            return this.html.Editor(propertyModelMetadata.PropertyName, "Bootstrap/AllorsString");

                        case UnitTags.AllorsUnique:
                            return this.html.Editor(propertyModelMetadata.PropertyName, "Bootstrap/AllorsUnique");
                    }
                }
            }

            if (propertyType != null)
            {
                return this.html.Editor(propertyModelMetadata.PropertyName, "Bootstrap/AllorsProperty");
            }

            if (modelType == typeof(DateTime))
            {
                return this.html.Editor(propertyModelMetadata.PropertyName, "Bootstrap/DateTime");
            }

            return this.html.Editor(propertyModelMetadata.PropertyName, new { htmlAttributes = new { @class = "form-control", placeholder = this.html.ViewData.ModelMetadata.Watermark } });
        }

        protected virtual MvcHtmlString OnValidationMessage(ModelMetadata propertyModelMetadata)
        {
            return this.html.ValidationMessage(propertyModelMetadata.PropertyName, new { @class = "col-md-2 control-label" });
        }
    }
}