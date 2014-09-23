// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{

	public partial class PickList
	{		
		internal override void OnBuild(global::Allors.IObjectBuilder objectBuilder)
		{
			var builder = (PickListBuilder)objectBuilder;
			

			if(builder.CreationDate.HasValue)
			{
				this.CreationDate = builder.CreationDate.Value;
			}			
		

			this.DisplayName = builder.DisplayName;
		

			this.PrintContent = builder.PrintContent;
					

			if(builder.UniqueId.HasValue)
			{
				this.UniqueId = builder.UniqueId.Value;
			}			
		

			this.CustomerShipmentCorrection = builder.CustomerShipmentCorrection;


			if(builder.PickListItems!=null)
			{
				this.PickListItems = builder.PickListItems.ToArray();
			}


			this.CurrentObjectState = builder.CurrentObjectState;



			this.CurrentPickListStatus = builder.CurrentPickListStatus;



			this.Picker = builder.Picker;


			if(builder.PickListStatuses!=null)
			{
				this.PickListStatuses = builder.PickListStatuses.ToArray();
			}


			this.PreviousObjectState = builder.PreviousObjectState;



			this.ShipToParty = builder.ShipToParty;



			this.Store = builder.Store;


			if(builder.DeniedPermissions!=null)
			{
				this.DeniedPermissions = builder.DeniedPermissions.ToArray();
			}

			if(builder.SecurityTokens!=null)
			{
				this.SecurityTokens = builder.SecurityTokens.ToArray();
			}


			this.SearchData = builder.SearchData;


		}
	}

	public partial class PickListBuilder : Allors.ObjectBuilder<PickList> , UserInterfaceableBuilder, SearchResultBuilder, PrintableBuilder, TransitionalBuilder, SearchableBuilder, UniquelyIdentifiableBuilder, global::System.IDisposable
	{		
		public PickListBuilder(Allors.ISession session) : base(session)
	    {
	    }

				public CustomerShipment CustomerShipmentCorrection {get; set;}

				/// <exclude/>
				public PickListBuilder WithCustomerShipmentCorrection(CustomerShipment value)
		        {
		            if(this.CustomerShipmentCorrection!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.CustomerShipmentCorrection = value;
		            return this;
		        }		

				
				public global::System.DateTime? CreationDate {get; set;}

				/// <exclude/>
				public PickListBuilder WithCreationDate(global::System.DateTime? value)
		        {
				    if(this.CreationDate!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.CreationDate = value;
		            return this;
		        }	

				public global::System.Collections.Generic.List<PickListItem> PickListItems {get; set;}	

				/// <exclude/>
				public PickListBuilder WithPickListItem(PickListItem value)
		        {
					if(this.PickListItems == null)
					{
						this.PickListItems = new global::System.Collections.Generic.List<PickListItem>(); 
					}
		            this.PickListItems.Add(value);
		            return this;
		        }		

				
				public PickListObjectState CurrentObjectState {get; set;}

				/// <exclude/>
				public PickListBuilder WithCurrentObjectState(PickListObjectState value)
		        {
		            if(this.CurrentObjectState!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.CurrentObjectState = value;
		            return this;
		        }		

				
				public PickListStatus CurrentPickListStatus {get; set;}

				/// <exclude/>
				public PickListBuilder WithCurrentPickListStatus(PickListStatus value)
		        {
		            if(this.CurrentPickListStatus!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.CurrentPickListStatus = value;
		            return this;
		        }		

				
				public Person Picker {get; set;}

				/// <exclude/>
				public PickListBuilder WithPicker(Person value)
		        {
		            if(this.Picker!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.Picker = value;
		            return this;
		        }		

				
				public global::System.Collections.Generic.List<PickListStatus> PickListStatuses {get; set;}	

				/// <exclude/>
				public PickListBuilder WithPickListStatus(PickListStatus value)
		        {
					if(this.PickListStatuses == null)
					{
						this.PickListStatuses = new global::System.Collections.Generic.List<PickListStatus>(); 
					}
		            this.PickListStatuses.Add(value);
		            return this;
		        }		

				
				public PickListObjectState PreviousObjectState {get; set;}

				/// <exclude/>
				public PickListBuilder WithPreviousObjectState(PickListObjectState value)
		        {
		            if(this.PreviousObjectState!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.PreviousObjectState = value;
		            return this;
		        }		

				
				public Party ShipToParty {get; set;}

				/// <exclude/>
				public PickListBuilder WithShipToParty(Party value)
		        {
		            if(this.ShipToParty!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.ShipToParty = value;
		            return this;
		        }		

				
				public Store Store {get; set;}

				/// <exclude/>
				public PickListBuilder WithStore(Store value)
		        {
		            if(this.Store!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.Store = value;
		            return this;
		        }		

				
				public global::System.String DisplayName {get; set;}

				/// <exclude/>
				public PickListBuilder WithDisplayName(global::System.String value)
		        {
				    if(this.DisplayName!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.DisplayName = value;
		            return this;
		        }	

				public global::System.Collections.Generic.List<Permission> DeniedPermissions {get; set;}	

				/// <exclude/>
				public PickListBuilder WithDeniedPermission(Permission value)
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
				public PickListBuilder WithSecurityToken(SecurityToken value)
		        {
					if(this.SecurityTokens == null)
					{
						this.SecurityTokens = new global::System.Collections.Generic.List<SecurityToken>(); 
					}
		            this.SecurityTokens.Add(value);
		            return this;
		        }		

				
				public global::System.String PrintContent {get; set;}

				/// <exclude/>
				public PickListBuilder WithPrintContent(global::System.String value)
		        {
				    if(this.PrintContent!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.PrintContent = value;
		            return this;
		        }	

				public global::System.Guid? UniqueId {get; set;}

				/// <exclude/>
				public PickListBuilder WithUniqueId(global::System.Guid? value)
		        {
				    if(this.UniqueId!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.UniqueId = value;
		            return this;
		        }	

				public SearchData SearchData {get; set;}

				/// <exclude/>
				public PickListBuilder WithSearchData(SearchData value)
		        {
		            if(this.SearchData!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.SearchData = value;
		            return this;
		        }		

				

	}

	public partial class PickLists : global::Allors.ObjectsBase<PickList>
	{
		public static readonly PickListMeta Meta = PickListMeta.Instance;

		public PickLists(Allors.ISession session) : base(session)
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