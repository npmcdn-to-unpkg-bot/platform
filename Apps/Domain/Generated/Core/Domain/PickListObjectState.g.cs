// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	[System.Diagnostics.DebuggerNonUserCode]
	public partial class PickListObjectState : Allors.ObjectBase , ObjectState
	{
		public static readonly PickListObjectStateMeta Meta = PickListObjectStateMeta.Instance;

		public PickListObjectState(Allors.IStrategy allors) : base(allors) {}

		public static PickListObjectState Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (PickListObjectState) allorsSession.Instantiate(allorsObjectId);		
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



		virtual public global::Allors.Extent<PickList> PickListsWhereCurrentObjectState
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.PickListsWhereCurrentObjectState);
			}
		}

		virtual public bool ExistPickListsWhereCurrentObjectState
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.PickListsWhereCurrentObjectState);
			}
		}


		virtual public global::Allors.Extent<PickList> PickListsWherePreviousObjectState
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.PickListsWherePreviousObjectState);
			}
		}

		virtual public bool ExistPickListsWherePreviousObjectState
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.PickListsWherePreviousObjectState);
			}
		}


		virtual public global::Allors.Extent<PickListStatus> PickListStatusesWherePickListObjectState
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.PickListStatusesWherePickListObjectState);
			}
		}

		virtual public bool ExistPickListStatusesWherePickListObjectState
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.PickListStatusesWherePickListObjectState);
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

	public class PickListObjectStateMeta
	{
		public static readonly PickListObjectStateMeta Instance = new PickListObjectStateMeta();

		public global::Allors.Meta.Class ObjectType = global::Allors.Meta.Classes.PickListObjectState;

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

		public global::Allors.Meta.AssociationType PickListsWhereCurrentObjectState 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.PickListCurrentObjectState;
			}
		} 
		public global::Allors.Meta.AssociationType PickListsWherePreviousObjectState 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.PickListPreviousObjectState;
			}
		} 
		public global::Allors.Meta.AssociationType PickListStatusesWherePickListObjectState 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.PickListStatusPickListObjectState;
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