// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	[System.Diagnostics.DebuggerNonUserCode]
	public partial class CustomerReturnObjectState : Allors.ObjectBase , ObjectState
	{
		public static readonly CustomerReturnObjectStateMeta Meta = CustomerReturnObjectStateMeta.Instance;

		public CustomerReturnObjectState(Allors.IStrategy allors) : base(allors) {}

		public static CustomerReturnObjectState Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (CustomerReturnObjectState) allorsSession.Instantiate(allorsObjectId);		
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



		virtual public global::Allors.Extent<CustomerReturn> CustomerReturnsWherePreviousObjectState
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.CustomerReturnsWherePreviousObjectState);
			}
		}

		virtual public bool ExistCustomerReturnsWherePreviousObjectState
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.CustomerReturnsWherePreviousObjectState);
			}
		}


		virtual public global::Allors.Extent<CustomerReturn> CustomerReturnsWhereCurrentObjectState
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.CustomerReturnsWhereCurrentObjectState);
			}
		}

		virtual public bool ExistCustomerReturnsWhereCurrentObjectState
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.CustomerReturnsWhereCurrentObjectState);
			}
		}


		virtual public global::Allors.Extent<CustomerReturnStatus> CustomerReturnStatusesWhereCustomerReturnObjectState
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.CustomerReturnStatusesWhereCustomerReturnObjectState);
			}
		}

		virtual public bool ExistCustomerReturnStatusesWhereCustomerReturnObjectState
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.CustomerReturnStatusesWhereCustomerReturnObjectState);
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

	public class CustomerReturnObjectStateMeta
	{
		public static readonly CustomerReturnObjectStateMeta Instance = new CustomerReturnObjectStateMeta();

		public global::Allors.Meta.Class ObjectType = global::Allors.Meta.Classes.CustomerReturnObjectState;

		public global::Allors.Meta.RoleType DeniedPermission 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.ObjectStateDeniedPermission;
			}
		} 
		public global::Allors.Meta.RoleType Name 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.ObjectStateName;
			}
		} 
		public global::Allors.Meta.RoleType UniqueId 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.UniquelyIdentifiableUniqueId;
			}
		} 

		public global::Allors.Meta.AssociationType CustomerReturnsWherePreviousObjectState 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.CustomerReturnPreviousObjectState;
			}
		} 
		public global::Allors.Meta.AssociationType CustomerReturnsWhereCurrentObjectState 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.CustomerReturnCurrentObjectState;
			}
		} 
		public global::Allors.Meta.AssociationType CustomerReturnStatusesWhereCustomerReturnObjectState 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.CustomerReturnStatusCustomerReturnObjectState;
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