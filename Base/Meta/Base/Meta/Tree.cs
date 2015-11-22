// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Tree.cs" company="Allors bvba">
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

namespace Allors.Meta
{
    using System.Collections.Generic;

    public class Tree
    {
        public Tree(Composite composite)
        {
            this.Composite = composite;
            this.Nodes = new List<TreeNode>();
        }

        public Composite Composite { get; }

        public List<TreeNode> Nodes { get; }

        public PrefetchPolicy BuildPrefetechPolicy()
        {
            var prefetchPolicyBuilder = new PrefetchPolicyBuilder();

            foreach (var node in this.Nodes)
            {
                node.BuildPrefetchPolicy(prefetchPolicyBuilder);
            }

            return prefetchPolicyBuilder.Build();
        }

        public Tree Add(RelationType relationType)
        {
            return this.Add(relationType.RoleType);
        }

        public Tree Add(RoleType roleType)
        {
            var tree = new TreeNode(roleType);
            this.Nodes.Add(tree);
            return this;
        }

        public Tree Add(RelationType relationType, Tree tree)
        {
            return this.Add(relationType.RoleType, tree);
        }

        public Tree Add(RoleType roleType, Tree tree)
        {
            var treeNode = new TreeNode(roleType, tree.Composite, tree.Nodes);
            this.Nodes.Add(treeNode);
            return this;
        }

        public void Resolve(IObject obj, HashSet<IObject> objects)
        {
            if (obj != null)
            {
                foreach (var node in this.Nodes)
                {
                    node.Resolve(obj, objects);
                }
            }
        }
    }
}