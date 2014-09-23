// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	[System.Diagnostics.DebuggerNonUserCode]
	public partial class SalesOrderItemObjectState : Allors.ObjectBase , ObjectState
	{
		public static readonly SalesOrderItemObjectStateMeta Meta = SalesOrderItemObjectStateMeta.Instance;

		public SalesOrderItemObjectState(Allors.IStrategy allors) : base(allors) {}

		public static SalesOrderItemObjectState Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (SalesOrderItemObjectState) allorsSession.Instantiate(allorsObjectId);		
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



		virtual public global::Allors.Extent<SalesOrderItem> SalesOrderItemsWhereCurrentObjectState
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.SalesOrderItemsWhereCurrentObjectState);
			}
		}

		virtual public bool ExistSalesOrderItemsWhereCurrentObjectState
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.SalesOrderItemsWhereCurrentObjectState);
			}
		}


		virtual public global::Allors.Extent<SalesOrderItem> SalesOrderItemsWherePreviousObjectState
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.SalesOrderItemsWherePreviousObjectState);
			}
		}

		virtual public bool ExistSalesOrderItemsWherePreviousObjectState
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.SalesOrderItemsWherePreviousObjectState);
			}
		}


		virtual public global::Allors.Extent<SalesOrderItemStatus> SalesOrderItemStatusesWhereSalesOrderItemObjectState
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.SalesOrderItemStatusesWhereSalesOrderItemObjectState);
			}
		}

		virtual public bool ExistSalesOrderItemStatusesWhereSalesOrderItemObjectState
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.SalesOrderItemStatusesWhereSalesOrderItemObjectState);
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

	public class SalesOrderItemObjectStateMeta
	{
		public static readonly SalesOrderItemObjectStateMeta Instance = new SalesOrderItemObjectStateMeta();

		public global::Allors.Meta.Class ObjectType = global::Allors.Meta.Classes.SalesOrderItemObjectState;

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

		public global::Allors.Meta.AssociationType SalesOrderItemsWhereCurrentObjectState 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.SalesOrderItemCurrentObjectState;
			}
		} 
		public global::Allors.Meta.AssociationType SalesOrderItemsWherePreviousObjectState 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.SalesOrderItemPreviousObjectState;
			}
		} 
		public global::Allors.Meta.AssociationType SalesOrderItemStatusesWhereSalesOrderItemObjectState 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.SalesOrderItemStatusSalesOrderItemObjectState;
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