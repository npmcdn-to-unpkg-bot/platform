namespace Allors.Meta
{
    public partial class GoodClass
    {
        internal override void AppsExtend()
        {
            this.Roles.AvailableToPromise.IsRequired = true;

            this.ConcreteRoles.UnitOfMeasure.IsRequiredOverride = true;
        }
    }
}