// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	[System.Diagnostics.DebuggerNonUserCode]
	public partial class Person : Allors.ObjectBase , Named
	{
		public static readonly PersonMeta Meta = PersonMeta.Instance;

		public Person(Allors.IStrategy allors) : base(allors) {}

		public static Person Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (Person) allorsSession.Instantiate(allorsObjectId);		
		}



		virtual public Person NextPerson
		{ 
			get
			{
				return (Person) Strategy.GetCompositeRole(Meta.NextPerson);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.NextPerson ,value);
			}
		}

		virtual public bool ExistNextPerson
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.NextPerson);
			}
		}

		virtual public void RemoveNextPerson()
		{
			Strategy.RemoveCompositeRole(Meta.NextPerson);
		}


		virtual public Company Company
		{ 
			get
			{
				return (Company) Strategy.GetCompositeRole(Meta.Company);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.Company ,value);
			}
		}

		virtual public bool ExistCompany
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.Company);
			}
		}

		virtual public void RemoveCompany()
		{
			Strategy.RemoveCompositeRole(Meta.Company);
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



		virtual public global::System.Int32? Index 
		{
			get
			{
				return (global::System.Int32?) Strategy.GetUnitRole(Meta.Index);
			}
			set
			{
				Strategy.SetUnitRole(Meta.Index, value);
			}
		}

		virtual public bool ExistIndex{
			get
			{
				return Strategy.ExistUnitRole(Meta.Index);
			}
		}

		virtual public void RemoveIndex()
		{
			Strategy.RemoveUnitRole(Meta.Index);
		}



		virtual public Person PersonWhereNextPerson
		{ 
			get
			{
				return (Person) Strategy.GetCompositeAssociation(Meta.PersonWhereNextPerson);
			}
		} 

		virtual public bool ExistPersonWhereNextPerson
		{
			get
			{
				return Strategy.ExistCompositeAssociation(Meta.PersonWhereNextPerson);
			}
		}


		virtual public global::Allors.Extent<Company> CompaniesWhereManager
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.CompaniesWhereManager);
			}
		}

		virtual public bool ExistCompaniesWhereManager
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.CompaniesWhereManager);
			}
		}


		virtual public Company CompanyWhereEmployee
		{ 
			get
			{
				return (Company) Strategy.GetCompositeAssociation(Meta.CompanyWhereEmployee);
			}
		} 

		virtual public bool ExistCompanyWhereEmployee
		{
			get
			{
				return Strategy.ExistCompositeAssociation(Meta.CompanyWhereEmployee);
			}
		}


		virtual public Company CompanyWhereFirstPerson
		{ 
			get
			{
				return (Company) Strategy.GetCompositeAssociation(Meta.CompanyWhereFirstPerson);
			}
		} 

		virtual public bool ExistCompanyWhereFirstPerson
		{
			get
			{
				return Strategy.ExistCompositeAssociation(Meta.CompanyWhereFirstPerson);
			}
		}


		virtual public global::Allors.Extent<Company> CompaniesWhereOwner
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.CompaniesWhereOwner);
			}
		}

		virtual public bool ExistCompaniesWhereOwner
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.CompaniesWhereOwner);
			}
		}


		virtual public global::Allors.Extent<Company> CompaniesWhereIndexedMany2ManyPerson
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.CompaniesWhereIndexedMany2ManyPerson);
			}
		}

		virtual public bool ExistCompaniesWhereIndexedMany2ManyPerson
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.CompaniesWhereIndexedMany2ManyPerson);
			}
		}


		virtual public Company CompanyWherePersonOneSort1
		{ 
			get
			{
				return (Company) Strategy.GetCompositeAssociation(Meta.CompanyWherePersonOneSort1);
			}
		} 

		virtual public bool ExistCompanyWherePersonOneSort1
		{
			get
			{
				return Strategy.ExistCompositeAssociation(Meta.CompanyWherePersonOneSort1);
			}
		}


		virtual public global::Allors.Extent<Company> CompaniesWherePersonManySort1
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.CompaniesWherePersonManySort1);
			}
		}

		virtual public bool ExistCompaniesWherePersonManySort1
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.CompaniesWherePersonManySort1);
			}
		}


		virtual public global::Allors.Extent<Company> CompaniesWherePersonManySort2
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.CompaniesWherePersonManySort2);
			}
		}

		virtual public bool ExistCompaniesWherePersonManySort2
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.CompaniesWherePersonManySort2);
			}
		}


		virtual public Company CompanyWherePersonOneSort2
		{ 
			get
			{
				return (Company) Strategy.GetCompositeAssociation(Meta.CompanyWherePersonOneSort2);
			}
		} 

		virtual public bool ExistCompanyWherePersonOneSort2
		{
			get
			{
				return Strategy.ExistCompositeAssociation(Meta.CompanyWherePersonOneSort2);
			}
		}


		virtual public global::Allors.Extent<Company> CompaniesWhereMany2ManyPerson
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.CompaniesWhereMany2ManyPerson);
			}
		}

		virtual public bool ExistCompaniesWhereMany2ManyPerson
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.CompaniesWhereMany2ManyPerson);
			}
		}


		virtual public Company CompanyWhereNamedOneSort2
		{ 
			get
			{
				return (Company) Strategy.GetCompositeAssociation(Meta.CompanyWhereNamedOneSort2);
			}
		} 

		virtual public bool ExistCompanyWhereNamedOneSort2
		{
			get
			{
				return Strategy.ExistCompositeAssociation(Meta.CompanyWhereNamedOneSort2);
			}
		}


		virtual public global::Allors.Extent<Company> CompaniesWhereNamedManySort1
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.CompaniesWhereNamedManySort1);
			}
		}

		virtual public bool ExistCompaniesWhereNamedManySort1
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.CompaniesWhereNamedManySort1);
			}
		}


		virtual public global::Allors.Extent<Company> CompaniesWhereNamedManySort2
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.CompaniesWhereNamedManySort2);
			}
		}

		virtual public bool ExistCompaniesWhereNamedManySort2
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.CompaniesWhereNamedManySort2);
			}
		}


		virtual public Company CompanyWhereNamedOneSort1
		{ 
			get
			{
				return (Company) Strategy.GetCompositeAssociation(Meta.CompanyWhereNamedOneSort1);
			}
		} 

		virtual public bool ExistCompanyWhereNamedOneSort1
		{
			get
			{
				return Strategy.ExistCompositeAssociation(Meta.CompanyWhereNamedOneSort1);
			}
		}

	}

	public class PersonMeta
	{
		public static readonly PersonMeta Instance = new PersonMeta();

		public global::Allors.Meta.Class ObjectType = global::Allors.Meta.Classes.Person;

		public global::Allors.Meta.RoleType NextPerson 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.PersonNextPerson;
			}
		} 
		public global::Allors.Meta.RoleType Company 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.PersonCompany;
			}
		} 
		public global::Allors.Meta.RoleType Name 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.NamedName;
			}
		} 
		public global::Allors.Meta.RoleType Index 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.NamedIndex;
			}
		} 

		public global::Allors.Meta.AssociationType PersonWhereNextPerson 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.PersonNextPerson;
			}
		} 
		public global::Allors.Meta.AssociationType CompaniesWhereManager 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.CompanyManager;
			}
		} 
		public global::Allors.Meta.AssociationType CompanyWhereEmployee 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.CompanyEmployee;
			}
		} 
		public global::Allors.Meta.AssociationType CompanyWhereFirstPerson 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.CompanyFirstPerson;
			}
		} 
		public global::Allors.Meta.AssociationType CompaniesWhereOwner 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.CompanyOwner;
			}
		} 
		public global::Allors.Meta.AssociationType CompaniesWhereIndexedMany2ManyPerson 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.CompanyIndexedMany2ManyPerson;
			}
		} 
		public global::Allors.Meta.AssociationType CompanyWherePersonOneSort1 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.CompanyPersonOneSort1;
			}
		} 
		public global::Allors.Meta.AssociationType CompaniesWherePersonManySort1 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.CompanyPersonManySort1;
			}
		} 
		public global::Allors.Meta.AssociationType CompaniesWherePersonManySort2 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.CompanyPersonManySort2;
			}
		} 
		public global::Allors.Meta.AssociationType CompanyWherePersonOneSort2 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.CompanyPersonOneSort2;
			}
		} 
		public global::Allors.Meta.AssociationType CompaniesWhereMany2ManyPerson 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.CompanyMany2ManyPerson;
			}
		} 
		public global::Allors.Meta.AssociationType CompanyWhereNamedOneSort2 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.CompanyNamedOneSort2;
			}
		} 
		public global::Allors.Meta.AssociationType CompaniesWhereNamedManySort1 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.CompanyNamedManySort1;
			}
		} 
		public global::Allors.Meta.AssociationType CompaniesWhereNamedManySort2 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.CompanyNamedManySort2;
			}
		} 
		public global::Allors.Meta.AssociationType CompanyWhereNamedOneSort1 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.CompanyNamedOneSort1;
			}
		} 

	}
}