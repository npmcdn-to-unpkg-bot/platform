namespace Allors.Meta.AllorsGenerated
{
	internal interface AllorsInternalDomain : AllorsInternalMetaObject, AllorsInternal
	{
		global::Allors.Meta.ObjectType[] MetaObjects
		{
			get;
			set;
		}

		void AddDeclaredObjectType( global::Allors.Meta.ObjectType addRole );

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


		global::Allors.Meta.MethodType[] DeclaredMethodTypes
		{
			get;
			set;
		}

		void AddDeclaredMethodType( global::Allors.Meta.MethodType addRole );

		void RemoveDeclaredMethodType( global::Allors.Meta.MethodType removeRole );

		void RemoveDeclaredMethodTypes();

		bool ExistDeclaredMethodTypes
		{
			get;
		}
		void AllorsRemoveDomainDeclaredMethodType();

		void AllorsRemoveDomainDeclaredMethodType( global::Allors.Meta.MethodType role );



		global::Allors.Meta.Inheritance[] DeclaredInheritances
		{
			get;
			set;
		}

		void AddDeclaredInheritance( global::Allors.Meta.Inheritance addRole );

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


		global::Allors.Meta.RelationType[] DeclaredRelationTypes
		{
			get;
			set;
		}

		void AddDeclaredRelationType( global::Allors.Meta.RelationType addRole );

		void RemoveDeclaredRelationType( global::Allors.Meta.RelationType removeRole );

		void RemoveDeclaredRelationTypes();

		bool ExistDeclaredRelationTypes
		{
			get;
		}
		void AllorsRemoveDomainDeclaredRelationType();

		void AllorsRemoveDomainDeclaredRelationType( global::Allors.Meta.RelationType role );


		global::Allors.Meta.RelationType[] DerivedRelationTypes
		{
			get;
			set;
		}

		void AddDerivedRelationType( global::Allors.Meta.RelationType addRole );

		void RemoveDerivedRelationType( global::Allors.Meta.RelationType removeRole );

		void RemoveDerivedRelationTypes();

		bool ExistDerivedRelationTypes
		{
			get;
		}
		void AllorsRemoveDomainDerivedRelationType();

		void AllorsRemoveDomainDerivedRelationType( global::Allors.Meta.RelationType role );


		global::Allors.Meta.MethodType[] DerivedMethodTypes
		{
			get;
			set;
		}

		void AddDerivedMethodType( global::Allors.Meta.MethodType addRole );

		void RemoveDerivedMethodType( global::Allors.Meta.MethodType removeRole );

		void RemoveDerivedMethodTypes();

		bool ExistDerivedMethodTypes
		{
			get;
		}
		void AllorsRemoveDomainDerivedMethodType();

		void AllorsRemoveDomainDerivedMethodType( global::Allors.Meta.MethodType role );


		global::Allors.Meta.Inheritance[] DerivedInheritances
		{
			get;
			set;
		}

		void AddDerivedInheritance( global::Allors.Meta.Inheritance addRole );

		void RemoveDerivedInheritance( global::Allors.Meta.Inheritance removeRole );

		void RemoveDerivedInheritances();

		bool ExistDerivedInheritances
		{
			get;
		}
		void AllorsRemoveDomainDerivedInheritance();

		void AllorsRemoveDomainDerivedInheritance( global::Allors.Meta.Inheritance role );


	}

	public interface AllorsInterfaceDomain :  AllorsEmbeddedObject 
	{
	}

	[System.Diagnostics.DebuggerNonUserCode]
	public abstract class AllorsClassDomain :  global::Allors.Meta.MetaBase,  AllorsInternalDomain , AllorsEmbeddedObject
	{
		protected global::Allors.Meta.ObjectType[] DomainMetaObject = AllorsEmbeddedArrays.EMPTY_ObjectType_ARRAY;


		protected System.Object _DomainName;


		protected global::Allors.Meta.MethodType[] _DomainDeclaredMethodType = AllorsEmbeddedArrays.EMPTY_MethodType_ARRAY;


		protected global::Allors.Meta.Inheritance[] _DomainDeclaredInheritance = AllorsEmbeddedArrays.EMPTY_Inheritance_ARRAY;


		protected global::Allors.Meta.ObjectType[] _DomainDerivedUnitObjectType = AllorsEmbeddedArrays.EMPTY_ObjectType_ARRAY;


		protected global::Allors.Meta.ObjectType[] _DomainDerivedCompositeObjectType = AllorsEmbeddedArrays.EMPTY_ObjectType_ARRAY;


		protected global::Allors.Meta.RelationType[] _DomainDeclaredRelationType = AllorsEmbeddedArrays.EMPTY_RelationType_ARRAY;


		protected global::Allors.Meta.RelationType[] _DomainDerivedRelationType = AllorsEmbeddedArrays.EMPTY_RelationType_ARRAY;


		protected global::Allors.Meta.MethodType[] _DomainDerivedMethodType = AllorsEmbeddedArrays.EMPTY_MethodType_ARRAY;


		protected global::Allors.Meta.Inheritance[] _DomainDerivedInheritance = AllorsEmbeddedArrays.EMPTY_Inheritance_ARRAY;


		protected global::Allors.Meta.ObjectType[] _DomainDerivedObjectType = AllorsEmbeddedArrays.EMPTY_ObjectType_ARRAY;


		protected global::Allors.Meta.Domain[] _DerivedSuperDomainDomain = AllorsEmbeddedArrays.EMPTY_Domain_ARRAY;


		protected global::Allors.Meta.Domain[] _DirectSuperDomainDomain = AllorsEmbeddedArrays.EMPTY_Domain_ARRAY;


		protected global::Allors.Meta.Domain[] _UnitDomainDomain = AllorsEmbeddedArrays.EMPTY_Domain_ARRAY;




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
					return this.DomainMetaObject;
				case AllorsRelationTags.DomainName:
					return _DomainName;
				case AllorsRelationTags.DomainDeclaredMethodType:
					return _DomainDeclaredMethodType;
				case AllorsRelationTags.DomainDeclaredInheritance:
					return _DomainDeclaredInheritance;
				case AllorsRelationTags.DomainDerivedUnitObjectType:
					return _DomainDerivedUnitObjectType;
				case AllorsRelationTags.DomainDerivedCompositeObjectType:
					return _DomainDerivedCompositeObjectType;
				case AllorsRelationTags.DomainDeclaredRelationType:
					return _DomainDeclaredRelationType;
				case AllorsRelationTags.DomainDerivedRelationType:
					return _DomainDerivedRelationType;
				case AllorsRelationTags.DomainDerivedMethodType:
					return _DomainDerivedMethodType;
				case AllorsRelationTags.DomainDerivedInheritance:
					return _DomainDerivedInheritance;
				case AllorsRelationTags.DomainDerivedObjectType:
					return _DomainDerivedObjectType;
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
				case AllorsRelationTags.DomainDerivedRelationType:
					RoleSetDomainDerivedRelationType((global::Allors.Meta.RelationType[])role);
					break;
				case AllorsRelationTags.DomainDerivedMethodType:
					RoleSetDomainDerivedMethodType((global::Allors.Meta.MethodType[])role);
					break;
				case AllorsRelationTags.DomainDerivedInheritance:
					RoleSetDomainDerivedInheritance((global::Allors.Meta.Inheritance[])role);
					break;
				case AllorsRelationTags.MetaObjectId:
					RoleSetMetaObjectId((global::System.Guid)role);
					break;

		default:
				throw new System.ArgumentException("Illegal relation " + relation.Name);
			}
		}


		public virtual global::Allors.Meta.ObjectType[] MetaObjects
		{
			get
			{
			    return this.DomainMetaObject;
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

		public virtual void AddDeclaredObjectType( global::Allors.Meta.ObjectType addRole )
		{
			RoleAddDeclaredObjectType( addRole );
		}

		void RoleAddDeclaredObjectType( global::Allors.Meta.ObjectType addRole )
		{
			AllorsAssert(addRole);
			if( addRole != null )
			{
				((AllorsInternalObjectType)addRole).AllorsRoleReleaseDeclaredObjectTypeDomain();
				if( !AllorsEmbeddedArrays.Exist( this.DomainMetaObject, addRole ) ) 
				{
					// association side
					this.DomainMetaObject = (global::Allors.Meta.ObjectType[]) AllorsEmbeddedArrays.Add( this.DomainMetaObject, addRole );
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
				if(AllorsEmbeddedArrays.Exist( this.DomainMetaObject, removeRole ) ) 
				{
					this.DomainMetaObject = (global::Allors.Meta.ObjectType[]) AllorsEmbeddedArrays.Remove( this.DomainMetaObject, removeRole );
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
		    if( this.DomainMetaObject!=null )
			{
				foreach( global::Allors.Meta.ObjectType role in this.DomainMetaObject ) 
				{
					// role side
					((AllorsInternalObjectType)role).AllorsRoleSyncSetDeclaredObjectTypeDomain( null ); 
				}
			}
			this.DomainMetaObject = AllorsEmbeddedArrays.EMPTY_ObjectType_ARRAY;
		}

		public virtual bool ExistDeclaredObjectTypes
		{
			get
			{
				return this.DomainMetaObject.Length > 0;
			}
		}


		public virtual global::System.String Name
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

		public virtual global::Allors.Meta.MethodType[] DeclaredMethodTypes
		{
			get
			{
			    return _DomainDeclaredMethodType;
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

		public virtual void AddDeclaredMethodType( global::Allors.Meta.MethodType addRole )
		{
			RoleAddDeclaredMethodType( addRole );
		}

		void RoleAddDeclaredMethodType( global::Allors.Meta.MethodType addRole )
		{
			AllorsAssert(addRole);
			if( addRole != null )
			{
				((AllorsInternalMethodType)addRole).AllorsRoleReleaseDeclaredMethodTypeDomain();
				if( !AllorsEmbeddedArrays.Exist( _DomainDeclaredMethodType, addRole ) ) 
				{
					// association side
					_DomainDeclaredMethodType = (global::Allors.Meta.MethodType[]) AllorsEmbeddedArrays.Add( _DomainDeclaredMethodType, addRole );
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
				if(AllorsEmbeddedArrays.Exist( _DomainDeclaredMethodType, removeRole ) ) 
				{
					_DomainDeclaredMethodType = (global::Allors.Meta.MethodType[]) AllorsEmbeddedArrays.Remove( _DomainDeclaredMethodType, removeRole );
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
		    if( _DomainDeclaredMethodType!=null )
			{
				foreach( global::Allors.Meta.MethodType role in _DomainDeclaredMethodType ) 
				{
					// role side
					((AllorsInternalMethodType)role).AllorsRoleSyncSetDeclaredMethodTypeDomain( null ); 
				}
			}
			_DomainDeclaredMethodType = AllorsEmbeddedArrays.EMPTY_MethodType_ARRAY;
		}

		public virtual bool ExistDeclaredMethodTypes
		{
			get
			{
				return _DomainDeclaredMethodType.Length > 0;
			}
		}


		public virtual global::Allors.Meta.Inheritance[] DeclaredInheritances
		{
			get
			{
			    return _DomainDeclaredInheritance;
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

		public virtual void AddDeclaredInheritance( global::Allors.Meta.Inheritance addRole )
		{
			RoleAddDeclaredInheritance( addRole );
		}

		void RoleAddDeclaredInheritance( global::Allors.Meta.Inheritance addRole )
		{
			AllorsAssert(addRole);
			if( addRole != null )
			{
				((AllorsInternalInheritance)addRole).AllorsRoleReleaseDeclaredInheritanceDomain();
				if( !AllorsEmbeddedArrays.Exist( _DomainDeclaredInheritance, addRole ) ) 
				{
					// association side
					_DomainDeclaredInheritance = (global::Allors.Meta.Inheritance[]) AllorsEmbeddedArrays.Add( _DomainDeclaredInheritance, addRole );
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
				if(AllorsEmbeddedArrays.Exist( _DomainDeclaredInheritance, removeRole ) ) 
				{
					_DomainDeclaredInheritance = (global::Allors.Meta.Inheritance[]) AllorsEmbeddedArrays.Remove( _DomainDeclaredInheritance, removeRole );
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
		    if( _DomainDeclaredInheritance!=null )
			{
				foreach( global::Allors.Meta.Inheritance role in _DomainDeclaredInheritance ) 
				{
					// role side
					((AllorsInternalInheritance)role).AllorsRoleSyncSetDeclaredInheritanceDomain( null ); 
				}
			}
			_DomainDeclaredInheritance = AllorsEmbeddedArrays.EMPTY_Inheritance_ARRAY;
		}

		public virtual bool ExistDeclaredInheritances
		{
			get
			{
				return _DomainDeclaredInheritance.Length > 0;
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


		public virtual global::Allors.Meta.RelationType[] DeclaredRelationTypes
		{
			get
			{
			    return _DomainDeclaredRelationType;
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

		public virtual void AddDeclaredRelationType( global::Allors.Meta.RelationType addRole )
		{
			RoleAddDeclaredRelationType( addRole );
		}

		void RoleAddDeclaredRelationType( global::Allors.Meta.RelationType addRole )
		{
			AllorsAssert(addRole);
			if( addRole != null )
			{
				((AllorsInternalRelationType)addRole).AllorsRoleReleaseDeclaredRelationTypeDomain();
				if( !AllorsEmbeddedArrays.Exist( _DomainDeclaredRelationType, addRole ) ) 
				{
					// association side
					_DomainDeclaredRelationType = (global::Allors.Meta.RelationType[]) AllorsEmbeddedArrays.Add( _DomainDeclaredRelationType, addRole );
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
				if(AllorsEmbeddedArrays.Exist( _DomainDeclaredRelationType, removeRole ) ) 
				{
					_DomainDeclaredRelationType = (global::Allors.Meta.RelationType[]) AllorsEmbeddedArrays.Remove( _DomainDeclaredRelationType, removeRole );
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
		    if( _DomainDeclaredRelationType!=null )
			{
				foreach( global::Allors.Meta.RelationType role in _DomainDeclaredRelationType ) 
				{
					// role side
					((AllorsInternalRelationType)role).AllorsRoleSyncSetDeclaredRelationTypeDomain( null ); 
				}
			}
			_DomainDeclaredRelationType = AllorsEmbeddedArrays.EMPTY_RelationType_ARRAY;
		}

		public virtual bool ExistDeclaredRelationTypes
		{
			get
			{
				return _DomainDeclaredRelationType.Length > 0;
			}
		}


		public virtual global::Allors.Meta.RelationType[] DerivedRelationTypes
		{
			get
			{
			    return _DomainDerivedRelationType;
			}

			set
			{ 
				AllorsAssert(value);
				RoleSetDomainDerivedRelationType(value);
			}
		}

		protected void RoleSetDomainDerivedRelationType(global::Allors.Meta.RelationType[] roles)
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDerivedRelationType();
			if( roles != null && roles.Length > 0 )
			{
				foreach( global::Allors.Meta.RelationType role in roles )
				{
					RoleAddDerivedRelationType(role);
				}
			}
		}

		public virtual void AddDerivedRelationType( global::Allors.Meta.RelationType addRole )
		{
			RoleAddDerivedRelationType( addRole );
		}

		void RoleAddDerivedRelationType( global::Allors.Meta.RelationType addRole )
		{
			AllorsAssert(addRole);
			if( addRole != null )
			{
				if( !AllorsEmbeddedArrays.Exist( _DomainDerivedRelationType, addRole ) ) 
				{
					// association side
					_DomainDerivedRelationType = (global::Allors.Meta.RelationType[]) AllorsEmbeddedArrays.Add( _DomainDerivedRelationType, addRole );
					// role side
					((AllorsInternalRelationType)addRole).AllorsRoleSyncAddDerivedRelationTypeDomain( (global::Allors.Meta.Domain) this ); 
				}
			}
		}

		public virtual void RemoveDerivedRelationType( global::Allors.Meta.RelationType removeRole ) 
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDerivedRelationType( removeRole );
		}

		void AllorsInternalDomain.AllorsRemoveDomainDerivedRelationType( global::Allors.Meta.RelationType removeRole ) 
		{
			AllorsAssert(removeRole);
			if( removeRole != null )
			{
				if(AllorsEmbeddedArrays.Exist( _DomainDerivedRelationType, removeRole ) ) 
				{
					_DomainDerivedRelationType = (global::Allors.Meta.RelationType[]) AllorsEmbeddedArrays.Remove( _DomainDerivedRelationType, removeRole );
					// role side
					((AllorsInternalRelationType)removeRole).AllorsRoleSyncRemoveDerivedRelationTypeDomain( (global::Allors.Meta.Domain) this ); 
			
				}
			}
		}

		public virtual void RemoveDerivedRelationTypes()
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDerivedRelationType();
		}

		void AllorsInternalDomain.AllorsRemoveDomainDerivedRelationType()
		{
		    if( _DomainDerivedRelationType!=null )
			{
				foreach( global::Allors.Meta.RelationType role in _DomainDerivedRelationType ) 
				{
					// role side
					((AllorsInternalRelationType)role).AllorsRoleSyncRemoveDerivedRelationTypeDomain( (global::Allors.Meta.Domain) this ); 
				}
			}
			_DomainDerivedRelationType = AllorsEmbeddedArrays.EMPTY_RelationType_ARRAY;
		}

		public virtual bool ExistDerivedRelationTypes
		{
			get
			{
				return _DomainDerivedRelationType.Length > 0;
			}
		}


		public virtual global::Allors.Meta.MethodType[] DerivedMethodTypes
		{
			get
			{
			    return _DomainDerivedMethodType;
			}

			set
			{ 
				AllorsAssert(value);
				RoleSetDomainDerivedMethodType(value);
			}
		}

		protected void RoleSetDomainDerivedMethodType(global::Allors.Meta.MethodType[] roles)
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDerivedMethodType();
			if( roles != null && roles.Length > 0 )
			{
				foreach( global::Allors.Meta.MethodType role in roles )
				{
					RoleAddDerivedMethodType(role);
				}
			}
		}

		public virtual void AddDerivedMethodType( global::Allors.Meta.MethodType addRole )
		{
			RoleAddDerivedMethodType( addRole );
		}

		void RoleAddDerivedMethodType( global::Allors.Meta.MethodType addRole )
		{
			AllorsAssert(addRole);
			if( addRole != null )
			{
				if( !AllorsEmbeddedArrays.Exist( _DomainDerivedMethodType, addRole ) ) 
				{
					// association side
					_DomainDerivedMethodType = (global::Allors.Meta.MethodType[]) AllorsEmbeddedArrays.Add( _DomainDerivedMethodType, addRole );
					// role side
					((AllorsInternalMethodType)addRole).AllorsRoleSyncAddDerivedMethodTypeDomain( (global::Allors.Meta.Domain) this ); 
				}
			}
		}

		public virtual void RemoveDerivedMethodType( global::Allors.Meta.MethodType removeRole ) 
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDerivedMethodType( removeRole );
		}

		void AllorsInternalDomain.AllorsRemoveDomainDerivedMethodType( global::Allors.Meta.MethodType removeRole ) 
		{
			AllorsAssert(removeRole);
			if( removeRole != null )
			{
				if(AllorsEmbeddedArrays.Exist( _DomainDerivedMethodType, removeRole ) ) 
				{
					_DomainDerivedMethodType = (global::Allors.Meta.MethodType[]) AllorsEmbeddedArrays.Remove( _DomainDerivedMethodType, removeRole );
					// role side
					((AllorsInternalMethodType)removeRole).AllorsRoleSyncRemoveDerivedMethodTypeDomain( (global::Allors.Meta.Domain) this ); 
			
				}
			}
		}

		public virtual void RemoveDerivedMethodTypes()
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDerivedMethodType();
		}

		void AllorsInternalDomain.AllorsRemoveDomainDerivedMethodType()
		{
		    if( _DomainDerivedMethodType!=null )
			{
				foreach( global::Allors.Meta.MethodType role in _DomainDerivedMethodType ) 
				{
					// role side
					((AllorsInternalMethodType)role).AllorsRoleSyncRemoveDerivedMethodTypeDomain( (global::Allors.Meta.Domain) this ); 
				}
			}
			_DomainDerivedMethodType = AllorsEmbeddedArrays.EMPTY_MethodType_ARRAY;
		}

		public virtual bool ExistDerivedMethodTypes
		{
			get
			{
				return _DomainDerivedMethodType.Length > 0;
			}
		}


		public virtual global::Allors.Meta.Inheritance[] DerivedInheritances
		{
			get
			{
			    return _DomainDerivedInheritance;
			}

			set
			{ 
				AllorsAssert(value);
				RoleSetDomainDerivedInheritance(value);
			}
		}

		protected void RoleSetDomainDerivedInheritance(global::Allors.Meta.Inheritance[] roles)
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDerivedInheritance();
			if( roles != null && roles.Length > 0 )
			{
				foreach( global::Allors.Meta.Inheritance role in roles )
				{
					RoleAddDerivedInheritance(role);
				}
			}
		}

		public virtual void AddDerivedInheritance( global::Allors.Meta.Inheritance addRole )
		{
			RoleAddDerivedInheritance( addRole );
		}

		void RoleAddDerivedInheritance( global::Allors.Meta.Inheritance addRole )
		{
			AllorsAssert(addRole);
			if( addRole != null )
			{
				if( !AllorsEmbeddedArrays.Exist( _DomainDerivedInheritance, addRole ) ) 
				{
					// association side
					_DomainDerivedInheritance = (global::Allors.Meta.Inheritance[]) AllorsEmbeddedArrays.Add( _DomainDerivedInheritance, addRole );
					// role side
					((AllorsInternalInheritance)addRole).AllorsRoleSyncAddDerivedInheritanceDomain( (global::Allors.Meta.Domain) this ); 
				}
			}
		}

		public virtual void RemoveDerivedInheritance( global::Allors.Meta.Inheritance removeRole ) 
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDerivedInheritance( removeRole );
		}

		void AllorsInternalDomain.AllorsRemoveDomainDerivedInheritance( global::Allors.Meta.Inheritance removeRole ) 
		{
			AllorsAssert(removeRole);
			if( removeRole != null )
			{
				if(AllorsEmbeddedArrays.Exist( _DomainDerivedInheritance, removeRole ) ) 
				{
					_DomainDerivedInheritance = (global::Allors.Meta.Inheritance[]) AllorsEmbeddedArrays.Remove( _DomainDerivedInheritance, removeRole );
					// role side
					((AllorsInternalInheritance)removeRole).AllorsRoleSyncRemoveDerivedInheritanceDomain( (global::Allors.Meta.Domain) this ); 
			
				}
			}
		}

		public virtual void RemoveDerivedInheritances()
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDerivedInheritance();
		}

		void AllorsInternalDomain.AllorsRemoveDomainDerivedInheritance()
		{
		    if( _DomainDerivedInheritance!=null )
			{
				foreach( global::Allors.Meta.Inheritance role in _DomainDerivedInheritance ) 
				{
					// role side
					((AllorsInternalInheritance)role).AllorsRoleSyncRemoveDerivedInheritanceDomain( (global::Allors.Meta.Domain) this ); 
				}
			}
			_DomainDerivedInheritance = AllorsEmbeddedArrays.EMPTY_Inheritance_ARRAY;
		}

		public virtual bool ExistDerivedInheritances
		{
			get
			{
				return _DomainDerivedInheritance.Length > 0;
			}
		}

    }
}