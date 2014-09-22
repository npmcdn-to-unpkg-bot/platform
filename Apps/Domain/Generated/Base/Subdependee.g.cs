// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{

	public partial class Subdependee
	{		
		internal override void OnBuild(global::Allors.IObjectBuilder objectBuilder)
		{
			var builder = (SubdependeeBuilder)objectBuilder;
			

			if(builder.Subcounter.HasValue)
			{
				this.Subcounter = builder.Subcounter.Value;
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

		public override Allors.Meta.Composite ObjectType
		{
			get
			{
				return Meta.ObjectType;
			}
		}
	}

}