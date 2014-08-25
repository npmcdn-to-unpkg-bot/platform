namespace Allors.Meta.AllorsGenerated
{
	internal interface AllorsInternalAssociationType : AllorsInternalPropertyType, AllorsInternal
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

		void AllorsRemoveAssociationTypeObjectType();


		global::System.Boolean IsMany
		{
			get;
			set;
		}

		void RemoveIsMany();

		bool ExistIsMany
		{
			get;
		}


		global::Allors.Meta.ObjectType[] ObjectTypesWhereDerivedExclusiveAssociationType
		{
			get;
		}

		bool ExistObjectTypesWhereDerivedExclusiveAssociationType
		{
			get;
		}

		void AllorsRoleSyncAddDerivedExclusiveAssociationTypeObjectType(global::Allors.Meta.ObjectType association );

		void AllorsRoleSyncRemoveDerivedExclusiveAssociationTypeObjectType(global::Allors.Meta.ObjectType association );

		void AllorsRoleReleaseDerivedExclusiveAssociationTypeObjectType();


		global::Allors.Meta.ObjectType[] ObjectTypesWhereDerivedAssociationType
		{
			get;
		}

		bool ExistObjectTypesWhereDerivedAssociationType
		{
			get;
		}

		void AllorsRoleSyncAddDerivedAssociationTypeObjectType(global::Allors.Meta.ObjectType association );

		void AllorsRoleSyncRemoveDerivedAssociationTypeObjectType(global::Allors.Meta.ObjectType association );

		void AllorsRoleReleaseDerivedAssociationTypeObjectType();



		global::Allors.Meta.RelationType RelationTypeWhereAssociationType
		{
			get;
		}

		bool ExistRelationTypeWhereAssociationType
		{
			get;
		}


		void AllorsRoleSyncSetAssociationTypeRelationType(global::Allors.Meta.RelationType association );


		void AllorsRoleReleaseAssociationTypeRelationType();


	}

	public interface AllorsInterfaceAssociationType :  AllorsEmbeddedObject 
	{
	}

	[System.Diagnostics.DebuggerNonUserCode]
	public abstract class AllorsClassAssociationType :  global::Allors.Meta.PropertyType,  AllorsInternalAssociationType , AllorsEmbeddedObject
	{



		/// <summary>
		/// Initializes a new instance of the <see cref="AllorsClassAssociationType"/> class.
		/// </summary>
		/// <param name="session">The Allors Session.</param>
		/// <param name="id">The Allors Object Id.</param>
		protected AllorsClassAssociationType(AllorsEmbeddedSession session, System.Int32 id) : base(session,id){}

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
				return new System.Guid("51df648e-4870-403a-8b2e-50f6e63d4749");
			}
		}

	    object AllorsInternal.GetRole(AllorsEmbeddedRelationType relation)
		{
	        switch(relation.Tag)
			{
				case AllorsRelationTags.AssociationTypeAssignedPluralName:
					return _AssociationTypeAssignedPluralName;
				case AllorsRelationTags.AssociationTypeObjectType:
					return _AssociationTypeObjectType;
				case AllorsRelationTags.AssociationTypeIsMany:
					return _AssociationTypeIsMany;
				case AllorsRelationTags.AssociationTypeDerivedRootObjectType:
					return _AssociationTypeDerivedRootObjectType;
				case AllorsRelationTags.AssociationTypeDerivedRootName:
					return _AssociationTypeDerivedRootName;
				case AllorsRelationTags.AssociationTypeAssignedSingularName:
					return _AssociationTypeAssignedSingularName;
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
				case AllorsRelationTags.AssociationTypeObjectType:
					RoleSetAssociationTypeObjectType((global::Allors.Meta.ObjectType)role);
					break;
				case AllorsRelationTags.AssociationTypeIsMany:
					RoleSetAssociationTypeIsMany((global::System.Boolean)role);
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
			    return _AssociationTypeObjectType;
			}

			set
			{
				AllorsAssert(value);
				RoleSetAssociationTypeObjectType(value);
			}
		}

		protected void RoleSetAssociationTypeObjectType(global::Allors.Meta.ObjectType value)
		{
			((AllorsInternalAssociationType)this).AllorsRemoveAssociationTypeObjectType();
			if( value != null ) 
			{
				_AssociationTypeObjectType = value;
				_AssociationTypeObjectType = value;
				// role side
				((AllorsInternalObjectType)_AssociationTypeObjectType).AllorsRoleSyncAddObjectTypeAssociationType((global::Allors.Meta.AssociationType) this ); 
			}
		}

		public virtual void RemoveObjectType()
		{
			((AllorsInternalAssociationType)this).AllorsRemoveAssociationTypeObjectType();
		}

		void AllorsInternalAssociationType.AllorsRemoveAssociationTypeObjectType()
		{
		    if( _AssociationTypeObjectType != null) 
			{
				((AllorsInternalObjectType)_AssociationTypeObjectType).AllorsRoleSyncRemoveObjectTypeAssociationType( (global::Allors.Meta.AssociationType) this ); 
				_AssociationTypeObjectType = null;
				_AssociationTypeObjectType = null;
			}
		}

		public virtual bool ExistObjectType
		{
			get
			{
				return _AssociationTypeObjectType != null;
			}
		}

		public virtual global::System.Boolean IsMany
		{
			get
			{
			    return (global::System.Boolean)_AssociationTypeIsMany;
			}

			set
			{
			    RoleSetAssociationTypeIsMany(value);
			}
		}

		protected void RoleSetAssociationTypeIsMany(global::System.Boolean role)
		{
			_AssociationTypeIsMany = role;
		}

		public virtual bool ExistIsMany
		{
			get
			{
				return _AssociationTypeIsMany != null;
			}
		}

		public virtual void RemoveIsMany()
		{
			_AssociationTypeIsMany = null;
		}

		public virtual global::Allors.Meta.ObjectType[] ObjectTypesWhereDerivedExclusiveAssociationType
		{
			get
			{
			    return _DerivedExclusiveAssociationTypeObjectType;
			}
		}

		public virtual bool ExistObjectTypesWhereDerivedExclusiveAssociationType
		{
			get
			{
				return _DerivedExclusiveAssociationTypeObjectType.Length > 0;
			}
		}

		void AllorsInternalAssociationType.AllorsRoleSyncAddDerivedExclusiveAssociationTypeObjectType(global::Allors.Meta.ObjectType association)
		{
		    if( !AllorsEmbeddedArrays.Exist( _DerivedExclusiveAssociationTypeObjectType, association ) ) 
			{
				_DerivedExclusiveAssociationTypeObjectType = (global::Allors.Meta.ObjectType[])AllorsEmbeddedArrays.Add(_DerivedExclusiveAssociationTypeObjectType,association);
			}
		}

		void AllorsInternalAssociationType.AllorsRoleSyncRemoveDerivedExclusiveAssociationTypeObjectType(global::Allors.Meta.ObjectType association)
		{
		    _DerivedExclusiveAssociationTypeObjectType = (global::Allors.Meta.ObjectType[]) AllorsEmbeddedArrays.Remove(_DerivedExclusiveAssociationTypeObjectType,association);
		}

		void AllorsInternalAssociationType.AllorsRoleReleaseDerivedExclusiveAssociationTypeObjectType()
		{
		    foreach( global::Allors.Meta.ObjectType association in _DerivedExclusiveAssociationTypeObjectType )
			{
				((AllorsInternalObjectType)association).AllorsRemoveObjectTypeDerivedExclusiveAssociationType((global::Allors.Meta.AssociationType) this);
			}
		}

		public virtual global::Allors.Meta.ObjectType[] ObjectTypesWhereDerivedAssociationType
		{
			get
			{
			    return _DerivedAssociationTypeObjectType;
			}
		}

		public virtual bool ExistObjectTypesWhereDerivedAssociationType
		{
			get
			{
				return _DerivedAssociationTypeObjectType.Length > 0;
			}
		}

		void AllorsInternalAssociationType.AllorsRoleSyncAddDerivedAssociationTypeObjectType(global::Allors.Meta.ObjectType association)
		{
		    if( !AllorsEmbeddedArrays.Exist( _DerivedAssociationTypeObjectType, association ) ) 
			{
				_DerivedAssociationTypeObjectType = (global::Allors.Meta.ObjectType[])AllorsEmbeddedArrays.Add(_DerivedAssociationTypeObjectType,association);
			}
		}

		void AllorsInternalAssociationType.AllorsRoleSyncRemoveDerivedAssociationTypeObjectType(global::Allors.Meta.ObjectType association)
		{
		    _DerivedAssociationTypeObjectType = (global::Allors.Meta.ObjectType[]) AllorsEmbeddedArrays.Remove(_DerivedAssociationTypeObjectType,association);
		}

		void AllorsInternalAssociationType.AllorsRoleReleaseDerivedAssociationTypeObjectType()
		{
		    foreach( global::Allors.Meta.ObjectType association in _DerivedAssociationTypeObjectType )
			{
				((AllorsInternalObjectType)association).AllorsRemoveObjectTypeDerivedAssociationType((global::Allors.Meta.AssociationType) this);
			}
		}


		public virtual global::Allors.Meta.RelationType RelationTypeWhereAssociationType
		{
			get
			{
			    return _AssociationTypeRelationType;
			}
		}

		public virtual bool ExistRelationTypeWhereAssociationType
		{
			get
			{
				return _AssociationTypeRelationType != null;
			}
		}

		void AllorsInternalAssociationType.AllorsRoleSyncSetAssociationTypeRelationType(global::Allors.Meta.RelationType association)
		{
		    _AssociationTypeRelationType = association;
		}

		void AllorsInternalAssociationType.AllorsRoleReleaseAssociationTypeRelationType()
		{
			if( _AssociationTypeRelationType != null )
			{
				((AllorsInternalRelationType)_AssociationTypeRelationType).AllorsRemoveRelationTypeAssociationType();
			}
		}


}
}