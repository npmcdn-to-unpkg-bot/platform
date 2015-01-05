namespace Allors.Web.Mvc
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Security.Principal;

    public class MenuForUser : IEnumerable<MenuItemForUser>
    {
        private readonly List<MenuItemForUser> items = new List<MenuItemForUser>();

        private readonly string controllerName;
        private readonly string actionName;

        private readonly IPrincipal user;

        public MenuForUser(Menu menu, string controllerName, string actionName, IPrincipal user)
        {
            this.controllerName = controllerName;
            this.actionName = actionName;
            this.user = user;

            foreach (var menuItem in menu)
            {
                if (this.Show(menuItem))
                {
                    var menuItemForView = new MenuItemForUser(menuItem, this);
                    this.items.Add(menuItemForView);
                }
            }

            foreach (var menuItemForView in this)
            {
                menuItemForView.IsActive = this.SetActive(menuItemForView);
            }
        }

        public IPrincipal User
        {
            get
            {
                return this.user;
            }
        }

        private bool SetActive(MenuItemForUser menuItemForUser)
        {
            if (this.controllerName.Equals(menuItemForUser.ControllerName) && this.actionName.Equals(menuItemForUser.ActionName))
            {
                menuItemForUser.IsActive = true;
                return true;
            }

            foreach (var childMenuItemForView in menuItemForUser)
            {
                if (this.SetActive(childMenuItemForView))
                {
                    menuItemForUser.IsActive = true;
                    return true;
                }
            }

            return false;
        }

        public IEnumerator<MenuItemForUser> GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        public bool Show(MenuItem menuItem)
        {
            return menuItem.Allow(this.User);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}