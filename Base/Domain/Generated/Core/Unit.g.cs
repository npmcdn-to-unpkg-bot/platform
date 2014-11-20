// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	public partial class Unit : Allors.ObjectBase , AccessControlledObject, UserInterfaceable
	{
		public static readonly UnitMeta Meta = UnitMeta.Instance;

		public Unit(Allors.IStrategy allors) : base(allors) {}

		public static Unit Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (Unit) allorsSession.Instantiate(allorsObjectId);		
		}




		virtual public global::System.Byte[] AllorsBinary 
		{
			get
			{
				return (global::System.Byte[]) Strategy.GetUnitRole(Meta.AllorsBinary);
			}
			set
			{
				Strategy.SetUnitRole(Meta.AllorsBinary, value);
			}
		}

		virtual public bool ExistAllorsBinary{
			get
			{
				return Strategy.ExistUnitRole(Meta.AllorsBinary);
			}
		}

		virtual public void RemoveAllorsBinary()
		{
			Strategy.RemoveUnitRole(Meta.AllorsBinary);
		}



		virtual public global::System.Boolean? AllorsBoolean 
		{
			get
			{
				return (global::System.Boolean?) Strategy.GetUnitRole(Meta.AllorsBoolean);
			}
			set
			{
				Strategy.SetUnitRole(Meta.AllorsBoolean, value);
			}
		}

		virtual public bool ExistAllorsBoolean{
			get
			{
				return Strategy.ExistUnitRole(Meta.AllorsBoolean);
			}
		}

		virtual public void RemoveAllorsBoolean()
		{
			Strategy.RemoveUnitRole(Meta.AllorsBoolean);
		}



		virtual public global::System.Double? AllorsFloat 
		{
			get
			{
				return (global::System.Double?) Strategy.GetUnitRole(Meta.AllorsFloat);
			}
			set
			{
				Strategy.SetUnitRole(Meta.AllorsFloat, value);
			}
		}

		virtual public bool ExistAllorsFloat{
			get
			{
				return Strategy.ExistUnitRole(Meta.AllorsFloat);
			}
		}

		virtual public void RemoveAllorsFloat()
		{
			Strategy.RemoveUnitRole(Meta.AllorsFloat);
		}



		virtual public global::System.Int32? AllorsInteger 
		{
			get
			{
				return (global::System.Int32?) Strategy.GetUnitRole(Meta.AllorsInteger);
			}
			set
			{
				Strategy.SetUnitRole(Meta.AllorsInteger, value);
			}
		}

		virtual public bool ExistAllorsInteger{
			get
			{
				return Strategy.ExistUnitRole(Meta.AllorsInteger);
			}
		}

		virtual public void RemoveAllorsInteger()
		{
			Strategy.RemoveUnitRole(Meta.AllorsInteger);
		}



		virtual public global::System.String AllorsString 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(Meta.AllorsString);
			}
			set
			{
				Strategy.SetUnitRole(Meta.AllorsString, value);
			}
		}

		virtual public bool ExistAllorsString{
			get
			{
				return Strategy.ExistUnitRole(Meta.AllorsString);
			}
		}

		virtual public void RemoveAllorsString()
		{
			Strategy.RemoveUnitRole(Meta.AllorsString);
		}



		virtual public global::System.Guid? AllorsUnique 
		{
			get
			{
				return (global::System.Guid?) Strategy.GetUnitRole(Meta.AllorsUnique);
			}
			set
			{
				Strategy.SetUnitRole(Meta.AllorsUnique, value);
			}
		}

		virtual public bool ExistAllorsUnique{
			get
			{
				return Strategy.ExistUnitRole(Meta.AllorsUnique);
			}
		}

		virtual public void RemoveAllorsUnique()
		{
			Strategy.RemoveUnitRole(Meta.AllorsUnique);
		}



		virtual public global::System.Decimal? AllorsDecimal 
		{
			get
			{
				return (global::System.Decimal?) Strategy.GetUnitRole(Meta.AllorsDecimal);
			}
			set
			{
				Strategy.SetUnitRole(Meta.AllorsDecimal, value);
			}
		}

		virtual public bool ExistAllorsDecimal{
			get
			{
				return Strategy.ExistUnitRole(Meta.AllorsDecimal);
			}
		}

		virtual public void RemoveAllorsDecimal()
		{
			Strategy.RemoveUnitRole(Meta.AllorsDecimal);
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

	}

	public class UnitMeta
	{
		public static readonly UnitMeta Instance = new UnitMeta();

		public global::Allors.Meta.Class ObjectType = global::Allors.Meta.Classes.Unit;

		public global::Allors.Meta.RoleType AllorsBinary 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.UnitAllorsBinary;
			}
		} 
		public global::Allors.Meta.RoleType AllorsBoolean 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.UnitAllorsBoolean;
			}
		} 
		public global::Allors.Meta.RoleType AllorsFloat 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.UnitAllorsFloat;
			}
		} 
		public global::Allors.Meta.RoleType AllorsInteger 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.UnitAllorsInteger;
			}
		} 
		public global::Allors.Meta.RoleType AllorsString 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.UnitAllorsString;
			}
		} 
		public global::Allors.Meta.RoleType AllorsUnique 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.UnitAllorsUnique;
			}
		} 
		public global::Allors.Meta.RoleType AllorsDecimal 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.UnitAllorsDecimal;
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

	}
}