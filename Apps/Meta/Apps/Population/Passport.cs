namespace Allors.Meta
{
    public partial class PassportClass
    {
        internal override void AppsExtend()
        {
            this.Roles.Number.IsRequired = true;
            this.Roles.Number.IsUnique = true;
        }
    }
}