// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Domain
{
	public partial class Dependee : DependeeAllors 
	{
		public Dependee(Allors.IStrategy allors) : base(allors) {}

		public static Dependee Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (Dependee) allorsSession.Instantiate(allorsObjectId);		
		}
	}

	[System.Diagnostics.DebuggerNonUserCode]
	public abstract class DependeeAllors : Allors.ObjectBase
	{
		protected DependeeAllors(Allors.IStrategy allors) : base(allors){}


		virtual public global::Domain.Subdependee Subdependee
		{ 
			get
			{
				return (global::Domain.Subdependee) Strategy.GetCompositeRole(DependeeMeta.Subdependee);
			}
			set
			{
				Strategy.SetCompositeRole(DependeeMeta.Subdependee ,value);
			}
		}

		virtual public bool ExistSubdependee
		{
			get
			{
				return Strategy.ExistCompositeRole(DependeeMeta.Subdependee);
			}
		}

		virtual public void RemoveSubdependee()
		{
			Strategy.RemoveCompositeRole(DependeeMeta.Subdependee);
		}



		virtual public global::System.Int32? Subcounter 
		{
			get
			{
				return (global::System.Int32?) Strategy.GetUnitRole(DependeeMeta.Subcounter);
			}
			set
			{
				Strategy.SetUnitRole(DependeeMeta.Subcounter, value);
			}
		}

		virtual public bool ExistSubcounter{
			get
			{
				return Strategy.ExistUnitRole(DependeeMeta.Subcounter);
			}
		}

		virtual public void RemoveSubcounter()
		{
			Strategy.RemoveUnitRole(DependeeMeta.Subcounter);
		}



		virtual public global::System.Int32? Counter 
		{
			get
			{
				return (global::System.Int32?) Strategy.GetUnitRole(DependeeMeta.Counter);
			}
			set
			{
				Strategy.SetUnitRole(DependeeMeta.Counter, value);
			}
		}

		virtual public bool ExistCounter{
			get
			{
				return Strategy.ExistUnitRole(DependeeMeta.Counter);
			}
		}

		virtual public void RemoveCounter()
		{
			Strategy.RemoveUnitRole(DependeeMeta.Counter);
		}



		virtual public global::System.Boolean? DeleteDependent 
		{
			get
			{
				return (global::System.Boolean?) Strategy.GetUnitRole(DependeeMeta.DeleteDependent);
			}
			set
			{
				Strategy.SetUnitRole(DependeeMeta.DeleteDependent, value);
			}
		}

		virtual public bool ExistDeleteDependent{
			get
			{
				return Strategy.ExistUnitRole(DependeeMeta.DeleteDependent);
			}
		}

		virtual public void RemoveDeleteDependent()
		{
			Strategy.RemoveUnitRole(DependeeMeta.DeleteDependent);
		}



		virtual public global::Domain.Dependent DependentWhereDependee
		{ 
			get
			{
				return (global::Domain.Dependent) Strategy.GetCompositeAssociation(DependeeMeta.DependentWhereDependee);
			}
		} 

		virtual public bool ExistDependentWhereDependee
		{
			get
			{
				return Strategy.ExistCompositeAssociation(DependeeMeta.DependentWhereDependee);
			}
		}

	}

	public static class DependeeMeta
	{
		public static readonly global::Allors.Meta.Class ObjectType = (Allors.Meta.Class)global::Allors.Meta.Repository.MetaPopulation.Find( new System.Guid("2cc9bde1-80da-4159-bb20-219074266101") );

		public static readonly global::Allors.Meta.RoleType Subdependee = ((Allors.Meta.RelationType)global::Allors.Meta.Repository.MetaPopulation.Find( new System.Guid("1b8e0350-c446-48dc-85c0-71130cc1490e"))).RoleType;
		public static readonly global::Allors.Meta.RoleType Subcounter = ((Allors.Meta.RelationType)global::Allors.Meta.Repository.MetaPopulation.Find( new System.Guid("c1e86449-e5a8-4911-97c7-b03de9142f98"))).RoleType;
		public static readonly global::Allors.Meta.RoleType Counter = ((Allors.Meta.RelationType)global::Allors.Meta.Repository.MetaPopulation.Find( new System.Guid("d58d1f28-3abd-4294-abde-885bdd16f466"))).RoleType;
		public static readonly global::Allors.Meta.RoleType DeleteDependent = ((Allors.Meta.RelationType)global::Allors.Meta.Repository.MetaPopulation.Find( new System.Guid("e73b8fc5-0148-486a-9379-cfb051b303d2"))).RoleType;

		public static readonly global::Allors.Meta.AssociationType DependentWhereDependee = ((Allors.Meta.RelationType)global::Allors.Meta.Repository.MetaPopulation.Find( new System.Guid("8859af04-ba38-42ce-8ac9-f428c3f92f31"))).AssociationType;

	}
}