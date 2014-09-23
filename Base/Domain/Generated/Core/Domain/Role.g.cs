// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	[System.Diagnostics.DebuggerNonUserCode]
	public partial class Role : Allors.ObjectBase , UserInterfaceable, UniquelyIdentifiable
	{
		public static readonly RoleMeta Meta = RoleMeta.Instance;

		public Role(Allors.IStrategy allors) : base(allors) {}

		public static Role Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (Role) allorsSession.Instantiate(allorsObjectId);		
		}



		virtual public global::Allors.Extent<Permission> Permissions
		{ 
			get
			{
				return Strategy.GetCompositeRoles(Meta.Permission);
			}
			set
			{
				Strategy.SetCompositeRoles(Meta.Permission, value);
			}
		}

		virtual public void AddPermission (Permission value)
		{
			Strategy.AddCompositeRole(Meta.Permission, value);
		}

		virtual public void RemovePermission (Permission value)
		{
			Strategy.RemoveCompositeRole(Meta.Permission,value);
		}

		virtual public bool ExistPermissions
		{
			get
			{
				return Strategy.ExistCompositeRoles(Meta.Permission);
			}
		}

		virtual public void RemovePermissions()
		{
			Strategy.RemoveCompositeRoles(Meta.Permission);
		}



		virtual public global::System.String Name 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(Meta.Name);
			}
			set
			{
				Strategy.SetUnitRole(Meta.Name, value);
			}
		}

		virtual public bool ExistName{
			get
			{
				return Strategy.ExistUnitRole(Meta.Name);
			}
		}

		virtual public void RemoveName()
		{
			Strategy.RemoveUnitRole(Meta.Name);
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



		virtual public global::System.Guid? UniqueId 
		{
			get
			{
				return (global::System.Guid?) Strategy.GetUnitRole(Meta.UniqueId);
			}
			set
			{
				Strategy.SetUnitRole(Meta.UniqueId, value);
			}
		}

		virtual public bool ExistUniqueId{
			get
			{
				return Strategy.ExistUnitRole(Meta.UniqueId);
			}
		}

		virtual public void RemoveUniqueId()
		{
			Strategy.RemoveUnitRole(Meta.UniqueId);
		}



		virtual public UserGroup UserGroupWhereRole
		{ 
			get
			{
				return (UserGroup) Strategy.GetCompositeAssociation(Meta.UserGroupWhereRole);
			}
		} 

		virtual public bool ExistUserGroupWhereRole
		{
			get
			{
				return Strategy.ExistCompositeAssociation(Meta.UserGroupWhereRole);
			}
		}


		virtual public global::Allors.Extent<AccessControl> AccessControlsWhereRole
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.AccessControlsWhereRole);
			}
		}

		virtual public bool ExistAccessControlsWhereRole
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.AccessControlsWhereRole);
			}
		}

	}

	public class RoleMeta
	{
		public static readonly RoleMeta Instance = new RoleMeta();

		public global::Allors.Meta.Class ObjectType = global::Allors.Meta.Classes.Role;

		public global::Allors.Meta.RoleType Permission 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.RolePermission;
			}
		} 
		public global::Allors.Meta.RoleType Name 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.RoleName;
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
		public global::Allors.Meta.RoleType UniqueId 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.UniquelyIdentifiableUniqueId;
			}
		} 

		public global::Allors.Meta.AssociationType UserGroupWhereRole 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.UserGroupRole;
			}
		} 
		public global::Allors.Meta.AssociationType AccessControlsWhereRole 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.AccessControlRole;
			}
		} 

	}
}