// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Domain
{
	public partial class MailboxAddress : MailboxAddressAllors , global::Domain.Searchable, global::Domain.Address
	{
		public MailboxAddress(Allors.IStrategy allors) : base(allors) {}

		public static MailboxAddress Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (MailboxAddress) allorsSession.Instantiate(allorsObjectId);		
		}
	}

	[System.Diagnostics.DebuggerNonUserCode]
	public abstract class MailboxAddressAllors : Allors.ObjectBase
	{
		protected MailboxAddressAllors(Allors.IStrategy allors) : base(allors){}



		virtual public global::System.String PoBox 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(MailboxAddressMeta.PoBox);
			}
			set
			{
				Strategy.SetUnitRole(MailboxAddressMeta.PoBox, value);
			}
		}

		virtual public bool ExistPoBox{
			get
			{
				return Strategy.ExistUnitRole(MailboxAddressMeta.PoBox);
			}
		}

		virtual public void RemovePoBox()
		{
			Strategy.RemoveUnitRole(MailboxAddressMeta.PoBox);
		}


		virtual public global::Domain.SearchData SearchData
		{ 
			get
			{
				return (global::Domain.SearchData) Strategy.GetCompositeRole(MailboxAddressMeta.SearchData);
			}
			set
			{
				Strategy.SetCompositeRole(MailboxAddressMeta.SearchData ,value);
			}
		}

		virtual public bool ExistSearchData
		{
			get
			{
				return Strategy.ExistCompositeRole(MailboxAddressMeta.SearchData);
			}
		}

		virtual public void RemoveSearchData()
		{
			Strategy.RemoveCompositeRole(MailboxAddressMeta.SearchData);
		}


		virtual public global::Domain.Place Place
		{ 
			get
			{
				return (global::Domain.Place) Strategy.GetCompositeRole(MailboxAddressMeta.Place);
			}
			set
			{
				Strategy.SetCompositeRole(MailboxAddressMeta.Place ,value);
			}
		}

		virtual public bool ExistPlace
		{
			get
			{
				return Strategy.ExistCompositeRole(MailboxAddressMeta.Place);
			}
		}

		virtual public void RemovePlace()
		{
			Strategy.RemoveCompositeRole(MailboxAddressMeta.Place);
		}



		virtual public global::System.String DisplayName 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(MailboxAddressMeta.DisplayName);
			}
			set
			{
				Strategy.SetUnitRole(MailboxAddressMeta.DisplayName, value);
			}
		}

		virtual public bool ExistDisplayName{
			get
			{
				return Strategy.ExistUnitRole(MailboxAddressMeta.DisplayName);
			}
		}

		virtual public void RemoveDisplayName()
		{
			Strategy.RemoveUnitRole(MailboxAddressMeta.DisplayName);
		}


		virtual public Allors.Extent<global::Domain.Permission> DeniedPermissions
		{ 
			get
			{
				return Strategy.GetCompositeRoles(MailboxAddressMeta.DeniedPermission);
			}
			set
			{
				Strategy.SetCompositeRoles(MailboxAddressMeta.DeniedPermission, value);
			}
		}

		virtual public void AddDeniedPermission (global::Domain.Permission value)
		{
			Strategy.AddCompositeRole(MailboxAddressMeta.DeniedPermission, value);
		}

		virtual public void RemoveDeniedPermission (global::Domain.Permission value)
		{
			Strategy.RemoveCompositeRole(MailboxAddressMeta.DeniedPermission,value);
		}

		virtual public bool ExistDeniedPermissions
		{
			get
			{
				return Strategy.ExistCompositeRoles(MailboxAddressMeta.DeniedPermission);
			}
		}

		virtual public void RemoveDeniedPermissions()
		{
			Strategy.RemoveCompositeRoles(MailboxAddressMeta.DeniedPermission);
		}


		virtual public Allors.Extent<global::Domain.SecurityToken> SecurityTokens
		{ 
			get
			{
				return Strategy.GetCompositeRoles(MailboxAddressMeta.SecurityToken);
			}
			set
			{
				Strategy.SetCompositeRoles(MailboxAddressMeta.SecurityToken, value);
			}
		}

		virtual public void AddSecurityToken (global::Domain.SecurityToken value)
		{
			Strategy.AddCompositeRole(MailboxAddressMeta.SecurityToken, value);
		}

		virtual public void RemoveSecurityToken (global::Domain.SecurityToken value)
		{
			Strategy.RemoveCompositeRole(MailboxAddressMeta.SecurityToken,value);
		}

		virtual public bool ExistSecurityTokens
		{
			get
			{
				return Strategy.ExistCompositeRoles(MailboxAddressMeta.SecurityToken);
			}
		}

		virtual public void RemoveSecurityTokens()
		{
			Strategy.RemoveCompositeRoles(MailboxAddressMeta.SecurityToken);
		}



		virtual public Allors.Extent<global::Domain.Person> PersonsWhereMailboxAddress
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(MailboxAddressMeta.PersonsWhereMailboxAddress);
			}
		}

		virtual public bool ExistPersonsWhereMailboxAddress
		{
			get
			{
				return Strategy.ExistCompositeAssociations(MailboxAddressMeta.PersonsWhereMailboxAddress);
			}
		}


		virtual public global::Domain.Organisation OrganisationWhereAddress
		{ 
			get
			{
				return (global::Domain.Organisation) Strategy.GetCompositeAssociation(MailboxAddressMeta.OrganisationWhereAddress);
			}
		} 

		virtual public bool ExistOrganisationWhereAddress
		{
			get
			{
				return Strategy.ExistCompositeAssociation(MailboxAddressMeta.OrganisationWhereAddress);
			}
		}


		virtual public Allors.Extent<global::Domain.Person> PersonsWhereMainAddress
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(MailboxAddressMeta.PersonsWhereMainAddress);
			}
		}

		virtual public bool ExistPersonsWhereMainAddress
		{
			get
			{
				return Strategy.ExistCompositeAssociations(MailboxAddressMeta.PersonsWhereMainAddress);
			}
		}


		virtual public Allors.Extent<global::Domain.Person> PersonsWhereAddress
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(MailboxAddressMeta.PersonsWhereAddress);
			}
		}

		virtual public bool ExistPersonsWhereAddress
		{
			get
			{
				return Strategy.ExistCompositeAssociations(MailboxAddressMeta.PersonsWhereAddress);
			}
		}

	}

	public static class MailboxAddressMeta
	{
		public static readonly global::Allors.Meta.Class ObjectType = (Allors.Meta.Class)global::Allors.Meta.Repository.MetaPopulation.Find( new System.Guid("7ee3b00b-4e63-4774-b744-3add2c6035ab") );

		public static readonly global::Allors.Meta.RoleType PoBox = ((Allors.Meta.RelationType)global::Allors.Meta.Repository.MetaPopulation.Find( new System.Guid("03c9970e-d9d6-427d-83d0-00e0888f5588"))).RoleType;
		public static readonly global::Allors.Meta.RoleType SearchData = ((Allors.Meta.RelationType)global::Allors.Meta.Repository.MetaPopulation.Find( new System.Guid("5f38c771-10db-456e-ac31-6833f7087b50"))).RoleType;
		public static readonly global::Allors.Meta.RoleType Place = ((Allors.Meta.RelationType)global::Allors.Meta.Repository.MetaPopulation.Find( new System.Guid("36e7d935-a9c7-484d-8551-9bdc5bdeab68"))).RoleType;
		public static readonly global::Allors.Meta.RoleType DisplayName = ((Allors.Meta.RelationType)global::Allors.Meta.Repository.MetaPopulation.Find( new System.Guid("6412301d-95ec-44c2-8c71-cc03de5327b9"))).RoleType;
		public static readonly global::Allors.Meta.RoleType DeniedPermission = ((Allors.Meta.RelationType)global::Allors.Meta.Repository.MetaPopulation.Find( new System.Guid("5c70ca14-4601-4c65-9b0d-cb189f90be27"))).RoleType;
		public static readonly global::Allors.Meta.RoleType SecurityToken = ((Allors.Meta.RelationType)global::Allors.Meta.Repository.MetaPopulation.Find( new System.Guid("b816fccd-08e0-46e0-a49c-7213c3604416"))).RoleType;

		public static readonly global::Allors.Meta.AssociationType PersonsWhereMailboxAddress = ((Allors.Meta.RelationType)global::Allors.Meta.Repository.MetaPopulation.Find( new System.Guid("6340de2a-c3b1-4893-a7f3-cb924b82fa0e"))).AssociationType;
		public static readonly global::Allors.Meta.AssociationType OrganisationWhereAddress = ((Allors.Meta.RelationType)global::Allors.Meta.Repository.MetaPopulation.Find( new System.Guid("73f23588-1444-416d-b43c-b3384ca87bfc"))).AssociationType;
		public static readonly global::Allors.Meta.AssociationType PersonsWhereMainAddress = ((Allors.Meta.RelationType)global::Allors.Meta.Repository.MetaPopulation.Find( new System.Guid("0375a3d3-1a1b-4cbb-b735-1fe508bcc672"))).AssociationType;
		public static readonly global::Allors.Meta.AssociationType PersonsWhereAddress = ((Allors.Meta.RelationType)global::Allors.Meta.Repository.MetaPopulation.Find( new System.Guid("e9e7c874-4d94-42ff-a4c9-414d05ff9533"))).AssociationType;

	}
}