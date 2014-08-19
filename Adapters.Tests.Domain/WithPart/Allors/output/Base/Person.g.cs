// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Domain
{
	public  partial class Person : PersonAllors , global::Domain.Named
	{
		public Person(Allors.R1.IStrategy allors) : base(allors) {}

		public static Person Instantiate (Allors.R1.ISession allorsSession, string allorsObjectId)
		{
			return (Person) allorsSession.Instantiate(allorsObjectId);		
		}
	}

	[System.Diagnostics.DebuggerNonUserCode]
	public abstract class PersonAllors :  Allors.R1.ObjectBase 
	{
		protected PersonAllors(Allors.R1.IStrategy allors) : base(allors){}


		virtual public global::Domain.Person NextPerson
		{ 
			get
			{
				return (global::Domain.Person) Strategy.GetCompositeRole(PersonMeta.NextPerson);
			}
			set
			{
				Strategy.SetCompositeRole(PersonMeta.NextPerson ,value);
			}
		}

		virtual public bool ExistNextPerson
		{
			get
			{
				return Strategy.ExistCompositeRole(PersonMeta.NextPerson);
			}
		}

		virtual public void RemoveNextPerson()
		{
			Strategy.RemoveCompositeRole(PersonMeta.NextPerson);
		}


		virtual public global::Domain.Company Company
		{ 
			get
			{
				return (global::Domain.Company) Strategy.GetCompositeRole(PersonMeta.Company);
			}
			set
			{
				Strategy.SetCompositeRole(PersonMeta.Company ,value);
			}
		}

		virtual public bool ExistCompany
		{
			get
			{
				return Strategy.ExistCompositeRole(PersonMeta.Company);
			}
		}

		virtual public void RemoveCompany()
		{
			Strategy.RemoveCompositeRole(PersonMeta.Company);
		}



		virtual public global::System.String Name 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(PersonMeta.Name);
			}
			set
			{
				Strategy.SetUnitRole(PersonMeta.Name, value);
			}
		}

		virtual public bool ExistName{
			get
			{
				return Strategy.ExistUnitRole(PersonMeta.Name);
			}
		}

		virtual public void RemoveName()
		{
			Strategy.RemoveUnitRole(PersonMeta.Name);
		}



		virtual public global::System.Int32? Index 
		{
			get
			{
				return (global::System.Int32?) Strategy.GetUnitRole(PersonMeta.Index);
			}
			set
			{
				Strategy.SetUnitRole(PersonMeta.Index, value);
			}
		}

		virtual public bool ExistIndex{
			get
			{
				return Strategy.ExistUnitRole(PersonMeta.Index);
			}
		}

		virtual public void RemoveIndex()
		{
			Strategy.RemoveUnitRole(PersonMeta.Index);
		}



		virtual public Allors.R1.Extent<global::Domain.Company> CompaniesWhereManager
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(PersonMeta.CompaniesWhereManager);
			}
		}

		virtual public bool ExistCompaniesWhereManager
		{
			get
			{
				return Strategy.ExistCompositeAssociations(PersonMeta.CompaniesWhereManager);
			}
		}


		virtual public global::Domain.Company EmployerWhereEmployee
		{ 
			get
			{
				return (global::Domain.Company) Strategy.GetCompositeAssociation(PersonMeta.EmployerWhereEmployee);
			}
		} 

		virtual public bool ExistEmployerWhereEmployee
		{
			get
			{
				return Strategy.ExistCompositeAssociation(PersonMeta.EmployerWhereEmployee);
			}
		}


		virtual public global::Domain.Person PersonWhereNextPerson
		{ 
			get
			{
				return (global::Domain.Person) Strategy.GetCompositeAssociation(PersonMeta.PersonWhereNextPerson);
			}
		} 

		virtual public bool ExistPersonWhereNextPerson
		{
			get
			{
				return Strategy.ExistCompositeAssociation(PersonMeta.PersonWhereNextPerson);
			}
		}


		virtual public global::Domain.Company CompanyWhereFirstPerson
		{ 
			get
			{
				return (global::Domain.Company) Strategy.GetCompositeAssociation(PersonMeta.CompanyWhereFirstPerson);
			}
		} 

		virtual public bool ExistCompanyWhereFirstPerson
		{
			get
			{
				return Strategy.ExistCompositeAssociation(PersonMeta.CompanyWhereFirstPerson);
			}
		}


		virtual public Allors.R1.Extent<global::Domain.Company> CompaniesWhereOwner
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(PersonMeta.CompaniesWhereOwner);
			}
		}

		virtual public bool ExistCompaniesWhereOwner
		{
			get
			{
				return Strategy.ExistCompositeAssociations(PersonMeta.CompaniesWhereOwner);
			}
		}


		virtual public Allors.R1.Extent<global::Domain.Company> CompaniesWhereIndexedMany2ManyPerson
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(PersonMeta.CompaniesWhereIndexedMany2ManyPerson);
			}
		}

		virtual public bool ExistCompaniesWhereIndexedMany2ManyPerson
		{
			get
			{
				return Strategy.ExistCompositeAssociations(PersonMeta.CompaniesWhereIndexedMany2ManyPerson);
			}
		}


		virtual public global::Domain.Company CompanyWherePersonOneSort1
		{ 
			get
			{
				return (global::Domain.Company) Strategy.GetCompositeAssociation(PersonMeta.CompanyWherePersonOneSort1);
			}
		} 

		virtual public bool ExistCompanyWherePersonOneSort1
		{
			get
			{
				return Strategy.ExistCompositeAssociation(PersonMeta.CompanyWherePersonOneSort1);
			}
		}


		virtual public Allors.R1.Extent<global::Domain.Company> CompaniesWherePersonManySort1
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(PersonMeta.CompaniesWherePersonManySort1);
			}
		}

		virtual public bool ExistCompaniesWherePersonManySort1
		{
			get
			{
				return Strategy.ExistCompositeAssociations(PersonMeta.CompaniesWherePersonManySort1);
			}
		}


		virtual public Allors.R1.Extent<global::Domain.Company> CompaniesWherePersonManySort2
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(PersonMeta.CompaniesWherePersonManySort2);
			}
		}

		virtual public bool ExistCompaniesWherePersonManySort2
		{
			get
			{
				return Strategy.ExistCompositeAssociations(PersonMeta.CompaniesWherePersonManySort2);
			}
		}


		virtual public global::Domain.Company CompanyWherePersonOneSort2
		{ 
			get
			{
				return (global::Domain.Company) Strategy.GetCompositeAssociation(PersonMeta.CompanyWherePersonOneSort2);
			}
		} 

		virtual public bool ExistCompanyWherePersonOneSort2
		{
			get
			{
				return Strategy.ExistCompositeAssociation(PersonMeta.CompanyWherePersonOneSort2);
			}
		}


		virtual public Allors.R1.Extent<global::Domain.Company> CompaniesWhereMany2ManyPerson
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(PersonMeta.CompaniesWhereMany2ManyPerson);
			}
		}

		virtual public bool ExistCompaniesWhereMany2ManyPerson
		{
			get
			{
				return Strategy.ExistCompositeAssociations(PersonMeta.CompaniesWhereMany2ManyPerson);
			}
		}


		virtual public global::Domain.Company CompanyWhereNamedOneSort2
		{ 
			get
			{
				return (global::Domain.Company) Strategy.GetCompositeAssociation(PersonMeta.CompanyWhereNamedOneSort2);
			}
		} 

		virtual public bool ExistCompanyWhereNamedOneSort2
		{
			get
			{
				return Strategy.ExistCompositeAssociation(PersonMeta.CompanyWhereNamedOneSort2);
			}
		}


		virtual public Allors.R1.Extent<global::Domain.Company> CompaniesWhereNamedManySort1
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(PersonMeta.CompaniesWhereNamedManySort1);
			}
		}

		virtual public bool ExistCompaniesWhereNamedManySort1
		{
			get
			{
				return Strategy.ExistCompositeAssociations(PersonMeta.CompaniesWhereNamedManySort1);
			}
		}


		virtual public Allors.R1.Extent<global::Domain.Company> CompaniesWhereNamedManySort2
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(PersonMeta.CompaniesWhereNamedManySort2);
			}
		}

		virtual public bool ExistCompaniesWhereNamedManySort2
		{
			get
			{
				return Strategy.ExistCompositeAssociations(PersonMeta.CompaniesWhereNamedManySort2);
			}
		}


		virtual public global::Domain.Company CompanyWhereNamedOneSort1
		{ 
			get
			{
				return (global::Domain.Company) Strategy.GetCompositeAssociation(PersonMeta.CompanyWhereNamedOneSort1);
			}
		} 

		virtual public bool ExistCompanyWhereNamedOneSort1
		{
			get
			{
				return Strategy.ExistCompositeAssociation(PersonMeta.CompanyWhereNamedOneSort1);
			}
		}

	}
}