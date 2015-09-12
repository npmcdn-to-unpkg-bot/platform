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
        private readonly RoleType roleType;

        private readonly List<TreeNode> nodes;

        public TreeNode(RoleType roleType, List<TreeNode> nodes = null)
        {
            this.roleType = roleType;
            this.nodes = nodes ?? new List<TreeNode>();
        }

        public RoleType RoleType
        {
            get { return roleType; }
        }

        public List<TreeNode> Nodes
        {
            get { return nodes; }
        }

        public void Resolve(IObject obj, HashSet<IObject> objects)
        {
            if (obj != null)
            {
                if (this.roleType.ObjectType.IsComposite)
                {
                    if (this.roleType.IsOne)
                    {
                        var role = obj.Strategy.GetCompositeRole(this.roleType);
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
                        var roles = obj.Strategy.GetCompositeRoles(this.roleType);
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
    }
}