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
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    using ExpressionHelper = Microsoft.Web.Mvc.Internal.ExpressionHelper;

    public partial class MenuItem : IEnumerable<MenuItem>
    {
        private readonly List<MenuItem> items = new List<MenuItem>();

        public MenuItem Action<TController>(Expression<Action<TController>> expression) where TController : System.Web.Mvc.Controller
        {
            var valuesFromExpression = ExpressionHelper.GetRouteValuesFromExpression(expression);

            this.ControllerName = (string)valuesFromExpression["Controller"];
            this.ActionName = (string)valuesFromExpression["Action"];
            this.Area = (string)valuesFromExpression["Area"];

            var call = ((MethodCallExpression)expression.Body).Method;
            
            var authorizeAttribute = (AuthorizeAttribute)call.GetCustomAttributes(typeof(AuthorizeAttribute), true).FirstOrDefault();
            if (authorizeAttribute != null)
            {
                this.Authorize = true;
                if (!string.IsNullOrWhiteSpace(authorizeAttribute.Roles))
                {
                    this.Roles = authorizeAttribute.Roles.Split(',').Select(role => role.Trim()).ToArray();
                }
            }

            if (string.IsNullOrEmpty(this.LinkText))
            {
                if (this.ActionName.ToLowerInvariant().Equals("index"))
                {
                    this.LinkText = this.ControllerName;
                }
                else if(this.ControllerName.ToLowerInvariant().Equals("home"))
                {
                    this.LinkText = this.ActionName;
                }
                else
                {
                    this.LinkText = this.ActionName + " " + this.ControllerName;
                }
            }

            return this;
        }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public string Area { get; set; }

        public string LinkText { get; set; }

        public bool Authorize { get; set; }

        public string[] Roles { get; set; }

        public IEnumerator<MenuItem> GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public MenuItem Add(MenuItem menuItem)
        {
            this.items.Add(menuItem);
            return this;
        }

        public MenuItemForView ForView()
        {
            return new MenuItemForView(this);
        }

        public override string ToString()
        {
            return this.LinkText;
        }
    }
}