namespace Allors.Meta
{
    public partial class GoodClass
    {
        internal override void AppsExtend()
        {
            this.AvailableToPromise.RoleType.IsRequired = true;

            this.ConcreteRoles.UnitOfMeasure.IsRequiredOverride = true;
        }
    }
}