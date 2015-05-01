namespace Website.OrganisationMvc
{
    using System.Linq;
    using System.Web.Mvc;
    using Allors;
    using Allors.Domain;
    using Allors.Web.Mvc.Models;

    //[Authorize]
    public class OrganisationController : Allors.Web.Mvc.Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var model = new Index();
            this.MapFilter(model, null);
            var organisations = new Organisations(this.AllorsSession).Extent();
            this.Map(model, organisations);
            return this.View(model);
        }

        [HttpPost]
        public ActionResult Index(Index model, Command? command)
        {
            string filterName;
            Extent<Organisation> filterOrganisations;

            this.GetFilter(model, out filterOrganisations, out filterName);

            if (command == Command.Filter)
            {
                this.ModelState.Clear();
            }

            this.Map(model, filterOrganisations);
            this.MapFilter(model, filterName);
            return this.View(model);
        }

        [HttpGet]
        public ActionResult Add(string id)
        {
            var organisation = (Organisation)this.AllorsSession.Instantiate(id);
            var model = new Add();
            this.Map(model, organisation);
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Add model, Command? command)
        {
            if (command == Command.Cancel)
            {
                return RedirectToRoute("Default", new { controller = "Organisation", action = "Index" });
            }

            var organisation = (Organisation)this.AllorsSession.Instantiate(model.Id);

            if (command == Command.Save)
            {
                if (this.ModelState.IsValid)
                {
                    if (organisation == null)
                    {
                        organisation = new OrganisationBuilder(this.AllorsSession).Build();
                    }

                    organisation.Name = model.Name;

                    var derivationLog = this.AllorsSession.Derive();
                    if (derivationLog.HasErrors)
                    {
                        foreach (var error in derivationLog.Errors)
                        {
                            this.ModelState.AddModelError(string.Empty, error.Message);
                        }
                    }
                    else
                    {
                        this.AllorsSession.Commit();
                        this.ModelState.Clear();
                    }
                }
            }

            this.Map(model, organisation);
            return this.View(model);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            var organisation = (Organisation)this.AllorsSession.Instantiate(id);
            var model = new Edit();
            this.Map(model, organisation);
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Edit model, Command? command)
        {
            if (command == Command.Cancel)
            {
                return RedirectToRoute("Default", new { controller = "Organisation", action = "Index" });
            }
            var organisation = (Organisation)this.AllorsSession.Instantiate(model.Id);

            if (command == Command.Save)
            {
                if (this.ModelState.IsValid)
                {
                    organisation.Name = model.Name;
                    organisation.Owner = (Person)this.AllorsSession.Instantiate(model.Owner.Id);

                    var derivationLog = this.AllorsSession.Derive();
                    if (derivationLog.HasErrors)
                    {
                        foreach (var error in derivationLog.Errors)
                        {
                            this.ModelState.AddModelError(string.Empty, error.Message);
                        }
                    }
                    else
                    {
                        this.AllorsSession.Commit();
                        this.ModelState.Clear();
                    }
                }
            }

            this.Map(model, organisation);
            return this.View(model);
        }

        private void Map(Index model, Extent<Organisation> organisations)
        {
            model.Organisations = organisations.Select(x => new Index_Organisation
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Owner = x.ExistOwner ? x.Owner.UserName : null,
            }).ToArray();
        }

        private void Map(Add add, Organisation organisation)
        {
            if (organisation != null)
            {
                add.Id = organisation.Id.ToString();
                add.Name = organisation.Name;
            }
        }

        private void Map(Edit edit, Organisation organisation)
        {
            if (organisation != null)
            {
                edit.Id = organisation.Id.ToString();
                edit.Name = organisation.Name;
                edit.Owner = new Select 
                { 
                    Id = organisation.ExistOwner ? organisation.Owner.Id.ToString() : "0", 
                    List = new[]
                               {
                                   new SelectListItem { Text = "Select a person", Value = "0" }
                               }
                            .Concat(this.AllorsSession.Extent<Person>().Select(x => new SelectListItem
                                                                                         {
                                                                                             Text = x.UserName, 
                                                                                             Value = x.Id.ToString(),
                                                                                         }))
                            .ToArray() 
                };
            }
        }
       
        private void MapFilter(IFilter model, string filterName)
        {
            model.FilterName = filterName;
        }

        private void GetFilter(IFilter model, out Extent<Organisation> filterOrganisations, out string filterName)
        {
            filterName = model.FilterName;

            filterOrganisations = new Organisations(this.AllorsSession).Extent();
            var filter = filterOrganisations.Filter;
            if (!string.IsNullOrWhiteSpace(filterName))
            {
                filter.AddLike(Organisations.Meta.Name, "%" + filterName + "%");
            }
        }
    }
}