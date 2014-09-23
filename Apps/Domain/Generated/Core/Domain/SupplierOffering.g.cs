// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	[System.Diagnostics.DebuggerNonUserCode]
	public partial class SupplierOffering : Allors.ObjectBase , Commentable, Period, UserInterfaceable
	{
		public static readonly SupplierOfferingMeta Meta = SupplierOfferingMeta.Instance;

		public SupplierOffering(Allors.IStrategy allors) : base(allors) {}

		public static SupplierOffering Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (SupplierOffering) allorsSession.Instantiate(allorsObjectId);		
		}



		virtual public RatingType Rating
		{ 
			get
			{
				return (RatingType) Strategy.GetCompositeRole(Meta.Rating);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.Rating ,value);
			}
		}

		virtual public bool ExistRating
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.Rating);
			}
		}

		virtual public void RemoveRating()
		{
			Strategy.RemoveCompositeRole(Meta.Rating);
		}



		virtual public global::System.Int32? StandardLeadTime 
		{
			get
			{
				return (global::System.Int32?) Strategy.GetUnitRole(Meta.StandardLeadTime);
			}
			set
			{
				Strategy.SetUnitRole(Meta.StandardLeadTime, value);
			}
		}

		virtual public bool ExistStandardLeadTime{
			get
			{
				return Strategy.ExistUnitRole(Meta.StandardLeadTime);
			}
		}

		virtual public void RemoveStandardLeadTime()
		{
			Strategy.RemoveUnitRole(Meta.StandardLeadTime);
		}


		virtual public global::Allors.Extent<ProductPurchasePrice> ProductPurchasePrices
		{ 
			get
			{
				return Strategy.GetCompositeRoles(Meta.ProductPurchasePrice);
			}
			set
			{
				Strategy.SetCompositeRoles(Meta.ProductPurchasePrice, value);
			}
		}

		virtual public void AddProductPurchasePrice (ProductPurchasePrice value)
		{
			Strategy.AddCompositeRole(Meta.ProductPurchasePrice, value);
		}

		virtual public void RemoveProductPurchasePrice (ProductPurchasePrice value)
		{
			Strategy.RemoveCompositeRole(Meta.ProductPurchasePrice,value);
		}

		virtual public bool ExistProductPurchasePrices
		{
			get
			{
				return Strategy.ExistCompositeRoles(Meta.ProductPurchasePrice);
			}
		}

		virtual public void RemoveProductPurchasePrices()
		{
			Strategy.RemoveCompositeRoles(Meta.ProductPurchasePrice);
		}


		virtual public Ordinal Preference
		{ 
			get
			{
				return (Ordinal) Strategy.GetCompositeRole(Meta.Preference);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.Preference ,value);
			}
		}

		virtual public bool ExistPreference
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.Preference);
			}
		}

		virtual public void RemovePreference()
		{
			Strategy.RemoveCompositeRole(Meta.Preference);
		}



		virtual public global::System.Decimal? MinimalOrderQuantity 
		{
			get
			{
				return (global::System.Decimal?) Strategy.GetUnitRole(Meta.MinimalOrderQuantity);
			}
			set
			{
				Strategy.SetUnitRole(Meta.MinimalOrderQuantity, value);
			}
		}

		virtual public bool ExistMinimalOrderQuantity{
			get
			{
				return Strategy.ExistUnitRole(Meta.MinimalOrderQuantity);
			}
		}

		virtual public void RemoveMinimalOrderQuantity()
		{
			Strategy.RemoveUnitRole(Meta.MinimalOrderQuantity);
		}


		virtual public Product Product
		{ 
			get
			{
				return (Product) Strategy.GetCompositeRole(Meta.Product);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.Product ,value);
			}
		}

		virtual public bool ExistProduct
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.Product);
			}
		}

		virtual public void RemoveProduct()
		{
			Strategy.RemoveCompositeRole(Meta.Product);
		}


		virtual public Party Supplier
		{ 
			get
			{
				return (Party) Strategy.GetCompositeRole(Meta.Supplier);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.Supplier ,value);
			}
		}

		virtual public bool ExistSupplier
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.Supplier);
			}
		}

		virtual public void RemoveSupplier()
		{
			Strategy.RemoveCompositeRole(Meta.Supplier);
		}



		virtual public global::System.String ReferenceNumber 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(Meta.ReferenceNumber);
			}
			set
			{
				Strategy.SetUnitRole(Meta.ReferenceNumber, value);
			}
		}

		virtual public bool ExistReferenceNumber{
			get
			{
				return Strategy.ExistUnitRole(Meta.ReferenceNumber);
			}
		}

		virtual public void RemoveReferenceNumber()
		{
			Strategy.RemoveUnitRole(Meta.ReferenceNumber);
		}


		virtual public Part Part
		{ 
			get
			{
				return (Part) Strategy.GetCompositeRole(Meta.Part);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.Part ,value);
			}
		}

		virtual public bool ExistPart
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.Part);
			}
		}

		virtual public void RemovePart()
		{
			Strategy.RemoveCompositeRole(Meta.Part);
		}



		virtual public global::System.String Comment 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(Meta.Comment);
			}
			set
			{
				Strategy.SetUnitRole(Meta.Comment, value);
			}
		}

		virtual public bool ExistComment{
			get
			{
				return Strategy.ExistUnitRole(Meta.Comment);
			}
		}

		virtual public void RemoveComment()
		{
			Strategy.RemoveUnitRole(Meta.Comment);
		}



		virtual public global::System.DateTime? FromDate 
		{
			get
			{
				return (global::System.DateTime?) Strategy.GetUnitRole(Meta.FromDate);
			}
			set
			{
				Strategy.SetUnitRole(Meta.FromDate, value);
			}
		}

		virtual public bool ExistFromDate{
			get
			{
				return Strategy.ExistUnitRole(Meta.FromDate);
			}
		}

		virtual public void RemoveFromDate()
		{
			Strategy.RemoveUnitRole(Meta.FromDate);
		}



		virtual public global::System.DateTime? ThroughDate 
		{
			get
			{
				return (global::System.DateTime?) Strategy.GetUnitRole(Meta.ThroughDate);
			}
			set
			{
				Strategy.SetUnitRole(Meta.ThroughDate, value);
			}
		}

		virtual public bool ExistThroughDate{
			get
			{
				return Strategy.ExistUnitRole(Meta.ThroughDate);
			}
		}

		virtual public void RemoveThroughDate()
		{
			Strategy.RemoveUnitRole(Meta.ThroughDate);
		}



		virtual public global::System.String DisplayName 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(Meta.DisplayName);
			}
			set
			{
				Strategy.SetUnitRole(Meta.DisplayName, value);
			}
		}

		virtual public bool ExistDisplayName{
			get
			{
				return Strategy.ExistUnitRole(Meta.DisplayName);
			}
		}

		virtual public void RemoveDisplayName()
		{
			Strategy.RemoveUnitRole(Meta.DisplayName);
		}


		virtual public global::Allors.Extent<Permission> DeniedPermissions
		{ 
			get
			{
				return Strategy.GetCompositeRoles(Meta.DeniedPermission);
			}
			set
			{
				Strategy.SetCompositeRoles(Meta.DeniedPermission, value);
			}
		}

		virtual public void AddDeniedPermission (Permission value)
		{
			Strategy.AddCompositeRole(Meta.DeniedPermission, value);
		}

		virtual public void RemoveDeniedPermission (Permission value)
		{
			Strategy.RemoveCompositeRole(Meta.DeniedPermission,value);
		}

		virtual public bool ExistDeniedPermissions
		{
			get
			{
				return Strategy.ExistCompositeRoles(Meta.DeniedPermission);
			}
		}

		virtual public void RemoveDeniedPermissions()
		{
			Strategy.RemoveCompositeRoles(Meta.DeniedPermission);
		}


		virtual public global::Allors.Extent<SecurityToken> SecurityTokens
		{ 
			get
			{
				return Strategy.GetCompositeRoles(Meta.SecurityToken);
			}
			set
			{
				Strategy.SetCompositeRoles(Meta.SecurityToken, value);
			}
		}

		virtual public void AddSecurityToken (SecurityToken value)
		{
			Strategy.AddCompositeRole(Meta.SecurityToken, value);
		}

		virtual public void RemoveSecurityToken (SecurityToken value)
		{
			Strategy.RemoveCompositeRole(Meta.SecurityToken,value);
		}

		virtual public bool ExistSecurityTokens
		{
			get
			{
				return Strategy.ExistCompositeRoles(Meta.SecurityToken);
			}
		}

		virtual public void RemoveSecurityTokens()
		{
			Strategy.RemoveCompositeRoles(Meta.SecurityToken);
		}

	}

	public class SupplierOfferingMeta
	{
		public static readonly SupplierOfferingMeta Instance = new SupplierOfferingMeta();

		public global::Allors.Meta.Class ObjectType = global::Allors.Meta.Classes.SupplierOffering;

		public global::Allors.Meta.RoleType Rating 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.SupplierOfferingRating;
			}
		} 
		public global::Allors.Meta.RoleType StandardLeadTime 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.SupplierOfferingStandardLeadTime;
			}
		} 
		public global::Allors.Meta.RoleType ProductPurchasePrice 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.SupplierOfferingProductPurchasePrice;
			}
		} 
		public global::Allors.Meta.RoleType Preference 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.SupplierOfferingPreference;
			}
		} 
		public global::Allors.Meta.RoleType MinimalOrderQuantity 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.SupplierOfferingMinimalOrderQuantity;
			}
		} 
		public global::Allors.Meta.RoleType Product 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.SupplierOfferingProduct;
			}
		} 
		public global::Allors.Meta.RoleType Supplier 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.SupplierOfferingSupplier;
			}
		} 
		public global::Allors.Meta.RoleType ReferenceNumber 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.SupplierOfferingReferenceNumber;
			}
		} 
		public global::Allors.Meta.RoleType Part 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.SupplierOfferingPart;
			}
		} 
		public global::Allors.Meta.RoleType Comment 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.CommentableComment;
			}
		} 
		public global::Allors.Meta.RoleType FromDate 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.PeriodFromDate;
			}
		} 
		public global::Allors.Meta.RoleType ThroughDate 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.PeriodThroughDate;
			}
		} 
		public global::Allors.Meta.RoleType DisplayName 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.UserInterfaceableDisplayName;
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

	}
}