// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	public partial interface Enumeration :  UserInterfaceable,UniquelyIdentifiable, Allors.IObject
	{


		global::Allors.Extent<LocalisedText> LocalisedNames
		{ 
			get;
			set;
		}

		void AddLocalisedName (LocalisedText value);

		void RemoveLocalisedName (LocalisedText value);

		bool ExistLocalisedNames
		{
			get;
		}

		void RemoveLocalisedNames();


		global::System.String Name 
		{
			get;
			set;
		}

		bool ExistName{get;}

		void RemoveName();


		global::System.Boolean IsActive 
		{
			get;
			set;
		}

		bool ExistIsActive{get;}

		void RemoveIsActive();

	}

	public class EnumerationMeta
	{
		public static readonly EnumerationMeta Instance = new EnumerationMeta();

		public global::Allors.Meta.Interface ObjectType = global::Allors.Meta.Interfaces.Enumeration;

		public global::Allors.Meta.RoleType LocalisedName 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.EnumerationLocalisedName;
			}
		} 
		public global::Allors.Meta.RoleType Name 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.EnumerationName;
			}
		} 
		public global::Allors.Meta.RoleType IsActive 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.EnumerationIsActive;
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
		public global::Allors.Meta.MethodType OnPostBuild 
		{
			get
			{
				return global::Allors.Meta.MethodTypes.ObjectOnPostBuild;
			}
		} 
		public global::Allors.Meta.MethodType ApplySecurityOnPostBuild 
		{
			get
			{
				return global::Allors.Meta.MethodTypes.ObjectApplySecurityOnPostBuild;
			}
		} 

	}

	public partial interface EnumerationBuilder : UserInterfaceableBuilder ,UniquelyIdentifiableBuilder , global::System.IDisposable
	{	

		global::System.Collections.Generic.List<LocalisedText> LocalisedNames {get;}		

		
		global::System.String Name {get;}
		
		global::System.Boolean? IsActive {get;}
		
	}

	public partial class Enumerations : global::Allors.ObjectsBase<Enumeration>
	{
		public static readonly EnumerationMeta Meta = EnumerationMeta.Instance;

		public Enumerations(Allors.ISession session) : base(session)
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