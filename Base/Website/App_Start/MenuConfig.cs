namespace Website
{
    using Allors.Web.Mvc;

    using Website.Controllers;

    public class MenuConfig
    {
        public static void Register()
        {
            Menus.Set("main", new Menu()
               .Add(new MenuItem().Action<HomeController>(c => c.Index()))
               .Add(new MenuItem { Text = "Relations" }
                    .Add(new MenuItem { Text = "Organisations", IsHeader = true })
                    )
               );
        }
    }
}
