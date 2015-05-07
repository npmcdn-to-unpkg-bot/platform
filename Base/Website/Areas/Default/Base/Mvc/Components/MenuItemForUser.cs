namespace Allors.Web.Mvc
{
    using System.Collections;
    using System.Collections.Generic;

    public class MenuItemForUser : IEnumerable<MenuItemForUser>
    {
        private readonly List<MenuItemForUser> items = new List<MenuItemForUser>();

        private readonly MenuItem menuItem;

        private bool isActive;

        private bool isHeader;

        public MenuItemForUser(MenuItem menuItem, MenuForUser menuForUser)
        {
            this.menuItem = menuItem;

            foreach (var childMenuItem in menuItem)
            {
                if (menuForUser.Show(menuItem))
                {
                    this.items.Add(new MenuItemForUser(childMenuItem, menuForUser));
                }
            }
        }

        public string Text
        {
            get
            {
                return this.menuItem.Text;
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

        public bool IsHeader
        {
            get
            {
                return this.menuItem.IsHeader;
            }
        }

        public bool IsDivider
        {
            get
            {
                return this.menuItem.IsDivider;
            }
        }

        public bool HasChildren
        {
            get
            {
                return this.items.Count > 0;
            }
        }

        public bool HasLink
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.ControllerName) && !string.IsNullOrWhiteSpace(this.ActionName);
            }
        }

        public override string ToString()
        {
            return this.Text;
        }

        public IEnumerator<MenuItemForUser> GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}