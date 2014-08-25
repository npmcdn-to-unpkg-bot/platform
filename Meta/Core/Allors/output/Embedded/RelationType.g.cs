namespace Allors.Meta.AllorsGenerated
{
	internal interface AllorsInternalRelationType : AllorsInternalMetaObject, AllorsInternal
	{
		global::System.Boolean IsDerived
		{
			get;
			set;
		}

		void RemoveIsDerived();

		bool ExistIsDerived
		{
			get;
		}


		global::System.Boolean IsIndexed
		{
			get;
			set;
		}

		void RemoveIsIndexed();

		bool ExistIsIndexed
		{
			get;
		}


		global::Allors.Meta.MetaRole RoleType
		{
			get;
			set;
		}

		void RemoveRoleType();

		bool ExistRoleType
		{
			get;
		}

		void AllorsRemoveRelationTypeRoleType();


		global::Allors.Meta.MetaAssociation AssociationType
		{
			get;
			set;
		}

		void RemoveAssociationType();

		bool ExistAssociationType
		{
			get;
		}

		void AllorsRemoveRelationTypeAssociationType();



		global::Allors.Meta.MetaDomain DomainWhereDeclaredRelationType
		{
			get;
		}

		bool ExistDomainWhereDeclaredRelationType
		{
			get;
		}


		void AllorsRoleSyncSetDeclaredRelationTypeDomain(global::Allors.Meta.MetaDomain association );


		void AllorsRoleReleaseDeclaredRelationTypeDomain();


		global::Allors.Meta.MetaDomain[] DomainsWhereDerivedRelationType
		{
			get;
		}

		bool ExistDomainsWhereDerivedRelationType
		{
			get;
		}

		void AllorsRoleSyncAddDerivedRelationTypeDomain(global::Allors.Meta.MetaDomain association );

		void AllorsRoleSyncRemoveDerivedRelationTypeDomain(global::Allors.Meta.MetaDomain association );

		void AllorsRoleReleaseDerivedRelationTypeDomain();


	}

	public interface AllorsInterfaceRelationType :  AllorsEmbeddedObject 
	{
	}

	[System.Diagnostics.DebuggerNonUserCode]
	public abstract class AllorsClassRelationType :  global::Allors.Meta.MetaBase,  AllorsInternalRelationType , AllorsEmbeddedObject
	{
		protected System.Object _RelationTypeIsDerived;


		protected System.Object _RelationTypeIsIndexed;


		protected global::Allors.Meta.MetaRole _RelationTypeRoleType;


		protected global::Allors.Meta.MetaAssociation _RelationTypeAssociationType;


		protected global::Allors.Meta.MetaDomain _DeclaredRelationTypeDomain;


		protected global::Allors.Meta.MetaDomain[] _DerivedRelationTypeDomain = AllorsEmbeddedArrays.EMPTY_Domain_ARRAY;




		/// <summary>
		/// Initializes a new instance of the <see cref="AllorsClassRelationType"/> class.
		/// </summary>
		/// <param name="session">The Allors Session.</param>
		/// <param name="id">The Allors Object Id.</param>
		protected AllorsClassRelationType(AllorsEmbeddedSession session, System.Int32 id) : base(session,id){}

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
				return new System.Guid("c03575fa-2d84-4096-9c4d-93cf05d3c1de");
			}
		}

		public override void Delete()
		{
			AllorsAssert();

			((AllorsInternalRelationType)this).AllorsRemoveRelationTypeRoleType();
			((AllorsInternalRelationType)this).AllorsRemoveRelationTypeAssociationType();

			((AllorsInternalRelationType)this).AllorsRoleReleaseDeclaredRelationTypeDomain();
			((AllorsInternalRelationType)this).AllorsRoleReleaseDerivedRelationTypeDomain();


			session.Delete(this);
			isDeleted = true;
		}

		object AllorsInternal.GetRole(AllorsEmbeddedRelationType relation)
		{
			AllorsAssert();
			switch(relation.Tag)
			{
				case AllorsRelationTags.RelationTypeIsDerived:
					return _RelationTypeIsDerived;
				case AllorsRelationTags.RelationTypeIsIndexed:
					return _RelationTypeIsIndexed;
				case AllorsRelationTags.RelationTypeRoleType:
					return _RelationTypeRoleType;
				case AllorsRelationTags.RelationTypeAssociationType:
					return _RelationTypeAssociationType;
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
				case AllorsRelationTags.RelationTypeIsDerived:
					RoleSetRelationTypeIsDerived((global::System.Boolean)role);
					break;
				case AllorsRelationTags.RelationTypeIsIndexed:
					RoleSetRelationTypeIsIndexed((global::System.Boolean)role);
					break;
				case AllorsRelationTags.RelationTypeRoleType:
					RoleSetRelationTypeRoleType((global::Allors.Meta.MetaRole)role);
					break;
				case AllorsRelationTags.RelationTypeAssociationType:
					RoleSetRelationTypeAssociationType((global::Allors.Meta.MetaAssociation)role);
					break;
				case AllorsRelationTags.MetaObjectId:
					RoleSetMetaObjectId((global::System.Guid)role);
					break;

		default:
				throw new System.ArgumentException("Illegal relation " + relation.Name);
			}
		}


		public virtual global::System.Boolean IsDerived
		{
			get
			{ 
				AllorsAssert();
				return (global::System.Boolean)_RelationTypeIsDerived;
			}

			set
			{
				AllorsAssert();
				RoleSetRelationTypeIsDerived(value);
			}
		}

		protected void RoleSetRelationTypeIsDerived(global::System.Boolean role)
		{
			_RelationTypeIsDerived = role;
		}

		public virtual bool ExistIsDerived
		{
			get
			{
				return _RelationTypeIsDerived != null;
			}
		}

		public virtual void RemoveIsDerived()
		{
			_RelationTypeIsDerived = null;
		}

		public virtual global::System.Boolean IsIndexed
		{
			get
			{ 
				AllorsAssert();
				return (global::System.Boolean)_RelationTypeIsIndexed;
			}

			set
			{
				AllorsAssert();
				RoleSetRelationTypeIsIndexed(value);
			}
		}

		protected void RoleSetRelationTypeIsIndexed(global::System.Boolean role)
		{
			_RelationTypeIsIndexed = role;
		}

		public virtual bool ExistIsIndexed
		{
			get
			{
				return _RelationTypeIsIndexed != null;
			}
		}

		public virtual void RemoveIsIndexed()
		{
			_RelationTypeIsIndexed = null;
		}

		public virtual global::Allors.Meta.MetaRole RoleType
		{
			get
			{
				AllorsAssert();
				return _RelationTypeRoleType;
			}

			set
			{
				AllorsAssert(value);
				RoleSetRelationTypeRoleType(value);
			}
		}

		protected void RoleSetRelationTypeRoleType(global::Allors.Meta.MetaRole value)
		{
			((AllorsInternalRelationType)this).AllorsRemoveRelationTypeRoleType();
			if( value != null ) 
			{
				_RelationTypeRoleType = value;
				((AllorsInternalRoleType)_RelationTypeRoleType).AllorsRoleReleaseRoleTypeRelationType();
				_RelationTypeRoleType = value;
				// role side
				((AllorsInternalRoleType)_RelationTypeRoleType).AllorsRoleSyncSetRoleTypeRelationType((global::Allors.Meta.MetaRelation) this ); 
			}
		}

		public virtual void RemoveRoleType()
		{
			((AllorsInternalRelationType)this).AllorsRemoveRelationTypeRoleType();
		}

		void AllorsInternalRelationType.AllorsRemoveRelationTypeRoleType()
		{
			AllorsAssert();
			if( _RelationTypeRoleType != null) 
			{
				((AllorsInternalRoleType)_RelationTypeRoleType).AllorsRoleSyncSetRoleTypeRelationType( null ); 
				_RelationTypeRoleType = null;
				_RelationTypeRoleType = null;
			}
		}

		public virtual bool ExistRoleType
		{
			get
			{
				return _RelationTypeRoleType != null;
			}
		}

		public virtual global::Allors.Meta.MetaAssociation AssociationType
		{
			get
			{
				AllorsAssert();
				return _RelationTypeAssociationType;
			}

			set
			{
				AllorsAssert(value);
				RoleSetRelationTypeAssociationType(value);
			}
		}

		protected void RoleSetRelationTypeAssociationType(global::Allors.Meta.MetaAssociation value)
		{
			((AllorsInternalRelationType)this).AllorsRemoveRelationTypeAssociationType();
			if( value != null ) 
			{
				_RelationTypeAssociationType = value;
				((AllorsInternalAssociationType)_RelationTypeAssociationType).AllorsRoleReleaseAssociationTypeRelationType();
				_RelationTypeAssociationType = value;
				// role side
				((AllorsInternalAssociationType)_RelationTypeAssociationType).AllorsRoleSyncSetAssociationTypeRelationType((global::Allors.Meta.MetaRelation) this ); 
			}
		}

		public virtual void RemoveAssociationType()
		{
			((AllorsInternalRelationType)this).AllorsRemoveRelationTypeAssociationType();
		}

		void AllorsInternalRelationType.AllorsRemoveRelationTypeAssociationType()
		{
			AllorsAssert();
			if( _RelationTypeAssociationType != null) 
			{
				((AllorsInternalAssociationType)_RelationTypeAssociationType).AllorsRoleSyncSetAssociationTypeRelationType( null ); 
				_RelationTypeAssociationType = null;
				_RelationTypeAssociationType = null;
			}
		}

		public virtual bool ExistAssociationType
		{
			get
			{
				return _RelationTypeAssociationType != null;
			}
		}


		public virtual global::Allors.Meta.MetaDomain DomainWhereDeclaredRelationType
		{
			get
			{
				AllorsAssert();
				return _DeclaredRelationTypeDomain;
			}
		}

		public virtual bool ExistDomainWhereDeclaredRelationType
		{
			get
			{
				return _DeclaredRelationTypeDomain != null;
			}
		}

		void AllorsInternalRelationType.AllorsRoleSyncSetDeclaredRelationTypeDomain(global::Allors.Meta.MetaDomain association)
		{
			AllorsAssert();
			_DeclaredRelationTypeDomain = association;
		}

		void AllorsInternalRelationType.AllorsRoleReleaseDeclaredRelationTypeDomain()
		{
			if( _DeclaredRelationTypeDomain != null )
			{
				((AllorsInternalDomain)_DeclaredRelationTypeDomain).AllorsRemoveDomainDeclaredRelationType( (global::Allors.Meta.MetaRelation) this);
			}
		}


		public virtual global::Allors.Meta.MetaDomain[] DomainsWhereDerivedRelationType
		{
			get
			{
				AllorsAssert();
				return _DerivedRelationTypeDomain;
			}
		}

		public virtual bool ExistDomainsWhereDerivedRelationType
		{
			get
			{
				return _DerivedRelationTypeDomain.Length > 0;
			}
		}

		void AllorsInternalRelationType.AllorsRoleSyncAddDerivedRelationTypeDomain(global::Allors.Meta.MetaDomain association)
		{
			AllorsAssert();
			if( !AllorsEmbeddedArrays.Exist( _DerivedRelationTypeDomain, association ) ) 
			{
				_DerivedRelationTypeDomain = (global::Allors.Meta.MetaDomain[])AllorsEmbeddedArrays.Add(_DerivedRelationTypeDomain,association);
			}
		}

		void AllorsInternalRelationType.AllorsRoleSyncRemoveDerivedRelationTypeDomain(global::Allors.Meta.MetaDomain association)
		{
			AllorsAssert();
			_DerivedRelationTypeDomain = (global::Allors.Meta.MetaDomain[]) AllorsEmbeddedArrays.Remove(_DerivedRelationTypeDomain,association);
		}

		void AllorsInternalRelationType.AllorsRoleReleaseDerivedRelationTypeDomain()
		{
			AllorsAssert();
			foreach( global::Allors.Meta.MetaDomain association in _DerivedRelationTypeDomain )
			{
				((AllorsInternalDomain)association).AllorsRemoveDomainDerivedRelationType((global::Allors.Meta.MetaRelation) this);
			}
		}

}
}