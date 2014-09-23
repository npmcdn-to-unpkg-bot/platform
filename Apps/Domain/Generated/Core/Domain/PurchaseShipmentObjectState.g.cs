// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	[System.Diagnostics.DebuggerNonUserCode]
	public partial class PurchaseShipmentObjectState : Allors.ObjectBase , ObjectState
	{
		public static readonly PurchaseShipmentObjectStateMeta Meta = PurchaseShipmentObjectStateMeta.Instance;

		public PurchaseShipmentObjectState(Allors.IStrategy allors) : base(allors) {}

		public static PurchaseShipmentObjectState Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (PurchaseShipmentObjectState) allorsSession.Instantiate(allorsObjectId);		
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



		virtual public global::Allors.Extent<PurchaseShipment> PurchaseShipmentsWhereCurrentObjectState
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.PurchaseShipmentsWhereCurrentObjectState);
			}
		}

		virtual public bool ExistPurchaseShipmentsWhereCurrentObjectState
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.PurchaseShipmentsWhereCurrentObjectState);
			}
		}


		virtual public global::Allors.Extent<PurchaseShipment> PurchaseShipmentsWherePreviousObjectState
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.PurchaseShipmentsWherePreviousObjectState);
			}
		}

		virtual public bool ExistPurchaseShipmentsWherePreviousObjectState
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.PurchaseShipmentsWherePreviousObjectState);
			}
		}


		virtual public global::Allors.Extent<PurchaseShipmentStatus> PurchaseShipmentStatusesWherePurchaseShipmentObjectState
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.PurchaseShipmentStatusesWherePurchaseShipmentObjectState);
			}
		}

		virtual public bool ExistPurchaseShipmentStatusesWherePurchaseShipmentObjectState
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.PurchaseShipmentStatusesWherePurchaseShipmentObjectState);
			}
		}


		virtual public global::Allors.Extent<Transition> TransitionsWhereFromState
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.TransitionsWhereFromState);
			}
		}

		virtual public bool ExistTransitionsWhereFromState
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.TransitionsWhereFromState);
			}
		}


		virtual public global::Allors.Extent<Transition> TransitionsWhereToState
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.TransitionsWhereToState);
			}
		}

		virtual public bool ExistTransitionsWhereToState
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.TransitionsWhereToState);
			}
		}

	}

	public class PurchaseShipmentObjectStateMeta
	{
		public static readonly PurchaseShipmentObjectStateMeta Instance = new PurchaseShipmentObjectStateMeta();

		public global::Allors.Meta.Class ObjectType = global::Allors.Meta.Classes.PurchaseShipmentObjectState;

		public global::Allors.Meta.RoleType DeniedPermission 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.ObjectStateDeniedPermission;
			}
		} 
		public global::Allors.Meta.RoleType UniqueId 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.UniquelyIdentifiableUniqueId;
			}
		} 

		public global::Allors.Meta.AssociationType PurchaseShipmentsWhereCurrentObjectState 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.PurchaseShipmentCurrentObjectState;
			}
		} 
		public global::Allors.Meta.AssociationType PurchaseShipmentsWherePreviousObjectState 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.PurchaseShipmentPreviousObjectState;
			}
		} 
		public global::Allors.Meta.AssociationType PurchaseShipmentStatusesWherePurchaseShipmentObjectState 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.PurchaseShipmentStatusPurchaseShipmentObjectState;
			}
		} 
		public global::Allors.Meta.AssociationType TransitionsWhereFromState 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.TransitionFromState;
			}
		} 
		public global::Allors.Meta.AssociationType TransitionsWhereToState 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.TransitionToState;
			}
		} 

	}
}