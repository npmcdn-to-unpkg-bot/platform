// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelectedEventArgs.cs" company="Allors bvba">
//   Copyright 2002-2009 Allors bvba.
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
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Meta.GtkSharp.Explorer
{
    using Allors.Meta;

    public delegate void SelectedEventHandler(object sender, SelectedEventArgs args);

    public class SelectedEventArgs
    {
        internal SelectedEventArgs(XmlRepository repository, object selection, object selectionForPropertyGrid)
        {
            this.Repository = repository;
            this.Selection = selection;
            this.SelectionForPropertyGrid = selectionForPropertyGrid;
        }

        public XmlRepository Repository { get; private set; }

        public object Selection { get; private set; }

        public object SelectionForPropertyGrid { get; private set; }
    }
}