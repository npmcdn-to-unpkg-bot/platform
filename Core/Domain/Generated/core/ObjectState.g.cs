// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	public partial class ObjectStateBuilder : Allors.ObjectBuilder<ObjectState> , UniquelyIdentifiableBuilder, global::System.IDisposable
	{		
		public ObjectStateBuilder(Allors.ISession session) : base(session)
	    {
	    }

				public global::System.Collections.Generic.List<Permission> DeniedPermission {get; set;}	

				/// <exclude/>
				public ObjectStateBuilder WithDeniedPermission(Permission value)
		        {
					if(this.DeniedPermission == null)
					{
						this.DeniedPermission = new global::System.Collections.Generic.List<Permission>(); 
					}
		            this.DeniedPermission.Add(value);
		            return this;
		        }		

				
				public global::System.Guid? UniqueId {get; set;}

				/// <exclude/>
				public ObjectStateBuilder WithUniqueId(global::System.Guid? value)
		        {
				    if(this.UniqueId!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.UniqueId = value;
		            return this;
		        }	


	}

	public partial class ObjectStates : global::Allors.ObjectsBase<ObjectState>
	{
		public static readonly ObjectStateMeta Meta = ObjectStateMeta.Instance;

		public ObjectStates(Allors.ISession session) : base(session)
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