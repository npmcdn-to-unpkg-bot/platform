// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
    using Allors.Meta;

    [System.Diagnostics.DebuggerNonUserCode]
	public partial class Subdependee : Allors.ObjectBase 
	{
		public static readonly SubdependeeMeta Meta = SubdependeeMeta.Instance;

		public Subdependee(Allors.IStrategy allors) : base(allors) {}

		public static Subdependee Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (Subdependee) allorsSession.Instantiate(allorsObjectId);		
		}




		virtual public global::System.Int32? Subcounter 
		{
			get
			{
				return (global::System.Int32?) Strategy.GetUnitRole(Meta.Subcounter);
			}
			set
			{
				Strategy.SetUnitRole(Meta.Subcounter, value);
			}
		}

		virtual public bool ExistSubcounter{
			get
			{
				return Strategy.ExistUnitRole(Meta.Subcounter);
			}
		}

		virtual public void RemoveSubcounter()
		{
			Strategy.RemoveUnitRole(Meta.Subcounter);
		}



		virtual public Dependee DependeeWhereSubdependee
		{ 
			get
			{
				return (Dependee) Strategy.GetCompositeAssociation(Meta.DependeeWhereSubdependee);
			}
		} 

		virtual public bool ExistDependeeWhereSubdependee
		{
			get
			{
				return Strategy.ExistCompositeAssociation(Meta.DependeeWhereSubdependee);
			}
		}

	}

	public class SubdependeeMeta
	{
		public static readonly SubdependeeMeta Instance = new SubdependeeMeta();

		public global::Allors.Meta.Class ObjectType = global::Allors.Meta.Classes.Subdependee;

		public global::Allors.Meta.RoleType Subcounter 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.SubdependeeSubcounter;
			}
		} 

		public global::Allors.Meta.AssociationType DependeeWhereSubdependee 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.DependeeSubdependee;
			}
		} 

	}


	public partial class SubdependeeBuilder : Allors.ObjectBuilder<Subdependee> , global::System.IDisposable
	{		
		public SubdependeeBuilder(Allors.ISession session) : base(session)
	    {
	    }

				public global::System.Int32? Subcounter {get; set;}

				/// <exclude/>
				public SubdependeeBuilder WithSubcounter(global::System.Int32? value)
		        {
				    if(this.Subcounter!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.Subcounter = value;
		            return this;
		        }	


	}

	public partial class Subdependees : global::Allors.ObjectsBase<Subdependee>
	{
		public static readonly SubdependeeMeta Meta = SubdependeeMeta.Instance;

		public Subdependees(Allors.ISession session) : base(session)
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