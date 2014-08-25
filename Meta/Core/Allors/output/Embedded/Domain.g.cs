namespace Allors.Meta.AllorsGenerated
{
	internal interface AllorsInternalDomain : AllorsInternalMetaObject, AllorsInternal
	{
		global::Allors.Meta.MetaObject[] MetaObjects
		{
			get;
			set;
		}

		void AddDeclaredObjectType( global::Allors.Meta.MetaObject addRole );

		void RemoveDeclaredObjectType( global::Allors.Meta.MetaObject removeRole );

		void RemoveDeclaredObjectTypes();

		bool ExistDeclaredObjectTypes
		{
			get;
		}
		void AllorsRemoveDomainDeclaredObjectType();

		void AllorsRemoveDomainDeclaredObjectType( global::Allors.Meta.MetaObject role );


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


		global::Allors.Meta.MetaMethod[] DeclaredMethodTypes
		{
			get;
			set;
		}

		void AddDeclaredMethodType( global::Allors.Meta.MetaMethod addRole );

		void RemoveDeclaredMethodType( global::Allors.Meta.MetaMethod removeRole );

		void RemoveDeclaredMethodTypes();

		bool ExistDeclaredMethodTypes
		{
			get;
		}
		void AllorsRemoveDomainDeclaredMethodType();

		void AllorsRemoveDomainDeclaredMethodType( global::Allors.Meta.MetaMethod role );



		global::Allors.Meta.MetaInheritance[] DeclaredInheritances
		{
			get;
			set;
		}

		void AddDeclaredInheritance( global::Allors.Meta.MetaInheritance addRole );

		void RemoveDeclaredInheritance( global::Allors.Meta.MetaInheritance removeRole );

		void RemoveDeclaredInheritances();

		bool ExistDeclaredInheritances
		{
			get;
		}
		void AllorsRemoveDomainDeclaredInheritance();

		void AllorsRemoveDomainDeclaredInheritance( global::Allors.Meta.MetaInheritance role );



		global::Allors.Meta.MetaObject[] DerivedUnitObjectTypes
		{
			get;
			set;
		}

		void AddDerivedUnitObjectType( global::Allors.Meta.MetaObject addRole );

		void RemoveDerivedUnitObjectType( global::Allors.Meta.MetaObject removeRole );

		void RemoveDerivedUnitObjectTypes();

		bool ExistDerivedUnitObjectTypes
		{
			get;
		}
		void AllorsRemoveDomainDerivedUnitObjectType();

		void AllorsRemoveDomainDerivedUnitObjectType( global::Allors.Meta.MetaObject role );


		global::Allors.Meta.MetaObject[] DerivedCompositeObjectTypes
		{
			get;
			set;
		}

		void AddDerivedCompositeObjectType( global::Allors.Meta.MetaObject addRole );

		void RemoveDerivedCompositeObjectType( global::Allors.Meta.MetaObject removeRole );

		void RemoveDerivedCompositeObjectTypes();

		bool ExistDerivedCompositeObjectTypes
		{
			get;
		}
		void AllorsRemoveDomainDerivedCompositeObjectType();

		void AllorsRemoveDomainDerivedCompositeObjectType( global::Allors.Meta.MetaObject role );


		global::Allors.Meta.MetaRelation[] DeclaredRelationTypes
		{
			get;
			set;
		}

		void AddDeclaredRelationType( global::Allors.Meta.MetaRelation addRole );

		void RemoveDeclaredRelationType( global::Allors.Meta.MetaRelation removeRole );

		void RemoveDeclaredRelationTypes();

		bool ExistDeclaredRelationTypes
		{
			get;
		}
		void AllorsRemoveDomainDeclaredRelationType();

		void AllorsRemoveDomainDeclaredRelationType( global::Allors.Meta.MetaRelation role );


		global::Allors.Meta.MetaRelation[] DerivedRelationTypes
		{
			get;
			set;
		}

		void AddDerivedRelationType( global::Allors.Meta.MetaRelation addRole );

		void RemoveDerivedRelationType( global::Allors.Meta.MetaRelation removeRole );

		void RemoveDerivedRelationTypes();

		bool ExistDerivedRelationTypes
		{
			get;
		}
		void AllorsRemoveDomainDerivedRelationType();

		void AllorsRemoveDomainDerivedRelationType( global::Allors.Meta.MetaRelation role );


		global::Allors.Meta.MetaMethod[] DerivedMethodTypes
		{
			get;
			set;
		}

		void AddDerivedMethodType( global::Allors.Meta.MetaMethod addRole );

		void RemoveDerivedMethodType( global::Allors.Meta.MetaMethod removeRole );

		void RemoveDerivedMethodTypes();

		bool ExistDerivedMethodTypes
		{
			get;
		}
		void AllorsRemoveDomainDerivedMethodType();

		void AllorsRemoveDomainDerivedMethodType( global::Allors.Meta.MetaMethod role );


		global::Allors.Meta.MetaInheritance[] DerivedInheritances
		{
			get;
			set;
		}

		void AddDerivedInheritance( global::Allors.Meta.MetaInheritance addRole );

		void RemoveDerivedInheritance( global::Allors.Meta.MetaInheritance removeRole );

		void RemoveDerivedInheritances();

		bool ExistDerivedInheritances
		{
			get;
		}
		void AllorsRemoveDomainDerivedInheritance();

		void AllorsRemoveDomainDerivedInheritance( global::Allors.Meta.MetaInheritance role );


	}

	public interface AllorsInterfaceDomain :  AllorsEmbeddedObject 
	{
	}

	[System.Diagnostics.DebuggerNonUserCode]
	public abstract class AllorsClassDomain :  global::Allors.Meta.MetaBase,  AllorsInternalDomain , AllorsEmbeddedObject
	{
		protected global::Allors.Meta.MetaObject[] DomainMetaObject = AllorsEmbeddedArrays.EMPTY_ObjectType_ARRAY;


		protected System.Object _DomainName;


		protected global::Allors.Meta.MetaMethod[] _DomainDeclaredMethodType = AllorsEmbeddedArrays.EMPTY_MethodType_ARRAY;


		protected global::Allors.Meta.MetaInheritance[] _DomainDeclaredInheritance = AllorsEmbeddedArrays.EMPTY_Inheritance_ARRAY;


		protected global::Allors.Meta.MetaObject[] _DomainDerivedUnitObjectType = AllorsEmbeddedArrays.EMPTY_ObjectType_ARRAY;


		protected global::Allors.Meta.MetaObject[] _DomainDerivedCompositeObjectType = AllorsEmbeddedArrays.EMPTY_ObjectType_ARRAY;


		protected global::Allors.Meta.MetaRelation[] _DomainDeclaredRelationType = AllorsEmbeddedArrays.EMPTY_RelationType_ARRAY;


		protected global::Allors.Meta.MetaRelation[] _DomainDerivedRelationType = AllorsEmbeddedArrays.EMPTY_RelationType_ARRAY;


		protected global::Allors.Meta.MetaMethod[] _DomainDerivedMethodType = AllorsEmbeddedArrays.EMPTY_MethodType_ARRAY;


		protected global::Allors.Meta.MetaInheritance[] _DomainDerivedInheritance = AllorsEmbeddedArrays.EMPTY_Inheritance_ARRAY;


		protected global::Allors.Meta.MetaObject[] _DomainDerivedObjectType = AllorsEmbeddedArrays.EMPTY_ObjectType_ARRAY;


		protected global::Allors.Meta.MetaDomain[] _DerivedSuperDomainDomain = AllorsEmbeddedArrays.EMPTY_Domain_ARRAY;


		protected global::Allors.Meta.MetaDomain[] _DirectSuperDomainDomain = AllorsEmbeddedArrays.EMPTY_Domain_ARRAY;


		protected global::Allors.Meta.MetaDomain[] _UnitDomainDomain = AllorsEmbeddedArrays.EMPTY_Domain_ARRAY;




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
					RoleSetDomainDeclaredObjectType((global::Allors.Meta.MetaObject[])role);
					break;
				case AllorsRelationTags.DomainName:
					RoleSetDomainName((global::System.String)role);
					break;
				case AllorsRelationTags.DomainDeclaredMethodType:
					RoleSetDomainDeclaredMethodType((global::Allors.Meta.MetaMethod[])role);
					break;
				case AllorsRelationTags.DomainDeclaredInheritance:
					RoleSetDomainDeclaredInheritance((global::Allors.Meta.MetaInheritance[])role);
					break;
				case AllorsRelationTags.DomainDerivedUnitObjectType:
					RoleSetDomainDerivedUnitObjectType((global::Allors.Meta.MetaObject[])role);
					break;
				case AllorsRelationTags.DomainDerivedCompositeObjectType:
					RoleSetDomainDerivedCompositeObjectType((global::Allors.Meta.MetaObject[])role);
					break;
				case AllorsRelationTags.DomainDeclaredRelationType:
					RoleSetDomainDeclaredRelationType((global::Allors.Meta.MetaRelation[])role);
					break;
				case AllorsRelationTags.DomainDerivedRelationType:
					RoleSetDomainDerivedRelationType((global::Allors.Meta.MetaRelation[])role);
					break;
				case AllorsRelationTags.DomainDerivedMethodType:
					RoleSetDomainDerivedMethodType((global::Allors.Meta.MetaMethod[])role);
					break;
				case AllorsRelationTags.DomainDerivedInheritance:
					RoleSetDomainDerivedInheritance((global::Allors.Meta.MetaInheritance[])role);
					break;
				case AllorsRelationTags.MetaObjectId:
					RoleSetMetaObjectId((global::System.Guid)role);
					break;

		default:
				throw new System.ArgumentException("Illegal relation " + relation.Name);
			}
		}


		public virtual global::Allors.Meta.MetaObject[] MetaObjects
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

		protected void RoleSetDomainDeclaredObjectType(global::Allors.Meta.MetaObject[] roles)
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDeclaredObjectType();
			if( roles != null && roles.Length > 0 )
			{
				foreach( global::Allors.Meta.MetaObject role in roles )
				{
					RoleAddDeclaredObjectType(role);
				}
			}
		}

		public virtual void AddDeclaredObjectType( global::Allors.Meta.MetaObject addRole )
		{
			RoleAddDeclaredObjectType( addRole );
		}

		void RoleAddDeclaredObjectType( global::Allors.Meta.MetaObject addRole )
		{
			AllorsAssert(addRole);
			if( addRole != null )
			{
				((AllorsInternalObjectType)addRole).AllorsRoleReleaseDeclaredObjectTypeDomain();
				if( !AllorsEmbeddedArrays.Exist( this.DomainMetaObject, addRole ) ) 
				{
					// association side
					this.DomainMetaObject = (global::Allors.Meta.MetaObject[]) AllorsEmbeddedArrays.Add( this.DomainMetaObject, addRole );
					// role side
					((AllorsInternalObjectType)addRole).AllorsRoleSyncSetDeclaredObjectTypeDomain( (global::Allors.Meta.MetaDomain) this ); 
				}
			}
		}

		public virtual void RemoveDeclaredObjectType( global::Allors.Meta.MetaObject removeRole ) 
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDeclaredObjectType( removeRole );
		}

		void AllorsInternalDomain.AllorsRemoveDomainDeclaredObjectType( global::Allors.Meta.MetaObject removeRole ) 
		{
			AllorsAssert(removeRole);
			if( removeRole != null )
			{
				if(AllorsEmbeddedArrays.Exist( this.DomainMetaObject, removeRole ) ) 
				{
					this.DomainMetaObject = (global::Allors.Meta.MetaObject[]) AllorsEmbeddedArrays.Remove( this.DomainMetaObject, removeRole );
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
				foreach( global::Allors.Meta.MetaObject role in this.DomainMetaObject ) 
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

		public virtual global::Allors.Meta.MetaMethod[] DeclaredMethodTypes
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

		protected void RoleSetDomainDeclaredMethodType(global::Allors.Meta.MetaMethod[] roles)
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDeclaredMethodType();
			if( roles != null && roles.Length > 0 )
			{
				foreach( global::Allors.Meta.MetaMethod role in roles )
				{
					RoleAddDeclaredMethodType(role);
				}
			}
		}

		public virtual void AddDeclaredMethodType( global::Allors.Meta.MetaMethod addRole )
		{
			RoleAddDeclaredMethodType( addRole );
		}

		void RoleAddDeclaredMethodType( global::Allors.Meta.MetaMethod addRole )
		{
			AllorsAssert(addRole);
			if( addRole != null )
			{
				((AllorsInternalMethodType)addRole).AllorsRoleReleaseDeclaredMethodTypeDomain();
				if( !AllorsEmbeddedArrays.Exist( _DomainDeclaredMethodType, addRole ) ) 
				{
					// association side
					_DomainDeclaredMethodType = (global::Allors.Meta.MetaMethod[]) AllorsEmbeddedArrays.Add( _DomainDeclaredMethodType, addRole );
					// role side
					((AllorsInternalMethodType)addRole).AllorsRoleSyncSetDeclaredMethodTypeDomain( (global::Allors.Meta.MetaDomain) this ); 
				}
			}
		}

		public virtual void RemoveDeclaredMethodType( global::Allors.Meta.MetaMethod removeRole ) 
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDeclaredMethodType( removeRole );
		}

		void AllorsInternalDomain.AllorsRemoveDomainDeclaredMethodType( global::Allors.Meta.MetaMethod removeRole ) 
		{
			AllorsAssert(removeRole);
			if( removeRole != null )
			{
				if(AllorsEmbeddedArrays.Exist( _DomainDeclaredMethodType, removeRole ) ) 
				{
					_DomainDeclaredMethodType = (global::Allors.Meta.MetaMethod[]) AllorsEmbeddedArrays.Remove( _DomainDeclaredMethodType, removeRole );
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
				foreach( global::Allors.Meta.MetaMethod role in _DomainDeclaredMethodType ) 
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


		public virtual global::Allors.Meta.MetaInheritance[] DeclaredInheritances
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

		protected void RoleSetDomainDeclaredInheritance(global::Allors.Meta.MetaInheritance[] roles)
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDeclaredInheritance();
			if( roles != null && roles.Length > 0 )
			{
				foreach( global::Allors.Meta.MetaInheritance role in roles )
				{
					RoleAddDeclaredInheritance(role);
				}
			}
		}

		public virtual void AddDeclaredInheritance( global::Allors.Meta.MetaInheritance addRole )
		{
			RoleAddDeclaredInheritance( addRole );
		}

		void RoleAddDeclaredInheritance( global::Allors.Meta.MetaInheritance addRole )
		{
			AllorsAssert(addRole);
			if( addRole != null )
			{
				((AllorsInternalInheritance)addRole).AllorsRoleReleaseDeclaredInheritanceDomain();
				if( !AllorsEmbeddedArrays.Exist( _DomainDeclaredInheritance, addRole ) ) 
				{
					// association side
					_DomainDeclaredInheritance = (global::Allors.Meta.MetaInheritance[]) AllorsEmbeddedArrays.Add( _DomainDeclaredInheritance, addRole );
					// role side
					((AllorsInternalInheritance)addRole).AllorsRoleSyncSetDeclaredInheritanceDomain( (global::Allors.Meta.MetaDomain) this ); 
				}
			}
		}

		public virtual void RemoveDeclaredInheritance( global::Allors.Meta.MetaInheritance removeRole ) 
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDeclaredInheritance( removeRole );
		}

		void AllorsInternalDomain.AllorsRemoveDomainDeclaredInheritance( global::Allors.Meta.MetaInheritance removeRole ) 
		{
			AllorsAssert(removeRole);
			if( removeRole != null )
			{
				if(AllorsEmbeddedArrays.Exist( _DomainDeclaredInheritance, removeRole ) ) 
				{
					_DomainDeclaredInheritance = (global::Allors.Meta.MetaInheritance[]) AllorsEmbeddedArrays.Remove( _DomainDeclaredInheritance, removeRole );
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
				foreach( global::Allors.Meta.MetaInheritance role in _DomainDeclaredInheritance ) 
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


		public virtual global::Allors.Meta.MetaObject[] DerivedUnitObjectTypes
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

		protected void RoleSetDomainDerivedUnitObjectType(global::Allors.Meta.MetaObject[] roles)
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDerivedUnitObjectType();
			if( roles != null && roles.Length > 0 )
			{
				foreach( global::Allors.Meta.MetaObject role in roles )
				{
					RoleAddDerivedUnitObjectType(role);
				}
			}
		}

		public virtual void AddDerivedUnitObjectType( global::Allors.Meta.MetaObject addRole )
		{
			RoleAddDerivedUnitObjectType( addRole );
		}

		void RoleAddDerivedUnitObjectType( global::Allors.Meta.MetaObject addRole )
		{
			AllorsAssert(addRole);
			if( addRole != null )
			{
				if( !AllorsEmbeddedArrays.Exist( _DomainDerivedUnitObjectType, addRole ) ) 
				{
					// association side
					_DomainDerivedUnitObjectType = (global::Allors.Meta.MetaObject[]) AllorsEmbeddedArrays.Add( _DomainDerivedUnitObjectType, addRole );
					// role side
					((AllorsInternalObjectType)addRole).AllorsRoleSyncAddDerivedUnitObjectTypeDomain( (global::Allors.Meta.MetaDomain) this ); 
				}
			}
		}

		public virtual void RemoveDerivedUnitObjectType( global::Allors.Meta.MetaObject removeRole ) 
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDerivedUnitObjectType( removeRole );
		}

		void AllorsInternalDomain.AllorsRemoveDomainDerivedUnitObjectType( global::Allors.Meta.MetaObject removeRole ) 
		{
			AllorsAssert(removeRole);
			if( removeRole != null )
			{
				if(AllorsEmbeddedArrays.Exist( _DomainDerivedUnitObjectType, removeRole ) ) 
				{
					_DomainDerivedUnitObjectType = (global::Allors.Meta.MetaObject[]) AllorsEmbeddedArrays.Remove( _DomainDerivedUnitObjectType, removeRole );
					// role side
					((AllorsInternalObjectType)removeRole).AllorsRoleSyncRemoveDerivedUnitObjectTypeDomain( (global::Allors.Meta.MetaDomain) this ); 
			
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
				foreach( global::Allors.Meta.MetaObject role in _DomainDerivedUnitObjectType ) 
				{
					// role side
					((AllorsInternalObjectType)role).AllorsRoleSyncRemoveDerivedUnitObjectTypeDomain( (global::Allors.Meta.MetaDomain) this ); 
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


		public virtual global::Allors.Meta.MetaObject[] DerivedCompositeObjectTypes
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

		protected void RoleSetDomainDerivedCompositeObjectType(global::Allors.Meta.MetaObject[] roles)
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDerivedCompositeObjectType();
			if( roles != null && roles.Length > 0 )
			{
				foreach( global::Allors.Meta.MetaObject role in roles )
				{
					RoleAddDerivedCompositeObjectType(role);
				}
			}
		}

		public virtual void AddDerivedCompositeObjectType( global::Allors.Meta.MetaObject addRole )
		{
			RoleAddDerivedCompositeObjectType( addRole );
		}

		void RoleAddDerivedCompositeObjectType( global::Allors.Meta.MetaObject addRole )
		{
			AllorsAssert(addRole);
			if( addRole != null )
			{
				if( !AllorsEmbeddedArrays.Exist( _DomainDerivedCompositeObjectType, addRole ) ) 
				{
					// association side
					_DomainDerivedCompositeObjectType = (global::Allors.Meta.MetaObject[]) AllorsEmbeddedArrays.Add( _DomainDerivedCompositeObjectType, addRole );
					// role side
					((AllorsInternalObjectType)addRole).AllorsRoleSyncAddDerivedCompositeObjectTypeDomain( (global::Allors.Meta.MetaDomain) this ); 
				}
			}
		}

		public virtual void RemoveDerivedCompositeObjectType( global::Allors.Meta.MetaObject removeRole ) 
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDerivedCompositeObjectType( removeRole );
		}

		void AllorsInternalDomain.AllorsRemoveDomainDerivedCompositeObjectType( global::Allors.Meta.MetaObject removeRole ) 
		{
			AllorsAssert(removeRole);
			if( removeRole != null )
			{
				if(AllorsEmbeddedArrays.Exist( _DomainDerivedCompositeObjectType, removeRole ) ) 
				{
					_DomainDerivedCompositeObjectType = (global::Allors.Meta.MetaObject[]) AllorsEmbeddedArrays.Remove( _DomainDerivedCompositeObjectType, removeRole );
					// role side
					((AllorsInternalObjectType)removeRole).AllorsRoleSyncRemoveDerivedCompositeObjectTypeDomain( (global::Allors.Meta.MetaDomain) this ); 
			
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
				foreach( global::Allors.Meta.MetaObject role in _DomainDerivedCompositeObjectType ) 
				{
					// role side
					((AllorsInternalObjectType)role).AllorsRoleSyncRemoveDerivedCompositeObjectTypeDomain( (global::Allors.Meta.MetaDomain) this ); 
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


		public virtual global::Allors.Meta.MetaRelation[] DeclaredRelationTypes
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

		protected void RoleSetDomainDeclaredRelationType(global::Allors.Meta.MetaRelation[] roles)
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDeclaredRelationType();
			if( roles != null && roles.Length > 0 )
			{
				foreach( global::Allors.Meta.MetaRelation role in roles )
				{
					RoleAddDeclaredRelationType(role);
				}
			}
		}

		public virtual void AddDeclaredRelationType( global::Allors.Meta.MetaRelation addRole )
		{
			RoleAddDeclaredRelationType( addRole );
		}

		void RoleAddDeclaredRelationType( global::Allors.Meta.MetaRelation addRole )
		{
			AllorsAssert(addRole);
			if( addRole != null )
			{
				((AllorsInternalRelationType)addRole).AllorsRoleReleaseDeclaredRelationTypeDomain();
				if( !AllorsEmbeddedArrays.Exist( _DomainDeclaredRelationType, addRole ) ) 
				{
					// association side
					_DomainDeclaredRelationType = (global::Allors.Meta.MetaRelation[]) AllorsEmbeddedArrays.Add( _DomainDeclaredRelationType, addRole );
					// role side
					((AllorsInternalRelationType)addRole).AllorsRoleSyncSetDeclaredRelationTypeDomain( (global::Allors.Meta.MetaDomain) this ); 
				}
			}
		}

		public virtual void RemoveDeclaredRelationType( global::Allors.Meta.MetaRelation removeRole ) 
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDeclaredRelationType( removeRole );
		}

		void AllorsInternalDomain.AllorsRemoveDomainDeclaredRelationType( global::Allors.Meta.MetaRelation removeRole ) 
		{
			AllorsAssert(removeRole);
			if( removeRole != null )
			{
				if(AllorsEmbeddedArrays.Exist( _DomainDeclaredRelationType, removeRole ) ) 
				{
					_DomainDeclaredRelationType = (global::Allors.Meta.MetaRelation[]) AllorsEmbeddedArrays.Remove( _DomainDeclaredRelationType, removeRole );
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
				foreach( global::Allors.Meta.MetaRelation role in _DomainDeclaredRelationType ) 
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


		public virtual global::Allors.Meta.MetaRelation[] DerivedRelationTypes
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

		protected void RoleSetDomainDerivedRelationType(global::Allors.Meta.MetaRelation[] roles)
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDerivedRelationType();
			if( roles != null && roles.Length > 0 )
			{
				foreach( global::Allors.Meta.MetaRelation role in roles )
				{
					RoleAddDerivedRelationType(role);
				}
			}
		}

		public virtual void AddDerivedRelationType( global::Allors.Meta.MetaRelation addRole )
		{
			RoleAddDerivedRelationType( addRole );
		}

		void RoleAddDerivedRelationType( global::Allors.Meta.MetaRelation addRole )
		{
			AllorsAssert(addRole);
			if( addRole != null )
			{
				if( !AllorsEmbeddedArrays.Exist( _DomainDerivedRelationType, addRole ) ) 
				{
					// association side
					_DomainDerivedRelationType = (global::Allors.Meta.MetaRelation[]) AllorsEmbeddedArrays.Add( _DomainDerivedRelationType, addRole );
					// role side
					((AllorsInternalRelationType)addRole).AllorsRoleSyncAddDerivedRelationTypeDomain( (global::Allors.Meta.MetaDomain) this ); 
				}
			}
		}

		public virtual void RemoveDerivedRelationType( global::Allors.Meta.MetaRelation removeRole ) 
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDerivedRelationType( removeRole );
		}

		void AllorsInternalDomain.AllorsRemoveDomainDerivedRelationType( global::Allors.Meta.MetaRelation removeRole ) 
		{
			AllorsAssert(removeRole);
			if( removeRole != null )
			{
				if(AllorsEmbeddedArrays.Exist( _DomainDerivedRelationType, removeRole ) ) 
				{
					_DomainDerivedRelationType = (global::Allors.Meta.MetaRelation[]) AllorsEmbeddedArrays.Remove( _DomainDerivedRelationType, removeRole );
					// role side
					((AllorsInternalRelationType)removeRole).AllorsRoleSyncRemoveDerivedRelationTypeDomain( (global::Allors.Meta.MetaDomain) this ); 
			
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
				foreach( global::Allors.Meta.MetaRelation role in _DomainDerivedRelationType ) 
				{
					// role side
					((AllorsInternalRelationType)role).AllorsRoleSyncRemoveDerivedRelationTypeDomain( (global::Allors.Meta.MetaDomain) this ); 
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


		public virtual global::Allors.Meta.MetaMethod[] DerivedMethodTypes
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

		protected void RoleSetDomainDerivedMethodType(global::Allors.Meta.MetaMethod[] roles)
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDerivedMethodType();
			if( roles != null && roles.Length > 0 )
			{
				foreach( global::Allors.Meta.MetaMethod role in roles )
				{
					RoleAddDerivedMethodType(role);
				}
			}
		}

		public virtual void AddDerivedMethodType( global::Allors.Meta.MetaMethod addRole )
		{
			RoleAddDerivedMethodType( addRole );
		}

		void RoleAddDerivedMethodType( global::Allors.Meta.MetaMethod addRole )
		{
			AllorsAssert(addRole);
			if( addRole != null )
			{
				if( !AllorsEmbeddedArrays.Exist( _DomainDerivedMethodType, addRole ) ) 
				{
					// association side
					_DomainDerivedMethodType = (global::Allors.Meta.MetaMethod[]) AllorsEmbeddedArrays.Add( _DomainDerivedMethodType, addRole );
					// role side
					((AllorsInternalMethodType)addRole).AllorsRoleSyncAddDerivedMethodTypeDomain( (global::Allors.Meta.MetaDomain) this ); 
				}
			}
		}

		public virtual void RemoveDerivedMethodType( global::Allors.Meta.MetaMethod removeRole ) 
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDerivedMethodType( removeRole );
		}

		void AllorsInternalDomain.AllorsRemoveDomainDerivedMethodType( global::Allors.Meta.MetaMethod removeRole ) 
		{
			AllorsAssert(removeRole);
			if( removeRole != null )
			{
				if(AllorsEmbeddedArrays.Exist( _DomainDerivedMethodType, removeRole ) ) 
				{
					_DomainDerivedMethodType = (global::Allors.Meta.MetaMethod[]) AllorsEmbeddedArrays.Remove( _DomainDerivedMethodType, removeRole );
					// role side
					((AllorsInternalMethodType)removeRole).AllorsRoleSyncRemoveDerivedMethodTypeDomain( (global::Allors.Meta.MetaDomain) this ); 
			
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
				foreach( global::Allors.Meta.MetaMethod role in _DomainDerivedMethodType ) 
				{
					// role side
					((AllorsInternalMethodType)role).AllorsRoleSyncRemoveDerivedMethodTypeDomain( (global::Allors.Meta.MetaDomain) this ); 
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


		public virtual global::Allors.Meta.MetaInheritance[] DerivedInheritances
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

		protected void RoleSetDomainDerivedInheritance(global::Allors.Meta.MetaInheritance[] roles)
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDerivedInheritance();
			if( roles != null && roles.Length > 0 )
			{
				foreach( global::Allors.Meta.MetaInheritance role in roles )
				{
					RoleAddDerivedInheritance(role);
				}
			}
		}

		public virtual void AddDerivedInheritance( global::Allors.Meta.MetaInheritance addRole )
		{
			RoleAddDerivedInheritance( addRole );
		}

		void RoleAddDerivedInheritance( global::Allors.Meta.MetaInheritance addRole )
		{
			AllorsAssert(addRole);
			if( addRole != null )
			{
				if( !AllorsEmbeddedArrays.Exist( _DomainDerivedInheritance, addRole ) ) 
				{
					// association side
					_DomainDerivedInheritance = (global::Allors.Meta.MetaInheritance[]) AllorsEmbeddedArrays.Add( _DomainDerivedInheritance, addRole );
					// role side
					((AllorsInternalInheritance)addRole).AllorsRoleSyncAddDerivedInheritanceDomain( (global::Allors.Meta.MetaDomain) this ); 
				}
			}
		}

		public virtual void RemoveDerivedInheritance( global::Allors.Meta.MetaInheritance removeRole ) 
		{
			((AllorsInternalDomain)this).AllorsRemoveDomainDerivedInheritance( removeRole );
		}

		void AllorsInternalDomain.AllorsRemoveDomainDerivedInheritance( global::Allors.Meta.MetaInheritance removeRole ) 
		{
			AllorsAssert(removeRole);
			if( removeRole != null )
			{
				if(AllorsEmbeddedArrays.Exist( _DomainDerivedInheritance, removeRole ) ) 
				{
					_DomainDerivedInheritance = (global::Allors.Meta.MetaInheritance[]) AllorsEmbeddedArrays.Remove( _DomainDerivedInheritance, removeRole );
					// role side
					((AllorsInternalInheritance)removeRole).AllorsRoleSyncRemoveDerivedInheritanceDomain( (global::Allors.Meta.MetaDomain) this ); 
			
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
				foreach( global::Allors.Meta.MetaInheritance role in _DomainDerivedInheritance ) 
				{
					// role side
					((AllorsInternalInheritance)role).AllorsRoleSyncRemoveDerivedInheritanceDomain( (global::Allors.Meta.MetaDomain) this ); 
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