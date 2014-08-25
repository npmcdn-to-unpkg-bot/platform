//------------------------------------------------------------------------------------------------- 
// <copyright file="MethodType.cs" company="Allors bvba">
// Copyright 2002-2013 Allors bvba.
// 
// Dual Licensed under
//   a) the Lesser General Public Licence v3 (LGPL)
//   b) the Allors License
// 
// The LGPL License is included in the file lgpl.txt.
// The Allors License is an addendum to your contract.
// 
// Allors Platform is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// For more information visit http://www.allors.com/legal
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace Allors.Meta
{
    using AllorsGenerated;

    public partial class MethodType
    {
        /// <summary>
        /// Gets the display name.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return this.Name;
            }
        }

        /// <summary>
        /// Gets the validation name.
        /// </summary>
        /// <value>The validation name.</value>
        protected override string ValidationName
        {
            get
            {
                if (ExistName)
                {
                    return "method type " + this.Name;
                }

                return "unknown method type";
            }
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <returns>The new instance.</returns>
        internal static MethodType Create(AllorsEmbeddedSession session)
        {
            var methodType = (MethodType)session.Create(AllorsEmbeddedDomain.MethodType);
            return methodType;
        }
        
        /// <summary>
        /// Validates the instance.
        /// </summary>
        /// <param name="validationLog">The validation.</param>
        protected internal override void Validate(ValidationLog validationLog)
        {
            base.Validate(validationLog);

            if (!ExistName)
            {
                var message = this.ValidationName + " has no name";
                validationLog.AddError(message, this, ValidationKind.Required, AllorsEmbeddedDomain.MethodTypeName);
            }
        }
    }
}