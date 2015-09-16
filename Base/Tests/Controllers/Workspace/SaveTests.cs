using System.Collections.Generic;

namespace Controllers.Workspace
{
    using System.Web.Mvc;

    using Allors;
    using Allors.Domain;
    using Allors.Web.Workspace;

    using System.Linq;
    using Website.Controllers;

    using NUnit.Framework;

    using Should;

    public class SaveTests : ControllersTest
    {
        [Test]
        public void GuestSetUnit()
        {
            // Arrange
            var c1a = new C1Builder(this.Session)
               .WithC1AllorsString("c1")
               .WithI1AllorsString("i1")
               .WithI12AllorsString("i12")
               .Build();

            this.Session.Derive();
            this.Session.Commit();

            var saveRequest = new SaveRequest
            {
                Objects = new[] {
                    new SaveRequestObject
                    {
                        I = c1a.Id.ToString(),
                        V = c1a.Strategy.ObjectVersion.ToString(),
                        Roles = new List<SaveRequestRole>
                        {
                            new SaveRequestRole
                            {
                                T = "C1AllorsString",
                                S = "new c1"
                            }
                        }
                    }
                }
            };

            var controller = new AngularController { AllorsSession = this.Session };

            // Act
            var jsonResult = (JsonResult)controller.Save(saveRequest);
            var saveResponse = (SaveResponse)jsonResult.Data;

            // Assert
            this.Session.Rollback();

            saveResponse.Errors.Count.ShouldEqual(1);

            c1a.C1AllorsString.ShouldEqual("c1");
        }

        [Test]
        public void AdministratorSetUnit()
        {
            // Arrange
            var administrator = new Persons(this.Session).FindBy(Persons.Meta.UserName, Users.AdministratorUserName);

            var c1a = new C1Builder(this.Session)
               .WithC1AllorsString("c1")
               .WithI1AllorsString("i1")
               .WithI12AllorsString("i12")
               .Build();

            this.Session.Derive();
            this.Session.Commit();

            var saveRequest = new SaveRequest
            {
                Objects = new[] {
                    new SaveRequestObject
                    {
                        I = c1a.Id.ToString(),
                        V = c1a.Strategy.ObjectVersion.ToString(),
                        Roles = new List<SaveRequestRole>
                        {
                            new SaveRequestRole
                            {
                                T = "C1AllorsString",
                                S = "new c1"
                            }   
                        }
                    }
                }
            };

            var controller = new AngularController { AllorsSession = this.Session,AuthenticatedUser = administrator};

            // Act
            var jsonResult = (JsonResult)controller.Save(saveRequest);
            var saveResponse = (SaveResponse)jsonResult.Data;

            // Assert
            this.Session.Rollback();

            saveResponse.Errors.Count.ShouldEqual(0);

            c1a.C1AllorsString.ShouldEqual("new c1");
        }

        [Test]
        public void AdministratorSetOne()
        {
            // Arrange
            var administrator = new Persons(this.Session).FindBy(Persons.Meta.UserName, Users.AdministratorUserName);

            var c1a = new C1Builder(this.Session)
               .Build();

            var c1b = new C1Builder(this.Session)
               .Build();

            this.Session.Derive();
            this.Session.Commit();

            var saveRequest = new SaveRequest
            {
                Objects = new[] {
                    new SaveRequestObject
                    {
                        I = c1a.Id.ToString(),
                        V = c1a.Strategy.ObjectVersion.ToString(),
                        Roles = new List<SaveRequestRole>
                        {
                            new SaveRequestRole
                            {
                                T = "C1C1One2One",
                                S = c1b.Id.ToString()
                            }
                        }
                    }
                }
            };

            var controller = new AngularController { AllorsSession = this.Session, AuthenticatedUser = administrator };

            // Act
            var jsonResult = (JsonResult)controller.Save(saveRequest);
            var saveResponse = (SaveResponse)jsonResult.Data;

            // Assert
            this.Session.Rollback();

            saveResponse.Errors.Count.ShouldEqual(0);

            c1a.C1C1One2One.ShouldEqual(c1b);
        }

