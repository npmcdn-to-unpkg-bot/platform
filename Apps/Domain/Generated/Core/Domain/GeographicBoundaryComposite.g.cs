// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	public partial interface GeographicBoundaryComposite :  GeographicBoundary, Allors.IObjectBase
	{


		global::Allors.Extent<GeographicBoundary> Associations
		{ 
			get;
			set;
		}

		void AddAssociation (GeographicBoundary value);

		void RemoveAssociation (GeographicBoundary value);

		bool ExistAssociations
		{
			get;
		}

		void RemoveAssociations();

	}

	public class GeographicBoundaryCompositeMeta
	{
		public static readonly GeographicBoundaryCompositeMeta Instance = new GeographicBoundaryCompositeMeta();

		public global::Allors.Meta.Interface ObjectType = global::Allors.Meta.Interfaces.GeographicBoundaryComposite;

		public global::Allors.Meta.RoleType Association 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.GeographicBoundaryCompositeAssociation;
			}
		} 
		public global::Allors.Meta.RoleType Abbreviation 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.GeographicBoundaryAbbreviation;
			}
		} 
		public global::Allors.Meta.RoleType SearchData 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.SearchableSearchData;
			}
		} 
		public global::Allors.Meta.RoleType Latitude 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.GeoLocatableLatitude;
			}
		} 
		public global::Allors.Meta.RoleType Longitude 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.GeoLocatableLongitude;
			}
		} 
		public global::Allors.Meta.RoleType DeniedPermission 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.AccessControlledObjectDeniedPermission;
			}
		} 
		public global::Allors.Meta.RoleType SecurityToken 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.AccessControlledObjectSecurityToken;
			}
		} 
		public global::Allors.Meta.RoleType DisplayName 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.UserInterfaceableDisplayName;
			}
		} 
		public global::Allors.Meta.RoleType UniqueId 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.UniquelyIdentifiableUniqueId;
			}
		} 

		public global::Allors.Meta.AssociationType ShippingAndHandlingComponentsWhereGeographicBoundary 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.ShippingAndHandlingComponentGeographicBoundary;
			}
		} 
		public global::Allors.Meta.AssociationType PriceComponentsWhereGeographicBoundary 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.PriceComponentGeographicBoundary;
			}
		} 
		public global::Allors.Meta.AssociationType GeographicBoundariesCompositesWhereAssociation 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.GeographicBoundaryCompositeAssociation;
			}
		} 
		public global::Allors.Meta.AssociationType EstimatedProductCostsWhereGeographicBoundary 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.EstimatedProductCostGeographicBoundary;
			}
		} 
		public global::Allors.Meta.AssociationType PostalAddressesWhereGeographicBoundary 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.PostalAddressGeographicBoundary;
			}
		} 

	}
}