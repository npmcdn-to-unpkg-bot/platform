// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
    using Allors.Meta;

    [System.Diagnostics.DebuggerNonUserCode]
	public partial class Third : Allors.ObjectBase 
	{
		public static readonly ThirdMeta Meta = ThirdMeta.Instance;

		public Third(Allors.IStrategy allors) : base(allors) {}

		public static Third Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (Third) allorsSession.Instantiate(allorsObjectId);		
		}




		virtual public global::System.Boolean? IsDerived 
		{
			get
			{
				return (global::System.Boolean?) Strategy.GetUnitRole(Meta.IsDerived);
			}
			set
			{
				Strategy.SetUnitRole(Meta.IsDerived, value);
			}
		}

		virtual public bool ExistIsDerived{
			get
			{
				return Strategy.ExistUnitRole(Meta.IsDerived);
			}
		}

		virtual public void RemoveIsDerived()
		{
			Strategy.RemoveUnitRole(Meta.IsDerived);
		}



		virtual public Second SecondWhereThird
		{ 
			get
			{
				return (Second) Strategy.GetCompositeAssociation(Meta.SecondWhereThird);
			}
		} 

		virtual public bool ExistSecondWhereThird
		{
			get
			{
				return Strategy.ExistCompositeAssociation(Meta.SecondWhereThird);
			}
		}

	}

	public class ThirdMeta
	{
		public static readonly ThirdMeta Instance = new ThirdMeta();

		public global::Allors.Meta.Class ObjectType = global::Allors.Meta.Classes.Third;

		public global::Allors.Meta.RoleType IsDerived 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.ThirdIsDerived;
			}
		} 

		public global::Allors.Meta.AssociationType SecondWhereThird 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.SecondThird;
			}
		} 

	}


	public partial class ThirdBuilder : Allors.ObjectBuilder<Third> , global::System.IDisposable
	{		
		public ThirdBuilder(Allors.ISession session) : base(session)
	    {
	    }

				public global::System.Boolean? IsDerived {get; set;}

				/// <exclude/>
				public ThirdBuilder WithIsDerived(global::System.Boolean? value)
		        {
				    if(this.IsDerived!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.IsDerived = value;
		            return this;
		        }	


	}

	public partial class Thirds : global::Allors.ObjectsBase<Third>
	{
		public static readonly ThirdMeta Meta = ThirdMeta.Instance;

		public Thirds(Allors.ISession session) : base(session)
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