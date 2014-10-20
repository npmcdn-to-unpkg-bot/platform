// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	public partial class From : Allors.ObjectBase , UserInterfaceable
	{
		public static readonly FromMeta Meta = FromMeta.Instance;

		public From(Allors.IStrategy allors) : base(allors) {}

		public static From Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (From) allorsSession.Instantiate(allorsObjectId);		
		}



		virtual public global::Allors.Extent<To> Tos
		{ 
			get
			{
				return Strategy.GetCompositeRoles(Meta.To);
			}
			set
			{
				Strategy.SetCompositeRoles(Meta.To, value);
			}
		}

		virtual public void AddTo (To value)
		{
			Strategy.AddCompositeRole(Meta.To, value);
		}

		virtual public void RemoveTo (To value)
		{
			Strategy.RemoveCompositeRole(Meta.To,value);
		}

		virtual public bool ExistTos
		{
			get
			{
				return Strategy.ExistCompositeRoles(Meta.To);
			}
		}

		virtual public void RemoveTos()
		{
			Strategy.RemoveCompositeRoles(Meta.To);
		}



		virtual public global::System.String DisplayName 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(Meta.DisplayName);
			}
			set
			{
				Strategy.SetUnitRole(Meta.DisplayName, value);
			}
		}

		virtual public bool ExistDisplayName{
			get
			{
				return Strategy.ExistUnitRole(Meta.DisplayName);
			}
		}

		virtual public void RemoveDisplayName()
		{
			Strategy.RemoveUnitRole(Meta.DisplayName);
		}


		virtual public global::Allors.Extent<Permission> DeniedPermissions
		{ 
			get
			{
				return Strategy.GetCompositeRoles(Meta.DeniedPermission);
			}
			set
			{
				Strategy.SetCompositeRoles(Meta.DeniedPermission, value);
			}
		}

		virtual public void AddDeniedPermission (Permission value)
		{
			Strategy.AddCompositeRole(Meta.DeniedPermission, value);
		}

		virtual public void RemoveDeniedPermission (Permission value)
		{
			Strategy.RemoveCompositeRole(Meta.DeniedPermission,value);
		}

		virtual public bool ExistDeniedPermissions
		{
			get
			{
				return Strategy.ExistCompositeRoles(Meta.DeniedPermission);
			}
		}

		virtual public void RemoveDeniedPermissions()
		{
			Strategy.RemoveCompositeRoles(Meta.DeniedPermission);
		}


		virtual public global::Allors.Extent<SecurityToken> SecurityTokens
		{ 
			get
			{
				return Strategy.GetCompositeRoles(Meta.SecurityToken);
			}
			set
			{
				Strategy.SetCompositeRoles(Meta.SecurityToken, value);
			}
		}

		virtual public void AddSecurityToken (SecurityToken value)
		{
			Strategy.AddCompositeRole(Meta.SecurityToken, value);
		}

		virtual public void RemoveSecurityToken (SecurityToken value)
		{
			Strategy.RemoveCompositeRole(Meta.SecurityToken,value);
		}

		virtual public bool ExistSecurityTokens
		{
			get
			{
				return Strategy.ExistCompositeRoles(Meta.SecurityToken);
			}
		}

		virtual public void RemoveSecurityTokens()
		{
			Strategy.RemoveCompositeRoles(Meta.SecurityToken);
		}

	}

	public class FromMeta
	{
		public static readonly FromMeta Instance = new FromMeta();

		public global::Allors.Meta.Class ObjectType = global::Allors.Meta.Classes.From;

		public global::Allors.Meta.RoleType To 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.FromTo;
			}
		} 
		public global::Allors.Meta.RoleType DisplayName 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.UserInterfaceableDisplayName;
			}
		} 
		public global::Allors.Meta.RoleType DeniedPermission 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.AccessControlledObjectDeniedPermission;
			}
		} 
		public global::Allors.Meta.RoleType SecurityToken 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.AccessControlledObjectSecurityToken;
			}
		} 

	}
}