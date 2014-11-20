// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	public partial class Dependent : Allors.ObjectBase 
	{
		public static readonly DependentMeta Meta = DependentMeta.Instance;

		public Dependent(Allors.IStrategy allors) : base(allors) {}

		public static Dependent Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (Dependent) allorsSession.Instantiate(allorsObjectId);		
		}



		virtual public Dependee Dependee
		{ 
			get
			{
				return (Dependee) Strategy.GetCompositeRole(Meta.Dependee);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.Dependee ,value);
			}
		}

		virtual public bool ExistDependee
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.Dependee);
			}
		}

		virtual public void RemoveDependee()
		{
			Strategy.RemoveCompositeRole(Meta.Dependee);
		}



		virtual public global::System.Int32? Counter 
		{
			get
			{
				return (global::System.Int32?) Strategy.GetUnitRole(Meta.Counter);
			}
			set
			{
				Strategy.SetUnitRole(Meta.Counter, value);
			}
		}

		virtual public bool ExistCounter{
			get
			{
				return Strategy.ExistUnitRole(Meta.Counter);
			}
		}

		virtual public void RemoveCounter()
		{
			Strategy.RemoveUnitRole(Meta.Counter);
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

	}

	public class DependentMeta
	{
		public static readonly DependentMeta Instance = new DependentMeta();

		public global::Allors.Meta.Class ObjectType = global::Allors.Meta.Classes.Dependent;

		public global::Allors.Meta.RoleType Dependee 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.DependentDependee;
			}
		} 
		public global::Allors.Meta.RoleType Counter 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.DependentCounter;
			}
		} 
		public global::Allors.Meta.RoleType Subcounter 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.DependentSubcounter;
			}
		} 

	}
}