namespace Tests.Remote
{
    using Allors.Workspace.Client;
    using Allors.Workspace.Domain;

    using Nito.AsyncEx;

    using NUnit.Framework;

    using Should;

    public class UnitSamplesTests : Test
    {
        [Test]
        public void Null()
        {
            AsyncContext.Run(
                   async () =>
                   {
                       var context = new Context("TestUnitSamples", this.Database, this.Workspace);

                       await context.Load(new { step = 0 });

                       var unitSample = (UnitSample)context.Objects["unitSample"];

                       unitSample.ExistAllorsBinary.ShouldBeFalse();
                       unitSample.ExistAllorsBoolean.ShouldBeFalse();
                       unitSample.ExistAllorsDateTime.ShouldBeFalse();
                       unitSample.ExistAllorsDecimal.ShouldBeFalse();
                       unitSample.ExistAllorsDouble.ShouldBeFalse();
                       unitSample.ExistAllorsInteger.ShouldBeFalse();
                       unitSample.ExistAllorsString.ShouldBeFalse();
                       unitSample.ExistAllorsUnique.ShouldBeFalse();
                   });
        }
    }
}