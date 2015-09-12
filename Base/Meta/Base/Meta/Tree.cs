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

using System.Collections.Generic;

namespace Allors.Meta
{
    public class Tree
    {
        private readonly Composite composite;

        private readonly List<TreeNode> nodes;

        public Tree(Composite composite)
        {
            this.composite = composite;
            this.nodes = new List<TreeNode>();
        }

        public Composite Composite
        {
            get { return composite; }
        }

        public List<TreeNode> Nodes
        {
            get { return nodes; }
        }

        public Tree Add(RoleType roleType)
        {
            var tree = new TreeNode(roleType);
            this.nodes.Add(tree);
            return this;
        }

        public Tree Add(RoleType roleType, Tree tree)
        {
            var treeNode = new TreeNode(roleType, tree.Nodes);
            this.nodes.Add(treeNode);
            return this;
        }

        public void Resolve(IObject obj, HashSet<IObject> objects)
        {
            if (obj != null)
            {
                objects.Add(obj);

                foreach (var node in this.Nodes)
                {
                    node.Resolve(obj, objects);
                }

            }
        }
    }
}