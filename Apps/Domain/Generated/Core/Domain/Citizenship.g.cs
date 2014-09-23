// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	[System.Diagnostics.DebuggerNonUserCode]
	public partial class Citizenship : Allors.ObjectBase , UserInterfaceable
	{
		public static readonly CitizenshipMeta Meta = CitizenshipMeta.Instance;

		public Citizenship(Allors.IStrategy allors) : base(allors) {}

		public static Citizenship Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (Citizenship) allorsSession.Instantiate(allorsObjectId);		
		}



		virtual public global::Allors.Extent<Passport> Passports
		{ 
			get
			{
				return Strategy.GetCompositeRoles(Meta.Passport);
			}
			set
			{
				Strategy.SetCompositeRoles(Meta.Passport, value);
			}
		}

		virtual public void AddPassport (Passport value)
		{
			Strategy.AddCompositeRole(Meta.Passport, value);
		}

		virtual public void RemovePassport (Passport value)
		{
			Strategy.RemoveCompositeRole(Meta.Passport,value);
		}

		virtual public bool ExistPassports
		{
			get
			{
				return Strategy.ExistCompositeRoles(Meta.Passport);
			}
		}

		virtual public void RemovePassports()
		{
			Strategy.RemoveCompositeRoles(Meta.Passport);
		}


		virtual public Country Country
		{ 
			get
			{
				return (Country) Strategy.GetCompositeRole(Meta.Country);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.Country ,value);
			}
		}

		virtual public bool ExistCountry
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.Country);
			}
		}

		virtual public void RemoveCountry()
		{
			Strategy.RemoveCompositeRole(Meta.Country);
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



		virtual public Person PersonWhereCitizenship
		{ 
			get
			{
				return (Person) Strategy.GetCompositeAssociation(Meta.PersonWhereCitizenship);
			}
		} 

		virtual public bool ExistPersonWhereCitizenship
		{
			get
			{
				return Strategy.ExistCompositeAssociation(Meta.PersonWhereCitizenship);
			}
		}

	}

	public class CitizenshipMeta
	{
		public static readonly CitizenshipMeta Instance = new CitizenshipMeta();

		public global::Allors.Meta.Class ObjectType = global::Allors.Meta.Classes.Citizenship;

		public global::Allors.Meta.RoleType Passport 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.CitizenshipPassport;
			}
		} 
		public global::Allors.Meta.RoleType Country 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.CitizenshipCountry;
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

		public global::Allors.Meta.AssociationType PersonWhereCitizenship 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.PersonCitizenship;
			}
		} 

	}
}