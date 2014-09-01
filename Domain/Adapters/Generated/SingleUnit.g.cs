// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Domain
{
	public partial class SingleUnit : SingleUnitAllors 
	{
		public SingleUnit(Allors.IStrategy allors) : base(allors) {}

		public static SingleUnit Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (SingleUnit) allorsSession.Instantiate(allorsObjectId);		
		}
	}

	[System.Diagnostics.DebuggerNonUserCode]
	public abstract class SingleUnitAllors : Allors.ObjectBase
	{
		protected SingleUnitAllors(Allors.IStrategy allors) : base(allors){}



		virtual public global::System.Int32? AllorsInteger 
		{
			get
			{
				return (global::System.Int32?) Strategy.GetUnitRole(SingleUnitMeta.AllorsInteger);
			}
			set
			{
				Strategy.SetUnitRole(SingleUnitMeta.AllorsInteger, value);
			}
		}

		virtual public bool ExistAllorsInteger{
			get
			{
				return Strategy.ExistUnitRole(SingleUnitMeta.AllorsInteger);
			}
		}

		virtual public void RemoveAllorsInteger()
		{
			Strategy.RemoveUnitRole(SingleUnitMeta.AllorsInteger);
		}

	}

	public static class SingleUnitMeta
	{
		public static readonly global::Allors.Meta.Class ObjectType = (Allors.Meta.Class)global::Allors.Meta.Repository.Environment.Find( new System.Guid("c3e82ab0-f586-4913-acb0-838ffd6701f8") );

		public static readonly global::Allors.Meta.RoleType AllorsInteger = ((Allors.Meta.RelationType)global::Allors.Meta.Repository.Environment.Find( new System.Guid("acf7d284-2480-4a09-a13b-ba4ba96e0892"))).RoleType;

	}
}