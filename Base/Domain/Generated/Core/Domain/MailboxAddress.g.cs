// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	[System.Diagnostics.DebuggerNonUserCode]
	public partial class MailboxAddress : Allors.ObjectBase , Searchable, Address
	{
		public static readonly MailboxAddressMeta Meta = MailboxAddressMeta.Instance;

		public MailboxAddress(Allors.IStrategy allors) : base(allors) {}

		public static MailboxAddress Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (MailboxAddress) allorsSession.Instantiate(allorsObjectId);		
		}




		virtual public global::System.String PoBox 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(Meta.PoBox);
			}
			set
			{
				Strategy.SetUnitRole(Meta.PoBox, value);
			}
		}

		virtual public bool ExistPoBox{
			get
			{
				return Strategy.ExistUnitRole(Meta.PoBox);
			}
		}

		virtual public void RemovePoBox()
		{
			Strategy.RemoveUnitRole(Meta.PoBox);
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


		virtual public Place Place
		{ 
			get
			{
				return (Place) Strategy.GetCompositeRole(Meta.Place);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.Place ,value);
			}
		}

		virtual public bool ExistPlace
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.Place);
			}
		}

		virtual public void RemovePlace()
		{
			Strategy.RemoveCompositeRole(Meta.Place);
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



		virtual public global::Allors.Extent<Person> PersonsWhereMailboxAddress
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.PersonsWhereMailboxAddress);
			}
		}

		virtual public bool ExistPersonsWhereMailboxAddress
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.PersonsWhereMailboxAddress);
			}
		}


		virtual public Organisation OrganisationWhereAddress
		{ 
			get
			{
				return (Organisation) Strategy.GetCompositeAssociation(Meta.OrganisationWhereAddress);
			}
		} 

		virtual public bool ExistOrganisationWhereAddress
		{
			get
			{
				return Strategy.ExistCompositeAssociation(Meta.OrganisationWhereAddress);
			}
		}


		virtual public global::Allors.Extent<Person> PersonsWhereMainAddress
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.PersonsWhereMainAddress);
			}
		}

		virtual public bool ExistPersonsWhereMainAddress
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.PersonsWhereMainAddress);
			}
		}


		virtual public global::Allors.Extent<Person> PersonsWhereAddress
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.PersonsWhereAddress);
			}
		}

		virtual public bool ExistPersonsWhereAddress
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.PersonsWhereAddress);
			}
		}

	}

	public class MailboxAddressMeta
	{
		public static readonly MailboxAddressMeta Instance = new MailboxAddressMeta();

		public global::Allors.Meta.Class ObjectType = global::Allors.Meta.Classes.MailboxAddress;

		public global::Allors.Meta.RoleType PoBox 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.MailboxAddressPoBox;
			}
		} 
		public global::Allors.Meta.RoleType SearchData 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.SearchableSearchData;
			}
		} 
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

		public global::Allors.Meta.AssociationType PersonsWhereMailboxAddress 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.PersonMailboxAddress;
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