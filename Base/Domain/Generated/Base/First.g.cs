// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	public partial class First : Allors.IObject , Derivable
	{
		public static readonly FirstMeta Meta = FirstMeta.Instance;

		private readonly IStrategy strategy;

		public First(Allors.IStrategy strategy) 
		{
			this.strategy = strategy;
		}

		public ObjectId Id
		{
			get { return this.strategy.ObjectId; }
		}

		public IStrategy Strategy
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return this.strategy; }
        }

		public static First Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (First) allorsSession.Instantiate(allorsObjectId);		
		}

		public void OnBuild(IObjectBuilder objectBuilder)
		{
			var builder = (FirstBuilder)objectBuilder;
			

			if(builder.CreateCycle.HasValue)
			{
				this.CreateCycle = builder.CreateCycle.Value;
			}			
					

			if(builder.IsDerived.HasValue)
			{
				this.IsDerived = builder.IsDerived.Value;
			}			
		

			this.Second = builder.Second;


		}

		public override bool Equals(object obj)
        {
            var typedObj = obj as IObject;
            return typedObj != null &&
                   typedObj.Strategy.ObjectId.Equals(this.Strategy.ObjectId) &&
                   typedObj.Strategy.Session.Population.Id.Equals(this.Strategy.Session.Population.Id);
        }

		public override int GetHashCode()
        {
            return this.Strategy.ObjectId.GetHashCode();
        }



		virtual public Second Second
		{ 
			get
			{
				return (Second) Strategy.GetCompositeRole(Meta.Second);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.Second ,value);
			}
		}

		virtual public bool ExistSecond
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.Second);
			}
		}

		virtual public void RemoveSecond()
		{
			Strategy.RemoveCompositeRole(Meta.Second);
		}


		virtual public global::System.Boolean? CreateCycle 
		{
			get
			{
				return (global::System.Boolean?) Strategy.GetUnitRole(Meta.CreateCycle);
			}
			set
			{
				Strategy.SetUnitRole(Meta.CreateCycle, value);
			}
		}

		virtual public bool ExistCreateCycle{
			get
			{
				return Strategy.ExistUnitRole(Meta.CreateCycle);
			}
		}

		virtual public void RemoveCreateCycle()
		{
			Strategy.RemoveUnitRole(Meta.CreateCycle);
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



		public DerivablePrepareDerivation PrepareDerivation()
		{ 
			return new FirstPrepareDerivation(this);
		}

		public DerivableDerive Derive()
		{ 
			return new FirstDerive(this);
		}

		public DerivableApplySecurityOnDerive ApplySecurityOnDerive()
		{ 
			return new FirstApplySecurityOnDerive(this);
		}

		public ObjectOnPostBuild OnPostBuild()
		{ 
			return new FirstOnPostBuild(this);
		}

		public ObjectApplySecurityOnPostBuild ApplySecurityOnPostBuild()
		{ 
			return new FirstApplySecurityOnPostBuild(this);
		}
	}

	public class FirstMeta
	{
		public static readonly FirstMeta Instance = new FirstMeta();

		public global::Allors.Meta.Class ObjectType = global::Allors.Meta.Classes.First;

		public global::Allors.Meta.RoleType Second 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.FirstSecond;
			}
		} 
		public global::Allors.Meta.RoleType CreateCycle 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.FirstCreateCycle;
			}
		} 
		public global::Allors.Meta.RoleType IsDerived 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.FirstIsDerived;
			}
		} 

		public global::Allors.Meta.MethodType PrepareDerivation 
		{
			get
			{
				return global::Allors.Meta.MethodTypes.DerivablePrepareDerivation;
			}
		} 
		public global::Allors.Meta.MethodType Derive 
		{
			get
			{
				return global::Allors.Meta.MethodTypes.DerivableDerive;
			}
		} 
		public global::Allors.Meta.MethodType ApplySecurityOnDerive 
		{
			get
			{
				return global::Allors.Meta.MethodTypes.DerivableApplySecurityOnDerive;
			}
		} 
		public global::Allors.Meta.MethodType OnPostBuild 
		{
			get
			{
				return global::Allors.Meta.MethodTypes.ObjectOnPostBuild;
			}
		} 
		public global::Allors.Meta.MethodType ApplySecurityOnPostBuild 
		{
			get
			{
				return global::Allors.Meta.MethodTypes.ObjectApplySecurityOnPostBuild;
			}
		} 

	}

	public partial class FirstBuilder : Allors.ObjectBuilder<First> , DerivableBuilder, global::System.IDisposable
	{		
		public FirstBuilder(Allors.ISession session) : base(session)
	    {
	    }

				public Second Second {get; set;}

				/// <exclude/>
				public FirstBuilder WithSecond(Second value)
		        {
		            if(this.Second!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.Second = value;
		            return this;
		        }		

				
				public global::System.Boolean? CreateCycle {get; set;}

				/// <exclude/>
				public FirstBuilder WithCreateCycle(global::System.Boolean? value)
		        {
				    if(this.CreateCycle!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.CreateCycle = value;
		            return this;
		        }	

				public global::System.Boolean? IsDerived {get; set;}

				/// <exclude/>
				public FirstBuilder WithIsDerived(global::System.Boolean? value)
		        {
				    if(this.IsDerived!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.IsDerived = value;
		            return this;
		        }	


	}

	public partial class Firsts : global::Allors.ObjectsBase<First>
	{
		public static readonly FirstMeta Meta = FirstMeta.Instance;

		public Firsts(Allors.ISession session) : base(session)
		{
		}

		public override Allors.Meta.Composite ObjectType
		{
			get
			{
				return Meta.ObjectType;
			}
		}
	}

}