// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{

	public partial class Deduction
	{		
		internal override void OnBuild(global::Allors.IObjectBuilder objectBuilder)
		{
			var builder = (DeductionBuilder)objectBuilder;
			

			if(builder.Amount.HasValue)
			{
				this.Amount = builder.Amount.Value;
			}			
		

			this.DisplayName = builder.DisplayName;
		

			this.DeductionType = builder.DeductionType;


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

	public partial class DeductionBuilder : Allors.ObjectBuilder<Deduction> , UserInterfaceableBuilder, global::System.IDisposable
	{		
		public DeductionBuilder(Allors.ISession session) : base(session)
	    {
	    }

				public DeductionType DeductionType {get; set;}

				/// <exclude/>
				public DeductionBuilder WithDeductionType(DeductionType value)
		        {
		            if(this.DeductionType!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.DeductionType = value;
		            return this;
		        }		

				
				public global::System.Decimal? Amount {get; set;}

				/// <exclude/>
				public DeductionBuilder WithAmount(global::System.Decimal? value)
		        {
				    if(this.Amount!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.Amount = value;
		            return this;
		        }	

				public global::System.String DisplayName {get; set;}

				/// <exclude/>
				public DeductionBuilder WithDisplayName(global::System.String value)
		        {
				    if(this.DisplayName!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.DisplayName = value;
		            return this;
		        }	

				public global::System.Collections.Generic.List<Permission> DeniedPermissions {get; set;}	

				/// <exclude/>
				public DeductionBuilder WithDeniedPermission(Permission value)
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
				public DeductionBuilder WithSecurityToken(SecurityToken value)
		        {
					if(this.SecurityTokens == null)
					{
						this.SecurityTokens = new global::System.Collections.Generic.List<SecurityToken>(); 
					}
		            this.SecurityTokens.Add(value);
		            return this;
		        }		

				

	}

	public partial class Deductions : global::Allors.ObjectsBase<Deduction>
	{
		public static readonly DeductionMeta Meta = DeductionMeta.Instance;

		public Deductions(Allors.ISession session) : base(session)
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