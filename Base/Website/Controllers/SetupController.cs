﻿namespace Website.Areas.Admin.Controllers
{
    using Allors;

    using Allors.Domain;

    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    public class SetupController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Populate()
        {
            var database = Config.Default;
            database.Init();

            using (var session = database.CreateSession())
            {
                new Setup(session).Apply();

                var passwordHasher = new PasswordHasher();

                var koen = new PersonBuilder(session).WithFirstName("Koen").WithLastName("Van Exem").WithUserName("koen@allors.com").WithUserEmail("koen@allors.com").WithUserEmailConfirmed(true).WithUserPasswordHash(passwordHasher.HashPassword("a")).Build();
                new UserGroups(session).Administrators.AddMember(koen);

                session.Derive();
                session.Commit();
            }

            return this.View("Index");
        }
    }
}