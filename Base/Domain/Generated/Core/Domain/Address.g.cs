// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	public partial interface Address :  UserInterfaceable, Allors.IObject
	{


		Place Place
		{ 
			get;
			set;
		}

		bool ExistPlace
		{
			get;
		}

		void RemovePlace();



		Organisation OrganisationWhereAddress
		{
			get;
		}

		bool ExistOrganisationWhereAddress
		{
			get;
		}


		global::Allors.Extent<Person> PersonsWhereMainAddress
		{ 
			get;
		}

		bool ExistPersonsWhereMainAddress
		{
			get;
		}


		global::Allors.Extent<Person> PersonsWhereAddress
		{ 
			get;
		}

		bool ExistPersonsWhereAddress
		{
			get;
		}

	}

	public class AddressMeta
	{
		public static readonly AddressMeta Instance = new AddressMeta();

		public global::Allors.Meta.Interface ObjectType = global::Allors.Meta.Interfaces.Address;

		public global::Allors.Meta.RoleType Place 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.AddressPlace;
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

		public global::Allors.Meta.AssociationType OrganisationWhereAddress 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.OrganisationAddress;
			}
		} 
		public global::Allors.Meta.AssociationType PersonsWhereMainAddress 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.PersonMainAddress;
			}
		} 
		public global::Allors.Meta.AssociationType PersonsWhereAddress 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.PersonAddress;
			}
		} 

	}
}