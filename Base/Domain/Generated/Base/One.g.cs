// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	public partial class One : Allors.IObject , Derivable, Shared, UserInterfaceable
	{
		public static readonly OneMeta Meta = OneMeta.Instance;

		private readonly IStrategy strategy;

		public One(Allors.IStrategy strategy) 
		{
			this.strategy = strategy;
		}

		public ObjectId Id
		{
			get { return this.strategy.ObjectId; }
		}

		public IStrategy Strategy
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return this.strategy; }
        }

		public static One Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (One) allorsSession.Instantiate(allorsObjectId);		
		}

		public void OnBuild(IObjectBuilder objectBuilder)
		{
			var builder = (OneBuilder)objectBuilder;

			this.DisplayName = builder.DisplayName;
		

			this.Two = builder.Two;


			if(builder.DeniedPermissions!=null)
			{
				this.DeniedPermissions = builder.DeniedPermissions.ToArray();
			}

			if(builder.SecurityTokens!=null)
			{
				this.SecurityTokens = builder.SecurityTokens.ToArray();
			}

		}

		public override bool Equals(object obj)
        {
            var typedObj = obj as IObject;
            return typedObj != null &&
                   typedObj.Strategy.ObjectId.Equals(this.Strategy.ObjectId) &&
                   typedObj.Strategy.Session.Population.Id.Equals(this.Strategy.Session.Population.Id);
        }

		public override int GetHashCode()
        {
            return this.Strategy.ObjectId.GetHashCode();
        }



		virtual public Two Two
		{ 
			get
			{
				return (Two) Strategy.GetCompositeRole(Meta.Two);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.Two ,value);
			}
		}

		virtual public bool ExistTwo
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.Two);
			}
		}

		virtual public void RemoveTwo()
		{
			Strategy.RemoveCompositeRole(Meta.Two);
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



		virtual public global::Allors.Extent<Two> TwosWhereShared
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.TwosWhereShared);
			}
		}

		virtual public bool ExistTwosWhereShared
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.TwosWhereShared);
			}
		}



		public DerivablePrepareDerivation PrepareDerivation()
		{ 
			return new OnePrepareDerivation(this);
		}

		public DerivableDerive Derive()
		{ 
			return new OneDerive(this);
		}

		public DerivableApplySecurityOnDerive ApplySecurityOnDerive()
		{ 
			return new OneApplySecurityOnDerive(this);
		}

		public ObjectOnPostBuild OnPostBuild()
		{ 
			return new OneOnPostBuild(this);
		}

		public ObjectApplySecurityOnPostBuild ApplySecurityOnPostBuild()
		{ 
			return new OneApplySecurityOnPostBuild(this);
		}
	}

	public class OneMeta
	{
		public static readonly OneMeta Instance = new OneMeta();

		public global::Allors.Meta.Class ObjectType = global::Allors.Meta.Classes.One;

		public global::Allors.Meta.RoleType Two 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.OneTwo;
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

		public global::Allors.Meta.AssociationType TwosWhereShared 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.TwoShared;
			}
		} 

		public global::Allors.Meta.MethodType PrepareDerivation 
		{
			get
			{
				return global::Allors.Meta.MethodTypes.DerivablePrepareDerivation;
			}
		} 
		public global::Allors.Meta.MethodType Derive 
		{
			get
			{
				return global::Allors.Meta.MethodTypes.DerivableDerive;
			}
		} 
		public global::Allors.Meta.MethodType ApplySecurityOnDerive 
		{
			get
			{
				return global::Allors.Meta.MethodTypes.DerivableApplySecurityOnDerive;
			}
		} 
		public global::Allors.Meta.MethodType OnPostBuild 
		{
			get
			{
				return global::Allors.Meta.MethodTypes.ObjectOnPostBuild;
			}
		} 
		public global::Allors.Meta.MethodType ApplySecurityOnPostBuild 
		{
			get
			{
				return global::Allors.Meta.MethodTypes.ObjectApplySecurityOnPostBuild;
			}
		} 

	}

	public partial class OneBuilder : Allors.ObjectBuilder<One> , DerivableBuilder, SharedBuilder, UserInterfaceableBuilder, global::System.IDisposable
	{		
		public OneBuilder(Allors.ISession session) : base(session)
	    {
	    }

				public Two Two {get; set;}

				/// <exclude/>
				public OneBuilder WithTwo(Two value)
		        {
		            if(this.Two!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.Two = value;
		            return this;
		        }		

				
				public global::System.String DisplayName {get; set;}

				/// <exclude/>
				public OneBuilder WithDisplayName(global::System.String value)
		        {
				    if(this.DisplayName!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.DisplayName = value;
		            return this;
		        }	

				public global::System.Collections.Generic.List<Permission> DeniedPermissions {get; set;}	

				/// <exclude/>
				public OneBuilder WithDeniedPermission(Permission value)
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
				public OneBuilder WithSecurityToken(SecurityToken value)
		        {
					if(this.SecurityTokens == null)
					{
						this.SecurityTokens = new global::System.Collections.Generic.List<SecurityToken>(); 
					}
		            this.SecurityTokens.Add(value);
		            return this;
		        }		

				

	}

	public partial class Ones : global::Allors.ObjectsBase<One>
	{
		public static readonly OneMeta Meta = OneMeta.Instance;

		public Ones(Allors.ISession session) : base(session)
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