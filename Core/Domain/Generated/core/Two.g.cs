// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	public partial class TwoBuilder : Allors.ObjectBuilder<Two> , UserInterfaceableBuilder, SharedBuilder, global::System.IDisposable
	{		
		public TwoBuilder(Allors.ISession session) : base(session)
	    {
	    }

				public Shared Shared {get; set;}

				/// <exclude/>
				public TwoBuilder WithShared(Shared value)
		        {
		            if(this.Shared!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.Shared = value;
		            return this;
		        }		

				
				public global::System.String DisplayName {get; set;}

				/// <exclude/>
				public TwoBuilder WithDisplayName(global::System.String value)
		        {
				    if(this.DisplayName!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.DisplayName = value;
		            return this;
		        }	

				public global::System.Collections.Generic.List<Permission> DeniedPermission {get; set;}	

				/// <exclude/>
				public TwoBuilder WithDeniedPermission(Permission value)
		        {
					if(this.DeniedPermission == null)
					{
						this.DeniedPermission = new global::System.Collections.Generic.List<Permission>(); 
					}
		            this.DeniedPermission.Add(value);
		            return this;
		        }		

				
				public global::System.Collections.Generic.List<SecurityToken> SecurityToken {get; set;}	

				/// <exclude/>
				public TwoBuilder WithSecurityToken(SecurityToken value)
		        {
					if(this.SecurityToken == null)
					{
						this.SecurityToken = new global::System.Collections.Generic.List<SecurityToken>(); 
					}
		            this.SecurityToken.Add(value);
		            return this;
		        }		

				

	}

	public partial class Twos : global::Allors.ObjectsBase<Two>
	{
		public static readonly TwoMeta Meta = TwoMeta.Instance;

		public Twos(Allors.ISession session) : base(session)
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