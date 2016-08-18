namespace Web.Controllers
{
    using System;
    using System.Web.Mvc;

    using Allors.Domain;
    using Allors.Web.Database;

    public class TestUnitSamplesController : PullController
    {
        [Authorize]
        [HttpPost]
        public ActionResult Pull(int step)
        {
            try
            {
                var unitSample = new UnitSamples(this.AllorsSession).Extent().First;
                if (unitSample == null)
                {
                    unitSample = new UnitSampleBuilder(this.AllorsSession).Build();
                    this.AllorsSession.Commit();
                }

                var responseBuilder = new PullResponseBuilder(this.AllorsUser);

                switch (step)
                {
                    case 0:
                        unitSample.RemoveAllorsBinary();
                        unitSample.RemoveAllorsBoolean();
                        unitSample.RemoveAllorsDateTime();
                        unitSample.RemoveAllorsDecimal();
                        unitSample.RemoveAllorsDouble();
                        unitSample.RemoveAllorsInteger();
                        unitSample.RemoveAllorsString();
                        unitSample.RemoveAllorsUnique();

                        break;
                }

                responseBuilder.AddObject("unitSample", unitSample);

                return this.JsonSuccess(responseBuilder.Build());
            }
            catch (Exception e)
            {
                return this.JsonError(e.Message);
            }
        }
    }
}