// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{

	public partial class WorkEffortFixedAssetStandard
	{		
		internal override void OnBuild(global::Allors.IObjectBuilder objectBuilder)
		{
			var builder = (WorkEffortFixedAssetStandardBuilder)objectBuilder;
			

			if(builder.EstimatedCost.HasValue)
			{
				this.EstimatedCost = builder.EstimatedCost.Value;
			}			
					

			if(builder.EstimatedDuration.HasValue)
			{
				this.EstimatedDuration = builder.EstimatedDuration.Value;
			}			
					

			if(builder.EstimatedQuantity.HasValue)
			{
				this.EstimatedQuantity = builder.EstimatedQuantity.Value;
			}			
		

			this.DisplayName = builder.DisplayName;
		

			this.FixedAsset = builder.FixedAsset;


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

	public partial class WorkEffortFixedAssetStandardBuilder : Allors.ObjectBuilder<WorkEffortFixedAssetStandard> , UserInterfaceableBuilder, global::System.IDisposable
	{		
		public WorkEffortFixedAssetStandardBuilder(Allors.ISession session) : base(session)
	    {
	    }

				public global::System.Decimal? EstimatedCost {get; set;}

				/// <exclude/>
				public WorkEffortFixedAssetStandardBuilder WithEstimatedCost(global::System.Decimal? value)
		        {
				    if(this.EstimatedCost!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.EstimatedCost = value;
		            return this;
		        }	

				public global::System.Decimal? EstimatedDuration {get; set;}

				/// <exclude/>
				public WorkEffortFixedAssetStandardBuilder WithEstimatedDuration(global::System.Decimal? value)
		        {
				    if(this.EstimatedDuration!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.EstimatedDuration = value;
		            return this;
		        }	

				public FixedAsset FixedAsset {get; set;}

				/// <exclude/>
				public WorkEffortFixedAssetStandardBuilder WithFixedAsset(FixedAsset value)
		        {
		            if(this.FixedAsset!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.FixedAsset = value;
		            return this;
		        }		

				
				public global::System.Int32? EstimatedQuantity {get; set;}

				/// <exclude/>
				public WorkEffortFixedAssetStandardBuilder WithEstimatedQuantity(global::System.Int32? value)
		        {
				    if(this.EstimatedQuantity!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.EstimatedQuantity = value;
		            return this;
		        }	

				public global::System.String DisplayName {get; set;}

				/// <exclude/>
				public WorkEffortFixedAssetStandardBuilder WithDisplayName(global::System.String value)
		        {
				    if(this.DisplayName!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.DisplayName = value;
		            return this;
		        }	

				public global::System.Collections.Generic.List<Permission> DeniedPermissions {get; set;}	

				/// <exclude/>
				public WorkEffortFixedAssetStandardBuilder WithDeniedPermission(Permission value)
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
				public WorkEffortFixedAssetStandardBuilder WithSecurityToken(SecurityToken value)
		        {
					if(this.SecurityTokens == null)
					{
						this.SecurityTokens = new global::System.Collections.Generic.List<SecurityToken>(); 
					}
		            this.SecurityTokens.Add(value);
		            return this;
		        }		

				

	}

	public partial class WorkEffortFixedAssetStandards : global::Allors.ObjectsBase<WorkEffortFixedAssetStandard>
	{
		public static readonly WorkEffortFixedAssetStandardMeta Meta = WorkEffortFixedAssetStandardMeta.Instance;

		public WorkEffortFixedAssetStandards(Allors.ISession session) : base(session)
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