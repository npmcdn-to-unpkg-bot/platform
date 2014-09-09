// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
    using Allors.Meta;

    [System.Diagnostics.DebuggerNonUserCode]
	public partial class StatefulCompany : Allors.ObjectBase 
	{
		public static readonly StatefulCompanyMeta Meta = StatefulCompanyMeta.Instance;

		public StatefulCompany(Allors.IStrategy allors) : base(allors) {}

		public static StatefulCompany Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (StatefulCompany) allorsSession.Instantiate(allorsObjectId);		
		}



		virtual public Person Employee
		{ 
			get
			{
				return (Person) Strategy.GetCompositeRole(Meta.Employee);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.Employee ,value);
			}
		}

		virtual public bool ExistEmployee
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.Employee);
			}
		}

		virtual public void RemoveEmployee()
		{
			Strategy.RemoveCompositeRole(Meta.Employee);
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


		virtual public Person Manager
		{ 
			get
			{
				return (Person) Strategy.GetCompositeRole(Meta.Manager);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.Manager ,value);
			}
		}

		virtual public bool ExistManager
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.Manager);
			}
		}

		virtual public void RemoveManager()
		{
			Strategy.RemoveCompositeRole(Meta.Manager);
		}

	}

	public class StatefulCompanyMeta
	{
		public static readonly StatefulCompanyMeta Instance = new StatefulCompanyMeta();

		public global::Allors.Meta.Class ObjectType = global::Allors.Meta.Classes.StatefulCompany;

		public global::Allors.Meta.RoleType Employee 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.StatefulCompanyEmployee;
			}
		} 
		public global::Allors.Meta.RoleType Name 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.StatefulCompanyName;
			}
		} 
		public global::Allors.Meta.RoleType Manager 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.StatefulCompanyManager;
			}
		} 

	}


	public partial class StatefulCompanyBuilder : Allors.ObjectBuilder<StatefulCompany> , global::System.IDisposable
	{		
		public StatefulCompanyBuilder(Allors.ISession session) : base(session)
	    {
	    }

				public Person Employee {get; set;}

				/// <exclude/>
				public StatefulCompanyBuilder WithEmployee(Person value)
		        {
		            if(this.Employee!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.Employee = value;
		            return this;
		        }		

				
				public global::System.String Name {get; set;}

				/// <exclude/>
				public StatefulCompanyBuilder WithName(global::System.String value)
		        {
				    if(this.Name!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.Name = value;
		            return this;
		        }	

				public Person Manager {get; set;}

				/// <exclude/>
				public StatefulCompanyBuilder WithManager(Person value)
		        {
		            if(this.Manager!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.Manager = value;
		            return this;
		        }		

				

	}

	public partial class StatefulCompanies : global::Allors.ObjectsBase<StatefulCompany>
	{
		public static readonly StatefulCompanyMeta Meta = StatefulCompanyMeta.Instance;

		public StatefulCompanies(Allors.ISession session) : base(session)
		{
		}

		public override Composite ObjectType
		{
			get
			{
				return Meta.ObjectType;
			}
		}
	}

}