// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	[System.Diagnostics.DebuggerNonUserCode]
	public partial class ServiceEntryHeader : Allors.ObjectBase , Period, UserInterfaceable
	{
		public static readonly ServiceEntryHeaderMeta Meta = ServiceEntryHeaderMeta.Instance;

		public ServiceEntryHeader(Allors.IStrategy allors) : base(allors) {}

		public static ServiceEntryHeader Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (ServiceEntryHeader) allorsSession.Instantiate(allorsObjectId);		
		}



		virtual public global::Allors.Extent<ServiceEntry> ServiceEntries
		{ 
			get
			{
				return Strategy.GetCompositeRoles(Meta.ServiceEntry);
			}
			set
			{
				Strategy.SetCompositeRoles(Meta.ServiceEntry, value);
			}
		}

		virtual public void AddServiceEntry (ServiceEntry value)
		{
			Strategy.AddCompositeRole(Meta.ServiceEntry, value);
		}

		virtual public void RemoveServiceEntry (ServiceEntry value)
		{
			Strategy.RemoveCompositeRole(Meta.ServiceEntry,value);
		}

		virtual public bool ExistServiceEntries
		{
			get
			{
				return Strategy.ExistCompositeRoles(Meta.ServiceEntry);
			}
		}

		virtual public void RemoveServiceEntries()
		{
			Strategy.RemoveCompositeRoles(Meta.ServiceEntry);
		}



		virtual public global::System.DateTime? SubmittedDate 
		{
			get
			{
				return (global::System.DateTime?) Strategy.GetUnitRole(Meta.SubmittedDate);
			}
			set
			{
				Strategy.SetUnitRole(Meta.SubmittedDate, value);
			}
		}

		virtual public bool ExistSubmittedDate{
			get
			{
				return Strategy.ExistUnitRole(Meta.SubmittedDate);
			}
		}

		virtual public void RemoveSubmittedDate()
		{
			Strategy.RemoveUnitRole(Meta.SubmittedDate);
		}


		virtual public Person SubmittedBy
		{ 
			get
			{
				return (Person) Strategy.GetCompositeRole(Meta.SubmittedBy);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.SubmittedBy ,value);
			}
		}

		virtual public bool ExistSubmittedBy
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.SubmittedBy);
			}
		}

		virtual public void RemoveSubmittedBy()
		{
			Strategy.RemoveCompositeRole(Meta.SubmittedBy);
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

	public class ServiceEntryHeaderMeta
	{
		public static readonly ServiceEntryHeaderMeta Instance = new ServiceEntryHeaderMeta();

		public global::Allors.Meta.Class ObjectType = global::Allors.Meta.Classes.ServiceEntryHeader;

		public global::Allors.Meta.RoleType ServiceEntry 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.ServiceEntryHeaderServiceEntry;
			}
		} 
		public global::Allors.Meta.RoleType SubmittedDate 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.ServiceEntryHeaderSubmittedDate;
			}
		} 
		public global::Allors.Meta.RoleType SubmittedBy 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.ServiceEntryHeaderSubmittedBy;
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