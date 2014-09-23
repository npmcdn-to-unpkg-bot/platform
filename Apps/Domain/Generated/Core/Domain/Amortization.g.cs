// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	[System.Diagnostics.DebuggerNonUserCode]
	public partial class Amortization : Allors.ObjectBase , InternalAccountingTransaction
	{
		public static readonly AmortizationMeta Meta = AmortizationMeta.Instance;

		public Amortization(Allors.IStrategy allors) : base(allors) {}

		public static Amortization Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (Amortization) allorsSession.Instantiate(allorsObjectId);		
		}



		virtual public InternalOrganisation InternalOrganisation
		{ 
			get
			{
				return (InternalOrganisation) Strategy.GetCompositeRole(Meta.InternalOrganisation);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.InternalOrganisation ,value);
			}
		}

		virtual public bool ExistInternalOrganisation
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.InternalOrganisation);
			}
		}

		virtual public void RemoveInternalOrganisation()
		{
			Strategy.RemoveCompositeRole(Meta.InternalOrganisation);
		}


		virtual public global::Allors.Extent<AccountingTransactionDetail> AccountingTransactionDetails
		{ 
			get
			{
				return Strategy.GetCompositeRoles(Meta.AccountingTransactionDetail);
			}
			set
			{
				Strategy.SetCompositeRoles(Meta.AccountingTransactionDetail, value);
			}
		}

		virtual public void AddAccountingTransactionDetail (AccountingTransactionDetail value)
		{
			Strategy.AddCompositeRole(Meta.AccountingTransactionDetail, value);
		}

		virtual public void RemoveAccountingTransactionDetail (AccountingTransactionDetail value)
		{
			Strategy.RemoveCompositeRole(Meta.AccountingTransactionDetail,value);
		}

		virtual public bool ExistAccountingTransactionDetails
		{
			get
			{
				return Strategy.ExistCompositeRoles(Meta.AccountingTransactionDetail);
			}
		}

		virtual public void RemoveAccountingTransactionDetails()
		{
			Strategy.RemoveCompositeRoles(Meta.AccountingTransactionDetail);
		}



		virtual public global::System.String Description 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(Meta.Description);
			}
			set
			{
				Strategy.SetUnitRole(Meta.Description, value);
			}
		}

		virtual public bool ExistDescription{
			get
			{
				return Strategy.ExistUnitRole(Meta.Description);
			}
		}

		virtual public void RemoveDescription()
		{
			Strategy.RemoveUnitRole(Meta.Description);
		}



		virtual public global::System.DateTime? TransactionDate 
		{
			get
			{
				return (global::System.DateTime?) Strategy.GetUnitRole(Meta.TransactionDate);
			}
			set
			{
				Strategy.SetUnitRole(Meta.TransactionDate, value);
			}
		}

		virtual public bool ExistTransactionDate{
			get
			{
				return Strategy.ExistUnitRole(Meta.TransactionDate);
			}
		}

		virtual public void RemoveTransactionDate()
		{
			Strategy.RemoveUnitRole(Meta.TransactionDate);
		}



		virtual public global::System.Decimal? DerivedTotalAmount 
		{
			get
			{
				return (global::System.Decimal?) Strategy.GetUnitRole(Meta.DerivedTotalAmount);
			}
			set
			{
				Strategy.SetUnitRole(Meta.DerivedTotalAmount, value);
			}
		}

		virtual public bool ExistDerivedTotalAmount{
			get
			{
				return Strategy.ExistUnitRole(Meta.DerivedTotalAmount);
			}
		}

		virtual public void RemoveDerivedTotalAmount()
		{
			Strategy.RemoveUnitRole(Meta.DerivedTotalAmount);
		}


		virtual public AccountingTransactionNumber AccountingTransactionNumber
		{ 
			get
			{
				return (AccountingTransactionNumber) Strategy.GetCompositeRole(Meta.AccountingTransactionNumber);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.AccountingTransactionNumber ,value);
			}
		}

		virtual public bool ExistAccountingTransactionNumber
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.AccountingTransactionNumber);
			}
		}

		virtual public void RemoveAccountingTransactionNumber()
		{
			Strategy.RemoveCompositeRole(Meta.AccountingTransactionNumber);
		}



		virtual public global::System.DateTime? EntryDate 
		{
			get
			{
				return (global::System.DateTime?) Strategy.GetUnitRole(Meta.EntryDate);
			}
			set
			{
				Strategy.SetUnitRole(Meta.EntryDate, value);
			}
		}

		virtual public bool ExistEntryDate{
			get
			{
				return Strategy.ExistUnitRole(Meta.EntryDate);
			}
		}

		virtual public void RemoveEntryDate()
		{
			Strategy.RemoveUnitRole(Meta.EntryDate);
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


		virtual public SearchData SearchData
		{ 
			get
			{
				return (SearchData) Strategy.GetCompositeRole(Meta.SearchData);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.SearchData ,value);
			}
		}

		virtual public bool ExistSearchData
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.SearchData);
			}
		}

		virtual public void RemoveSearchData()
		{
			Strategy.RemoveCompositeRole(Meta.SearchData);
		}

	}

	public class AmortizationMeta
	{
		public static readonly AmortizationMeta Instance = new AmortizationMeta();

		public global::Allors.Meta.Class ObjectType = global::Allors.Meta.Classes.Amortization;

		public global::Allors.Meta.RoleType InternalOrganisation 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.InternalAccountingTransactionInternalOrganisation;
			}
		} 
		public global::Allors.Meta.RoleType AccountingTransactionDetail 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.AccountingTransactionAccountingTransactionDetail;
			}
		} 
		public global::Allors.Meta.RoleType Description 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.AccountingTransactionDescription;
			}
		} 
		public global::Allors.Meta.RoleType TransactionDate 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.AccountingTransactionTransactionDate;
			}
		} 
		public global::Allors.Meta.RoleType DerivedTotalAmount 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.AccountingTransactionDerivedTotalAmount;
			}
		} 
		public global::Allors.Meta.RoleType AccountingTransactionNumber 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.AccountingTransactionAccountingTransactionNumber;
			}
		} 
		public global::Allors.Meta.RoleType EntryDate 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.AccountingTransactionEntryDate;
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
		public global::Allors.Meta.RoleType SearchData 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.SearchableSearchData;
			}
		} 

	}
}