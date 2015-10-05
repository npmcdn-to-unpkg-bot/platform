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
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed partial class MethodType : OperandType
    {
        private static readonly string[] EmptyGroups = { };

        private string[] groups;
        private string name;
        private Composite objectType;

        public MethodType(MetaPopulation metaPopulation, Guid id)
            : base(metaPopulation)
        {
            this.Id = id;

            metaPopulation.OnMethodTypeCreated(this);
        }
        
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.MetaPopulation.AssertUnlocked();
                this.name = value;
                this.MetaPopulation.Stale();
            }
        }

        public string FullName
        {
            get
            {
                return this.ObjectType != null ? this.ObjectType.Name + this.name : this.Name;
            }
        }

        public Composite ObjectType
        {
            get
            {
                return this.objectType;
            }

            set
            {
                this.MetaPopulation.AssertUnlocked();
                this.objectType = value;
                this.MetaPopulation.Stale();
            }
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return this.name;
            }
        }

        public string[] Groups
        {
            get
            {
                return this.groups ?? EmptyGroups;
            }

            set
            {
                this.MetaPopulation.AssertUnlocked();

                this.groups = null;
                if (value != null)
                {
                    this.groups = new HashSet<string>(value).ToArray();
                }

                this.MetaPopulation.Stale();
            }
        }

        public void AddGroup(string @group)
        {
            if (@group != null)
            {
                this.Groups = new List<string>(this.Groups) { @group }.ToArray();
            }
        }

        public void AddGroups(string[] groups)
        {
            if (groups != null)
            {
                var newTags = new List<string>(this.Groups);
                newTags.AddRange(groups);
                this.Groups = newTags.ToArray();
            }
        }

        public void RemoveGroup(string @group)
        {
            if (@group != null)
            {
                var newGroup = new List<string>(this.Groups);
                newGroup.Remove(@group);
                this.Groups = newGroup.ToArray();
            }
        }

        /// <summary>
        /// Gets the validation name.
        /// </summary>
        /// <value>The validation name.</value>
        protected internal override string ValidationName
        {
            get
            {
                if (!string.IsNullOrEmpty(this.name))
                {
                    return "method type " + this.name;
                }

                return "unknown method type";
            }
        }

        /// <summary>
        /// Validates the instance.
        /// </summary>
        /// <param name="validationLog">The validation.</param>
        protected internal override void Validate(ValidationLog validationLog)
        {
            base.Validate(validationLog);

            if (string.IsNullOrEmpty(this.name))
            {
                var message = this.ValidationName + " has no name";
                validationLog.AddError(message, this, ValidationKind.Required, "MethodType.Name");
            }
        }
    }
}