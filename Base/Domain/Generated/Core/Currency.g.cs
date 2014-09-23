// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	[System.Diagnostics.DebuggerNonUserCode]
	public partial class Currency : Allors.ObjectBase , UserInterfaceable
	{
		public static readonly CurrencyMeta Meta = CurrencyMeta.Instance;

		public Currency(Allors.IStrategy allors) : base(allors) {}

		public static Currency Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (Currency) allorsSession.Instantiate(allorsObjectId);		
		}




		virtual public global::System.String IsoCode 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(Meta.IsoCode);
			}
			set
			{
				Strategy.SetUnitRole(Meta.IsoCode, value);
			}
		}

		virtual public bool ExistIsoCode{
			get
			{
				return Strategy.ExistUnitRole(Meta.IsoCode);
			}
		}

		virtual public void RemoveIsoCode()
		{
			Strategy.RemoveUnitRole(Meta.IsoCode);
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



		virtual public global::System.String Symbol 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(Meta.Symbol);
			}
			set
			{
				Strategy.SetUnitRole(Meta.Symbol, value);
			}
		}

		virtual public bool ExistSymbol{
			get
			{
				return Strategy.ExistUnitRole(Meta.Symbol);
			}
		}

		virtual public void RemoveSymbol()
		{
			Strategy.RemoveUnitRole(Meta.Symbol);
		}


		virtual public global::Allors.Extent<LocalisedText> LocalisedNames
		{ 
			get
			{
				return Strategy.GetCompositeRoles(Meta.LocalisedName);
			}
			set
			{
				Strategy.SetCompositeRoles(Meta.LocalisedName, value);
			}
		}

		virtual public void AddLocalisedName (LocalisedText value)
		{
			Strategy.AddCompositeRole(Meta.LocalisedName, value);
		}

		virtual public void RemoveLocalisedName (LocalisedText value)
		{
			Strategy.RemoveCompositeRole(Meta.LocalisedName,value);
		}

		virtual public bool ExistLocalisedNames
		{
			get
			{
				return Strategy.ExistCompositeRoles(Meta.LocalisedName);
			}
		}

		virtual public void RemoveLocalisedNames()
		{
			Strategy.RemoveCompositeRoles(Meta.LocalisedName);
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



		virtual public global::Allors.Extent<Country> CountriesWhereCurrency
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.CountriesWhereCurrency);
			}
		}

		virtual public bool ExistCountriesWhereCurrency
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.CountriesWhereCurrency);
			}
		}

	}

	public class CurrencyMeta
	{
		public static readonly CurrencyMeta Instance = new CurrencyMeta();

		public global::Allors.Meta.Class ObjectType = global::Allors.Meta.Classes.Currency;

		public global::Allors.Meta.RoleType IsoCode 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.CurrencyIsoCode;
			}
		} 
		public global::Allors.Meta.RoleType Name 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.CurrencyName;
			}
		} 
		public global::Allors.Meta.RoleType Symbol 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.CurrencySymbol;
			}
		} 
		public global::Allors.Meta.RoleType LocalisedName 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.CurrencyLocalisedName;
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

		public global::Allors.Meta.AssociationType CountriesWhereCurrency 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.CountryCurrency;
			}
		} 

	}
}