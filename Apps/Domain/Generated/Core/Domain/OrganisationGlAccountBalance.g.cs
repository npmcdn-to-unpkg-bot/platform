// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	[System.Diagnostics.DebuggerNonUserCode]
	public partial class OrganisationGlAccountBalance : Allors.ObjectBase , UserInterfaceable
	{
		public static readonly OrganisationGlAccountBalanceMeta Meta = OrganisationGlAccountBalanceMeta.Instance;

		public OrganisationGlAccountBalance(Allors.IStrategy allors) : base(allors) {}

		public static OrganisationGlAccountBalance Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (OrganisationGlAccountBalance) allorsSession.Instantiate(allorsObjectId);		
		}



		virtual public OrganisationGlAccount OrganisationGlAccount
		{ 
			get
			{
				return (OrganisationGlAccount) Strategy.GetCompositeRole(Meta.OrganisationGlAccount);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.OrganisationGlAccount ,value);
			}
		}

		virtual public bool ExistOrganisationGlAccount
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.OrganisationGlAccount);
			}
		}

		virtual public void RemoveOrganisationGlAccount()
		{
			Strategy.RemoveCompositeRole(Meta.OrganisationGlAccount);
		}



		virtual public global::System.Decimal? Amount 
		{
			get
			{
				return (global::System.Decimal?) Strategy.GetUnitRole(Meta.Amount);
			}
			set
			{
				Strategy.SetUnitRole(Meta.Amount, value);
			}
		}

		virtual public bool ExistAmount{
			get
			{
				return Strategy.ExistUnitRole(Meta.Amount);
			}
		}

		virtual public void RemoveAmount()
		{
			Strategy.RemoveUnitRole(Meta.Amount);
		}


		virtual public AccountingPeriod AccountingPeriod
		{ 
			get
			{
				return (AccountingPeriod) Strategy.GetCompositeRole(Meta.AccountingPeriod);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.AccountingPeriod ,value);
			}
		}

		virtual public bool ExistAccountingPeriod
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.AccountingPeriod);
			}
		}

		virtual public void RemoveAccountingPeriod()
		{
			Strategy.RemoveCompositeRole(Meta.AccountingPeriod);
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



		virtual public global::Allors.Extent<AccountingTransactionDetail> AccountingTransactionDetailsWhereOrganisationGlAccountBalance
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.AccountingTransactionDetailsWhereOrganisationGlAccountBalance);
			}
		}

		virtual public bool ExistAccountingTransactionDetailsWhereOrganisationGlAccountBalance
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.AccountingTransactionDetailsWhereOrganisationGlAccountBalance);
			}
		}

	}

	public class OrganisationGlAccountBalanceMeta
	{
		public static readonly OrganisationGlAccountBalanceMeta Instance = new OrganisationGlAccountBalanceMeta();

		public global::Allors.Meta.Class ObjectType = global::Allors.Meta.Classes.OrganisationGlAccountBalance;

		public global::Allors.Meta.RoleType OrganisationGlAccount 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.OrganisationGlAccountBalanceOrganisationGlAccount;
			}
		} 
		public global::Allors.Meta.RoleType Amount 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.OrganisationGlAccountBalanceAmount;
			}
		} 
		public global::Allors.Meta.RoleType AccountingPeriod 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.OrganisationGlAccountBalanceAccountingPeriod;
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

		public global::Allors.Meta.AssociationType AccountingTransactionDetailsWhereOrganisationGlAccountBalance 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.AccountingTransactionDetailOrganisationGlAccountBalance;
			}
		} 

	}
}