// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Domain
{
	public partial class ClassWithoutRoles : ClassWithoutRolesAllors , global::Domain.UserInterfaceable
	{
		public ClassWithoutRoles(Allors.IStrategy allors) : base(allors) {}

		public static ClassWithoutRoles Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (ClassWithoutRoles) allorsSession.Instantiate(allorsObjectId);		
		}
	}

	[System.Diagnostics.DebuggerNonUserCode]
	public abstract class ClassWithoutRolesAllors : Allors.ObjectBase
	{
		protected ClassWithoutRolesAllors(Allors.IStrategy allors) : base(allors){}



		virtual public global::System.String DisplayName 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(ClassWithoutRolesMeta.DisplayName);
			}
			set
			{
				Strategy.SetUnitRole(ClassWithoutRolesMeta.DisplayName, value);
			}
		}

		virtual public bool ExistDisplayName{
			get
			{
				return Strategy.ExistUnitRole(ClassWithoutRolesMeta.DisplayName);
			}
		}

		virtual public void RemoveDisplayName()
		{
			Strategy.RemoveUnitRole(ClassWithoutRolesMeta.DisplayName);
		}


		virtual public Allors.Extent<global::Domain.Permission> DeniedPermissions
		{ 
			get
			{
				return Strategy.GetCompositeRoles(ClassWithoutRolesMeta.DeniedPermission);
			}
			set
			{
				Strategy.SetCompositeRoles(ClassWithoutRolesMeta.DeniedPermission, value);
			}
		}

		virtual public void AddDeniedPermission (global::Domain.Permission value)
		{
			Strategy.AddCompositeRole(ClassWithoutRolesMeta.DeniedPermission, value);
		}

		virtual public void RemoveDeniedPermission (global::Domain.Permission value)
		{
			Strategy.RemoveCompositeRole(ClassWithoutRolesMeta.DeniedPermission,value);
		}

		virtual public bool ExistDeniedPermissions
		{
			get
			{
				return Strategy.ExistCompositeRoles(ClassWithoutRolesMeta.DeniedPermission);
			}
		}

		virtual public void RemoveDeniedPermissions()
		{
			Strategy.RemoveCompositeRoles(ClassWithoutRolesMeta.DeniedPermission);
		}


		virtual public Allors.Extent<global::Domain.SecurityToken> SecurityTokens
		{ 
			get
			{
				return Strategy.GetCompositeRoles(ClassWithoutRolesMeta.SecurityToken);
			}
			set
			{
				Strategy.SetCompositeRoles(ClassWithoutRolesMeta.SecurityToken, value);
			}
		}

		virtual public void AddSecurityToken (global::Domain.SecurityToken value)
		{
			Strategy.AddCompositeRole(ClassWithoutRolesMeta.SecurityToken, value);
		}

		virtual public void RemoveSecurityToken (global::Domain.SecurityToken value)
		{
			Strategy.RemoveCompositeRole(ClassWithoutRolesMeta.SecurityToken,value);
		}

		virtual public bool ExistSecurityTokens
		{
			get
			{
				return Strategy.ExistCompositeRoles(ClassWithoutRolesMeta.SecurityToken);
			}
		}

		virtual public void RemoveSecurityTokens()
		{
			Strategy.RemoveCompositeRoles(ClassWithoutRolesMeta.SecurityToken);
		}

	}

	public static class ClassWithoutRolesMeta
	{
		public static readonly global::Allors.Meta.Class ObjectType = (Allors.Meta.Class)global::Allors.Meta.Repository.MetaPopulation.Find( new System.Guid("e1008840-6d7c-4d44-b2ad-1545d23f90d8") );

		public static readonly global::Allors.Meta.RoleType DisplayName = ((Allors.Meta.RelationType)global::Allors.Meta.Repository.MetaPopulation.Find( new System.Guid("6412301d-95ec-44c2-8c71-cc03de5327b9"))).RoleType;
		public static readonly global::Allors.Meta.RoleType DeniedPermission = ((Allors.Meta.RelationType)global::Allors.Meta.Repository.MetaPopulation.Find( new System.Guid("5c70ca14-4601-4c65-9b0d-cb189f90be27"))).RoleType;
		public static readonly global::Allors.Meta.RoleType SecurityToken = ((Allors.Meta.RelationType)global::Allors.Meta.Repository.MetaPopulation.Find( new System.Guid("b816fccd-08e0-46e0-a49c-7213c3604416"))).RoleType;

	}
}