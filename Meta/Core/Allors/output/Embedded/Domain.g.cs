namespace Allors.Meta.AllorsGenerated
{
	internal interface AllorsInternalDomain : AllorsInternalMetaObject, AllorsInternal
	{
		global::Allors.Meta.ObjectType[] ObjectTypes
		{
			get;
			set;
		}

		void AddObjectType( global::Allors.Meta.ObjectType addRole );

		void RemoveDeclaredObjectType( global::Allors.Meta.ObjectType removeRole );

		void RemoveDeclaredObjectTypes();

		bool ExistDeclaredObjectTypes
		{
			get;
		}
		void AllorsRemoveDomainDeclaredObjectType();

		void AllorsRemoveDomainDeclaredObjectType( global::Allors.Meta.ObjectType role );


		global::System.String Name
		{
			get;
			set;
		}

		void RemoveName();

		bool ExistName
		{
			get;
		}


		global::Allors.Meta.MethodType[] MethodTypes
		{
			get;
			set;
		}

		void AddMethodType( global::Allors.Meta.MethodType addRole );

		void RemoveDeclaredMethodType( global::Allors.Meta.MethodType removeRole );

		void RemoveDeclaredMethodTypes();

		bool ExistDeclaredMethodTypes
		{
			get;
		}
		void AllorsRemoveDomainDeclaredMethodType();

		void AllorsRemoveDomainDeclaredMethodType( global::Allors.Meta.MethodType role );



		global::Allors.Meta.Inheritance[] Inheritances
		{
			get;
			set;
		}

		void AddInheritance( global::Allors.Meta.Inheritance addRole );

		void RemoveDeclaredInheritance( global::Allors.Meta.Inheritance removeRole );

		void RemoveDeclaredInheritances();

		bool ExistDeclaredInheritances
		{
			get;
		}
		void AllorsRemoveDomainDeclaredInheritance();

		void AllorsRemoveDomainDeclaredInheritance( global::Allors.Meta.Inheritance role );



		global::Allors.Meta.ObjectType[] DerivedUnitObjectTypes
		{
			get;
			set;
		}

		void AddDerivedUnitObjectType( global::Allors.Meta.ObjectType addRole );

		void RemoveDerivedUnitObjectType( global::Allors.Meta.ObjectType removeRole );

		void RemoveDerivedUnitObjectTypes();

		bool ExistDerivedUnitObjectTypes
		{
			get;
		}
		void AllorsRemoveDomainDerivedUnitObjectType();

		void AllorsRemoveDomainDerivedUnitObjectType( global::Allors.Meta.ObjectType role );


		global::Allors.Meta.ObjectType[] DerivedCompositeObjectTypes
		{
			get;
			set;
		}

		void AddDerivedCompositeObjectType( global::Allors.Meta.ObjectType addRole );

		void RemoveDerivedCompositeObjectType( global::Allors.Meta.ObjectType removeRole );

		void RemoveDerivedCompositeObjectTypes();

		bool ExistDerivedCompositeObjectTypes
		{
			get;
		}
		void AllorsRemoveDomainDerivedCompositeObjectType();

		void AllorsRemoveDomainDerivedCompositeObjectType( global::Allors.Meta.ObjectType role );


		global::Allors.Meta.RelationType[] RelationTypes
		{
			get;
			set;
		}

		void AddRelationType( global::Allors.Meta.RelationType addRole );

		void RemoveDeclaredRelationType( global::Allors.Meta.RelationType removeRole );

		void RemoveDeclaredRelationTypes();

		bool ExistDeclaredRelationTypes
		{
			get;
		}
		void AllorsRemoveDomainDeclaredRelationType();

		void AllorsRemoveDomainDeclaredRelationType( global::Allors.Meta.RelationType role );

	}

	public interface AllorsInterfaceDomain :  AllorsEmbeddedObject 
	{
	}

	[System.Diagnostics.DebuggerNonUserCode]
	public abstract class AllorsClassDomain :  global::Allors.Meta.MetaObject,  AllorsInternalDomain , AllorsEmbeddedObject
	{
	




		/// <summary>
		/// Initializes a new instance of the <see cref="AllorsClassDomain"/> class.
		/// </summary>
		/// <param name="session">The Allors Session.</param>
		/// <param name="id">The Allors Object Id.</param>
		protected AllorsClassDomain(AllorsEmbeddedSession session, System.Int32 id) : base(session,id){}

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </returns>
		public override System.String ToString() {
			return this.GetType().Name + " (" + this.AllorsObjectId + ")";
		}

		/// <summary>
		/// Gets the Object Type Id.
		/// </summary>
		/// <value>The Object Type Id.</value>
		public override System.Guid AllorsObjectTypeId
		{ 
			get
			{
				//TODO: make static
				return new System.Guid("804929af-0208-4384-a74d-17353963d105");
			}
		}

	    object AllorsInternal.GetRole(AllorsEmbeddedRelationType relation)
		{
	        switch(relation.Tag)
			{
				case AllorsRelationTags.DomainDeclaredObjectType:
					return this.DomainObjectType;
				case AllorsRelationTags.DomainName:
					return _DomainName;
				case AllorsRelationTags.DomainDeclaredMethodType:
					return this.DomainMethodType;
				case AllorsRelationTags.DomainDeclaredInheritance:
					return this.DomainInheritance;
				case AllorsRelationTags.DomainDerivedUnitObjectType:
					return _DomainDerivedUnitObjectType;
				case AllorsRelationTags.DomainDerivedCompositeObjectType:
					return _DomainDerivedCompositeObjectType;
				case AllorsRelationTags.DomainDeclaredRelationType:
					return this.DomainRelationType;
				case AllorsRelationTags.MetaObjectId:
					return _MetaObjectId;

		default:
				throw new System.ArgumentException("Illegal relation " + relation.Name);
			}
		}

		void AllorsInternal.SetRole(AllorsEmbeddedRelationType relation, object role)
		{
			switch(relation.Tag)
			{
				case AllorsRelationTags.DomainDeclaredObjectType:
					RoleSetDomainDeclaredObjectType((global::Allors.Meta.ObjectType[])role);
					break;
				case AllorsRelationTags.DomainName:
					RoleSetDomainName((global::System.String)role);
					break;
				case AllorsRelationTags.DomainDeclaredMethodType:
					RoleSetDomainDeclaredMethodType((global::Allors.Meta.MethodType[])role);
					break;
				case AllorsRelationTags.DomainDeclaredInheritance:
					RoleSetDomainDeclaredInheritance((global::Allors.Meta.Inheritance[])role);
					break;
				case AllorsRelationTags.DomainDerivedUnitObjectType:
					RoleSetDomainDerivedUnitObjectType((global::Allors.Meta.ObjectType[])role);
					break;
				case AllorsRelationTags.DomainDerivedCompositeObjectType:
					RoleSetDomainDerivedCompositeObjectType((global::Allors.Meta.ObjectType[])role);
					break;
				case AllorsRelationTags.DomainDeclaredRelationType:
					RoleSetDomainDeclaredRelationType((global::Allors.Meta.RelationType[])role);
					break;
				case AllorsRelationTags.MetaObjectId:
					RoleSetMetaObjectId((global::System.Guid)role);
					break;

		default:
				throw new System.ArgumentException("Illegal relation " + relation.Name);
			}
		}


		public virtual global::Allors.Meta.ObjectType[] ObjectTypes
		{
			get
			{
			    return this.DomainObjectType;
			}

			set
			{ 
				AllorsAssert(value);
				RoleSetDomainDeclaredObjectType(value);
			}
		}

		protected void RoleSetDomainDeclaredObjectType(global::Allors.Meta.ObjectType[] roles)
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDeclaredObjectType();
			if( roles != null && roles.Length > 0 )
			{
				foreach( global::Allors.Meta.ObjectType role in roles )
				{
					RoleAddDeclaredObjectType(role);
				}
			}
		}

		public virtual void AddObjectType( global::Allors.Meta.ObjectType addRole )
		{
			RoleAddDeclaredObjectType( addRole );
		}

		void RoleAddDeclaredObjectType( global::Allors.Meta.ObjectType addRole )
		{
			AllorsAssert(addRole);
			if( addRole != null )
			{
				((AllorsInternalObjectType)addRole).AllorsRoleReleaseDeclaredObjectTypeDomain();
				if( !AllorsEmbeddedArrays.Exist( this.DomainObjectType, addRole ) ) 
				{
					// association side
					this.DomainObjectType = (global::Allors.Meta.ObjectType[]) AllorsEmbeddedArrays.Add( this.DomainObjectType, addRole );
					// role side
					((AllorsInternalObjectType)addRole).AllorsRoleSyncSetDeclaredObjectTypeDomain( (global::Allors.Meta.Domain) this ); 
				}
			}
		}

		public virtual void RemoveDeclaredObjectType( global::Allors.Meta.ObjectType removeRole ) 
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDeclaredObjectType( removeRole );
		}

		void AllorsInternalDomain.AllorsRemoveDomainDeclaredObjectType( global::Allors.Meta.ObjectType removeRole ) 
		{
			AllorsAssert(removeRole);
			if( removeRole != null )
			{
				if(AllorsEmbeddedArrays.Exist( this.DomainObjectType, removeRole ) ) 
				{
					this.DomainObjectType = (global::Allors.Meta.ObjectType[]) AllorsEmbeddedArrays.Remove( this.DomainObjectType, removeRole );
					// role side
					((AllorsInternalObjectType)removeRole).AllorsRoleSyncSetDeclaredObjectTypeDomain( null ); 
			
				}
			}
		}

		public virtual void RemoveDeclaredObjectTypes()
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDeclaredObjectType();
		}

		void AllorsInternalDomain.AllorsRemoveDomainDeclaredObjectType()
		{
		    if( this.DomainObjectType!=null )
			{
				foreach( global::Allors.Meta.ObjectType role in this.DomainObjectType ) 
				{
					// role side
					((AllorsInternalObjectType)role).AllorsRoleSyncSetDeclaredObjectTypeDomain( null ); 
				}
			}
			this.DomainObjectType = AllorsEmbeddedArrays.EMPTY_ObjectType_ARRAY;
		}

		public virtual bool ExistDeclaredObjectTypes
		{
			get
			{
				return this.DomainObjectType.Length > 0;
			}
		}


		public global::System.String Name
		{
			get
			{
			    return (global::System.String)_DomainName;
			}

			set
			{
			    RoleSetDomainName(value);
			}
		}

		protected void RoleSetDomainName(global::System.String role)
		{
			_DomainName = role;
		}

		public virtual bool ExistName
		{
			get
			{
				return _DomainName != null;
			}
		}

		public virtual void RemoveName()
		{
			_DomainName = null;
		}

		public virtual global::Allors.Meta.MethodType[] MethodTypes
		{
			get
			{
			    return this.DomainMethodType;
			}

			set
			{ 
				AllorsAssert(value);
				RoleSetDomainDeclaredMethodType(value);
			}
		}

		protected void RoleSetDomainDeclaredMethodType(global::Allors.Meta.MethodType[] roles)
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDeclaredMethodType();
			if( roles != null && roles.Length > 0 )
			{
				foreach( global::Allors.Meta.MethodType role in roles )
				{
					RoleAddDeclaredMethodType(role);
				}
			}
		}

		public virtual void AddMethodType( global::Allors.Meta.MethodType addRole )
		{
			RoleAddDeclaredMethodType( addRole );
		}

		void RoleAddDeclaredMethodType( global::Allors.Meta.MethodType addRole )
		{
			AllorsAssert(addRole);
			if( addRole != null )
			{
				((AllorsInternalMethodType)addRole).AllorsRoleReleaseDeclaredMethodTypeDomain();
				if( !AllorsEmbeddedArrays.Exist( this.DomainMethodType, addRole ) ) 
				{
					// association side
					this.DomainMethodType = (global::Allors.Meta.MethodType[]) AllorsEmbeddedArrays.Add( this.DomainMethodType, addRole );
					// role side
					((AllorsInternalMethodType)addRole).AllorsRoleSyncSetDeclaredMethodTypeDomain( (global::Allors.Meta.Domain) this ); 
				}
			}
		}

		public virtual void RemoveDeclaredMethodType( global::Allors.Meta.MethodType removeRole ) 
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDeclaredMethodType( removeRole );
		}

		void AllorsInternalDomain.AllorsRemoveDomainDeclaredMethodType( global::Allors.Meta.MethodType removeRole ) 
		{
			AllorsAssert(removeRole);
			if( removeRole != null )
			{
				if(AllorsEmbeddedArrays.Exist( this.DomainMethodType, removeRole ) ) 
				{
					this.DomainMethodType = (global::Allors.Meta.MethodType[]) AllorsEmbeddedArrays.Remove( this.DomainMethodType, removeRole );
					// role side
					((AllorsInternalMethodType)removeRole).AllorsRoleSyncSetDeclaredMethodTypeDomain( null ); 
			
				}
			}
		}

		public virtual void RemoveDeclaredMethodTypes()
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDeclaredMethodType();
		}

		void AllorsInternalDomain.AllorsRemoveDomainDeclaredMethodType()
		{
		    if( this.DomainMethodType!=null )
			{
				foreach( global::Allors.Meta.MethodType role in this.DomainMethodType ) 
				{
					// role side
					((AllorsInternalMethodType)role).AllorsRoleSyncSetDeclaredMethodTypeDomain( null ); 
				}
			}
			this.DomainMethodType = AllorsEmbeddedArrays.EMPTY_MethodType_ARRAY;
		}

		public virtual bool ExistDeclaredMethodTypes
		{
			get
			{
				return this.DomainMethodType.Length > 0;
			}
		}


		public virtual global::Allors.Meta.Inheritance[] Inheritances
		{
			get
			{
			    return this.DomainInheritance;
			}

			set
			{ 
				AllorsAssert(value);
				RoleSetDomainDeclaredInheritance(value);
			}
		}

		protected void RoleSetDomainDeclaredInheritance(global::Allors.Meta.Inheritance[] roles)
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDeclaredInheritance();
			if( roles != null && roles.Length > 0 )
			{
				foreach( global::Allors.Meta.Inheritance role in roles )
				{
					RoleAddDeclaredInheritance(role);
				}
			}
		}

		public virtual void AddInheritance( global::Allors.Meta.Inheritance addRole )
		{
			RoleAddDeclaredInheritance( addRole );
		}

		void RoleAddDeclaredInheritance( global::Allors.Meta.Inheritance addRole )
		{
			AllorsAssert(addRole);
			if( addRole != null )
			{
				((AllorsInternalInheritance)addRole).AllorsRoleReleaseDeclaredInheritanceDomain();
				if( !AllorsEmbeddedArrays.Exist( this.DomainInheritance, addRole ) ) 
				{
					// association side
					this.DomainInheritance = (global::Allors.Meta.Inheritance[]) AllorsEmbeddedArrays.Add( this.DomainInheritance, addRole );
					// role side
					((AllorsInternalInheritance)addRole).AllorsRoleSyncSetDeclaredInheritanceDomain( (global::Allors.Meta.Domain) this ); 
				}
			}
		}

		public virtual void RemoveDeclaredInheritance( global::Allors.Meta.Inheritance removeRole ) 
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDeclaredInheritance( removeRole );
		}

		void AllorsInternalDomain.AllorsRemoveDomainDeclaredInheritance( global::Allors.Meta.Inheritance removeRole ) 
		{
			AllorsAssert(removeRole);
			if( removeRole != null )
			{
				if(AllorsEmbeddedArrays.Exist( this.DomainInheritance, removeRole ) ) 
				{
					this.DomainInheritance = (global::Allors.Meta.Inheritance[]) AllorsEmbeddedArrays.Remove( this.DomainInheritance, removeRole );
					// role side
					((AllorsInternalInheritance)removeRole).AllorsRoleSyncSetDeclaredInheritanceDomain( null ); 
			
				}
			}
		}

		public virtual void RemoveDeclaredInheritances()
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDeclaredInheritance();
		}

		void AllorsInternalDomain.AllorsRemoveDomainDeclaredInheritance()
		{
		    if( this.DomainInheritance!=null )
			{
				foreach( global::Allors.Meta.Inheritance role in this.DomainInheritance ) 
				{
					// role side
					((AllorsInternalInheritance)role).AllorsRoleSyncSetDeclaredInheritanceDomain( null ); 
				}
			}
			this.DomainInheritance = AllorsEmbeddedArrays.EMPTY_Inheritance_ARRAY;
		}

		public virtual bool ExistDeclaredInheritances
		{
			get
			{
				return this.DomainInheritance.Length > 0;
			}
		}


		public virtual global::Allors.Meta.ObjectType[] DerivedUnitObjectTypes
		{
			get
			{
			    return _DomainDerivedUnitObjectType;
			}

			set
			{ 
				AllorsAssert(value);
				RoleSetDomainDerivedUnitObjectType(value);
			}
		}

		protected void RoleSetDomainDerivedUnitObjectType(global::Allors.Meta.ObjectType[] roles)
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDerivedUnitObjectType();
			if( roles != null && roles.Length > 0 )
			{
				foreach( global::Allors.Meta.ObjectType role in roles )
				{
					RoleAddDerivedUnitObjectType(role);
				}
			}
		}

		public virtual void AddDerivedUnitObjectType( global::Allors.Meta.ObjectType addRole )
		{
			RoleAddDerivedUnitObjectType( addRole );
		}

		void RoleAddDerivedUnitObjectType( global::Allors.Meta.ObjectType addRole )
		{
			AllorsAssert(addRole);
			if( addRole != null )
			{
				if( !AllorsEmbeddedArrays.Exist( _DomainDerivedUnitObjectType, addRole ) ) 
				{
					// association side
					_DomainDerivedUnitObjectType = (global::Allors.Meta.ObjectType[]) AllorsEmbeddedArrays.Add( _DomainDerivedUnitObjectType, addRole );
					// role side
					((AllorsInternalObjectType)addRole).AllorsRoleSyncAddDerivedUnitObjectTypeDomain( (global::Allors.Meta.Domain) this ); 
				}
			}
		}

		public virtual void RemoveDerivedUnitObjectType( global::Allors.Meta.ObjectType removeRole ) 
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDerivedUnitObjectType( removeRole );
		}

		void AllorsInternalDomain.AllorsRemoveDomainDerivedUnitObjectType( global::Allors.Meta.ObjectType removeRole ) 
		{
			AllorsAssert(removeRole);
			if( removeRole != null )
			{
				if(AllorsEmbeddedArrays.Exist( _DomainDerivedUnitObjectType, removeRole ) ) 
				{
					_DomainDerivedUnitObjectType = (global::Allors.Meta.ObjectType[]) AllorsEmbeddedArrays.Remove( _DomainDerivedUnitObjectType, removeRole );
					// role side
					((AllorsInternalObjectType)removeRole).AllorsRoleSyncRemoveDerivedUnitObjectTypeDomain( (global::Allors.Meta.Domain) this ); 
			
				}
			}
		}

		public virtual void RemoveDerivedUnitObjectTypes()
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDerivedUnitObjectType();
		}

		void AllorsInternalDomain.AllorsRemoveDomainDerivedUnitObjectType()
		{
		    if( _DomainDerivedUnitObjectType!=null )
			{
				foreach( global::Allors.Meta.ObjectType role in _DomainDerivedUnitObjectType ) 
				{
					// role side
					((AllorsInternalObjectType)role).AllorsRoleSyncRemoveDerivedUnitObjectTypeDomain( (global::Allors.Meta.Domain) this ); 
				}
			}
			_DomainDerivedUnitObjectType = AllorsEmbeddedArrays.EMPTY_ObjectType_ARRAY;
		}

		public virtual bool ExistDerivedUnitObjectTypes
		{
			get
			{
				return _DomainDerivedUnitObjectType.Length > 0;
			}
		}


		public virtual global::Allors.Meta.ObjectType[] DerivedCompositeObjectTypes
		{
			get
			{
			    return _DomainDerivedCompositeObjectType;
			}

			set
			{ 
				AllorsAssert(value);
				RoleSetDomainDerivedCompositeObjectType(value);
			}
		}

		protected void RoleSetDomainDerivedCompositeObjectType(global::Allors.Meta.ObjectType[] roles)
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDerivedCompositeObjectType();
			if( roles != null && roles.Length > 0 )
			{
				foreach( global::Allors.Meta.ObjectType role in roles )
				{
					RoleAddDerivedCompositeObjectType(role);
				}
			}
		}

		public virtual void AddDerivedCompositeObjectType( global::Allors.Meta.ObjectType addRole )
		{
			RoleAddDerivedCompositeObjectType( addRole );
		}

		void RoleAddDerivedCompositeObjectType( global::Allors.Meta.ObjectType addRole )
		{
			AllorsAssert(addRole);
			if( addRole != null )
			{
				if( !AllorsEmbeddedArrays.Exist( _DomainDerivedCompositeObjectType, addRole ) ) 
				{
					// association side
					_DomainDerivedCompositeObjectType = (global::Allors.Meta.ObjectType[]) AllorsEmbeddedArrays.Add( _DomainDerivedCompositeObjectType, addRole );
					// role side
					((AllorsInternalObjectType)addRole).AllorsRoleSyncAddDerivedCompositeObjectTypeDomain( (global::Allors.Meta.Domain) this ); 
				}
			}
		}

		public virtual void RemoveDerivedCompositeObjectType( global::Allors.Meta.ObjectType removeRole ) 
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDerivedCompositeObjectType( removeRole );
		}

		void AllorsInternalDomain.AllorsRemoveDomainDerivedCompositeObjectType( global::Allors.Meta.ObjectType removeRole ) 
		{
			AllorsAssert(removeRole);
			if( removeRole != null )
			{
				if(AllorsEmbeddedArrays.Exist( _DomainDerivedCompositeObjectType, removeRole ) ) 
				{
					_DomainDerivedCompositeObjectType = (global::Allors.Meta.ObjectType[]) AllorsEmbeddedArrays.Remove( _DomainDerivedCompositeObjectType, removeRole );
					// role side
					((AllorsInternalObjectType)removeRole).AllorsRoleSyncRemoveDerivedCompositeObjectTypeDomain( (global::Allors.Meta.Domain) this ); 
			
				}
			}
		}

		public virtual void RemoveDerivedCompositeObjectTypes()
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDerivedCompositeObjectType();
		}

		void AllorsInternalDomain.AllorsRemoveDomainDerivedCompositeObjectType()
		{
		    if( _DomainDerivedCompositeObjectType!=null )
			{
				foreach( global::Allors.Meta.ObjectType role in _DomainDerivedCompositeObjectType ) 
				{
					// role side
					((AllorsInternalObjectType)role).AllorsRoleSyncRemoveDerivedCompositeObjectTypeDomain( (global::Allors.Meta.Domain) this ); 
				}
			}
			_DomainDerivedCompositeObjectType = AllorsEmbeddedArrays.EMPTY_ObjectType_ARRAY;
		}

		public virtual bool ExistDerivedCompositeObjectTypes
		{
			get
			{
				return _DomainDerivedCompositeObjectType.Length > 0;
			}
		}


		public virtual global::Allors.Meta.RelationType[] RelationTypes
		{
			get
			{
			    return this.DomainRelationType;
			}

			set
			{ 
				AllorsAssert(value);
				RoleSetDomainDeclaredRelationType(value);
			}
		}

		protected void RoleSetDomainDeclaredRelationType(global::Allors.Meta.RelationType[] roles)
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDeclaredRelationType();
			if( roles != null && roles.Length > 0 )
			{
				foreach( global::Allors.Meta.RelationType role in roles )
				{
					RoleAddDeclaredRelationType(role);
				}
			}
		}

		public virtual void AddRelationType( global::Allors.Meta.RelationType addRole )
		{
			RoleAddDeclaredRelationType( addRole );
		}

		void RoleAddDeclaredRelationType( global::Allors.Meta.RelationType addRole )
		{
			AllorsAssert(addRole);
			if( addRole != null )
			{
				((AllorsInternalRelationType)addRole).AllorsRoleReleaseDeclaredRelationTypeDomain();
				if( !AllorsEmbeddedArrays.Exist( this.DomainRelationType, addRole ) ) 
				{
					// association side
					this.DomainRelationType = (global::Allors.Meta.RelationType[]) AllorsEmbeddedArrays.Add( this.DomainRelationType, addRole );
					// role side
					((AllorsInternalRelationType)addRole).AllorsRoleSyncSetDeclaredRelationTypeDomain( (global::Allors.Meta.Domain) this ); 
				}
			}
		}

		public virtual void RemoveDeclaredRelationType( global::Allors.Meta.RelationType removeRole ) 
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDeclaredRelationType( removeRole );
		}

		void AllorsInternalDomain.AllorsRemoveDomainDeclaredRelationType( global::Allors.Meta.RelationType removeRole ) 
		{
			AllorsAssert(removeRole);
			if( removeRole != null )
			{
				if(AllorsEmbeddedArrays.Exist( this.DomainRelationType, removeRole ) ) 
				{
					this.DomainRelationType = (global::Allors.Meta.RelationType[]) AllorsEmbeddedArrays.Remove( this.DomainRelationType, removeRole );
					// role side
					((AllorsInternalRelationType)removeRole).AllorsRoleSyncSetDeclaredRelationTypeDomain( null ); 
			
				}
			}
		}

		public virtual void RemoveDeclaredRelationTypes()
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDeclaredRelationType();
		}

		void AllorsInternalDomain.AllorsRemoveDomainDeclaredRelationType()
		{
		    if( this.DomainRelationType!=null )
			{
				foreach( global::Allors.Meta.RelationType role in this.DomainRelationType ) 
				{
					// role side
					((AllorsInternalRelationType)role).AllorsRoleSyncSetDeclaredRelationTypeDomain( null ); 
				}
			}
			this.DomainRelationType = AllorsEmbeddedArrays.EMPTY_RelationType_ARRAY;
		}

		public virtual bool ExistDeclaredRelationTypes
		{
			get
			{
				return this.DomainRelationType.Length > 0;
			}
		}

    }
}