// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	public partial class Dependee : Allors.IObject , Derivable
	{
		public static readonly DependeeMeta Meta = DependeeMeta.Instance;

		private readonly IStrategy strategy;

		public Dependee(Allors.IStrategy strategy) 
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

		public static Dependee Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (Dependee) allorsSession.Instantiate(allorsObjectId);		
		}

		public void OnBuild(IObjectBuilder objectBuilder)
		{
			var builder = (DependeeBuilder)objectBuilder;
			

			if(builder.Subcounter.HasValue)
			{
				this.Subcounter = builder.Subcounter.Value;
			}			
					

			if(builder.Counter.HasValue)
			{
				this.Counter = builder.Counter.Value;
			}			
					

			if(builder.DeleteDependent.HasValue)
			{
				this.DeleteDependent = builder.DeleteDependent.Value;
			}			
		

			this.Subdependee = builder.Subdependee;


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



		virtual public Subdependee Subdependee
		{ 
			get
			{
				return (Subdependee) Strategy.GetCompositeRole(Meta.Subdependee);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.Subdependee ,value);
			}
		}

		virtual public bool ExistSubdependee
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.Subdependee);
			}
		}

		virtual public void RemoveSubdependee()
		{
			Strategy.RemoveCompositeRole(Meta.Subdependee);
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


		virtual public global::System.Boolean? DeleteDependent 
		{
			get
			{
				return (global::System.Boolean?) Strategy.GetUnitRole(Meta.DeleteDependent);
			}
			set
			{
				Strategy.SetUnitRole(Meta.DeleteDependent, value);
			}
		}

		virtual public bool ExistDeleteDependent{
			get
			{
				return Strategy.ExistUnitRole(Meta.DeleteDependent);
			}
		}

		virtual public void RemoveDeleteDependent()
		{
			Strategy.RemoveUnitRole(Meta.DeleteDependent);
		}



		virtual public Dependent DependentWhereDependee
		{ 
			get
			{
				return (Dependent) Strategy.GetCompositeAssociation(Meta.DependentWhereDependee);
			}
		} 

		virtual public bool ExistDependentWhereDependee
		{
			get
			{
				return Strategy.ExistCompositeAssociation(Meta.DependentWhereDependee);
			}
		}



		public DerivablePrepareDerivation PrepareDerivation()
		{ 
			return new DependeePrepareDerivation(this);
		}

		public DerivableDerive Derive()
		{ 
			return new DependeeDerive(this);
		}

		public DerivableApplySecurityOnDerive ApplySecurityOnDerive()
		{ 
			return new DependeeApplySecurityOnDerive(this);
		}

		public ObjectOnPostBuild OnPostBuild()
		{ 
			return new DependeeOnPostBuild(this);
		}

		public ObjectApplySecurityOnPostBuild ApplySecurityOnPostBuild()
		{ 
			return new DependeeApplySecurityOnPostBuild(this);
		}
	}

	public class DependeeMeta
	{
		public static readonly DependeeMeta Instance = new DependeeMeta();

		public global::Allors.Meta.Class ObjectType = global::Allors.Meta.Classes.Dependee;

		public global::Allors.Meta.RoleType Subdependee 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.DependeeSubdependee;
			}
		} 
		public global::Allors.Meta.RoleType Subcounter 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.DependeeSubcounter;
			}
		} 
		public global::Allors.Meta.RoleType Counter 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.DependeeCounter;
			}
		} 
		public global::Allors.Meta.RoleType DeleteDependent 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.DependeeDeleteDependent;
			}
		} 

		public global::Allors.Meta.AssociationType DependentWhereDependee 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.DependentDependee;
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

	public partial class DependeeBuilder : Allors.ObjectBuilder<Dependee> , DerivableBuilder, global::System.IDisposable
	{		
		public DependeeBuilder(Allors.ISession session) : base(session)
	    {
	    }

				public Subdependee Subdependee {get; set;}

				/// <exclude/>
				public DependeeBuilder WithSubdependee(Subdependee value)
		        {
		            if(this.Subdependee!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.Subdependee = value;
		            return this;
		        }		

				
				public global::System.Int32? Subcounter {get; set;}

				/// <exclude/>
				public DependeeBuilder WithSubcounter(global::System.Int32? value)
		        {
				    if(this.Subcounter!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.Subcounter = value;
		            return this;
		        }	

				public global::System.Int32? Counter {get; set;}

				/// <exclude/>
				public DependeeBuilder WithCounter(global::System.Int32? value)
		        {
				    if(this.Counter!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.Counter = value;
		            return this;
		        }	

				public global::System.Boolean? DeleteDependent {get; set;}

				/// <exclude/>
				public DependeeBuilder WithDeleteDependent(global::System.Boolean? value)
		        {
				    if(this.DeleteDependent!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.DeleteDependent = value;
		            return this;
		        }	


	}

	public partial class Dependees : global::Allors.ObjectsBase<Dependee>
	{
		public static readonly DependeeMeta Meta = DependeeMeta.Instance;

		public Dependees(Allors.ISession session) : base(session)
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