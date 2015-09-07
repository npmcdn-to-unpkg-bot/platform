namespace Allors.Meta
{
	using System;

	public partial class BudgetInterface
	{
	    internal override void AppsExtend()
        {
            new MethodType(AppsDomain.Instance, new Guid("3E913270-98BC-4A29-8C54-AD94B78D62A3")){ObjectType=this, Name="Close"};
            new MethodType(AppsDomain.Instance, new Guid("4D8FD306-049E-4909-AFA8-91A615B76314")){ObjectType=this, Name="Reopen"};

            this.Roles.Description.IsRequired = true;
        }
	}
}