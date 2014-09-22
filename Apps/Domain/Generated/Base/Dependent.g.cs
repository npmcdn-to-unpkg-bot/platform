// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{

	public partial class Dependent
	{		
		internal override void OnBuild(global::Allors.IObjectBuilder objectBuilder)
		{
			var builder = (DependentBuilder)objectBuilder;
			

			if(builder.Counter.HasValue)
			{
				this.Counter = builder.Counter.Value;
			}			
					

			if(builder.Subcounter.HasValue)
			{
				this.Subcounter = builder.Subcounter.Value;
			}			
		

			this.Dependee = builder.Dependee;


		}
	}

	public partial class DependentBuilder : Allors.ObjectBuilder<Dependent> , global::System.IDisposable
	{		
		public DependentBuilder(Allors.ISession session) : base(session)
	    {
	    }

				public Dependee Dependee {get; set;}

				/// <exclude/>
				public DependentBuilder WithDependee(Dependee value)
		        {
		            if(this.Dependee!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.Dependee = value;
		            return this;
		        }		

				
				public global::System.Int32? Counter {get; set;}

				/// <exclude/>
				public DependentBuilder WithCounter(global::System.Int32? value)
		        {
				    if(this.Counter!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.Counter = value;
		            return this;
		        }	

				public global::System.Int32? Subcounter {get; set;}

				/// <exclude/>
				public DependentBuilder WithSubcounter(global::System.Int32? value)
		        {
				    if(this.Subcounter!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.Subcounter = value;
		            return this;
		        }	


	}

	public partial class Dependents : global::Allors.ObjectsBase<Dependent>
	{
		public static readonly DependentMeta Meta = DependentMeta.Instance;

		public Dependents(Allors.ISession session) : base(session)
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