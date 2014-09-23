// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{

	public partial class SalesInvoiceItemStatus
	{		
		internal override void OnBuild(global::Allors.IObjectBuilder objectBuilder)
		{
			var builder = (SalesInvoiceItemStatusBuilder)objectBuilder;
			

			if(builder.StartDateTime.HasValue)
			{
				this.StartDateTime = builder.StartDateTime.Value;
			}			
		

			this.DisplayName = builder.DisplayName;
		

			this.SalesInvoiceItemObjectState = builder.SalesInvoiceItemObjectState;


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

	public partial class SalesInvoiceItemStatusBuilder : Allors.ObjectBuilder<SalesInvoiceItemStatus> , UserInterfaceableBuilder, global::System.IDisposable
	{		
		public SalesInvoiceItemStatusBuilder(Allors.ISession session) : base(session)
	    {
	    }

				public global::System.DateTime? StartDateTime {get; set;}

				/// <exclude/>
				public SalesInvoiceItemStatusBuilder WithStartDateTime(global::System.DateTime? value)
		        {
				    if(this.StartDateTime!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.StartDateTime = value;
		            return this;
		        }	

				public SalesInvoiceItemObjectState SalesInvoiceItemObjectState {get; set;}

				/// <exclude/>
				public SalesInvoiceItemStatusBuilder WithSalesInvoiceItemObjectState(SalesInvoiceItemObjectState value)
		        {
		            if(this.SalesInvoiceItemObjectState!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.SalesInvoiceItemObjectState = value;
		            return this;
		        }		

				
				public global::System.String DisplayName {get; set;}

				/// <exclude/>
				public SalesInvoiceItemStatusBuilder WithDisplayName(global::System.String value)
		        {
				    if(this.DisplayName!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.DisplayName = value;
		            return this;
		        }	

				public global::System.Collections.Generic.List<Permission> DeniedPermissions {get; set;}	

				/// <exclude/>
				public SalesInvoiceItemStatusBuilder WithDeniedPermission(Permission value)
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
				public SalesInvoiceItemStatusBuilder WithSecurityToken(SecurityToken value)
		        {
					if(this.SecurityTokens == null)
					{
						this.SecurityTokens = new global::System.Collections.Generic.List<SecurityToken>(); 
					}
		            this.SecurityTokens.Add(value);
		            return this;
		        }		

				

	}

	public partial class SalesInvoiceItemStatuses : global::Allors.ObjectsBase<SalesInvoiceItemStatus>
	{
		public static readonly SalesInvoiceItemStatusMeta Meta = SalesInvoiceItemStatusMeta.Instance;

		public SalesInvoiceItemStatuses(Allors.ISession session) : base(session)
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