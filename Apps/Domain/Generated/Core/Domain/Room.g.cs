// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	[System.Diagnostics.DebuggerNonUserCode]
	public partial class Room : Allors.ObjectBase , Facility, Container
	{
		public static readonly RoomMeta Meta = RoomMeta.Instance;

		public Room(Allors.IStrategy allors) : base(allors) {}

		public static Room Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (Room) allorsSession.Instantiate(allorsObjectId);		
		}



		virtual public Facility MadeUpOf
		{ 
			get
			{
				return (Facility) Strategy.GetCompositeRole(Meta.MadeUpOf);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.MadeUpOf ,value);
			}
		}

		virtual public bool ExistMadeUpOf
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.MadeUpOf);
			}
		}

		virtual public void RemoveMadeUpOf()
		{
			Strategy.RemoveCompositeRole(Meta.MadeUpOf);
		}



		virtual public global::System.Decimal? SquareFootage 
		{
			get
			{
				return (global::System.Decimal?) Strategy.GetUnitRole(Meta.SquareFootage);
			}
			set
			{
				Strategy.SetUnitRole(Meta.SquareFootage, value);
			}
		}

		virtual public bool ExistSquareFootage{
			get
			{
				return Strategy.ExistUnitRole(Meta.SquareFootage);
			}
		}

		virtual public void RemoveSquareFootage()
		{
			Strategy.RemoveUnitRole(Meta.SquareFootage);
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


		virtual public global::Allors.Extent<ContactMechanism> FacilityContactMechanisms
		{ 
			get
			{
				return Strategy.GetCompositeRoles(Meta.FacilityContactMechanism);
			}
			set
			{
				Strategy.SetCompositeRoles(Meta.FacilityContactMechanism, value);
			}
		}

		virtual public void AddFacilityContactMechanism (ContactMechanism value)
		{
			Strategy.AddCompositeRole(Meta.FacilityContactMechanism, value);
		}

		virtual public void RemoveFacilityContactMechanism (ContactMechanism value)
		{
			Strategy.RemoveCompositeRole(Meta.FacilityContactMechanism,value);
		}

		virtual public bool ExistFacilityContactMechanisms
		{
			get
			{
				return Strategy.ExistCompositeRoles(Meta.FacilityContactMechanism);
			}
		}

		virtual public void RemoveFacilityContactMechanisms()
		{
			Strategy.RemoveCompositeRoles(Meta.FacilityContactMechanism);
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


		virtual public InternalOrganisation Owner
		{ 
			get
			{
				return (InternalOrganisation) Strategy.GetCompositeRole(Meta.Owner);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.Owner ,value);
			}
		}

		virtual public bool ExistOwner
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.Owner);
			}
		}

		virtual public void RemoveOwner()
		{
			Strategy.RemoveCompositeRole(Meta.Owner);
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



		virtual public global::System.Decimal? Latitude 
		{
			get
			{
				return (global::System.Decimal?) Strategy.GetUnitRole(Meta.Latitude);
			}
			set
			{
				Strategy.SetUnitRole(Meta.Latitude, value);
			}
		}

		virtual public bool ExistLatitude{
			get
			{
				return Strategy.ExistUnitRole(Meta.Latitude);
			}
		}

		virtual public void RemoveLatitude()
		{
			Strategy.RemoveUnitRole(Meta.Latitude);
		}



		virtual public global::System.Decimal? Longitude 
		{
			get
			{
				return (global::System.Decimal?) Strategy.GetUnitRole(Meta.Longitude);
			}
			set
			{
				Strategy.SetUnitRole(Meta.Longitude, value);
			}
		}

		virtual public bool ExistLongitude{
			get
			{
				return Strategy.ExistUnitRole(Meta.Longitude);
			}
		}

		virtual public void RemoveLongitude()
		{
			Strategy.RemoveUnitRole(Meta.Longitude);
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



		virtual public global::System.String ContainerDescription 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(Meta.ContainerDescription);
			}
			set
			{
				Strategy.SetUnitRole(Meta.ContainerDescription, value);
			}
		}

		virtual public bool ExistContainerDescription{
			get
			{
				return Strategy.ExistUnitRole(Meta.ContainerDescription);
			}
		}

		virtual public void RemoveContainerDescription()
		{
			Strategy.RemoveUnitRole(Meta.ContainerDescription);
		}



		virtual public global::Allors.Extent<PurchaseOrder> PurchaseOrdersWhereFacility
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.PurchaseOrdersWhereFacility);
			}
		}

		virtual public bool ExistPurchaseOrdersWhereFacility
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.PurchaseOrdersWhereFacility);
			}
		}


		virtual public global::Allors.Extent<WorkEffortPartyAssignment> WorkEffortPartyAssignmentsWhereFacility
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.WorkEffortPartyAssignmentsWhereFacility);
			}
		}

		virtual public bool ExistWorkEffortPartyAssignmentsWhereFacility
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.WorkEffortPartyAssignmentsWhereFacility);
			}
		}


		virtual public global::Allors.Extent<PurchaseShipment> PurchaseShipmentsWhereFacility
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.PurchaseShipmentsWhereFacility);
			}
		}

		virtual public bool ExistPurchaseShipmentsWhereFacility
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.PurchaseShipmentsWhereFacility);
			}
		}


		virtual public global::Allors.Extent<WorkEffort> WorkEffortsWhereFacility
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.WorkEffortsWhereFacility);
			}
		}

		virtual public bool ExistWorkEffortsWhereFacility
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.WorkEffortsWhereFacility);
			}
		}


		virtual public global::Allors.Extent<InventoryItem> InventoryItemsWhereFacility
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.InventoryItemsWhereFacility);
			}
		}

		virtual public bool ExistInventoryItemsWhereFacility
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.InventoryItemsWhereFacility);
			}
		}


		virtual public global::Allors.Extent<ShipmentRouteSegment> ShipmentRouteSegmentsWhereFromFacility
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.ShipmentRouteSegmentsWhereFromFacility);
			}
		}

		virtual public bool ExistShipmentRouteSegmentsWhereFromFacility
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.ShipmentRouteSegmentsWhereFromFacility);
			}
		}


		virtual public global::Allors.Extent<ShipmentRouteSegment> ShipmentRouteSegmentsWhereToFacility
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.ShipmentRouteSegmentsWhereToFacility);
			}
		}

		virtual public bool ExistShipmentRouteSegmentsWhereToFacility
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.ShipmentRouteSegmentsWhereToFacility);
			}
		}


		virtual public global::Allors.Extent<Container> ContainersWhereFacility
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.ContainersWhereFacility);
			}
		}

		virtual public bool ExistContainersWhereFacility
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.ContainersWhereFacility);
			}
		}


		virtual public global::Allors.Extent<InternalOrganisation> InternalOrganisationsWhereDefaultFacility
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.InternalOrganisationsWhereDefaultFacility);
			}
		}

		virtual public bool ExistInternalOrganisationsWhereDefaultFacility
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.InternalOrganisationsWhereDefaultFacility);
			}
		}


		virtual public global::Allors.Extent<Facility> FacilitiesWhereMadeUpOf
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.FacilitiesWhereMadeUpOf);
			}
		}

		virtual public bool ExistFacilitiesWhereMadeUpOf
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.FacilitiesWhereMadeUpOf);
			}
		}


		virtual public global::Allors.Extent<Requirement> RequirementsWhereFacility
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.RequirementsWhereFacility);
			}
		}

		virtual public bool ExistRequirementsWhereFacility
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.RequirementsWhereFacility);
			}
		}


		virtual public global::Allors.Extent<Store> StoresWhereDefaultFacility
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.StoresWhereDefaultFacility);
			}
		}

		virtual public bool ExistStoresWhereDefaultFacility
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.StoresWhereDefaultFacility);
			}
		}


		virtual public global::Allors.Extent<InventoryItem> InventoryItemsWhereContainer
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.InventoryItemsWhereContainer);
			}
		}

		virtual public bool ExistInventoryItemsWhereContainer
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.InventoryItemsWhereContainer);
			}
		}

	}

	public class RoomMeta
	{
		public static readonly RoomMeta Instance = new RoomMeta();

		public global::Allors.Meta.Class ObjectType = global::Allors.Meta.Classes.Room;

		public global::Allors.Meta.RoleType MadeUpOf 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.FacilityMadeUpOf;
			}
		} 
		public global::Allors.Meta.RoleType SquareFootage 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.FacilitySquareFootage;
			}
		} 
		public global::Allors.Meta.RoleType Description 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.FacilityDescription;
			}
		} 
		public global::Allors.Meta.RoleType FacilityContactMechanism 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.FacilityFacilityContactMechanism;
			}
		} 
		public global::Allors.Meta.RoleType Name 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.FacilityName;
			}
		} 
		public global::Allors.Meta.RoleType Owner 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.FacilityOwner;
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
		public global::Allors.Meta.RoleType Latitude 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.GeoLocatableLatitude;
			}
		} 
		public global::Allors.Meta.RoleType Longitude 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.GeoLocatableLongitude;
			}
		} 
		public global::Allors.Meta.RoleType SearchData 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.SearchableSearchData;
			}
		} 
		public global::Allors.Meta.RoleType UniqueId 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.UniquelyIdentifiableUniqueId;
			}
		} 
		public global::Allors.Meta.RoleType Facility 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.ContainerFacility;
			}
		} 
		public global::Allors.Meta.RoleType ContainerDescription 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.ContainerContainerDescription;
			}
		} 

		public global::Allors.Meta.AssociationType PurchaseOrdersWhereFacility 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.PurchaseOrderFacility;
			}
		} 
		public global::Allors.Meta.AssociationType WorkEffortPartyAssignmentsWhereFacility 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.WorkEffortPartyAssignmentFacility;
			}
		} 
		public global::Allors.Meta.AssociationType PurchaseShipmentsWhereFacility 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.PurchaseShipmentFacility;
			}
		} 
		public global::Allors.Meta.AssociationType WorkEffortsWhereFacility 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.WorkEffortFacility;
			}
		} 
		public global::Allors.Meta.AssociationType InventoryItemsWhereFacility 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.InventoryItemFacility;
			}
		} 
		public global::Allors.Meta.AssociationType ShipmentRouteSegmentsWhereFromFacility 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.ShipmentRouteSegmentFromFacility;
			}
		} 
		public global::Allors.Meta.AssociationType ShipmentRouteSegmentsWhereToFacility 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.ShipmentRouteSegmentToFacility;
			}
		} 
		public global::Allors.Meta.AssociationType ContainersWhereFacility 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.ContainerFacility;
			}
		} 
		public global::Allors.Meta.AssociationType InternalOrganisationsWhereDefaultFacility 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.InternalOrganisationDefaultFacility;
			}
		} 
		public global::Allors.Meta.AssociationType FacilitiesWhereMadeUpOf 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.FacilityMadeUpOf;
			}
		} 
		public global::Allors.Meta.AssociationType RequirementsWhereFacility 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.RequirementFacility;
			}
		} 
		public global::Allors.Meta.AssociationType StoresWhereDefaultFacility 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.StoreDefaultFacility;
			}
		} 
		public global::Allors.Meta.AssociationType InventoryItemsWhereContainer 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.InventoryItemContainer;
			}
		} 

	}
}