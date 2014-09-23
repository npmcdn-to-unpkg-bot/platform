// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	[System.Diagnostics.DebuggerNonUserCode]
	public partial class PickList : Allors.ObjectBase , UserInterfaceable, SearchResult, Printable, Transitional, Searchable, UniquelyIdentifiable
	{
		public static readonly PickListMeta Meta = PickListMeta.Instance;

		public PickList(Allors.IStrategy allors) : base(allors) {}

		public static PickList Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (PickList) allorsSession.Instantiate(allorsObjectId);		
		}



		virtual public CustomerShipment CustomerShipmentCorrection
		{ 
			get
			{
				return (CustomerShipment) Strategy.GetCompositeRole(Meta.CustomerShipmentCorrection);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.CustomerShipmentCorrection ,value);
			}
		}

		virtual public bool ExistCustomerShipmentCorrection
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.CustomerShipmentCorrection);
			}
		}

		virtual public void RemoveCustomerShipmentCorrection()
		{
			Strategy.RemoveCompositeRole(Meta.CustomerShipmentCorrection);
		}



		virtual public global::System.DateTime? CreationDate 
		{
			get
			{
				return (global::System.DateTime?) Strategy.GetUnitRole(Meta.CreationDate);
			}
			set
			{
				Strategy.SetUnitRole(Meta.CreationDate, value);
			}
		}

		virtual public bool ExistCreationDate{
			get
			{
				return Strategy.ExistUnitRole(Meta.CreationDate);
			}
		}

		virtual public void RemoveCreationDate()
		{
			Strategy.RemoveUnitRole(Meta.CreationDate);
		}


		virtual public global::Allors.Extent<PickListItem> PickListItems
		{ 
			get
			{
				return Strategy.GetCompositeRoles(Meta.PickListItem);
			}
			set
			{
				Strategy.SetCompositeRoles(Meta.PickListItem, value);
			}
		}

		virtual public void AddPickListItem (PickListItem value)
		{
			Strategy.AddCompositeRole(Meta.PickListItem, value);
		}

		virtual public void RemovePickListItem (PickListItem value)
		{
			Strategy.RemoveCompositeRole(Meta.PickListItem,value);
		}

		virtual public bool ExistPickListItems
		{
			get
			{
				return Strategy.ExistCompositeRoles(Meta.PickListItem);
			}
		}

		virtual public void RemovePickListItems()
		{
			Strategy.RemoveCompositeRoles(Meta.PickListItem);
		}


		virtual public PickListObjectState CurrentObjectState
		{ 
			get
			{
				return (PickListObjectState) Strategy.GetCompositeRole(Meta.CurrentObjectState);
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


		virtual public PickListStatus CurrentPickListStatus
		{ 
			get
			{
				return (PickListStatus) Strategy.GetCompositeRole(Meta.CurrentPickListStatus);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.CurrentPickListStatus ,value);
			}
		}

		virtual public bool ExistCurrentPickListStatus
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.CurrentPickListStatus);
			}
		}

		virtual public void RemoveCurrentPickListStatus()
		{
			Strategy.RemoveCompositeRole(Meta.CurrentPickListStatus);
		}


		virtual public Person Picker
		{ 
			get
			{
				return (Person) Strategy.GetCompositeRole(Meta.Picker);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.Picker ,value);
			}
		}

		virtual public bool ExistPicker
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.Picker);
			}
		}

		virtual public void RemovePicker()
		{
			Strategy.RemoveCompositeRole(Meta.Picker);
		}


		virtual public global::Allors.Extent<PickListStatus> PickListStatuses
		{ 
			get
			{
				return Strategy.GetCompositeRoles(Meta.PickListStatus);
			}
			set
			{
				Strategy.SetCompositeRoles(Meta.PickListStatus, value);
			}
		}

		virtual public void AddPickListStatus (PickListStatus value)
		{
			Strategy.AddCompositeRole(Meta.PickListStatus, value);
		}

		virtual public void RemovePickListStatus (PickListStatus value)
		{
			Strategy.RemoveCompositeRole(Meta.PickListStatus,value);
		}

		virtual public bool ExistPickListStatuses
		{
			get
			{
				return Strategy.ExistCompositeRoles(Meta.PickListStatus);
			}
		}

		virtual public void RemovePickListStatuses()
		{
			Strategy.RemoveCompositeRoles(Meta.PickListStatus);
		}


		virtual public PickListObjectState PreviousObjectState
		{ 
			get
			{
				return (PickListObjectState) Strategy.GetCompositeRole(Meta.PreviousObjectState);
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


		virtual public Party ShipToParty
		{ 
			get
			{
				return (Party) Strategy.GetCompositeRole(Meta.ShipToParty);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.ShipToParty ,value);
			}
		}

		virtual public bool ExistShipToParty
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.ShipToParty);
			}
		}

		virtual public void RemoveShipToParty()
		{
			Strategy.RemoveCompositeRole(Meta.ShipToParty);
		}


		virtual public Store Store
		{ 
			get
			{
				return (Store) Strategy.GetCompositeRole(Meta.Store);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.Store ,value);
			}
		}

		virtual public bool ExistStore
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.Store);
			}
		}

		virtual public void RemoveStore()
		{
			Strategy.RemoveCompositeRole(Meta.Store);
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



		virtual public global::System.String PrintContent 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(Meta.PrintContent);
			}
			set
			{
				Strategy.SetUnitRole(Meta.PrintContent, value);
			}
		}

		virtual public bool ExistPrintContent{
			get
			{
				return Strategy.ExistUnitRole(Meta.PrintContent);
			}
		}

		virtual public void RemovePrintContent()
		{
			Strategy.RemoveUnitRole(Meta.PrintContent);
		}



		virtual public global::System.Guid? UniqueId 
		{
			get
			{
				return (global::System.Guid?) Strategy.GetUnitRole(Meta.UniqueId);
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



		virtual public global::Allors.Extent<PrintQueue> PrintQueuesWherePrintable
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.PrintQueuesWherePrintable);
			}
		}

		virtual public bool ExistPrintQueuesWherePrintable
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.PrintQueuesWherePrintable);
			}
		}

	}

	public class PickListMeta
	{
		public static readonly PickListMeta Instance = new PickListMeta();

		public global::Allors.Meta.Class ObjectType = global::Allors.Meta.Classes.PickList;

		public global::Allors.Meta.RoleType CustomerShipmentCorrection 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.PickListCustomerShipmentCorrection;
			}
		} 
		public global::Allors.Meta.RoleType CreationDate 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.PickListCreationDate;
			}
		} 
		public global::Allors.Meta.RoleType PickListItem 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.PickListPickListItem;
			}
		} 
		public global::Allors.Meta.RoleType CurrentObjectState 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.PickListCurrentObjectState;
			}
		} 
		public global::Allors.Meta.RoleType CurrentPickListStatus 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.PickListCurrentPickListStatus;
			}
		} 
		public global::Allors.Meta.RoleType Picker 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.PickListPicker;
			}
		} 
		public global::Allors.Meta.RoleType PickListStatus 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.PickListPickListStatus;
			}
		} 
		public global::Allors.Meta.RoleType PreviousObjectState 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.PickListPreviousObjectState;
			}
		} 
		public global::Allors.Meta.RoleType ShipToParty 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.PickListShipToParty;
			}
		} 
		public global::Allors.Meta.RoleType Store 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.PickListStore;
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
		public global::Allors.Meta.RoleType PrintContent 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.PrintablePrintContent;
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

		public global::Allors.Meta.AssociationType PrintQueuesWherePrintable 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.PrintQueuePrintable;
			}
		} 

	}
}