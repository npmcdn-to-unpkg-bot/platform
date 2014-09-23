// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	[System.Diagnostics.DebuggerNonUserCode]
	public partial class PersonTraining : Allors.ObjectBase , Period, UserInterfaceable
	{
		public static readonly PersonTrainingMeta Meta = PersonTrainingMeta.Instance;

		public PersonTraining(Allors.IStrategy allors) : base(allors) {}

		public static PersonTraining Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (PersonTraining) allorsSession.Instantiate(allorsObjectId);		
		}



		virtual public Training Training
		{ 
			get
			{
				return (Training) Strategy.GetCompositeRole(Meta.Training);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.Training ,value);
			}
		}

		virtual public bool ExistTraining
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.Training);
			}
		}

		virtual public void RemoveTraining()
		{
			Strategy.RemoveCompositeRole(Meta.Training);
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



		virtual public global::Allors.Extent<Person> PersonsWherePersonTraining
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.PersonsWherePersonTraining);
			}
		}

		virtual public bool ExistPersonsWherePersonTraining
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.PersonsWherePersonTraining);
			}
		}

	}

	public class PersonTrainingMeta
	{
		public static readonly PersonTrainingMeta Instance = new PersonTrainingMeta();

		public global::Allors.Meta.Class ObjectType = global::Allors.Meta.Classes.PersonTraining;

		public global::Allors.Meta.RoleType Training 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.PersonTrainingTraining;
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

		public global::Allors.Meta.AssociationType PersonsWherePersonTraining 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.PersonPersonTraining;
			}
		} 

	}
}