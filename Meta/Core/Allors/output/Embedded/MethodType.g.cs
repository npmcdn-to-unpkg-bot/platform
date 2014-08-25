namespace Allors.Meta.AllorsGenerated
{
	internal interface AllorsInternalMethodType : AllorsInternalOperandType, AllorsInternal
	{
		global::Allors.Meta.ObjectType ObjectType
		{
			get;
			set;
		}

		void RemoveObjectType();

		bool ExistObjectType
		{
			get;
		}

		void AllorsRemoveMethodTypeObjectType();


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



		global::Allors.Meta.Domain DomainWhereDeclaredMethodType
		{
			get;
		}

		bool ExistDomainWhereDeclaredMethodType
		{
			get;
		}


		void AllorsRoleSyncSetDeclaredMethodTypeDomain(global::Allors.Meta.Domain association );


		void AllorsRoleReleaseDeclaredMethodTypeDomain();


		global::Allors.Meta.ObjectType[] ObjectTypesWhereDerivedMethodType
		{
			get;
		}

		bool ExistObjectTypesWhereDerivedMethodType
		{
			get;
		}

		void AllorsRoleSyncAddDerivedMethodTypeObjectType(global::Allors.Meta.ObjectType association );

		void AllorsRoleSyncRemoveDerivedMethodTypeObjectType(global::Allors.Meta.ObjectType association );

		void AllorsRoleReleaseDerivedMethodTypeObjectType();


	}

	public interface AllorsInterfaceMethodType :  AllorsEmbeddedObject 
	{
	}

	[System.Diagnostics.DebuggerNonUserCode]
	public abstract class AllorsClassMethodType :  global::Allors.Meta.OperandType,  AllorsInternalMethodType , AllorsEmbeddedObject
	{
		protected global::Allors.Meta.ObjectType _MethodTypeObjectType;


		protected System.Object _MethodTypeName;


		protected global::Allors.Meta.Domain _DeclaredMethodTypeDomain;


		protected global::Allors.Meta.ObjectType[] _DerivedMethodTypeObjectType = AllorsEmbeddedArrays.EMPTY_ObjectType_ARRAY;


		protected global::Allors.Meta.Domain[] _DerivedMethodTypeDomain = AllorsEmbeddedArrays.EMPTY_Domain_ARRAY;




		/// <summary>
		/// Initializes a new instance of the <see cref="AllorsClassMethodType"/> class.
		/// </summary>
		/// <param name="session">The Allors Session.</param>
		/// <param name="id">The Allors Object Id.</param>
		protected AllorsClassMethodType(AllorsEmbeddedSession session, System.Int32 id) : base(session,id){}

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
				return new System.Guid("edf4fdb9-9fc9-4914-bd67-c781f9199f98");
			}
		}

	    object AllorsInternal.GetRole(AllorsEmbeddedRelationType relation)
		{
	        switch(relation.Tag)
			{
				case AllorsRelationTags.MethodTypeObjectType:
					return _MethodTypeObjectType;
				case AllorsRelationTags.MethodTypeName:
					return _MethodTypeName;
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
				case AllorsRelationTags.MethodTypeObjectType:
					RoleSetMethodTypeObjectType((global::Allors.Meta.ObjectType)role);
					break;
				case AllorsRelationTags.MethodTypeName:
					RoleSetMethodTypeName((global::System.String)role);
					break;
				case AllorsRelationTags.MetaObjectId:
					RoleSetMetaObjectId((global::System.Guid)role);
					break;

		default:
				throw new System.ArgumentException("Illegal relation " + relation.Name);
			}
		}


		public virtual global::Allors.Meta.ObjectType ObjectType
		{
			get
			{
			    return _MethodTypeObjectType;
			}

			set
			{
				AllorsAssert(value);
				RoleSetMethodTypeObjectType(value);
			}
		}

		protected void RoleSetMethodTypeObjectType(global::Allors.Meta.ObjectType value)
		{
			((AllorsInternalMethodType)this).AllorsRemoveMethodTypeObjectType();
			if( value != null ) 
			{
				_MethodTypeObjectType = value;
				_MethodTypeObjectType = value;
				// role side
				((AllorsInternalObjectType)_MethodTypeObjectType).AllorsRoleSyncAddObjectTypeMethodType((global::Allors.Meta.MethodType) this ); 
			}
		}

		public virtual void RemoveObjectType()
		{
			((AllorsInternalMethodType)this).AllorsRemoveMethodTypeObjectType();
		}

		void AllorsInternalMethodType.AllorsRemoveMethodTypeObjectType()
		{
		    if( _MethodTypeObjectType != null) 
			{
				((AllorsInternalObjectType)_MethodTypeObjectType).AllorsRoleSyncRemoveObjectTypeMethodType( (global::Allors.Meta.MethodType) this ); 
				_MethodTypeObjectType = null;
				_MethodTypeObjectType = null;
			}
		}

		public virtual bool ExistObjectType
		{
			get
			{
				return _MethodTypeObjectType != null;
			}
		}

		public virtual global::System.String Name
		{
			get
			{
			    return (global::System.String)_MethodTypeName;
			}

			set
			{
			    RoleSetMethodTypeName(value);
			}
		}

		protected void RoleSetMethodTypeName(global::System.String role)
		{
			_MethodTypeName = role;
		}

		public virtual bool ExistName
		{
			get
			{
				return _MethodTypeName != null;
			}
		}

		public virtual void RemoveName()
		{
			_MethodTypeName = null;
		}


		public virtual global::Allors.Meta.Domain DomainWhereDeclaredMethodType
		{
			get
			{
			    return _DeclaredMethodTypeDomain;
			}
		}

		public virtual bool ExistDomainWhereDeclaredMethodType
		{
			get
			{
				return _DeclaredMethodTypeDomain != null;
			}
		}

		void AllorsInternalMethodType.AllorsRoleSyncSetDeclaredMethodTypeDomain(global::Allors.Meta.Domain association)
		{
		    _DeclaredMethodTypeDomain = association;
		}

		void AllorsInternalMethodType.AllorsRoleReleaseDeclaredMethodTypeDomain()
		{
			if( _DeclaredMethodTypeDomain != null )
			{
				((AllorsInternalDomain)_DeclaredMethodTypeDomain).AllorsRemoveDomainDeclaredMethodType( (global::Allors.Meta.MethodType) this);
			}
		}


		public virtual global::Allors.Meta.ObjectType[] ObjectTypesWhereDerivedMethodType
		{
			get
			{
			    return _DerivedMethodTypeObjectType;
			}
		}

		public virtual bool ExistObjectTypesWhereDerivedMethodType
		{
			get
			{
				return _DerivedMethodTypeObjectType.Length > 0;
			}
		}

		void AllorsInternalMethodType.AllorsRoleSyncAddDerivedMethodTypeObjectType(global::Allors.Meta.ObjectType association)
		{
		    if( !AllorsEmbeddedArrays.Exist( _DerivedMethodTypeObjectType, association ) ) 
			{
				_DerivedMethodTypeObjectType = (global::Allors.Meta.ObjectType[])AllorsEmbeddedArrays.Add(_DerivedMethodTypeObjectType,association);
			}
		}

		void AllorsInternalMethodType.AllorsRoleSyncRemoveDerivedMethodTypeObjectType(global::Allors.Meta.ObjectType association)
		{
		    _DerivedMethodTypeObjectType = (global::Allors.Meta.ObjectType[]) AllorsEmbeddedArrays.Remove(_DerivedMethodTypeObjectType,association);
		}

		void AllorsInternalMethodType.AllorsRoleReleaseDerivedMethodTypeObjectType()
		{
		    foreach( global::Allors.Meta.ObjectType association in _DerivedMethodTypeObjectType )
			{
				((AllorsInternalObjectType)association).AllorsRemoveObjectTypeDerivedMethodType((global::Allors.Meta.MethodType) this);
			}
		}

		public virtual global::Allors.Meta.Domain[] DomainsWhereDerivedMethodType
		{
			get
			{
			    return _DerivedMethodTypeDomain;
			}
		}

		public virtual bool ExistDomainsWhereDerivedMethodType
		{
			get
			{
				return _DerivedMethodTypeDomain.Length > 0;
			}
		}

    }
}