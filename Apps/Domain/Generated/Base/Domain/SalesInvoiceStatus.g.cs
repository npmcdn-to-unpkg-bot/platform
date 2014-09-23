// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{

	public partial class SalesInvoiceStatus
	{		
		internal override void OnBuild(global::Allors.IObjectBuilder objectBuilder)
		{
			var builder = (SalesInvoiceStatusBuilder)objectBuilder;
			

			if(builder.StartDateTime.HasValue)
			{
				this.StartDateTime = builder.StartDateTime.Value;
			}			
		

			this.DisplayName = builder.DisplayName;
		

			this.SalesInvoiceObjectState = builder.SalesInvoiceObjectState;


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

	public partial class SalesInvoiceStatusBuilder : Allors.ObjectBuilder<SalesInvoiceStatus> , UserInterfaceableBuilder, global::System.IDisposable
	{		
		public SalesInvoiceStatusBuilder(Allors.ISession session) : base(session)
	    {
	    }

				public SalesInvoiceObjectState SalesInvoiceObjectState {get; set;}

				/// <exclude/>
				public SalesInvoiceStatusBuilder WithSalesInvoiceObjectState(SalesInvoiceObjectState value)
		        {
		            if(this.SalesInvoiceObjectState!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.SalesInvoiceObjectState = value;
		            return this;
		        }		

				
				public global::System.DateTime? StartDateTime {get; set;}

				/// <exclude/>
				public SalesInvoiceStatusBuilder WithStartDateTime(global::System.DateTime? value)
		        {
				    if(this.StartDateTime!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.StartDateTime = value;
		            return this;
		        }	

				public global::System.String DisplayName {get; set;}

				/// <exclude/>
				public SalesInvoiceStatusBuilder WithDisplayName(global::System.String value)
		        {
				    if(this.DisplayName!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.DisplayName = value;
		            return this;
		        }	

				public global::System.Collections.Generic.List<Permission> DeniedPermissions {get; set;}	

				/// <exclude/>
				public SalesInvoiceStatusBuilder WithDeniedPermission(Permission value)
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
				public SalesInvoiceStatusBuilder WithSecurityToken(SecurityToken value)
		        {
					if(this.SecurityTokens == null)
					{
						this.SecurityTokens = new global::System.Collections.Generic.List<SecurityToken>(); 
					}
		            this.SecurityTokens.Add(value);
		            return this;
		        }		

				

	}

	public partial class SalesInvoiceStatuses : global::Allors.ObjectsBase<SalesInvoiceStatus>
	{
		public static readonly SalesInvoiceStatusMeta Meta = SalesInvoiceStatusMeta.Instance;

		public SalesInvoiceStatuses(Allors.ISession session) : base(session)
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