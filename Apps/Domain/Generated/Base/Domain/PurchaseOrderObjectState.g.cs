// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{

	public partial class PurchaseOrderObjectState
	{		
		internal override void OnBuild(global::Allors.IObjectBuilder objectBuilder)
		{
			var builder = (PurchaseOrderObjectStateBuilder)objectBuilder;
			

			if(builder.UniqueId.HasValue)
			{
				this.UniqueId = builder.UniqueId.Value;
			}			
		
			if(builder.DeniedPermissions!=null)
			{
				this.DeniedPermissions = builder.DeniedPermissions.ToArray();
			}

		}
	}

	public partial class PurchaseOrderObjectStateBuilder : Allors.ObjectBuilder<PurchaseOrderObjectState> , ObjectStateBuilder, global::System.IDisposable
	{		
		public PurchaseOrderObjectStateBuilder(Allors.ISession session) : base(session)
	    {
	    }

				public global::System.Collections.Generic.List<Permission> DeniedPermissions {get; set;}	

				/// <exclude/>
				public PurchaseOrderObjectStateBuilder WithDeniedPermission(Permission value)
		        {
					if(this.DeniedPermissions == null)
					{
						this.DeniedPermissions = new global::System.Collections.Generic.List<Permission>(); 
					}
		            this.DeniedPermissions.Add(value);
		            return this;
		        }		

				
				public global::System.Guid? UniqueId {get; set;}

				/// <exclude/>
				public PurchaseOrderObjectStateBuilder WithUniqueId(global::System.Guid? value)
		        {
				    if(this.UniqueId!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.UniqueId = value;
		            return this;
		        }	


	}

	public partial class PurchaseOrderObjectStates : global::Allors.ObjectsBase<PurchaseOrderObjectState>
	{
		public static readonly PurchaseOrderObjectStateMeta Meta = PurchaseOrderObjectStateMeta.Instance;

		public PurchaseOrderObjectStates(Allors.ISession session) : base(session)
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