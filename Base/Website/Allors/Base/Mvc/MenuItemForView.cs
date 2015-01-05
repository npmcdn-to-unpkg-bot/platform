namespace Allors.Web.Mvc
{
    using System.Collections;
    using System.Collections.Generic;

    public class MenuItemForView : IEnumerable<MenuItemForView>
    {
        private readonly List<MenuItemForView> items = new List<MenuItemForView>();

        private readonly MenuItem menuItem;

        private bool isActive;

        public MenuItemForView(MenuItem menuItem, MenuForView menuForView)
        {
            this.menuItem = menuItem;

            foreach (var childMenuItem in menuItem)
            {
                if (menuForView.Show(menuItem))
                {
                    this.items.Add(new MenuItemForView(childMenuItem, menuForView));
                }
            }
        }

        public string LinkText
        {
            get
            {
                return this.menuItem.LinkText;
            }
        }

        public string ActionName
        {
            get
            {
                return this.menuItem.ActionName;
            }
        }

        public string ControllerName
        {
            get
            {
                return this.menuItem.ControllerName;
            }
        }

        public bool IsActive
        {
            get
            {
                return this.isActive;
            }

            set
            {
                this.isActive = value;
            }
        }

        public override string ToString()
        {
            return this.LinkText;
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