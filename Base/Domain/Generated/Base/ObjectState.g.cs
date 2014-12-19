// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	public partial interface ObjectState :  UniquelyIdentifiable, Allors.IObjectBase
	{


		global::Allors.Extent<Permission> DeniedPermissions
		{ 
			get;
			set;
		}

		void AddDeniedPermission (Permission value);

		void RemoveDeniedPermission (Permission value);

		bool ExistDeniedPermissions
		{
			get;
		}

		void RemoveDeniedPermissions();


		global::System.String Name 
		{
			get;
			set;
		}

		bool ExistName{get;}

		void RemoveName();



		global::Allors.Extent<Transition> TransitionsWhereFromState
		{ 
			get;
		}

		bool ExistTransitionsWhereFromState
		{
			get;
		}


		global::Allors.Extent<Transition> TransitionsWhereToState
		{ 
			get;
		}

		bool ExistTransitionsWhereToState
		{
			get;
		}

	}

	public class ObjectStateMeta
	{
		public static readonly ObjectStateMeta Instance = new ObjectStateMeta();

		public global::Allors.Meta.Interface ObjectType = global::Allors.Meta.Interfaces.ObjectState;

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

	public partial interface ObjectStateBuilder : UniquelyIdentifiableBuilder , global::System.IDisposable
	{	

		global::System.Collections.Generic.List<Permission> DeniedPermissions {get;}		

		
		global::System.String Name {get;}
		
	}

	public partial class ObjectStates : global::Allors.ObjectsBase<ObjectState>
	{
		public static readonly ObjectStateMeta Meta = ObjectStateMeta.Instance;

		public ObjectStates(Allors.ISession session) : base(session)
		{
		}

		public override Allors.Meta.Composite ObjectType
		{
			get
			{
				return Meta.ObjectType;
			}
		}
	}

}