        [Test]
        public void AdministratorSetMany()
        {
            // Arrange
            var administrator = new Persons(this.Session).FindBy(Persons.Meta.UserName, Users.AdministratorUserName);

            var c1a = new C1Builder(this.Session)
               .Build();

            var c1b = new C1Builder(this.Session)
               .Build();

            var c1c = new C1Builder(this.Session)
               .Build();

            c1a.AddC1C1One2Many(c1b);

            this.Session.Derive();
            this.Session.Commit();

            var saveRequest = new SaveRequest
            {
                Objects = new[] {
                    new SaveRequestObject
                    {
                        I = c1a.Id.ToString(),
                        V = c1a.Strategy.ObjectVersion.ToString(),
                        Roles = new List<SaveRequestRole>
                        {
                            new SaveRequestRole
                            {
                                T = "C1C1One2Many",
                                S = new [] { c1c.Id.ToString() } 
                            }
                        }
                    }
                }
            };

            var controller = new AngularController { AllorsSession = this.Session, AuthenticatedUser = administrator };

            // Act
            var jsonResult = (JsonResult)controller.Save(saveRequest);
            var saveResponse = (SaveResponse)jsonResult.Data;

            // Assert
            this.Session.Rollback();

            saveResponse.Errors.Count.ShouldEqual(0);

            c1a.C1C1One2Manies.ShouldNotBeSameAs(new [] { c1c } );
        }

        [Test]
        public void AdministratorAddMany()
        {
            // Arrange
            var administrator = new Persons(this.Session).FindBy(Persons.Meta.UserName, Users.AdministratorUserName);

            var c1a = new C1Builder(this.Session)
               .Build();

            var c1b = new C1Builder(this.Session)
               .Build();

            var c1c = new C1Builder(this.Session)
               .Build();

            c1a.AddC1C1One2Many(c1b);

            this.Session.Derive();
            this.Session.Commit();

            var saveRequest = new SaveRequest
            {
                Objects = new[] {
                    new SaveRequestObject
                    {
                        I = c1a.Id.ToString(),
                        V = c1a.Strategy.ObjectVersion.ToString(),
                        Roles = new List<SaveRequestRole>
                        {
                            new SaveRequestRole
                            {
                                T = "C1C1One2Many",
                                A = new [] { c1c.Id.ToString() }
                            }
                        }
                    }
                }
            };

            var controller = new AngularController { AllorsSession = this.Session, AuthenticatedUser = administrator };

            // Act
            var jsonResult = (JsonResult)controller.Save(saveRequest);
            var saveResponse = (SaveResponse)jsonResult.Data;

            // Assert
            this.Session.Rollback();

            saveResponse.Errors.Count.ShouldEqual(0);

            c1a.C1C1One2Manies.ShouldNotBeSameAs(new[] { c1b, c1c });
        }

        [Test]
        public void AdministratorRemoveMany()
        {
            // Arrange
            var administrator = new Persons(this.Session).FindBy(Persons.Meta.UserName, Users.AdministratorUserName);

            var c1a = new C1Builder(this.Session)
               .Build();

            var c1b = new C1Builder(this.Session)
               .Build();

            var c1c = new C1Builder(this.Session)
               .Build();

            c1a.AddC1C1One2Many(c1b);
            c1a.AddC1C1One2Many(c1c);

            this.Session.Derive();
            this.Session.Commit();

            var saveRequest = new SaveRequest
            {
                Objects = new[] {
                    new SaveRequestObject
                    {
                        I = c1a.Id.ToString(),
                        V = c1a.Strategy.ObjectVersion.ToString(),
                        Roles = new List<SaveRequestRole>
                        {
                            new SaveRequestRole
                            {
                                T = "C1C1One2Many",
                                R = new [] { c1c.Id.ToString() }
                            }
                        }
                    }
                }
            };

            var controller = new AngularController { AllorsSession = this.Session, AuthenticatedUser = administrator };

            // Act
            var jsonResult = (JsonResult)controller.Save(saveRequest);
            var saveResponse = (SaveResponse)jsonResult.Data;

            // Assert
            this.Session.Rollback();

            saveResponse.Errors.Count.ShouldEqual(0);

            c1a.C1C1One2Manies.ShouldNotBeSameAs(new[] { c1b });
        }
    }
}