// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	[System.Diagnostics.DebuggerNonUserCode]
	public partial class ProductRequirement : Allors.ObjectBase , Requirement
	{
		public static readonly ProductRequirementMeta Meta = ProductRequirementMeta.Instance;

		public ProductRequirement(Allors.IStrategy allors) : base(allors) {}

		public static ProductRequirement Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (ProductRequirement) allorsSession.Instantiate(allorsObjectId);		
		}



		virtual public Product Product
		{ 
			get
			{
				return (Product) Strategy.GetCompositeRole(Meta.Product);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.Product ,value);
			}
		}

		virtual public bool ExistProduct
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.Product);
			}
		}

		virtual public void RemoveProduct()
		{
			Strategy.RemoveCompositeRole(Meta.Product);
		}


		virtual public global::Allors.Extent<DesiredProductFeature> DesiredProductFeatures
		{ 
			get
			{
				return Strategy.GetCompositeRoles(Meta.DesiredProductFeature);
			}
			set
			{
				Strategy.SetCompositeRoles(Meta.DesiredProductFeature, value);
			}
		}

		virtual public void AddDesiredProductFeature (DesiredProductFeature value)
		{
			Strategy.AddCompositeRole(Meta.DesiredProductFeature, value);
		}

		virtual public void RemoveDesiredProductFeature (DesiredProductFeature value)
		{
			Strategy.RemoveCompositeRole(Meta.DesiredProductFeature,value);
		}

		virtual public bool ExistDesiredProductFeatures
		{
			get
			{
				return Strategy.ExistCompositeRoles(Meta.DesiredProductFeature);
			}
		}

		virtual public void RemoveDesiredProductFeatures()
		{
			Strategy.RemoveCompositeRoles(Meta.DesiredProductFeature);
		}



		virtual public global::System.DateTime RequiredByDate 
		{
			get
			{
				return (global::System.DateTime) Strategy.GetUnitRole(Meta.RequiredByDate);
			}
			set
			{
				Strategy.SetUnitRole(Meta.RequiredByDate, value);
			}
		}

		virtual public bool ExistRequiredByDate{
			get
			{
				return Strategy.ExistUnitRole(Meta.RequiredByDate);
			}
		}

		virtual public void RemoveRequiredByDate()
		{
			Strategy.RemoveUnitRole(Meta.RequiredByDate);
		}


		virtual public RequirementObjectState PreviousObjectState
		{ 
			get
			{
				return (RequirementObjectState) Strategy.GetCompositeRole(Meta.PreviousObjectState);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.PreviousObjectState ,value);
			}
		}

		virtual public bool ExistPreviousObjectState
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.PreviousObjectState);
			}
		}

		virtual public void RemovePreviousObjectState()
		{
			Strategy.RemoveCompositeRole(Meta.PreviousObjectState);
		}


		virtual public Party Authorizer
		{ 
			get
			{
				return (Party) Strategy.GetCompositeRole(Meta.Authorizer);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.Authorizer ,value);
			}
		}

		virtual public bool ExistAuthorizer
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.Authorizer);
			}
		}

		virtual public void RemoveAuthorizer()
		{
			Strategy.RemoveCompositeRole(Meta.Authorizer);
		}


		virtual public global::Allors.Extent<RequirementStatus> RequirementStatuses
		{ 
			get
			{
				return Strategy.GetCompositeRoles(Meta.RequirementStatus);
			}
			set
			{
				Strategy.SetCompositeRoles(Meta.RequirementStatus, value);
			}
		}

		virtual public void AddRequirementStatus (RequirementStatus value)
		{
			Strategy.AddCompositeRole(Meta.RequirementStatus, value);
		}

		virtual public void RemoveRequirementStatus (RequirementStatus value)
		{
			Strategy.RemoveCompositeRole(Meta.RequirementStatus,value);
		}

		virtual public bool ExistRequirementStatuses
		{
			get
			{
				return Strategy.ExistCompositeRoles(Meta.RequirementStatus);
			}
		}

		virtual public void RemoveRequirementStatuses()
		{
			Strategy.RemoveCompositeRoles(Meta.RequirementStatus);
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


		virtual public global::Allors.Extent<Requirement> Children
		{ 
			get
			{
				return Strategy.GetCompositeRoles(Meta.Child);
			}
			set
			{
				Strategy.SetCompositeRoles(Meta.Child, value);
			}
		}

		virtual public void AddChild (Requirement value)
		{
			Strategy.AddCompositeRole(Meta.Child, value);
		}

		virtual public void RemoveChild (Requirement value)
		{
			Strategy.RemoveCompositeRole(Meta.Child,value);
		}

		virtual public bool ExistChildren
		{
			get
			{
				return Strategy.ExistCompositeRoles(Meta.Child);
			}
		}

		virtual public void RemoveChildren()
		{
			Strategy.RemoveCompositeRoles(Meta.Child);
		}


		virtual public Party NeededFor
		{ 
			get
			{
				return (Party) Strategy.GetCompositeRole(Meta.NeededFor);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.NeededFor ,value);
			}
		}

		virtual public bool ExistNeededFor
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.NeededFor);
			}
		}

		virtual public void RemoveNeededFor()
		{
			Strategy.RemoveCompositeRole(Meta.NeededFor);
		}


		virtual public Party Originator
		{ 
			get
			{
				return (Party) Strategy.GetCompositeRole(Meta.Originator);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.Originator ,value);
			}
		}

		virtual public bool ExistOriginator
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.Originator);
			}
		}

		virtual public void RemoveOriginator()
		{
			Strategy.RemoveCompositeRole(Meta.Originator);
		}


		virtual public RequirementObjectState CurrentObjectState
		{ 
			get
			{
				return (RequirementObjectState) Strategy.GetCompositeRole(Meta.CurrentObjectState);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.CurrentObjectState ,value);
			}
		}

		virtual public bool ExistCurrentObjectState
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.CurrentObjectState);
			}
		}

		virtual public void RemoveCurrentObjectState()
		{
			Strategy.RemoveCompositeRole(Meta.CurrentObjectState);
		}


		virtual public RequirementStatus CurrentRequirementStatus
		{ 
			get
			{
				return (RequirementStatus) Strategy.GetCompositeRole(Meta.CurrentRequirementStatus);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.CurrentRequirementStatus ,value);
			}
		}

		virtual public bool ExistCurrentRequirementStatus
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.CurrentRequirementStatus);
			}
		}

		virtual public void RemoveCurrentRequirementStatus()
		{
			Strategy.RemoveCompositeRole(Meta.CurrentRequirementStatus);
		}


		virtual public Facility Facility
		{ 
			get
			{
				return (Facility) Strategy.GetCompositeRole(Meta.Facility);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.Facility ,value);
			}
		}

		virtual public bool ExistFacility
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.Facility);
			}
		}

		virtual public void RemoveFacility()
		{
			Strategy.RemoveCompositeRole(Meta.Facility);
		}


		virtual public Party ServicedBy
		{ 
			get
			{
				return (Party) Strategy.GetCompositeRole(Meta.ServicedBy);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.ServicedBy ,value);
			}
		}

		virtual public bool ExistServicedBy
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.ServicedBy);
			}
		}

		virtual public void RemoveServicedBy()
		{
			Strategy.RemoveCompositeRole(Meta.ServicedBy);
		}



		virtual public global::System.Decimal EstimatedBudget 
		{
			get
			{
				return (global::System.Decimal) Strategy.GetUnitRole(Meta.EstimatedBudget);
			}
			set
			{
				Strategy.SetUnitRole(Meta.EstimatedBudget, value);
			}
		}

		virtual public bool ExistEstimatedBudget{
			get
			{
				return Strategy.ExistUnitRole(Meta.EstimatedBudget);
			}
		}

		virtual public void RemoveEstimatedBudget()
		{
			Strategy.RemoveUnitRole(Meta.EstimatedBudget);
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



		virtual public global::System.Int32 Quantity 
		{
			get
			{
				return (global::System.Int32) Strategy.GetUnitRole(Meta.Quantity);
			}
			set
			{
				Strategy.SetUnitRole(Meta.Quantity, value);
			}
		}

		virtual public bool ExistQuantity{
			get
			{
				return Strategy.ExistUnitRole(Meta.Quantity);
			}
		}

		virtual public void RemoveQuantity()
		{
			Strategy.RemoveUnitRole(Meta.Quantity);
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



		virtual public global::System.Guid UniqueId 
		{
			get
			{
				return (global::System.Guid) Strategy.GetUnitRole(Meta.UniqueId);
			}
			set
			{
				Strategy.SetUnitRole(Meta.UniqueId, value);
			}
		}

		virtual public bool ExistUniqueId{
			get
			{
				return Strategy.ExistUnitRole(Meta.UniqueId);
			}
		}

		virtual public void RemoveUniqueId()
		{
			Strategy.RemoveUnitRole(Meta.UniqueId);
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



		virtual public global::Allors.Extent<OrderRequirementCommitment> OrderRequirementCommitmentsWhereRequirement
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.OrderRequirementCommitmentsWhereRequirement);
			}
		}

		virtual public bool ExistOrderRequirementCommitmentsWhereRequirement
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.OrderRequirementCommitmentsWhereRequirement);
			}
		}


		virtual public global::Allors.Extent<RequirementCommunication> RequirementCommunicationsWhereRequirement
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.RequirementCommunicationsWhereRequirement);
			}
		}

		virtual public bool ExistRequirementCommunicationsWhereRequirement
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.RequirementCommunicationsWhereRequirement);
			}
		}


		virtual public global::Allors.Extent<WorkEffort> WorkEffortsWhereRequirementFulfillment
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.WorkEffortsWhereRequirementFulfillment);
			}
		}

		virtual public bool ExistWorkEffortsWhereRequirementFulfillment
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.WorkEffortsWhereRequirementFulfillment);
			}
		}


		virtual public global::Allors.Extent<RequirementBudgetAllocation> RequirementBudgetAllocationsWhereRequirement
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.RequirementBudgetAllocationsWhereRequirement);
			}
		}

		virtual public bool ExistRequirementBudgetAllocationsWhereRequirement
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.RequirementBudgetAllocationsWhereRequirement);
			}
		}


		virtual public Requirement RequirementWhereChild
		{ 
			get
			{
				return (Requirement) Strategy.GetCompositeAssociation(Meta.RequirementWhereChild);
			}
		} 

		virtual public bool ExistRequirementWhereChild
		{
			get
			{
				return Strategy.ExistCompositeAssociation(Meta.RequirementWhereChild);
			}
		}


		virtual public global::Allors.Extent<RequestItem> RequestItemsWhereRequirement
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.RequestItemsWhereRequirement);
			}
		}

		virtual public bool ExistRequestItemsWhereRequirement
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.RequestItemsWhereRequirement);
			}
		}

	}

	public class ProductRequirementMeta
	{
		public static readonly ProductRequirementMeta Instance = new ProductRequirementMeta();

		public global::Allors.Meta.Class ObjectType = global::Allors.Meta.Classes.ProductRequirement;

		public global::Allors.Meta.RoleType Product 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.ProductRequirementProduct;
			}
		} 
		public global::Allors.Meta.RoleType DesiredProductFeature 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.ProductRequirementDesiredProductFeature;
			}
		} 
		public global::Allors.Meta.RoleType RequiredByDate 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.RequirementRequiredByDate;
			}
		} 
		public global::Allors.Meta.RoleType PreviousObjectState 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.RequirementPreviousObjectState;
			}
		} 
		public global::Allors.Meta.RoleType Authorizer 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.RequirementAuthorizer;
			}
		} 
		public global::Allors.Meta.RoleType RequirementStatus 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.RequirementRequirementStatus;
			}
		} 
		public global::Allors.Meta.RoleType Reason 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.RequirementReason;
			}
		} 
		public global::Allors.Meta.RoleType Child 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.RequirementChild;
			}
		} 
		public global::Allors.Meta.RoleType NeededFor 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.RequirementNeededFor;
			}
		} 
		public global::Allors.Meta.RoleType Originator 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.RequirementOriginator;
			}
		} 
		public global::Allors.Meta.RoleType CurrentObjectState 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.RequirementCurrentObjectState;
			}
		} 
		public global::Allors.Meta.RoleType CurrentRequirementStatus 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.RequirementCurrentRequirementStatus;
			}
		} 
		public global::Allors.Meta.RoleType Facility 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.RequirementFacility;
			}
		} 
		public global::Allors.Meta.RoleType ServicedBy 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.RequirementServicedBy;
			}
		} 
		public global::Allors.Meta.RoleType EstimatedBudget 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.RequirementEstimatedBudget;
			}
		} 
		public global::Allors.Meta.RoleType Description 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.RequirementDescription;
			}
		} 
		public global::Allors.Meta.RoleType Quantity 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.RequirementQuantity;
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
		public global::Allors.Meta.RoleType UniqueId 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.UniquelyIdentifiableUniqueId;
			}
		} 
		public global::Allors.Meta.RoleType SearchData 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.SearchableSearchData;
			}
		} 

		public global::Allors.Meta.AssociationType OrderRequirementCommitmentsWhereRequirement 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.OrderRequirementCommitmentRequirement;
			}
		} 
		public global::Allors.Meta.AssociationType RequirementCommunicationsWhereRequirement 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.RequirementCommunicationRequirement;
			}
		} 
		public global::Allors.Meta.AssociationType WorkEffortsWhereRequirementFulfillment 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.WorkEffortRequirementFulfillment;
			}
		} 
		public global::Allors.Meta.AssociationType RequirementBudgetAllocationsWhereRequirement 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.RequirementBudgetAllocationRequirement;
			}
		} 
		public global::Allors.Meta.AssociationType RequirementWhereChild 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.RequirementChild;
			}
		} 
		public global::Allors.Meta.AssociationType RequestItemsWhereRequirement 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.RequestItemRequirement;
			}
		} 

		public global::Allors.Meta.MethodType Cancel 
		{
			get
			{
				return global::Allors.Meta.MethodTypes.RequirementCancel;
			}
		} 
		public global::Allors.Meta.MethodType Close 
		{
			get
			{
				return global::Allors.Meta.MethodTypes.RequirementClose;
			}
		} 
		public global::Allors.Meta.MethodType Ready 
		{
			get
			{
				return global::Allors.Meta.MethodTypes.RequirementReady;
			}
		} 

	}
}