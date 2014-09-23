// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	[System.Diagnostics.DebuggerNonUserCode]
	public partial class AmountDue : Allors.ObjectBase , AccessControlledObject, Searchable, UserInterfaceable
	{
		public static readonly AmountDueMeta Meta = AmountDueMeta.Instance;

		public AmountDue(Allors.IStrategy allors) : base(allors) {}

		public static AmountDue Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (AmountDue) allorsSession.Instantiate(allorsObjectId);		
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


		virtual public PaymentMethod PaymentMethod
		{ 
			get
			{
				return (PaymentMethod) Strategy.GetCompositeRole(Meta.PaymentMethod);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.PaymentMethod ,value);
			}
		}

		virtual public bool ExistPaymentMethod
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.PaymentMethod);
			}
		}

		virtual public void RemovePaymentMethod()
		{
			Strategy.RemoveCompositeRole(Meta.PaymentMethod);
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



		virtual public global::System.DateTime? BlockedForDunning 
		{
			get
			{
				return (global::System.DateTime?) Strategy.GetUnitRole(Meta.BlockedForDunning);
			}
			set
			{
				Strategy.SetUnitRole(Meta.BlockedForDunning, value);
			}
		}

		virtual public bool ExistBlockedForDunning{
			get
			{
				return Strategy.ExistUnitRole(Meta.BlockedForDunning);
			}
		}

		virtual public void RemoveBlockedForDunning()
		{
			Strategy.RemoveUnitRole(Meta.BlockedForDunning);
		}



		virtual public global::System.Decimal? AmountVat 
		{
			get
			{
				return (global::System.Decimal?) Strategy.GetUnitRole(Meta.AmountVat);
			}
			set
			{
				Strategy.SetUnitRole(Meta.AmountVat, value);
			}
		}

		virtual public bool ExistAmountVat{
			get
			{
				return Strategy.ExistUnitRole(Meta.AmountVat);
			}
		}

		virtual public void RemoveAmountVat()
		{
			Strategy.RemoveUnitRole(Meta.AmountVat);
		}


		virtual public BankAccount BankAccount
		{ 
			get
			{
				return (BankAccount) Strategy.GetCompositeRole(Meta.BankAccount);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.BankAccount ,value);
			}
		}

		virtual public bool ExistBankAccount
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.BankAccount);
			}
		}

		virtual public void RemoveBankAccount()
		{
			Strategy.RemoveCompositeRole(Meta.BankAccount);
		}



		virtual public global::System.DateTime? ReconciliationDate 
		{
			get
			{
				return (global::System.DateTime?) Strategy.GetUnitRole(Meta.ReconciliationDate);
			}
			set
			{
				Strategy.SetUnitRole(Meta.ReconciliationDate, value);
			}
		}

		virtual public bool ExistReconciliationDate{
			get
			{
				return Strategy.ExistUnitRole(Meta.ReconciliationDate);
			}
		}

		virtual public void RemoveReconciliationDate()
		{
			Strategy.RemoveUnitRole(Meta.ReconciliationDate);
		}



		virtual public global::System.String InvoiceNumber 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(Meta.InvoiceNumber);
			}
			set
			{
				Strategy.SetUnitRole(Meta.InvoiceNumber, value);
			}
		}

		virtual public bool ExistInvoiceNumber{
			get
			{
				return Strategy.ExistUnitRole(Meta.InvoiceNumber);
			}
		}

		virtual public void RemoveInvoiceNumber()
		{
			Strategy.RemoveUnitRole(Meta.InvoiceNumber);
		}



		virtual public global::System.Int32? DunningStep 
		{
			get
			{
				return (global::System.Int32?) Strategy.GetUnitRole(Meta.DunningStep);
			}
			set
			{
				Strategy.SetUnitRole(Meta.DunningStep, value);
			}
		}

		virtual public bool ExistDunningStep{
			get
			{
				return Strategy.ExistUnitRole(Meta.DunningStep);
			}
		}

		virtual public void RemoveDunningStep()
		{
			Strategy.RemoveUnitRole(Meta.DunningStep);
		}



		virtual public global::System.Int32? SubAccountNumber 
		{
			get
			{
				return (global::System.Int32?) Strategy.GetUnitRole(Meta.SubAccountNumber);
			}
			set
			{
				Strategy.SetUnitRole(Meta.SubAccountNumber, value);
			}
		}

		virtual public bool ExistSubAccountNumber{
			get
			{
				return Strategy.ExistUnitRole(Meta.SubAccountNumber);
			}
		}

		virtual public void RemoveSubAccountNumber()
		{
			Strategy.RemoveUnitRole(Meta.SubAccountNumber);
		}



		virtual public global::System.String TransactionNumber 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(Meta.TransactionNumber);
			}
			set
			{
				Strategy.SetUnitRole(Meta.TransactionNumber, value);
			}
		}

		virtual public bool ExistTransactionNumber{
			get
			{
				return Strategy.ExistUnitRole(Meta.TransactionNumber);
			}
		}

		virtual public void RemoveTransactionNumber()
		{
			Strategy.RemoveUnitRole(Meta.TransactionNumber);
		}


		virtual public DebitCreditConstant Side
		{ 
			get
			{
				return (DebitCreditConstant) Strategy.GetCompositeRole(Meta.Side);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.Side ,value);
			}
		}

		virtual public bool ExistSide
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.Side);
			}
		}

		virtual public void RemoveSide()
		{
			Strategy.RemoveCompositeRole(Meta.Side);
		}


		virtual public Currency Currency
		{ 
			get
			{
				return (Currency) Strategy.GetCompositeRole(Meta.Currency);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.Currency ,value);
			}
		}

		virtual public bool ExistCurrency
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.Currency);
			}
		}

		virtual public void RemoveCurrency()
		{
			Strategy.RemoveCompositeRole(Meta.Currency);
		}



		virtual public global::System.Boolean? BlockedForPayment 
		{
			get
			{
				return (global::System.Boolean?) Strategy.GetUnitRole(Meta.BlockedForPayment);
			}
			set
			{
				Strategy.SetUnitRole(Meta.BlockedForPayment, value);
			}
		}

		virtual public bool ExistBlockedForPayment{
			get
			{
				return Strategy.ExistUnitRole(Meta.BlockedForPayment);
			}
		}

		virtual public void RemoveBlockedForPayment()
		{
			Strategy.RemoveUnitRole(Meta.BlockedForPayment);
		}



		virtual public global::System.DateTime? DateLastReminder 
		{
			get
			{
				return (global::System.DateTime?) Strategy.GetUnitRole(Meta.DateLastReminder);
			}
			set
			{
				Strategy.SetUnitRole(Meta.DateLastReminder, value);
			}
		}

		virtual public bool ExistDateLastReminder{
			get
			{
				return Strategy.ExistUnitRole(Meta.DateLastReminder);
			}
		}

		virtual public void RemoveDateLastReminder()
		{
			Strategy.RemoveUnitRole(Meta.DateLastReminder);
		}



		virtual public global::System.String YourReference 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(Meta.YourReference);
			}
			set
			{
				Strategy.SetUnitRole(Meta.YourReference, value);
			}
		}

		virtual public bool ExistYourReference{
			get
			{
				return Strategy.ExistUnitRole(Meta.YourReference);
			}
		}

		virtual public void RemoveYourReference()
		{
			Strategy.RemoveUnitRole(Meta.YourReference);
		}



		virtual public global::System.String OurReference 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(Meta.OurReference);
			}
			set
			{
				Strategy.SetUnitRole(Meta.OurReference, value);
			}
		}

		virtual public bool ExistOurReference{
			get
			{
				return Strategy.ExistUnitRole(Meta.OurReference);
			}
		}

		virtual public void RemoveOurReference()
		{
			Strategy.RemoveUnitRole(Meta.OurReference);
		}



		virtual public global::System.String ReconciliationNumber 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(Meta.ReconciliationNumber);
			}
			set
			{
				Strategy.SetUnitRole(Meta.ReconciliationNumber, value);
			}
		}

		virtual public bool ExistReconciliationNumber{
			get
			{
				return Strategy.ExistUnitRole(Meta.ReconciliationNumber);
			}
		}

		virtual public void RemoveReconciliationNumber()
		{
			Strategy.RemoveUnitRole(Meta.ReconciliationNumber);
		}



		virtual public global::System.DateTime? DueDate 
		{
			get
			{
				return (global::System.DateTime?) Strategy.GetUnitRole(Meta.DueDate);
			}
			set
			{
				Strategy.SetUnitRole(Meta.DueDate, value);
			}
		}

		virtual public bool ExistDueDate{
			get
			{
				return Strategy.ExistUnitRole(Meta.DueDate);
			}
		}

		virtual public void RemoveDueDate()
		{
			Strategy.RemoveUnitRole(Meta.DueDate);
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

	}

	public class AmountDueMeta
	{
		public static readonly AmountDueMeta Instance = new AmountDueMeta();

		public global::Allors.Meta.Class ObjectType = global::Allors.Meta.Classes.AmountDue;

		public global::Allors.Meta.RoleType Amount 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.AmountDueAmount;
			}
		} 
		public global::Allors.Meta.RoleType PaymentMethod 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.AmountDuePaymentMethod;
			}
		} 
		public global::Allors.Meta.RoleType TransactionDate 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.AmountDueTransactionDate;
			}
		} 
		public global::Allors.Meta.RoleType BlockedForDunning 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.AmountDueBlockedForDunning;
			}
		} 
		public global::Allors.Meta.RoleType AmountVat 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.AmountDueAmountVat;
			}
		} 
		public global::Allors.Meta.RoleType BankAccount 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.AmountDueBankAccount;
			}
		} 
		public global::Allors.Meta.RoleType ReconciliationDate 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.AmountDueReconciliationDate;
			}
		} 
		public global::Allors.Meta.RoleType InvoiceNumber 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.AmountDueInvoiceNumber;
			}
		} 
		public global::Allors.Meta.RoleType DunningStep 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.AmountDueDunningStep;
			}
		} 
		public global::Allors.Meta.RoleType SubAccountNumber 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.AmountDueSubAccountNumber;
			}
		} 
		public global::Allors.Meta.RoleType TransactionNumber 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.AmountDueTransactionNumber;
			}
		} 
		public global::Allors.Meta.RoleType Side 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.AmountDueSide;
			}
		} 
		public global::Allors.Meta.RoleType Currency 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.AmountDueCurrency;
			}
		} 
		public global::Allors.Meta.RoleType BlockedForPayment 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.AmountDueBlockedForPayment;
			}
		} 
		public global::Allors.Meta.RoleType DateLastReminder 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.AmountDueDateLastReminder;
			}
		} 
		public global::Allors.Meta.RoleType YourReference 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.AmountDueYourReference;
			}
		} 
		public global::Allors.Meta.RoleType OurReference 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.AmountDueOurReference;
			}
		} 
		public global::Allors.Meta.RoleType ReconciliationNumber 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.AmountDueReconciliationNumber;
			}
		} 
		public global::Allors.Meta.RoleType DueDate 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.AmountDueDueDate;
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
		public global::Allors.Meta.RoleType DisplayName 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.UserInterfaceableDisplayName;
			}
		} 

	}
}