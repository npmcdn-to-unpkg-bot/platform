namespace Allors.Web.Mvc
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class MenuForView : IEnumerable<MenuItemForView>
    {
        private readonly List<MenuItemForView> items = new List<MenuItemForView>();

        private readonly string controllerName;
        private readonly string actionName;

        public MenuForView(Menu menu, ViewContext context)
        {
            var controller = context.Controller;
            while (controller.ControllerContext.IsChildAction)
            {
                controller = controller.ControllerContext.ParentActionViewContext.Controller;
            }

            var typeName = controller.GetType().Name;
            this.controllerName = typeName.ToLowerInvariant().EndsWith("controller")
                                      ? typeName.Substring(0, typeName.ToLowerInvariant().LastIndexOf("controller", StringComparison.Ordinal))
                                      : null;
            this.actionName = (string)controller.ControllerContext.RouteData.Values["action"];

            foreach (var menuItem in menu)
            {
                this.items.Add(menuItem.ForView());
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

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}