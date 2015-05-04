// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IdPostfixAnnotation.cs" company="Allors bvba">
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

namespace Allors.Web.Mvc.Models.Annotations
{
    using System.Web.Mvc;

    public partial class HidePostfixIdsAnnotation : IPropertyMetadataAware 
    {
        public void OnPropertyMetadataCreated(ModelMetadata modelMetadata)
        {
            if (modelMetadata.PropertyName.EndsWith("Id"))
            {
                modelMetadata.TemplateHint = @"HiddenInput";
            }
        }
    }
}