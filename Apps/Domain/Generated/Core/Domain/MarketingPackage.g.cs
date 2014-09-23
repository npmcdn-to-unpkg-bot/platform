// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	[System.Diagnostics.DebuggerNonUserCode]
	public partial class MarketingPackage : Allors.ObjectBase , ProductAssociation
	{
		public static readonly MarketingPackageMeta Meta = MarketingPackageMeta.Instance;

		public MarketingPackage(Allors.IStrategy allors) : base(allors) {}

		public static MarketingPackage Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (MarketingPackage) allorsSession.Instantiate(allorsObjectId);		
		}




		virtual public global::System.String Instruction 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(Meta.Instruction);
			}
			set
			{
				Strategy.SetUnitRole(Meta.Instruction, value);
			}
		}

		virtual public bool ExistInstruction{
			get
			{
				return Strategy.ExistUnitRole(Meta.Instruction);
			}
		}

		virtual public void RemoveInstruction()
		{
			Strategy.RemoveUnitRole(Meta.Instruction);
		}


		virtual public global::Allors.Extent<Product> ProductsUsedIn
		{ 
			get
			{
				return Strategy.GetCompositeRoles(Meta.ProductUsedIn);
			}
			set
			{
				Strategy.SetCompositeRoles(Meta.ProductUsedIn, value);
			}
		}

		virtual public void AddProductUsedIn (Product value)
		{
			Strategy.AddCompositeRole(Meta.ProductUsedIn, value);
		}

		virtual public void RemoveProductUsedIn (Product value)
		{
			Strategy.RemoveCompositeRole(Meta.ProductUsedIn,value);
		}

		virtual public bool ExistProductsUsedIn
		{
			get
			{
				return Strategy.ExistCompositeRoles(Meta.ProductUsedIn);
			}
		}

		virtual public void RemoveProductsUsedIn()
		{
			Strategy.RemoveCompositeRoles(Meta.ProductUsedIn);
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



		virtual public global::System.String Description 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(Meta.Description);
			}
			set
			{
				Strategy.SetUnitRole(Meta.Description, value);
			}
		}

		virtual public bool ExistDescription{
			get
			{
				return Strategy.ExistUnitRole(Meta.Description);
			}
		}

		virtual public void RemoveDescription()
		{
			Strategy.RemoveUnitRole(Meta.Description);
		}



		virtual public global::System.Int32? QuantityUsed 
		{
			get
			{
				return (global::System.Int32?) Strategy.GetUnitRole(Meta.QuantityUsed);
			}
			set
			{
				Strategy.SetUnitRole(Meta.QuantityUsed, value);
			}
		}

		virtual public bool ExistQuantityUsed{
			get
			{
				return Strategy.ExistUnitRole(Meta.QuantityUsed);
			}
		}

		virtual public void RemoveQuantityUsed()
		{
			Strategy.RemoveUnitRole(Meta.QuantityUsed);
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

	}

	public class MarketingPackageMeta
	{
		public static readonly MarketingPackageMeta Instance = new MarketingPackageMeta();

		public global::Allors.Meta.Class ObjectType = global::Allors.Meta.Classes.MarketingPackage;

		public global::Allors.Meta.RoleType Instruction 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.MarketingPackageInstruction;
			}
		} 
		public global::Allors.Meta.RoleType ProductUsedIn 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.MarketingPackageProductUsedIn;
			}
		} 
		public global::Allors.Meta.RoleType Product 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.MarketingPackageProduct;
			}
		} 
		public global::Allors.Meta.RoleType Description 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.MarketingPackageDescription;
			}
		} 
		public global::Allors.Meta.RoleType QuantityUsed 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.MarketingPackageQuantityUsed;
			}
		} 
		public global::Allors.Meta.RoleType Comment 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.CommentableComment;
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

	}
}