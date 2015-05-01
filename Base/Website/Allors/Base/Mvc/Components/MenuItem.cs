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
    using System.Security.Principal;
    using System.Web.Mvc;

    using ExpressionHelper = Microsoft.Web.Mvc.Internal.ExpressionHelper;

    public partial class MenuItem : IEnumerable<MenuItem>
    {
        private readonly List<MenuItem> items = new List<MenuItem>();

        public MenuItem()
        {
            this.AllowAnonymous = true;
        }

        public MenuItem Action<TController>(Expression<Action<TController>> expression) where TController : System.Web.Mvc.Controller
        {
            var valuesFromExpression = ExpressionHelper.GetRouteValuesFromExpression(expression);

            this.ControllerName = (string)valuesFromExpression["Controller"];
            this.ActionName = (string)valuesFromExpression["Action"];
            this.Area = (string)valuesFromExpression["Area"];

            var call = ((MethodCallExpression)expression.Body).Method;

            var allowAnonymousAttribute = (AllowAnonymousAttribute)call.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).FirstOrDefault();
            this.AllowAnonymous = allowAnonymousAttribute != null;

            if (!this.AllowAnonymous)
            {
                var authorizeAttributes = call.GetCustomAttributes(typeof(AuthorizeAttribute), true).Cast<AuthorizeAttribute>().ToArray();
                if (authorizeAttributes.Length == 0)
                {
                    if (call.DeclaringType != null)
                    {
                        authorizeAttributes = call.DeclaringType.GetCustomAttributes(typeof(AuthorizeAttribute), true).Cast<AuthorizeAttribute>().ToArray();
                    }

                    if (authorizeAttributes.Length == 0)
                    {
                        authorizeAttributes = GlobalFilters.Filters.Where(filter => filter.Instance is AuthorizeAttribute).Select(filter => filter.Instance).Cast<AuthorizeAttribute>().ToArray();
                    }
                }

                if (authorizeAttributes.Length == 0)
                {
                    this.AllowAnonymous = true;
                }
                else
                {
                    var roles = new List<string>();
                    foreach (var authorizeAttribute in authorizeAttributes)
                    {
                        if (!string.IsNullOrWhiteSpace(authorizeAttribute.Roles))
                        {
                            var newRoles = authorizeAttribute.Roles.Split(',');
                            roles.AddRange(newRoles);
                        }
                    }

                    this.Roles = roles.Select(role => role.Trim()).ToArray();
                }
            }

            if (!this.IsDivider && !this.IsHeader && string.IsNullOrEmpty(this.Text))
            {
                if (this.ActionName.ToLowerInvariant().Equals("index"))
                {
                    this.Text = this.ControllerName;
                }
                else if(this.ControllerName.ToLowerInvariant().Equals("home"))
                {
                    this.Text = this.ActionName;
                }
                else
                {
                    this.Text = this.ActionName + " " + this.ControllerName;
                }
            }

            return this;
        }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public string Area { get; set; }

        public string Text { get; set; }

        public bool IsDivider { get; set; }

        public bool IsHeader { get; set; }

        public bool AllowAnonymous { get; set; }

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

        public override string ToString()
        {
            return this.Text;
        }

        public bool Allow(IPrincipal user)
        {
            if (this.AllowAnonymous)
            {
                return true;
            }

            if (user != null && user.Identity != null && user.Identity.IsAuthenticated)
            {
                return this.Roles == null || this.Roles.Length == 0 || this.Roles.Any(user.IsInRole);
            }

            return false;
        }
    }
}