namespace Allors.Meta
{
    public partial class MetaPeriod
    {
        internal override void BaseExtend()
        {
            this.FromDate.IsRequired = true;
        }
    }
}