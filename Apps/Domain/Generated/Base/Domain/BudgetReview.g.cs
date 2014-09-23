// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{

	public partial class BudgetReview
	{		
		internal override void OnBuild(global::Allors.IObjectBuilder objectBuilder)
		{
			var builder = (BudgetReviewBuilder)objectBuilder;
			

			if(builder.ReviewDate.HasValue)
			{
				this.ReviewDate = builder.ReviewDate.Value;
			}			
		

			this.Description = builder.Description;
		

			this.Comment = builder.Comment;
		

			this.DisplayName = builder.DisplayName;
		

			this.SearchData = builder.SearchData;


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

	public partial class BudgetReviewBuilder : Allors.ObjectBuilder<BudgetReview> , SearchableBuilder, CommentableBuilder, UserInterfaceableBuilder, global::System.IDisposable
	{		
		public BudgetReviewBuilder(Allors.ISession session) : base(session)
	    {
	    }

				public global::System.DateTime? ReviewDate {get; set;}

				/// <exclude/>
				public BudgetReviewBuilder WithReviewDate(global::System.DateTime? value)
		        {
				    if(this.ReviewDate!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.ReviewDate = value;
		            return this;
		        }	

				public global::System.String Description {get; set;}

				/// <exclude/>
				public BudgetReviewBuilder WithDescription(global::System.String value)
		        {
				    if(this.Description!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.Description = value;
		            return this;
		        }	

				public SearchData SearchData {get; set;}

				/// <exclude/>
				public BudgetReviewBuilder WithSearchData(SearchData value)
		        {
		            if(this.SearchData!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.SearchData = value;
		            return this;
		        }		

				
				public global::System.String Comment {get; set;}

				/// <exclude/>
				public BudgetReviewBuilder WithComment(global::System.String value)
		        {
				    if(this.Comment!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.Comment = value;
		            return this;
		        }	

				public global::System.String DisplayName {get; set;}

				/// <exclude/>
				public BudgetReviewBuilder WithDisplayName(global::System.String value)
		        {
				    if(this.DisplayName!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.DisplayName = value;
		            return this;
		        }	

				public global::System.Collections.Generic.List<Permission> DeniedPermissions {get; set;}	

				/// <exclude/>
				public BudgetReviewBuilder WithDeniedPermission(Permission value)
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
				public BudgetReviewBuilder WithSecurityToken(SecurityToken value)
		        {
					if(this.SecurityTokens == null)
					{
						this.SecurityTokens = new global::System.Collections.Generic.List<SecurityToken>(); 
					}
		            this.SecurityTokens.Add(value);
		            return this;
		        }		

				

	}

	public partial class BudgetReviews : global::Allors.ObjectsBase<BudgetReview>
	{
		public static readonly BudgetReviewMeta Meta = BudgetReviewMeta.Instance;

		public BudgetReviews(Allors.ISession session) : base(session)
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