// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	[System.Diagnostics.DebuggerNonUserCode]
	public partial class PartRevision : Allors.ObjectBase , Period, UserInterfaceable
	{
		public static readonly PartRevisionMeta Meta = PartRevisionMeta.Instance;

		public PartRevision(Allors.IStrategy allors) : base(allors) {}

		public static PartRevision Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (PartRevision) allorsSession.Instantiate(allorsObjectId);		
		}




		virtual public global::System.String Reason 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(Meta.Reason);
			}
			set
			{
				Strategy.SetUnitRole(Meta.Reason, value);
			}
		}

		virtual public bool ExistReason{
			get
			{
				return Strategy.ExistUnitRole(Meta.Reason);
			}
		}

		virtual public void RemoveReason()
		{
			Strategy.RemoveUnitRole(Meta.Reason);
		}


		virtual public Part SupersededByPart
		{ 
			get
			{
				return (Part) Strategy.GetCompositeRole(Meta.SupersededByPart);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.SupersededByPart ,value);
			}
		}

		virtual public bool ExistSupersededByPart
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.SupersededByPart);
			}
		}

		virtual public void RemoveSupersededByPart()
		{
			Strategy.RemoveCompositeRole(Meta.SupersededByPart);
		}


		virtual public Part Part
		{ 
			get
			{
				return (Part) Strategy.GetCompositeRole(Meta.Part);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.Part ,value);
			}
		}

		virtual public bool ExistPart
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.Part);
			}
		}

		virtual public void RemovePart()
		{
			Strategy.RemoveCompositeRole(Meta.Part);
		}



		virtual public global::System.DateTime? FromDate 
		{
			get
			{
				return (global::System.DateTime?) Strategy.GetUnitRole(Meta.FromDate);
			}
			set
			{
				Strategy.SetUnitRole(Meta.FromDate, value);
			}
		}

		virtual public bool ExistFromDate{
			get
			{
				return Strategy.ExistUnitRole(Meta.FromDate);
			}
		}

		virtual public void RemoveFromDate()
		{
			Strategy.RemoveUnitRole(Meta.FromDate);
		}



		virtual public global::System.DateTime? ThroughDate 
		{
			get
			{
				return (global::System.DateTime?) Strategy.GetUnitRole(Meta.ThroughDate);
			}
			set
			{
				Strategy.SetUnitRole(Meta.ThroughDate, value);
			}
		}

		virtual public bool ExistThroughDate{
			get
			{
				return Strategy.ExistUnitRole(Meta.ThroughDate);
			}
		}

		virtual public void RemoveThroughDate()
		{
			Strategy.RemoveUnitRole(Meta.ThroughDate);
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

	public class PartRevisionMeta
	{
		public static readonly PartRevisionMeta Instance = new PartRevisionMeta();

		public global::Allors.Meta.Class ObjectType = global::Allors.Meta.Classes.PartRevision;

		public global::Allors.Meta.RoleType Reason 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.PartRevisionReason;
			}
		} 
		public global::Allors.Meta.RoleType SupersededByPart 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.PartRevisionSupersededByPart;
			}
		} 
		public global::Allors.Meta.RoleType Part 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.PartRevisionPart;
			}
		} 
		public global::Allors.Meta.RoleType FromDate 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.PeriodFromDate;
			}
		} 
		public global::Allors.Meta.RoleType ThroughDate 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.PeriodThroughDate;
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