namespace Allors.Meta.AllorsGenerated
{
	internal interface AllorsInternalMethodType : AllorsInternalOperandType, AllorsInternal
	{
		global::Allors.Meta.MetaObject ObjectType
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



		global::Allors.Meta.MetaDomain DomainWhereDeclaredMethodType
		{
			get;
		}

		bool ExistDomainWhereDeclaredMethodType
		{
			get;
		}


		void AllorsRoleSyncSetDeclaredMethodTypeDomain(global::Allors.Meta.MetaDomain association );


		void AllorsRoleReleaseDeclaredMethodTypeDomain();


		global::Allors.Meta.MetaObject[] ObjectTypesWhereDerivedMethodType
		{
			get;
		}

		bool ExistObjectTypesWhereDerivedMethodType
		{
			get;
		}

		void AllorsRoleSyncAddDerivedMethodTypeObjectType(global::Allors.Meta.MetaObject association );

		void AllorsRoleSyncRemoveDerivedMethodTypeObjectType(global::Allors.Meta.MetaObject association );

		void AllorsRoleReleaseDerivedMethodTypeObjectType();


		global::Allors.Meta.MetaDomain[] DomainsWhereDerivedMethodType
		{
			get;
		}

		bool ExistDomainsWhereDerivedMethodType
		{
			get;
		}

		void AllorsRoleSyncAddDerivedMethodTypeDomain(global::Allors.Meta.MetaDomain association );

		void AllorsRoleSyncRemoveDerivedMethodTypeDomain(global::Allors.Meta.MetaDomain association );

		void AllorsRoleReleaseDerivedMethodTypeDomain();


	}

	public interface AllorsInterfaceMethodType :  AllorsEmbeddedObject 
	{
	}

	[System.Diagnostics.DebuggerNonUserCode]
	public abstract class AllorsClassMethodType :  global::Allors.Meta.MetaOperand,  AllorsInternalMethodType , AllorsEmbeddedObject
	{
		protected global::Allors.Meta.MetaObject _MethodTypeObjectType;


		protected System.Object _MethodTypeName;


		protected global::Allors.Meta.MetaDomain _DeclaredMethodTypeDomain;


		protected global::Allors.Meta.MetaObject[] _DerivedMethodTypeObjectType = AllorsEmbeddedArrays.EMPTY_ObjectType_ARRAY;


		protected global::Allors.Meta.MetaDomain[] _DerivedMethodTypeDomain = AllorsEmbeddedArrays.EMPTY_Domain_ARRAY;




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

		public override void Delete()
		{
			AllorsAssert();

			((AllorsInternalMethodType)this).AllorsRemoveMethodTypeObjectType();

			((AllorsInternalMethodType)this).AllorsRoleReleaseDeclaredMethodTypeDomain();
			((AllorsInternalMethodType)this).AllorsRoleReleaseDerivedMethodTypeObjectType();
			((AllorsInternalMethodType)this).AllorsRoleReleaseDerivedMethodTypeDomain();


			session.Delete(this);
			isDeleted = true;
		}

		object AllorsInternal.GetRole(AllorsEmbeddedRelationType relation)
		{
			AllorsAssert();
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
					RoleSetMethodTypeObjectType((global::Allors.Meta.MetaObject)role);
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


		public virtual global::Allors.Meta.MetaObject ObjectType
		{
			get
			{
				AllorsAssert();
				return _MethodTypeObjectType;
			}

			set
			{
				AllorsAssert(value);
				RoleSetMethodTypeObjectType(value);
			}
		}

		protected void RoleSetMethodTypeObjectType(global::Allors.Meta.MetaObject value)
		{
			((AllorsInternalMethodType)this).AllorsRemoveMethodTypeObjectType();
			if( value != null ) 
			{
				_MethodTypeObjectType = value;
				_MethodTypeObjectType = value;
				// role side
				((AllorsInternalObjectType)_MethodTypeObjectType).AllorsRoleSyncAddObjectTypeMethodType((global::Allors.Meta.MetaMethod) this ); 
			}
		}

		public virtual void RemoveObjectType()
		{
			((AllorsInternalMethodType)this).AllorsRemoveMethodTypeObjectType();
		}

