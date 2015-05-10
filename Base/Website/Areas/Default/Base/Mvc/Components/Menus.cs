// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Controller.cs" company="Allors bvba">
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

namespace Allors.Web.Mvc
{
    using System.Collections.Generic;

    public static partial class Menus
    {
        private readonly static Dictionary<string, Menu> MenuByName = new Dictionary<string, Menu>();

        public static Menu Get(string name)
        {
            Menu menu;
            MenuByName.TryGetValue(name.ToLowerInvariant(), out menu);
            return menu;
        }

        public static void Set(string name, Menu menu)
        {
            MenuByName[name.ToLowerInvariant()] = menu;
        }

        public static void Remove(string name)
        {
            MenuByName.Remove(name);
        }
    }
}