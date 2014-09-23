// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	[System.Diagnostics.DebuggerNonUserCode]
	public partial class CreditCard : Allors.ObjectBase , FinancialAccount, UserInterfaceable, Searchable
	{
		public static readonly CreditCardMeta Meta = CreditCardMeta.Instance;

		public CreditCard(Allors.IStrategy allors) : base(allors) {}

		public static CreditCard Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (CreditCard) allorsSession.Instantiate(allorsObjectId);		
		}




		virtual public global::System.String NameOnCard 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(Meta.NameOnCard);
			}
			set
			{
				Strategy.SetUnitRole(Meta.NameOnCard, value);
			}
		}

		virtual public bool ExistNameOnCard{
			get
			{
				return Strategy.ExistUnitRole(Meta.NameOnCard);
			}
		}

		virtual public void RemoveNameOnCard()
		{
			Strategy.RemoveUnitRole(Meta.NameOnCard);
		}


		virtual public CreditCardCompany CreditCardCompany
		{ 
			get
			{
				return (CreditCardCompany) Strategy.GetCompositeRole(Meta.CreditCardCompany);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.CreditCardCompany ,value);
			}
		}

		virtual public bool ExistCreditCardCompany
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.CreditCardCompany);
			}
		}

		virtual public void RemoveCreditCardCompany()
		{
			Strategy.RemoveCompositeRole(Meta.CreditCardCompany);
		}



		virtual public global::System.Int32? ExpirationYear 
		{
			get
			{
				return (global::System.Int32?) Strategy.GetUnitRole(Meta.ExpirationYear);
			}
			set
			{
				Strategy.SetUnitRole(Meta.ExpirationYear, value);
			}
		}

		virtual public bool ExistExpirationYear{
			get
			{
				return Strategy.ExistUnitRole(Meta.ExpirationYear);
			}
		}

		virtual public void RemoveExpirationYear()
		{
			Strategy.RemoveUnitRole(Meta.ExpirationYear);
		}



		virtual public global::System.Int32? ExpirationMonth 
		{
			get
			{
				return (global::System.Int32?) Strategy.GetUnitRole(Meta.ExpirationMonth);
			}
			set
			{
				Strategy.SetUnitRole(Meta.ExpirationMonth, value);
			}
		}

		virtual public bool ExistExpirationMonth{
			get
			{
				return Strategy.ExistUnitRole(Meta.ExpirationMonth);
			}
		}

		virtual public void RemoveExpirationMonth()
		{
			Strategy.RemoveUnitRole(Meta.ExpirationMonth);
		}



		virtual public global::System.String CardNumber 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(Meta.CardNumber);
			}
			set
			{
				Strategy.SetUnitRole(Meta.CardNumber, value);
			}
		}

		virtual public bool ExistCardNumber{
			get
			{
				return Strategy.ExistUnitRole(Meta.CardNumber);
			}
		}

		virtual public void RemoveCardNumber()
		{
			Strategy.RemoveUnitRole(Meta.CardNumber);
		}


		virtual public global::Allors.Extent<FinancialAccountTransaction> FinancialAccountTransactions
		{ 
			get
			{
				return Strategy.GetCompositeRoles(Meta.FinancialAccountTransaction);
			}
			set
			{
				Strategy.SetCompositeRoles(Meta.FinancialAccountTransaction, value);
			}
		}

		virtual public void AddFinancialAccountTransaction (FinancialAccountTransaction value)
		{
			Strategy.AddCompositeRole(Meta.FinancialAccountTransaction, value);
		}

		virtual public void RemoveFinancialAccountTransaction (FinancialAccountTransaction value)
		{
			Strategy.RemoveCompositeRole(Meta.FinancialAccountTransaction,value);
		}

		virtual public bool ExistFinancialAccountTransactions
		{
			get
			{
				return Strategy.ExistCompositeRoles(Meta.FinancialAccountTransaction);
			}
		}

		virtual public void RemoveFinancialAccountTransactions()
		{
			Strategy.RemoveCompositeRoles(Meta.FinancialAccountTransaction);
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



		virtual public global::Allors.Extent<OwnCreditCard> OwnCreditCardsWhereCreditCard
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.OwnCreditCardsWhereCreditCard);
			}
		}

		virtual public bool ExistOwnCreditCardsWhereCreditCard
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.OwnCreditCardsWhereCreditCard);
			}
		}


		virtual public Party PartyWhereCreditCard
		{ 
			get
			{
				return (Party) Strategy.GetCompositeAssociation(Meta.PartyWhereCreditCard);
			}
		} 

		virtual public bool ExistPartyWhereCreditCard
		{
			get
			{
				return Strategy.ExistCompositeAssociation(Meta.PartyWhereCreditCard);
			}
		}

	}

	public class CreditCardMeta
	{
		public static readonly CreditCardMeta Instance = new CreditCardMeta();

		public global::Allors.Meta.Class ObjectType = global::Allors.Meta.Classes.CreditCard;

		public global::Allors.Meta.RoleType NameOnCard 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.CreditCardNameOnCard;
			}
		} 
		public global::Allors.Meta.RoleType CreditCardCompany 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.CreditCardCreditCardCompany;
			}
		} 
		public global::Allors.Meta.RoleType ExpirationYear 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.CreditCardExpirationYear;
			}
		} 
		public global::Allors.Meta.RoleType ExpirationMonth 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.CreditCardExpirationMonth;
			}
		} 
		public global::Allors.Meta.RoleType CardNumber 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.CreditCardCardNumber;
			}
		} 
		public global::Allors.Meta.RoleType FinancialAccountTransaction 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.FinancialAccountFinancialAccountTransaction;
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

		public global::Allors.Meta.AssociationType OwnCreditCardsWhereCreditCard 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.OwnCreditCardCreditCard;
			}
		} 
		public global::Allors.Meta.AssociationType PartyWhereCreditCard 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.PartyCreditCard;
			}
		} 

	}
}