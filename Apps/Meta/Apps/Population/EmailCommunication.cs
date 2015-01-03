namespace Allors.Meta
{
	using System;

	public partial class EmailCommunicationClass
	{
	    internal override void AppsExtend()
        {
            this.ConcreteRoles.Subject.IsRequiredOverride = true;
        }
	}
}