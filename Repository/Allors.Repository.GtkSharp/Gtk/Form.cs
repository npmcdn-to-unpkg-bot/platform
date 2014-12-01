﻿//------------------------------------------------------------------------------------------------- 
// <copyright file="Form.cs" company="Allors bvba">
// Copyright 2002-2009 Allors bvba.
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

namespace Allors.Meta.GtkSharp
{
    using Gtk;

    public class Form : Table
    {
        public Form()
            : base(1, 1, false)
        {
            this.RowSpacing = 6;
            this.ColumnSpacing = 15;
            this.BorderWidth = 15;
        }
    }
}
