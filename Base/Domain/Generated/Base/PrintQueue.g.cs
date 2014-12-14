// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	public partial class PrintQueue : Allors.ObjectBase , AccessControlledObject, UserInterfaceable, UniquelyIdentifiable
	{
		public static readonly PrintQueueMeta Meta = PrintQueueMeta.Instance;

		public PrintQueue(Allors.IStrategy allors) : base(allors) {}

		public static PrintQueue Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (PrintQueue) allorsSession.Instantiate(allorsObjectId);		
		}

		internal override void OnBuild(global::Allors.IObjectBuilder objectBuilder)
		{
			var builder = (PrintQueueBuilder)objectBuilder;

			this.Name = builder.Name;
		

			this.DisplayName = builder.DisplayName;
					

			if(builder.UniqueId.HasValue)
			{
				this.UniqueId = builder.UniqueId.Value;
			}			
		
			if(builder.Printables!=null)
			{
				this.Printables = builder.Printables.ToArray();
			}

			if(builder.DeniedPermissions!=null)
			{
				this.DeniedPermissions = builder.DeniedPermissions.ToArray();
			}

			if(builder.SecurityTokens!=null)
			{
				this.SecurityTokens = builder.SecurityTokens.ToArray();
			}

		}



		virtual public global::Allors.Extent<Printable> Printables
		{ 
			get
			{
				return Strategy.GetCompositeRoles(Meta.Printable);
			}
			set
			{
				Strategy.SetCompositeRoles(Meta.Printable, value);
			}
		}

		virtual public void AddPrintable (Printable value)
		{
			Strategy.AddCompositeRole(Meta.Printable, value);
		}

		virtual public void RemovePrintable (Printable value)
		{
			Strategy.RemoveCompositeRole(Meta.Printable,value);
		}

		virtual public bool ExistPrintables
		{
			get
			{
				return Strategy.ExistCompositeRoles(Meta.Printable);
			}
		}

		virtual public void RemovePrintables()
		{
			Strategy.RemoveCompositeRoles(Meta.Printable);
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



		virtual public global::System.Guid? UniqueId 
		{
			get
			{
				return (global::System.Guid?) Strategy.GetUnitRole(Meta.UniqueId);
			}
			set
			{
				Strategy.SetUnitRole(Meta.UniqueId, value);
			}
		}

		virtual public bool ExistUniqueId{
			get
			{
				return Strategy.ExistUnitRole(Meta.UniqueId);
			}
		}

		virtual public void RemoveUniqueId()
		{
			Strategy.RemoveUnitRole(Meta.UniqueId);
		}



		virtual public Singleton SingletonWhereDefaultPrintQueue
		{ 
			get
			{
				return (Singleton) Strategy.GetCompositeAssociation(Meta.SingletonWhereDefaultPrintQueue);
			}
		} 

		virtual public bool ExistSingletonWhereDefaultPrintQueue
		{
			get
			{
				return Strategy.ExistCompositeAssociation(Meta.SingletonWhereDefaultPrintQueue);
			}
		}

	}

	public class PrintQueueMeta
	{
		public static readonly PrintQueueMeta Instance = new PrintQueueMeta();

		public global::Allors.Meta.Class class = global::Allors.Meta.Classes.PrintQueue;

		public global::Allors.Meta.RoleType Printable 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.PrintQueuePrintable;
			}
		} 
		public global::Allors.Meta.RoleType Name 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.PrintQueueName;
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

		public global::Allors.Meta.AssociationType SingletonWhereDefaultPrintQueue 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.SingletonDefaultPrintQueue;
			}
		} 

	}

	public partial class PrintQueueBuilder : Allors.ObjectBuilder<PrintQueue> , AccessControlledObjectBuilder, UserInterfaceableBuilder, UniquelyIdentifiableBuilder, global::System.IDisposable
	{		
		public PrintQueueBuilder(Allors.ISession session) : base(session)
	    {
	    }

				public global::System.Collections.Generic.List<Printable> Printables {get; set;}	

				/// <exclude/>
				public PrintableBuilder WithPrintable(Printable value)
		        {
					if(this.Printables == null)
					{
						this.Printables = new global::System.Collections.Generic.List<Printable>(); 
					}
		            this.Printables.Add(value);
		            return this;
		        }		

				
				public global::System.String Name {get; set;}

				/// <exclude/>
				public AllorsStringBuilder WithName(global::System.String value)
		        {
				    if(this.Name!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.Name = value;
		            return this;
		        }	

				public global::System.Collections.Generic.List<Permission> DeniedPermissions {get; set;}	

				/// <exclude/>
				public PermissionBuilder WithDeniedPermission(Permission value)
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
				public SecurityTokenBuilder WithSecurityToken(SecurityToken value)
		        {
					if(this.SecurityTokens == null)
					{
						this.SecurityTokens = new global::System.Collections.Generic.List<SecurityToken>(); 
					}
		            this.SecurityTokens.Add(value);
		            return this;
		        }		

				
				public global::System.String DisplayName {get; set;}

				/// <exclude/>
				public AllorsStringBuilder WithDisplayName(global::System.String value)
		        {
				    if(this.DisplayName!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.DisplayName = value;
		            return this;
		        }	

				public global::System.Guid? UniqueId {get; set;}

				/// <exclude/>
				public AllorsUniqueBuilder WithUniqueId(global::System.Guid? value)
		        {
				    if(this.UniqueId!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.UniqueId = value;
		            return this;
		        }	


	}

	public partial class PrintQueues : global::Allors.ObjectsBase<PrintQueue>
	{
		public static readonly PrintQueueMeta Meta = PrintQueueMeta.Instance;

		public PrintQueues(Allors.ISession session) : base(session)
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