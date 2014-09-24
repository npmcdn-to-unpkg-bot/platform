// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	public partial interface WorkEffort :  Searchable,UserInterfaceable,SearchResult,Transitional,UniquelyIdentifiable, Allors.IObjectBase
	{


		WorkEffortStatus CurrentWorkEffortStatus
		{ 
			get;
			set;
		}

		bool ExistCurrentWorkEffortStatus
		{
			get;
		}

		void RemoveCurrentWorkEffortStatus();


		global::Allors.Extent<WorkEffort> Precendencies
		{ 
			get;
			set;
		}

		void AddPrecendency (WorkEffort value);

		void RemovePrecendency (WorkEffort value);

		bool ExistPrecendencies
		{
			get;
		}

		void RemovePrecendencies();


		Facility Facility
		{ 
			get;
			set;
		}

		bool ExistFacility
		{
			get;
		}

		void RemoveFacility();


		global::Allors.Extent<Deliverable> DeliverablesProduced
		{ 
			get;
			set;
		}

		void AddDeliverableProduced (Deliverable value);

		void RemoveDeliverableProduced (Deliverable value);

		bool ExistDeliverablesProduced
		{
			get;
		}

		void RemoveDeliverablesProduced();


		global::Allors.Extent<WorkEffortInventoryAssignment> InventoryItemsNeeded
		{ 
			get;
			set;
		}

		void AddInventoryItemNeeded (WorkEffortInventoryAssignment value);

		void RemoveInventoryItemNeeded (WorkEffortInventoryAssignment value);

		bool ExistInventoryItemsNeeded
		{
			get;
		}

		void RemoveInventoryItemsNeeded();


		global::Allors.Extent<WorkEffort> Children
		{ 
			get;
			set;
		}

		void AddChild (WorkEffort value);

		void RemoveChild (WorkEffort value);

		bool ExistChildren
		{
			get;
		}

		void RemoveChildren();


		OrderItem OrderItemFulfillment
		{ 
			get;
			set;
		}

		bool ExistOrderItemFulfillment
		{
			get;
		}

		void RemoveOrderItemFulfillment();


		global::Allors.Extent<WorkEffortStatus> WorkEffortStatuses
		{ 
			get;
			set;
		}

		void AddWorkEffortStatus (WorkEffortStatus value);

		void RemoveWorkEffortStatus (WorkEffortStatus value);

		bool ExistWorkEffortStatuses
		{
			get;
		}

		void RemoveWorkEffortStatuses();


		WorkEffortType WorkEffortType
		{ 
			get;
			set;
		}

		bool ExistWorkEffortType
		{
			get;
		}

		void RemoveWorkEffortType();


		global::Allors.Extent<InventoryItem> InventoryItemsProduced
		{ 
			get;
			set;
		}

		void AddInventoryItemProduced (InventoryItem value);

		void RemoveInventoryItemProduced (InventoryItem value);

		bool ExistInventoryItemsProduced
		{
			get;
		}

		void RemoveInventoryItemsProduced();


		global::Allors.Extent<Requirement> RequirementFulfillments
		{ 
			get;
			set;
		}

		void AddRequirementFulfillment (Requirement value);

		void RemoveRequirementFulfillment (Requirement value);

		bool ExistRequirementFulfillments
		{
			get;
		}

		void RemoveRequirementFulfillments();


		global::System.String SpecialTerms 
		{
			get;
			set;
		}

		bool ExistSpecialTerms{get;}

		void RemoveSpecialTerms();


		global::Allors.Extent<WorkEffort> Concurrencies
		{ 
			get;
			set;
		}

		void AddConcurrency (WorkEffort value);

		void RemoveConcurrency (WorkEffort value);

		bool ExistConcurrencies
		{
			get;
		}

		void RemoveConcurrencies();


		global::System.Decimal ActualHours 
		{
			get;
			set;
		}

		bool ExistActualHours{get;}

		void RemoveActualHours();


		global::System.String Description 
		{
			get;
			set;
		}

		bool ExistDescription{get;}

		void RemoveDescription();


		WorkEffortObjectState PreviousObjectState
		{ 
			get;
			set;
		}

		bool ExistPreviousObjectState
		{
			get;
		}

		void RemovePreviousObjectState();


		WorkEffortObjectState CurrentObjectState
		{ 
			get;
			set;
		}

		bool ExistCurrentObjectState
		{
			get;
		}

		void RemoveCurrentObjectState();


		global::System.Decimal EstimatedHours 
		{
			get;
			set;
		}

		bool ExistEstimatedHours{get;}

		void RemoveEstimatedHours();



		global::Allors.Extent<QuoteItem> QuoteItemsWhereWorkEffort
		{ 
			get;
		}

		bool ExistQuoteItemsWhereWorkEffort
		{
			get;
		}


		global::Allors.Extent<WorkEffortPartyAssignment> WorkEffortPartyAssignmentsWhereAssignment
		{ 
			get;
		}

		bool ExistWorkEffortPartyAssignmentsWhereAssignment
		{
			get;
		}


		global::Allors.Extent<WorkEffortBilling> WorkEffortBillingsWhereWorkEffort
		{ 
			get;
		}

		bool ExistWorkEffortBillingsWhereWorkEffort
		{
			get;
		}


		global::Allors.Extent<WorkEffortAssignment> WorkEffortAssignmentsWhereAssignment
		{ 
			get;
		}

		bool ExistWorkEffortAssignmentsWhereAssignment
		{
			get;
		}


		global::Allors.Extent<WorkEffortFixedAssetAssignment> WorkEffortFixedAssetAssignmentsWhereAssignment
		{ 
			get;
		}

		bool ExistWorkEffortFixedAssetAssignmentsWhereAssignment
		{
			get;
		}


		global::Allors.Extent<ServiceEntry> ServiceEntriesWhereWorkEffort
		{ 
			get;
		}

		bool ExistServiceEntriesWhereWorkEffort
		{
			get;
		}


		global::Allors.Extent<WorkEffort> WorkEffortsWherePrecendency
		{ 
			get;
		}

		bool ExistWorkEffortsWherePrecendency
		{
			get;
		}


		global::Allors.Extent<WorkEffort> WorkEffortsWhereChild
		{ 
			get;
		}

		bool ExistWorkEffortsWhereChild
		{
			get;
		}


		global::Allors.Extent<WorkEffort> WorkEffortsWhereConcurrency
		{ 
			get;
		}

		bool ExistWorkEffortsWhereConcurrency
		{
			get;
		}


		global::Allors.Extent<EngagementItem> EngagementItemsWhereEngagementWorkFulfillment
		{ 
			get;
		}

		bool ExistEngagementItemsWhereEngagementWorkFulfillment
		{
			get;
		}


		global::Allors.Extent<CommunicationEvent> CommunicationEventsWhereWorkEffort
		{ 
			get;
		}

		bool ExistCommunicationEventsWhereWorkEffort
		{
			get;
		}


		global::Allors.Extent<WorkEffortInventoryAssignment> WorkEffortInventoryAssignmentsWhereAssignment
		{ 
			get;
		}

		bool ExistWorkEffortInventoryAssignmentsWhereAssignment
		{
			get;
		}

	}

	public class WorkEffortMeta
	{
		public static readonly WorkEffortMeta Instance = new WorkEffortMeta();

		public global::Allors.Meta.Interface ObjectType = global::Allors.Meta.Interfaces.WorkEffort;

		public global::Allors.Meta.RoleType CurrentWorkEffortStatus 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.WorkEffortCurrentWorkEffortStatus;
			}
		} 
		public global::Allors.Meta.RoleType Precendency 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.WorkEffortPrecendency;
			}
		} 
		public global::Allors.Meta.RoleType Facility 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.WorkEffortFacility;
			}
		} 
		public global::Allors.Meta.RoleType DeliverableProduced 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.WorkEffortDeliverableProduced;
			}
		} 
		public global::Allors.Meta.RoleType InventoryItemNeeded 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.WorkEffortInventoryItemNeeded;
			}
		} 
		public global::Allors.Meta.RoleType Child 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.WorkEffortChild;
			}
		} 
		public global::Allors.Meta.RoleType OrderItemFulfillment 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.WorkEffortOrderItemFulfillment;
			}
		} 
		public global::Allors.Meta.RoleType WorkEffortStatus 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.WorkEffortWorkEffortStatus;
			}
		} 
		public global::Allors.Meta.RoleType WorkEffortType 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.WorkEffortWorkEffortType;
			}
		} 
		public global::Allors.Meta.RoleType InventoryItemProduced 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.WorkEffortInventoryItemProduced;
			}
		} 
		public global::Allors.Meta.RoleType RequirementFulfillment 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.WorkEffortRequirementFulfillment;
			}
		} 
		public global::Allors.Meta.RoleType SpecialTerms 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.WorkEffortSpecialTerms;
			}
		} 
		public global::Allors.Meta.RoleType Concurrency 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.WorkEffortConcurrency;
			}
		} 
		public global::Allors.Meta.RoleType ActualHours 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.WorkEffortActualHours;
			}
		} 
		public global::Allors.Meta.RoleType Description 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.WorkEffortDescription;
			}
		} 
		public global::Allors.Meta.RoleType PreviousObjectState 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.WorkEffortPreviousObjectState;
			}
		} 
		public global::Allors.Meta.RoleType CurrentObjectState 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.WorkEffortCurrentObjectState;
			}
		} 
		public global::Allors.Meta.RoleType EstimatedHours 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.WorkEffortEstimatedHours;
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

		public global::Allors.Meta.AssociationType QuoteItemsWhereWorkEffort 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.QuoteItemWorkEffort;
			}
		} 
		public global::Allors.Meta.AssociationType WorkEffortPartyAssignmentsWhereAssignment 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.WorkEffortPartyAssignmentAssignment;
			}
		} 
		public global::Allors.Meta.AssociationType WorkEffortBillingsWhereWorkEffort 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.WorkEffortBillingWorkEffort;
			}
		} 
		public global::Allors.Meta.AssociationType WorkEffortAssignmentsWhereAssignment 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.WorkEffortAssignmentAssignment;
			}
		} 
		public global::Allors.Meta.AssociationType WorkEffortFixedAssetAssignmentsWhereAssignment 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.WorkEffortFixedAssetAssignmentAssignment;
			}
		} 
		public global::Allors.Meta.AssociationType ServiceEntriesWhereWorkEffort 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.ServiceEntryWorkEffort;
			}
		} 
		public global::Allors.Meta.AssociationType WorkEffortsWherePrecendency 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.WorkEffortPrecendency;
			}
		} 
		public global::Allors.Meta.AssociationType WorkEffortsWhereChild 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.WorkEffortChild;
			}
		} 
		public global::Allors.Meta.AssociationType WorkEffortsWhereConcurrency 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.WorkEffortConcurrency;
			}
		} 
		public global::Allors.Meta.AssociationType EngagementItemsWhereEngagementWorkFulfillment 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.EngagementItemEngagementWorkFulfillment;
			}
		} 
		public global::Allors.Meta.AssociationType CommunicationEventsWhereWorkEffort 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.CommunicationEventWorkEffort;
			}
		} 
		public global::Allors.Meta.AssociationType WorkEffortInventoryAssignmentsWhereAssignment 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.WorkEffortInventoryAssignmentAssignment;
			}
		} 

		public global::Allors.Meta.MethodType Confirm 
		{
			get
			{
				return global::Allors.Meta.MethodTypes.WorkEffortConfirm;
			}
		} 
		public global::Allors.Meta.MethodType WorkDone 
		{
			get
			{
				return global::Allors.Meta.MethodTypes.WorkEffortWorkDone;
			}
		} 
		public global::Allors.Meta.MethodType Finish 
		{
			get
			{
				return global::Allors.Meta.MethodTypes.WorkEffortFinish;
			}
		} 
		public global::Allors.Meta.MethodType Cancel 
		{
			get
			{
				return global::Allors.Meta.MethodTypes.WorkEffortCancel;
			}
		} 
		public global::Allors.Meta.MethodType Reopen 
		{
			get
			{
				return global::Allors.Meta.MethodTypes.WorkEffortReopen;
			}
		} 

	}
}