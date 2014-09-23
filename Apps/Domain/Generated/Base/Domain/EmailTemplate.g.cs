// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{

	public partial class EmailTemplate
	{		
		internal override void OnBuild(global::Allors.IObjectBuilder objectBuilder)
		{
			var builder = (EmailTemplateBuilder)objectBuilder;

			this.Description = builder.Description;
		

			this.BodyTemplate = builder.BodyTemplate;
		

			this.SubjectTemplate = builder.SubjectTemplate;
		

			this.DisplayName = builder.DisplayName;
		
			if(builder.DeniedPermissions!=null)
			{
				this.DeniedPermissions = builder.DeniedPermissions.ToArray();
			}

			if(builder.SecurityTokens!=null)
			{
				this.SecurityTokens = builder.SecurityTokens.ToArray();
			}

		}
	}

	public partial class EmailTemplateBuilder : Allors.ObjectBuilder<EmailTemplate> , UserInterfaceableBuilder, global::System.IDisposable
	{		
		public EmailTemplateBuilder(Allors.ISession session) : base(session)
	    {
	    }

				public global::System.String Description {get; set;}

				/// <exclude/>
				public EmailTemplateBuilder WithDescription(global::System.String value)
		        {
				    if(this.Description!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.Description = value;
		            return this;
		        }	

				public global::System.String BodyTemplate {get; set;}

				/// <exclude/>
				public EmailTemplateBuilder WithBodyTemplate(global::System.String value)
		        {
				    if(this.BodyTemplate!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.BodyTemplate = value;
		            return this;
		        }	

				public global::System.String SubjectTemplate {get; set;}

				/// <exclude/>
				public EmailTemplateBuilder WithSubjectTemplate(global::System.String value)
		        {
				    if(this.SubjectTemplate!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.SubjectTemplate = value;
		            return this;
		        }	

				public global::System.String DisplayName {get; set;}

				/// <exclude/>
				public EmailTemplateBuilder WithDisplayName(global::System.String value)
		        {
				    if(this.DisplayName!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.DisplayName = value;
		            return this;
		        }	

				public global::System.Collections.Generic.List<Permission> DeniedPermissions {get; set;}	

				/// <exclude/>
				public EmailTemplateBuilder WithDeniedPermission(Permission value)
		        {
					if(this.DeniedPermissions == null)
					{
						this.DeniedPermissions = new global::System.Collections.Generic.List<Permission>(); 
					}
		            this.DeniedPermissions.Add(value);
		            return this;
		        }		

				
				public global::System.Collections.Generic.List<SecurityToken> SecurityTokens {get; set;}	

				/// <exclude/>
				public EmailTemplateBuilder WithSecurityToken(SecurityToken value)
		        {
					if(this.SecurityTokens == null)
					{
						this.SecurityTokens = new global::System.Collections.Generic.List<SecurityToken>(); 
					}
		            this.SecurityTokens.Add(value);
		            return this;
		        }		

				

	}

	public partial class EmailTemplates : global::Allors.ObjectsBase<EmailTemplate>
	{
		public static readonly EmailTemplateMeta Meta = EmailTemplateMeta.Instance;

		public EmailTemplates(Allors.ISession session) : base(session)
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