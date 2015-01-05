namespace Allors.Web.Mvc
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Allors.Domain;

    public class MenuForView : IEnumerable<MenuItemForView>
    {
        private readonly List<MenuItemForView> items = new List<MenuItemForView>();

        private readonly string controllerName;
        private readonly string actionName;

        private readonly HashSet<string> userRoles; 

        public MenuForView(Menu menu, string controllerName, string actionName, User user)
        {
            this.controllerName = controllerName;
            this.actionName = actionName;

            if (user != null)
            {
                this.userRoles = new HashSet<string>(user.UserGroupsWhereMember.Select(group => group.Name));
            }

            foreach (var menuItem in menu)
            {
                if (this.Show(menuItem))
                {
                    var menuItemForView = new MenuItemForView(menuItem, this);
                    this.items.Add(menuItemForView);
                }
            }

            foreach (var menuItemForView in this)
            {
                menuItemForView.IsActive = this.SetActive(menuItemForView);
            }
        }

        private bool SetActive(MenuItemForView menuItemForView)
        {
            if (menuItemForView.ControllerName.Equals(this.controllerName) && menuItemForView.ActionName.Equals(this.actionName))
            {
                menuItemForView.IsActive = true;
                return true;
            }

            foreach (var childMenuItemForView in menuItemForView)
            {
                if (this.SetActive(childMenuItemForView))
                {
                    menuItemForView.IsActive = true;
                    return true;
                }
            }

            return false;
        }

        public IEnumerator<MenuItemForView> GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        public bool Show(MenuItem menuItem)
        {
            // Anonymous user
            if (this.userRoles == null)
            {
                return menuItem.AllowAnonymous;
            }

            // Authenticated User
            return menuItem.AllowAnonymous || menuItem.Roles.Length == 0 || menuItem.Roles.Any(role => this.userRoles.Contains(role));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}