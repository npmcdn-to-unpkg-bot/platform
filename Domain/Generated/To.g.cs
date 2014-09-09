// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
    using Allors.Meta;

    [System.Diagnostics.DebuggerNonUserCode]
	public partial class To : Allors.ObjectBase , UserInterfaceable
	{
		public static readonly ToMeta Meta = ToMeta.Instance;

		public To(Allors.IStrategy allors) : base(allors) {}

		public static To Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (To) allorsSession.Instantiate(allorsObjectId);		
		}




		virtual public global::System.String Name 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(Meta.Name);
			}
			set
			{
				Strategy.SetUnitRole(Meta.Name, value);
			}
		}

		virtual public bool ExistName{
			get
			{
				return Strategy.ExistUnitRole(Meta.Name);
			}
		}

		virtual public void RemoveName()
		{
			Strategy.RemoveUnitRole(Meta.Name);
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



		virtual public From FromWhereTo
		{ 
			get
			{
				return (From) Strategy.GetCompositeAssociation(Meta.FromWhereTo);
			}
		} 

		virtual public bool ExistFromWhereTo
		{
			get
			{
				return Strategy.ExistCompositeAssociation(Meta.FromWhereTo);
			}
		}

	}

	public class ToMeta
	{
		public static readonly ToMeta Instance = new ToMeta();

		public global::Allors.Meta.Class ObjectType = global::Allors.Meta.Classes.To;

		public global::Allors.Meta.RoleType Name 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.ToName;
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

		public global::Allors.Meta.AssociationType FromWhereTo 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.FromTo;
			}
		} 

	}


	public partial class ToBuilder : Allors.ObjectBuilder<To> , UserInterfaceableBuilder, global::System.IDisposable
	{		
		public ToBuilder(Allors.ISession session) : base(session)
	    {
	    }

				public global::System.String Name {get; set;}

				/// <exclude/>
				public ToBuilder WithName(global::System.String value)
		        {
				    if(this.Name!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.Name = value;
		            return this;
		        }	

				public global::System.String DisplayName {get; set;}

				/// <exclude/>
				public ToBuilder WithDisplayName(global::System.String value)
		        {
				    if(this.DisplayName!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.DisplayName = value;
		            return this;
		        }	

				public global::System.Collections.Generic.List<Permission> DeniedPermission {get; set;}	

				/// <exclude/>
				public ToBuilder WithDeniedPermission(Permission value)
		        {
					if(this.DeniedPermission == null)
					{
						this.DeniedPermission = new global::System.Collections.Generic.List<Permission>(); 
					}
		            this.DeniedPermission.Add(value);
		            return this;
		        }		

				
				public global::System.Collections.Generic.List<SecurityToken> SecurityToken {get; set;}	

				/// <exclude/>
				public ToBuilder WithSecurityToken(SecurityToken value)
		        {
					if(this.SecurityToken == null)
					{
						this.SecurityToken = new global::System.Collections.Generic.List<SecurityToken>(); 
					}
		            this.SecurityToken.Add(value);
		            return this;
		        }		

				

	}

	public partial class Tos : global::Allors.ObjectsBase<To>
	{
		public static readonly ToMeta Meta = ToMeta.Instance;

		public Tos(Allors.ISession session) : base(session)
		{
		}

		public override Composite ObjectType
		{
			get
			{
				return Meta.ObjectType;
			}
		}
	}

}