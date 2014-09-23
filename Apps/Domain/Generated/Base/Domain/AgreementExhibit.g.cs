// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{

	public partial class AgreementExhibit
	{		
		internal override void OnBuild(global::Allors.IObjectBuilder objectBuilder)
		{
			var builder = (AgreementExhibitBuilder)objectBuilder;

			this.Text = builder.Text;
		

			this.Description = builder.Description;
		

			this.DisplayName = builder.DisplayName;
		
			if(builder.Addenda!=null)
			{
				this.Addenda = builder.Addenda.ToArray();
			}

			if(builder.Children!=null)
			{
				this.Children = builder.Children.ToArray();
			}

			if(builder.AgreementTerms!=null)
			{
				this.AgreementTerms = builder.AgreementTerms.ToArray();
			}

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

	public partial class AgreementExhibitBuilder : Allors.ObjectBuilder<AgreementExhibit> , AgreementItemBuilder, global::System.IDisposable
	{		
		public AgreementExhibitBuilder(Allors.ISession session) : base(session)
	    {
	    }

				public global::System.String Text {get; set;}

				/// <exclude/>
				public AgreementExhibitBuilder WithText(global::System.String value)
		        {
				    if(this.Text!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.Text = value;
		            return this;
		        }	

				public global::System.Collections.Generic.List<Addendum> Addenda {get; set;}	

				/// <exclude/>
				public AgreementExhibitBuilder WithAddendum(Addendum value)
		        {
					if(this.Addenda == null)
					{
						this.Addenda = new global::System.Collections.Generic.List<Addendum>(); 
					}
		            this.Addenda.Add(value);
		            return this;
		        }		

				
				public global::System.Collections.Generic.List<AgreementItem> Children {get; set;}	

				/// <exclude/>
				public AgreementExhibitBuilder WithChild(AgreementItem value)
		        {
					if(this.Children == null)
					{
						this.Children = new global::System.Collections.Generic.List<AgreementItem>(); 
					}
		            this.Children.Add(value);
		            return this;
		        }		

				
				public global::System.String Description {get; set;}

				/// <exclude/>
				public AgreementExhibitBuilder WithDescription(global::System.String value)
		        {
				    if(this.Description!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.Description = value;
		            return this;
		        }	

				public global::System.Collections.Generic.List<AgreementTerm> AgreementTerms {get; set;}	

				/// <exclude/>
				public AgreementExhibitBuilder WithAgreementTerm(AgreementTerm value)
		        {
					if(this.AgreementTerms == null)
					{
						this.AgreementTerms = new global::System.Collections.Generic.List<AgreementTerm>(); 
					}
		            this.AgreementTerms.Add(value);
		            return this;
		        }		

				
				public global::System.String DisplayName {get; set;}

				/// <exclude/>
				public AgreementExhibitBuilder WithDisplayName(global::System.String value)
		        {
				    if(this.DisplayName!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.DisplayName = value;
		            return this;
		        }	

				public global::System.Collections.Generic.List<Permission> DeniedPermissions {get; set;}	

				/// <exclude/>
				public AgreementExhibitBuilder WithDeniedPermission(Permission value)
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
				public AgreementExhibitBuilder WithSecurityToken(SecurityToken value)
		        {
					if(this.SecurityTokens == null)
					{
						this.SecurityTokens = new global::System.Collections.Generic.List<SecurityToken>(); 
					}
		            this.SecurityTokens.Add(value);
		            return this;
		        }		

				

	}

	public partial class AgreementExhibits : global::Allors.ObjectsBase<AgreementExhibit>
	{
		public static readonly AgreementExhibitMeta Meta = AgreementExhibitMeta.Instance;

		public AgreementExhibits(Allors.ISession session) : base(session)
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