		void AllorsInternalMethodType.AllorsRemoveMethodTypeObjectType()
		{
			AllorsAssert();
			if( _MethodTypeObjectType != null) 
			{
				((AllorsInternalObjectType)_MethodTypeObjectType).AllorsRoleSyncRemoveObjectTypeMethodType( (global::Allors.Meta.MetaMethod) this ); 
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
				AllorsAssert();
				return (global::System.String)_MethodTypeName;
			}

			set
			{
				AllorsAssert();
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


		public virtual global::Allors.Meta.MetaDomain DomainWhereDeclaredMethodType
		{
			get
			{
				AllorsAssert();
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

		void AllorsInternalMethodType.AllorsRoleSyncSetDeclaredMethodTypeDomain(global::Allors.Meta.MetaDomain association)
		{
			AllorsAssert();
			_DeclaredMethodTypeDomain = association;
		}

		void AllorsInternalMethodType.AllorsRoleReleaseDeclaredMethodTypeDomain()
		{
			if( _DeclaredMethodTypeDomain != null )
			{
				((AllorsInternalDomain)_DeclaredMethodTypeDomain).AllorsRemoveDomainDeclaredMethodType( (global::Allors.Meta.MetaMethod) this);
			}
		}


		public virtual global::Allors.Meta.MetaObject[] ObjectTypesWhereDerivedMethodType
		{
			get
			{
				AllorsAssert();
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

		void AllorsInternalMethodType.AllorsRoleSyncAddDerivedMethodTypeObjectType(global::Allors.Meta.MetaObject association)
		{
			AllorsAssert();
			if( !AllorsEmbeddedArrays.Exist( _DerivedMethodTypeObjectType, association ) ) 
			{
				_DerivedMethodTypeObjectType = (global::Allors.Meta.MetaObject[])AllorsEmbeddedArrays.Add(_DerivedMethodTypeObjectType,association);
			}
		}

		void AllorsInternalMethodType.AllorsRoleSyncRemoveDerivedMethodTypeObjectType(global::Allors.Meta.MetaObject association)
		{
			AllorsAssert();
			_DerivedMethodTypeObjectType = (global::Allors.Meta.MetaObject[]) AllorsEmbeddedArrays.Remove(_DerivedMethodTypeObjectType,association);
		}

		void AllorsInternalMethodType.AllorsRoleReleaseDerivedMethodTypeObjectType()
		{
			AllorsAssert();
			foreach( global::Allors.Meta.MetaObject association in _DerivedMethodTypeObjectType )
			{
				((AllorsInternalObjectType)association).AllorsRemoveObjectTypeDerivedMethodType((global::Allors.Meta.MetaMethod) this);
			}
		}

		public virtual global::Allors.Meta.MetaDomain[] DomainsWhereDerivedMethodType
		{
			get
			{
				AllorsAssert();
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

		void AllorsInternalMethodType.AllorsRoleSyncAddDerivedMethodTypeDomain(global::Allors.Meta.MetaDomain association)
		{
			AllorsAssert();
			if( !AllorsEmbeddedArrays.Exist( _DerivedMethodTypeDomain, association ) ) 
			{
				_DerivedMethodTypeDomain = (global::Allors.Meta.MetaDomain[])AllorsEmbeddedArrays.Add(_DerivedMethodTypeDomain,association);
			}
		}

		void AllorsInternalMethodType.AllorsRoleSyncRemoveDerivedMethodTypeDomain(global::Allors.Meta.MetaDomain association)
		{
			AllorsAssert();
			_DerivedMethodTypeDomain = (global::Allors.Meta.MetaDomain[]) AllorsEmbeddedArrays.Remove(_DerivedMethodTypeDomain,association);
		}

		void AllorsInternalMethodType.AllorsRoleReleaseDerivedMethodTypeDomain()
		{
			AllorsAssert();
			foreach( global::Allors.Meta.MetaDomain association in _DerivedMethodTypeDomain )
			{
				((AllorsInternalDomain)association).AllorsRemoveDomainDerivedMethodType((global::Allors.Meta.MetaMethod) this);
			}
		}

}
}