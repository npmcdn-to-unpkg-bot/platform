// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Domain
{
	public partial class Third : ThirdAllors 
	{
		public Third(Allors.IStrategy allors) : base(allors) {}

		public static Third Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (Third) allorsSession.Instantiate(allorsObjectId);		
		}
	}

	[System.Diagnostics.DebuggerNonUserCode]
	public abstract class ThirdAllors : Allors.ObjectBase
	{
		protected ThirdAllors(Allors.IStrategy allors) : base(allors){}



		virtual public global::System.Boolean? IsDerived 
		{
			get
			{
				return (global::System.Boolean?) Strategy.GetUnitRole(ThirdMeta.IsDerived);
			}
			set
			{
				Strategy.SetUnitRole(ThirdMeta.IsDerived, value);
			}
		}

		virtual public bool ExistIsDerived{
			get
			{
				return Strategy.ExistUnitRole(ThirdMeta.IsDerived);
			}
		}

		virtual public void RemoveIsDerived()
		{
			Strategy.RemoveUnitRole(ThirdMeta.IsDerived);
		}



		virtual public global::Domain.Second SecondWhereThird
		{ 
			get
			{
				return (global::Domain.Second) Strategy.GetCompositeAssociation(ThirdMeta.SecondWhereThird);
			}
		} 

		virtual public bool ExistSecondWhereThird
		{
			get
			{
				return Strategy.ExistCompositeAssociation(ThirdMeta.SecondWhereThird);
			}
		}

	}

	public static class ThirdMeta
	{
		public static readonly global::Allors.Meta.Class ObjectType = (Allors.Meta.Class)global::Allors.Meta.Repository.MetaPopulation.Find( new System.Guid("39116edf-34cf-45a6-ac09-2e4f98f28e14") );

		public static readonly global::Allors.Meta.RoleType IsDerived = ((Allors.Meta.RelationType)global::Allors.Meta.Repository.MetaPopulation.Find( new System.Guid("6ab5a7af-a0f0-4940-9be3-6f6430a9e728"))).RoleType;

		public static readonly global::Allors.Meta.AssociationType SecondWhereThird = ((Allors.Meta.RelationType)global::Allors.Meta.Repository.MetaPopulation.Find( new System.Guid("4f0eba0d-09b4-4bbc-8e42-15de94921ab5"))).AssociationType;

	}
}