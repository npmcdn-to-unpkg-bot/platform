//------------------------------------------------------------------------------------------------- 
// <copyright file="PrefetchRule.cs" company="Allors bvba">
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
// <summary>Defines the ObjectIdInteger type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    using Allors.Meta;

    public sealed class PrefetchPolicy : IEnumerable<PrefetchRule>
    {
        private readonly PrefetchRule[] prefetchRules;
        private readonly Guid id;

        internal PrefetchPolicy(PrefetchRule[] rules)
        {
            if (rules == null)
            {
                throw new ArgumentNullException("rules");
            }

            this.prefetchRules = new List<PrefetchRule>(rules).ToArray();
            this.id = Guid.NewGuid();
        }

        public Guid Id
        {
            get
            {
                return this.id;
            }
        }

        public PrefetchRule[] PrefetchRules
        {
            get
            {
                return this.prefetchRules;
            }
        }

        public bool AllowCompilation { get; set; }

        public static implicit operator PrefetchPolicy(PrefetchRule prefetchRule)
        {
            var rules = new[] { prefetchRule };
            return new PrefetchPolicy(rules)
                        {
                            AllowCompilation = false
                        };
        }

        public static implicit operator PrefetchPolicy(IPropertyType[] propertyTypes)
        {
            var rules = propertyTypes.Select(x => new PrefetchRule(x, null)).ToArray();
            return new PrefetchPolicy(rules)
                       {
                           AllowCompilation = false
                       };
        }

        public IEnumerator<PrefetchRule> GetEnumerator()
        {
            return ((IEnumerable<PrefetchRule>)this.prefetchRules).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.prefetchRules.GetEnumerator();
        }
    }
}