// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	public partial class SecurityToken : Allors.ObjectBase 
	{
		public static readonly SecurityTokenMeta Meta = SecurityTokenMeta.Instance;

		public SecurityToken(Allors.IStrategy allors) : base(allors) {}

		public static SecurityToken Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (SecurityToken) allorsSession.Instantiate(allorsObjectId);		
		}

		internal override void OnBuild(global::Allors.IObjectBuilder objectBuilder)
		{
			var builder = (SecurityTokenBuilder)objectBuilder;
		}



		virtual public global::Allors.Extent<Singleton> SingletonsWhereAdministratorSecurityToken
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.SingletonsWhereAdministratorSecurityToken);
			}
		}

		virtual public bool ExistSingletonsWhereAdministratorSecurityToken
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.SingletonsWhereAdministratorSecurityToken);
			}
		}


		virtual public global::Allors.Extent<Singleton> SingletonsWhereDefaultSecurityToken
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.SingletonsWhereDefaultSecurityToken);
			}
		}

		virtual public bool ExistSingletonsWhereDefaultSecurityToken
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.SingletonsWhereDefaultSecurityToken);
			}
		}


		virtual public SecurityTokenOwner SecurityTokenOwnerWhereOwnerSecurityToken
		{ 
			get
			{
				return (SecurityTokenOwner) Strategy.GetCompositeAssociation(Meta.SecurityTokenOwnerWhereOwnerSecurityToken);
			}
		} 

		virtual public bool ExistSecurityTokenOwnerWhereOwnerSecurityToken
		{
			get
			{
				return Strategy.ExistCompositeAssociation(Meta.SecurityTokenOwnerWhereOwnerSecurityToken);
			}
		}


		virtual public global::Allors.Extent<AccessControl> AccessControlsWhereObject
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.AccessControlsWhereObject);
			}
		}

		virtual public bool ExistAccessControlsWhereObject
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.AccessControlsWhereObject);
			}
		}


		virtual public global::Allors.Extent<AccessControlledObject> AccessControlledObjectsWhereSecurityToken
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.AccessControlledObjectsWhereSecurityToken);
			}
		}

		virtual public bool ExistAccessControlledObjectsWhereSecurityToken
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.AccessControlledObjectsWhereSecurityToken);
			}
		}

	}

	public class SecurityTokenMeta
	{
		public static readonly SecurityTokenMeta Instance = new SecurityTokenMeta();

		public global::Allors.Meta.Class class = global::Allors.Meta.Classes.SecurityToken;

		public global::Allors.Meta.AssociationType SingletonsWhereAdministratorSecurityToken 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.SingletonAdministratorSecurityToken;
			}
		} 
		public global::Allors.Meta.AssociationType SingletonsWhereDefaultSecurityToken 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.SingletonDefaultSecurityToken;
			}
		} 
		public global::Allors.Meta.AssociationType SecurityTokenOwnerWhereOwnerSecurityToken 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.SecurityTokenOwnerOwnerSecurityToken;
			}
		} 
		public global::Allors.Meta.AssociationType AccessControlsWhereObject 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.AccessControlObject;
			}
		} 
		public global::Allors.Meta.AssociationType AccessControlledObjectsWhereSecurityToken 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.AccessControlledObjectSecurityToken;
			}
		} 

	}

	public partial class SecurityTokenBuilder : Allors.ObjectBuilder<SecurityToken> , global::System.IDisposable
	{		
		public SecurityTokenBuilder(Allors.ISession session) : base(session)
	    {
	    }

	}

	public partial class SecurityTokens : global::Allors.ObjectsBase<SecurityToken>
	{
		public static readonly SecurityTokenMeta Meta = SecurityTokenMeta.Instance;

		public SecurityTokens(Allors.ISession session) : base(session)
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