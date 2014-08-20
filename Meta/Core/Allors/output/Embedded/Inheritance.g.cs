namespace Allors.Meta.AllorsGenerated
{
	internal interface AllorsInternalInheritance : AllorsInternalMetaObject, AllorsInternal
	{
		global::Allors.Meta.ObjectType Subtype
		{
			get;
			set;
		}

		void RemoveSubtype();

		bool ExistSubtype
		{
			get;
		}

		void AllorsRemoveInheritanceSubtype();


		global::Allors.Meta.ObjectType Supertype
		{
			get;
			set;
		}

		void RemoveSupertype();

		bool ExistSupertype
		{
			get;
		}

		void AllorsRemoveInheritanceSupertype();



		global::Allors.Meta.Domain DomainWhereDeclaredInheritance
		{
			get;
		}

		bool ExistDomainWhereDeclaredInheritance
		{
			get;
		}


		void AllorsRoleSyncSetDeclaredInheritanceDomain(global::Allors.Meta.Domain association );


		void AllorsRoleReleaseDeclaredInheritanceDomain();


		global::Allors.Meta.Domain[] DomainsWhereDerivedInheritance
		{
			get;
		}

		bool ExistDomainsWhereDerivedInheritance
		{
			get;
		}

		void AllorsRoleSyncAddDerivedInheritanceDomain(global::Allors.Meta.Domain association );

		void AllorsRoleSyncRemoveDerivedInheritanceDomain(global::Allors.Meta.Domain association );

		void AllorsRoleReleaseDerivedInheritanceDomain();


	}

	public interface AllorsInterfaceInheritance :  AllorsEmbeddedObject 
	{
	}

	[System.Diagnostics.DebuggerNonUserCode]
	public abstract class AllorsClassInheritance :  global::Allors.Meta.MetaObject,  AllorsInternalInheritance , AllorsEmbeddedObject
	{
		protected global::Allors.Meta.ObjectType _InheritanceSubtype;


		protected global::Allors.Meta.ObjectType _InheritanceSupertype;


		protected global::Allors.Meta.Domain _DeclaredInheritanceDomain;


		protected global::Allors.Meta.Domain[] _DerivedInheritanceDomain = AllorsEmbeddedArrays.EMPTY_Domain_ARRAY;




		/// <summary>
		/// Initializes a new instance of the <see cref="AllorsClassInheritance"/> class.
		/// </summary>
		/// <param name="session">The Allors Session.</param>
		/// <param name="id">The Allors Object Id.</param>
		protected AllorsClassInheritance(AllorsEmbeddedSession session, System.Int32 id) : base(session,id){}

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
				return new System.Guid("ceb95f73-a297-48cc-85bc-92efa5954efc");
			}
		}

		public override void Delete()
		{
			AllorsAssert();

			((AllorsInternalInheritance)this).AllorsRemoveInheritanceSubtype();
			((AllorsInternalInheritance)this).AllorsRemoveInheritanceSupertype();

			((AllorsInternalInheritance)this).AllorsRoleReleaseDeclaredInheritanceDomain();
			((AllorsInternalInheritance)this).AllorsRoleReleaseDerivedInheritanceDomain();


			session.Delete(this);
			isDeleted = true;
		}

		object AllorsInternal.GetRole(AllorsEmbeddedRelationType relation)
		{
			AllorsAssert();
			switch(relation.Tag)
			{
				case AllorsRelationTags.InheritanceSubtype:
					return _InheritanceSubtype;
				case AllorsRelationTags.InheritanceSupertype:
					return _InheritanceSupertype;
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
				case AllorsRelationTags.InheritanceSubtype:
					RoleSetInheritanceSubtype((global::Allors.Meta.ObjectType)role);
					break;
				case AllorsRelationTags.InheritanceSupertype:
					RoleSetInheritanceSupertype((global::Allors.Meta.ObjectType)role);
					break;
				case AllorsRelationTags.MetaObjectId:
					RoleSetMetaObjectId((global::System.Guid)role);
					break;

		default:
				throw new System.ArgumentException("Illegal relation " + relation.Name);
			}
		}


		public virtual global::Allors.Meta.ObjectType Subtype
		{
			get
			{
				AllorsAssert();
				return _InheritanceSubtype;
			}

			set
			{
				AllorsAssert(value);
				RoleSetInheritanceSubtype(value);
			}
		}

		protected void RoleSetInheritanceSubtype(global::Allors.Meta.ObjectType value)
		{
			((AllorsInternalInheritance)this).AllorsRemoveInheritanceSubtype();
			if( value != null ) 
			{
				_InheritanceSubtype = value;
				_InheritanceSubtype = value;
				// role side
				((AllorsInternalObjectType)_InheritanceSubtype).AllorsRoleSyncAddSubtypeInheritance((global::Allors.Meta.Inheritance) this ); 
			}
		}

		public virtual void RemoveSubtype()
		{
			((AllorsInternalInheritance)this).AllorsRemoveInheritanceSubtype();
		}

		void AllorsInternalInheritance.AllorsRemoveInheritanceSubtype()
		{
			AllorsAssert();
			if( _InheritanceSubtype != null) 
			{
				((AllorsInternalObjectType)_InheritanceSubtype).AllorsRoleSyncRemoveSubtypeInheritance( (global::Allors.Meta.Inheritance) this ); 
				_InheritanceSubtype = null;
				_InheritanceSubtype = null;
			}
		}

		public virtual bool ExistSubtype
		{
			get
			{
				return _InheritanceSubtype != null;
			}
		}

		public virtual global::Allors.Meta.ObjectType Supertype
		{
			get
			{
				AllorsAssert();
				return _InheritanceSupertype;
			}

			set
			{
				AllorsAssert(value);
				RoleSetInheritanceSupertype(value);
			}
		}

		protected void RoleSetInheritanceSupertype(global::Allors.Meta.ObjectType value)
		{
			((AllorsInternalInheritance)this).AllorsRemoveInheritanceSupertype();
			if( value != null ) 
			{
				_InheritanceSupertype = value;
				_InheritanceSupertype = value;
				// role side
				((AllorsInternalObjectType)_InheritanceSupertype).AllorsRoleSyncAddSupertypeInheritance((global::Allors.Meta.Inheritance) this ); 
			}
		}

		public virtual void RemoveSupertype()
		{
			((AllorsInternalInheritance)this).AllorsRemoveInheritanceSupertype();
		}

		void AllorsInternalInheritance.AllorsRemoveInheritanceSupertype()
		{
			AllorsAssert();
			if( _InheritanceSupertype != null) 
			{
				((AllorsInternalObjectType)_InheritanceSupertype).AllorsRoleSyncRemoveSupertypeInheritance( (global::Allors.Meta.Inheritance) this ); 
				_InheritanceSupertype = null;
				_InheritanceSupertype = null;
			}
		}

		public virtual bool ExistSupertype
		{
			get
			{
				return _InheritanceSupertype != null;
			}
		}


		public virtual global::Allors.Meta.Domain DomainWhereDeclaredInheritance
		{
			get
			{
				AllorsAssert();
				return _DeclaredInheritanceDomain;
			}
		}

		public virtual bool ExistDomainWhereDeclaredInheritance
		{
			get
			{
				return _DeclaredInheritanceDomain != null;
			}
		}

		void AllorsInternalInheritance.AllorsRoleSyncSetDeclaredInheritanceDomain(global::Allors.Meta.Domain association)
		{
			AllorsAssert();
			_DeclaredInheritanceDomain = association;
		}

		void AllorsInternalInheritance.AllorsRoleReleaseDeclaredInheritanceDomain()
		{
			if( _DeclaredInheritanceDomain != null )
			{
				((AllorsInternalDomain)_DeclaredInheritanceDomain).AllorsRemoveDomainDeclaredInheritance( (global::Allors.Meta.Inheritance) this);
			}
		}


		public virtual global::Allors.Meta.Domain[] DomainsWhereDerivedInheritance
		{
			get
			{
				AllorsAssert();
				return _DerivedInheritanceDomain;
			}
		}

		public virtual bool ExistDomainsWhereDerivedInheritance
		{
			get
			{
				return _DerivedInheritanceDomain.Length > 0;
			}
		}

		void AllorsInternalInheritance.AllorsRoleSyncAddDerivedInheritanceDomain(global::Allors.Meta.Domain association)
		{
			AllorsAssert();
			if( !AllorsEmbeddedArrays.Exist( _DerivedInheritanceDomain, association ) ) 
			{
				_DerivedInheritanceDomain = (global::Allors.Meta.Domain[])AllorsEmbeddedArrays.Add(_DerivedInheritanceDomain,association);
			}
		}

		void AllorsInternalInheritance.AllorsRoleSyncRemoveDerivedInheritanceDomain(global::Allors.Meta.Domain association)
		{
			AllorsAssert();
			_DerivedInheritanceDomain = (global::Allors.Meta.Domain[]) AllorsEmbeddedArrays.Remove(_DerivedInheritanceDomain,association);
		}

		void AllorsInternalInheritance.AllorsRoleReleaseDerivedInheritanceDomain()
		{
			AllorsAssert();
			foreach( global::Allors.Meta.Domain association in _DerivedInheritanceDomain )
			{
				((AllorsInternalDomain)association).AllorsRemoveDomainDerivedInheritance((global::Allors.Meta.Inheritance) this);
			}
		}

}
}