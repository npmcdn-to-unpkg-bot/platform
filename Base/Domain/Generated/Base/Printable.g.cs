// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	public partial interface Printable :  UserInterfaceable,UniquelyIdentifiable, Allors.IObjectBase
	{


		global::System.String PrintContent 
		{
			get;
			set;
		}

		bool ExistPrintContent{get;}

		void RemovePrintContent();



		global::Allors.Extent<PrintQueue> PrintQueuesWherePrintable
		{ 
			get;
		}

		bool ExistPrintQueuesWherePrintable
		{
			get;
		}



		DerivablePrepareDerivation PrepareDerivation();

		DerivableDerive Derive();

		DerivableApplySecurityOnDerive ApplySecurityOnDerive();
	}

	public class PrintableMeta
	{
		public static readonly PrintableMeta Instance = new PrintableMeta();

		public global::Allors.Meta.Interface ObjectType = global::Allors.Meta.Interfaces.Printable;

		public global::Allors.Meta.RoleType PrintContent 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.PrintablePrintContent;
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

		public global::Allors.Meta.AssociationType PrintQueuesWherePrintable 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.PrintQueuePrintable;
			}
		} 

		public global::Allors.Meta.MethodType PrepareDerivation 
		{
			get
			{
				return global::Allors.Meta.MethodTypes.DerivablePrepareDerivation;
			}
		} 
		public global::Allors.Meta.MethodType Derive 
		{
			get
			{
				return global::Allors.Meta.MethodTypes.DerivableDerive;
			}
		} 
		public global::Allors.Meta.MethodType ApplySecurityOnDerive 
		{
			get
			{
				return global::Allors.Meta.MethodTypes.DerivableApplySecurityOnDerive;
			}
		} 

	}

	public partial interface PrintableBuilder : UserInterfaceableBuilder ,UniquelyIdentifiableBuilder , global::System.IDisposable
	{	
		global::System.String PrintContent {get;}
		
	}

	public partial class Printables : global::Allors.ObjectsBase<Printable>
	{
		public static readonly PrintableMeta Meta = PrintableMeta.Instance;

		public Printables(Allors.ISession session) : base(session)
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