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
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public partial class Menu : IEnumerable<MenuItem>
    {
        private readonly List<MenuItem> items = new List<MenuItem>();

        public IEnumerator<MenuItem> GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public Menu Add(MenuItem menuItem)
        {
            this.items.Add(menuItem);
            return this;
        }

        public MenuForView For(ViewContext context)
        {
            var controller = context.RootController();
            var typeName = controller.GetType().Name;
            var controllerName = typeName.ToLowerInvariant().EndsWith("controller")
                                      ? typeName.Substring(0, typeName.ToLowerInvariant().LastIndexOf("controller", StringComparison.Ordinal))
                                      : null;
            var actionName = (string)controller.ControllerContext.RouteData.Values["action"];

            if (controller is Controller)
            {
                return new MenuForView(this, controllerName, actionName, ((Controller)controller).AuthenticatedUser);
            }

            return new MenuForView(this, controllerName, actionName, null);
        }
    }
}

 