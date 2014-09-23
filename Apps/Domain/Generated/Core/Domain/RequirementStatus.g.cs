// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	[System.Diagnostics.DebuggerNonUserCode]
	public partial class RequirementStatus : Allors.ObjectBase , UserInterfaceable
	{
		public static readonly RequirementStatusMeta Meta = RequirementStatusMeta.Instance;

		public RequirementStatus(Allors.IStrategy allors) : base(allors) {}

		public static RequirementStatus Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (RequirementStatus) allorsSession.Instantiate(allorsObjectId);		
		}



		virtual public RequirementObjectState RequirementObjectState
		{ 
			get
			{
				return (RequirementObjectState) Strategy.GetCompositeRole(Meta.RequirementObjectState);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.RequirementObjectState ,value);
			}
		}

		virtual public bool ExistRequirementObjectState
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.RequirementObjectState);
			}
		}

		virtual public void RemoveRequirementObjectState()
		{
			Strategy.RemoveCompositeRole(Meta.RequirementObjectState);
		}



		virtual public global::System.DateTime? StartDateTime 
		{
			get
			{
				return (global::System.DateTime?) Strategy.GetUnitRole(Meta.StartDateTime);
			}
			set
			{
				Strategy.SetUnitRole(Meta.StartDateTime, value);
			}
		}

		virtual public bool ExistStartDateTime{
			get
			{
				return Strategy.ExistUnitRole(Meta.StartDateTime);
			}
		}

		virtual public void RemoveStartDateTime()
		{
			Strategy.RemoveUnitRole(Meta.StartDateTime);
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



		virtual public Requirement RequirementWhereRequirementStatus
		{ 
			get
			{
				return (Requirement) Strategy.GetCompositeAssociation(Meta.RequirementWhereRequirementStatus);
			}
		} 

		virtual public bool ExistRequirementWhereRequirementStatus
		{
			get
			{
				return Strategy.ExistCompositeAssociation(Meta.RequirementWhereRequirementStatus);
			}
		}


		virtual public Requirement RequirementWhereCurrentRequirementStatus
		{ 
			get
			{
				return (Requirement) Strategy.GetCompositeAssociation(Meta.RequirementWhereCurrentRequirementStatus);
			}
		} 

		virtual public bool ExistRequirementWhereCurrentRequirementStatus
		{
			get
			{
				return Strategy.ExistCompositeAssociation(Meta.RequirementWhereCurrentRequirementStatus);
			}
		}

	}

	public class RequirementStatusMeta
	{
		public static readonly RequirementStatusMeta Instance = new RequirementStatusMeta();

		public global::Allors.Meta.Class ObjectType = global::Allors.Meta.Classes.RequirementStatus;

		public global::Allors.Meta.RoleType RequirementObjectState 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.RequirementStatusRequirementObjectState;
			}
		} 
		public global::Allors.Meta.RoleType StartDateTime 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.RequirementStatusStartDateTime;
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

		public global::Allors.Meta.AssociationType RequirementWhereRequirementStatus 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.RequirementRequirementStatus;
			}
		} 
		public global::Allors.Meta.AssociationType RequirementWhereCurrentRequirementStatus 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.RequirementCurrentRequirementStatus;
			}
		} 

	}
}