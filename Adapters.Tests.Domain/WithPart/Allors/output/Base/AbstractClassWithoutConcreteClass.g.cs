// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Domain
{
	public abstract partial class AbstractClassWithoutConcreteClass : AbstractClassWithoutConcreteClassAllors 
	{
		public AbstractClassWithoutConcreteClass(Allors.R1.IStrategy allors) : base(allors) {}

		public static AbstractClassWithoutConcreteClass Instantiate (Allors.R1.ISession allorsSession, string allorsObjectId)
		{
			return (AbstractClassWithoutConcreteClass) allorsSession.Instantiate(allorsObjectId);		
		}
	}

	[System.Diagnostics.DebuggerNonUserCode]
	public abstract class AbstractClassWithoutConcreteClassAllors :  Allors.R1.ObjectBase 
	{
		protected AbstractClassWithoutConcreteClassAllors(Allors.R1.IStrategy allors) : base(allors){}



		virtual public global::System.Boolean? AllorsBoolean 
		{
			get
			{
				return (global::System.Boolean?) Strategy.GetUnitRole(AbstractClassWithoutConcreteClassMeta.AllorsBoolean);
			}
			set
			{
				Strategy.SetUnitRole(AbstractClassWithoutConcreteClassMeta.AllorsBoolean, value);
			}
		}

		virtual public bool ExistAllorsBoolean{
			get
			{
				return Strategy.ExistUnitRole(AbstractClassWithoutConcreteClassMeta.AllorsBoolean);
			}
		}

		virtual public void RemoveAllorsBoolean()
		{
			Strategy.RemoveUnitRole(AbstractClassWithoutConcreteClassMeta.AllorsBoolean);
		}

	}
}