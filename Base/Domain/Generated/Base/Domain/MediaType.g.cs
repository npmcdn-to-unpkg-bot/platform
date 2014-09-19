// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	public partial class MediaTypeBuilder : Allors.ObjectBuilder<MediaType> , UserInterfaceableBuilder, global::System.IDisposable
	{		
		public MediaTypeBuilder(Allors.ISession session) : base(session)
	    {
	    }

				public global::System.String DefaultFileExtension {get; set;}

				/// <exclude/>
				public MediaTypeBuilder WithDefaultFileExtension(global::System.String value)
		        {
				    if(this.DefaultFileExtension!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.DefaultFileExtension = value;
		            return this;
		        }	

				public global::System.String Name {get; set;}

				/// <exclude/>
				public MediaTypeBuilder WithName(global::System.String value)
		        {
				    if(this.Name!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.Name = value;
		            return this;
		        }	

				public global::System.String DisplayName {get; set;}

				/// <exclude/>
				public MediaTypeBuilder WithDisplayName(global::System.String value)
		        {
				    if(this.DisplayName!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.DisplayName = value;
		            return this;
		        }	

				public global::System.Collections.Generic.List<Permission> DeniedPermission {get; set;}	

				/// <exclude/>
				public MediaTypeBuilder WithDeniedPermission(Permission value)
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
				public MediaTypeBuilder WithSecurityToken(SecurityToken value)
		        {
					if(this.SecurityToken == null)
					{
						this.SecurityToken = new global::System.Collections.Generic.List<SecurityToken>(); 
					}
		            this.SecurityToken.Add(value);
		            return this;
		        }		

				

	}

	public partial class MediaTypes : global::Allors.ObjectsBase<MediaType>
	{
		public static readonly MediaTypeMeta Meta = MediaTypeMeta.Instance;

		public MediaTypes(Allors.ISession session) : base(session)
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