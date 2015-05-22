namespace Areas.Default.Tests.OrganisationMvc
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Allors;
    using Allors.Domain;
    using Allors.Web.Content;
    using Allors.Web.Mvc.Models;
    using Allors.Web.Mvc.Views;

    //[Authorize]
    [CssFramework("Bootstrap")]
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
                return this.RedirectToRoute("Default", new { controller = "Organisation", action = "Index" });
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
                        return this.RedirectToAction("Index");
                    }
                }
            }

            this.Map(model, organisation);
            return this.View(model);
        }

        [HttpGet]
        public ActionResult View(string id)
        {
            var organisation = (Organisation)this.AllorsSession.Instantiate(id);
            var model = new View();
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
                return this.RedirectToRoute("Default", new { controller = "Organisation", action = "Index" });
            }
            var organisation = (Organisation)this.AllorsSession.Instantiate(model.Id);
            
            var currentCultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            var currentUICultureInfo = System.Threading.Thread.CurrentThread.CurrentUICulture;

            if (command == Command.Save)
            {
                if (this.ModelState.IsValid)
                {
                    organisation.Name = model.Name;
                    organisation.Description = model.Description;
                    organisation.Information = model.Information;
                    organisation.Incorporated = model.Incorporated;
                    organisation.IncorporationDate = model.IncorporationDate.HasValue ? model.IncorporationDate.Value.ToUniversalTime() : (DateTime?)null;
                    organisation.Owner = (Person)this.AllorsSession.Instantiate(model.Owner.Id);
                    organisation.Employees = model.Werknemers != null ? this.AllorsSession.Instantiate(model.Werknemers.Ids) : null;

                    if (model.Logo.PostedFile != null)
                    {
                        if (organisation.ExistLogo)
                        {
                            organisation.Logo.Delete();
                        }

                        var media = new MediaBuilder(this.AllorsSession).WithContent(model.Logo.PostedFile.GetContent()).Build();
                        var image = new ImageBuilder(this.AllorsSession).WithOriginalFilename(model.Logo.PostedFile.FileName).WithOriginal(media).Build();

                        organisation.Logo = image;
                    }

                    if (model.Images.PostedFile != null)
                    {
                        var media = new MediaBuilder(this.AllorsSession).WithContent(model.Images.PostedFile.GetContent()).Build();
                        var image = new ImageBuilder(this.AllorsSession).WithOriginalFilename(model.Images.PostedFile.FileName).WithOriginal(media).Build();

                        organisation.AddImage(image);
                    }

                    foreach (var item in model.Images.Items)
                    {
                        if (item.Delete)
                        {
                            var image = (Image)this.AllorsSession.Instantiate(item.Id);
                            image.Delete();
                        }
                    }


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

        private void Map(View view, Organisation organisation)
        {
            if (organisation != null)
            {
                view.Id = organisation.Id.ToString();
                view.Name = organisation.Name;
                view.Description = organisation.Description;
                view.Information = organisation.Information;
                view.Incorporated = organisation.Incorporated;
                view.IncorporationDate = organisation.IncorporationDate;
                view.EmployeeCount = organisation.Employees.Count;
                view.Owner = new Select
                {
                    Id = organisation.ExistOwner ? organisation.Owner.Id.ToString() : "0",
                    List = new[]
                               {
                                   new SelectListItem { Value = "0" }
                               }
                            .Concat(this.AllorsSession.Extent<Person>().Select(x => new SelectListItem
                            {
                                Text = x.UserName,
                                Value = x.Id.ToString(),
                            }))
                            .ToArray()
                };
                view.Werknemers = new MultipleSelect
                {
                    Ids = organisation.Employees.Select(x => x.Id.ToString()).ToArray(),
                    List = this.AllorsSession.Extent<Person>().Select(x => new SelectListItem
                    {
                        Text = x.UserName,
                        Value = x.Id.ToString(),
                    }).ToArray()
                };
            }
        }

        private void Map(Edit edit, Organisation organisation)
        {
            if (organisation != null)
            {
                edit.Id = organisation.Id.ToString();
                edit.Name = organisation.Name;
                edit.Description = organisation.Description;
                edit.Information = organisation.Information;
                edit.Incorporated = organisation.Incorporated;
                edit.IncorporationDate = organisation.IncorporationDate;
                edit.EmployeeCount = organisation.Employees.Count;
                edit.Owner = new Select 
                { 
                    Id = organisation.ExistOwner ? organisation.Owner.Id.ToString() : "0", 
                    List = new[]
                               {
                                   new SelectListItem { Value = "0" }
                               }
                            .Concat(this.AllorsSession.Extent<Person>().Select(x => new SelectListItem
                                                                                         {
                                                                                             Text = x.UserName, 
                                                                                             Value = x.Id.ToString(),
                                                                                         }))
                            .ToArray() 
                };
                edit.Werknemers = new MultipleSelect
                {
                    Ids = organisation.Employees.Select(x => x.Id.ToString()).ToArray(),
                    List = this.AllorsSession.Extent<Person>().Select(x => new SelectListItem
                            {
                                Text = x.UserName,
                                Value = x.Id.ToString(),
                            }).ToArray()
                };
                edit.Logo = new ImageModel
                                {
                                    UniqueId = organisation.ExistLogo ? organisation.Logo.Thumbnail.UniqueId.ToString("N") : null, 
                                    FileName = organisation.ExistLogo ? organisation.Logo.OriginalFilename : null
                                };
                edit.Images = new ImagesModel
                {
                    Items = organisation.Images.Select(x => new ImagesItemModel
                                                                {
                                                                    Id = x.Id.ToString(),
                                                                    UniqueId = x.Thumbnail.UniqueId.ToString("N"),
                                                                    FileName = x.OriginalFilename
                                                                }).ToArray()
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