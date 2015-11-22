// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TreeNode.cs" company="Allors bvba">
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

using System.Collections.Generic;

namespace Allors.Meta
{
    public class TreeNode
    {
        public TreeNode(RoleType roleType, Composite composite = null, List<TreeNode> nodes = null)
        {
            this.RoleType = roleType;
            this.Composite = composite;
            this.Nodes = nodes ?? new List<TreeNode>();
        }
        
        public RoleType RoleType { get; }

        public Composite Composite { get; }

        public List<TreeNode> Nodes { get; }

        public void Resolve(IObject obj, HashSet<IObject> objects)
        {
            if (obj != null)
            {
                if (this.RoleType.ObjectType.IsComposite)
                {
                    if (this.RoleType.IsOne)
                    {
                        var role = obj.Strategy.GetCompositeRole(this.RoleType);
                        if (role != null)
                        {
                            objects.Add(role);

                            foreach (var node in this.Nodes)
                            {
                                node.Resolve(role, objects);
                            }

                        }
                    }
                    else
                    {
                        var roles = obj.Strategy.GetCompositeRoles(this.RoleType);
                        foreach (IObject role in roles)
                        {
                            objects.Add(role);

                            foreach (var node in this.Nodes)
                            {
                                node.Resolve(role, objects);
                            }

                        }
                    }
                }

            }


        }

        public void BuildPrefetchPolicy(PrefetchPolicyBuilder prefetchPolicyBuilder)
        {
            prefetchPolicyBuilder.WithRule(this.RoleType);

            foreach (var node in this.Nodes)
            {
                node.BuildPrefetchPolicy(prefetchPolicyBuilder);
            }
        }
    }
}