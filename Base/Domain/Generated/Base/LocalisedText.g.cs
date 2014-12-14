// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	public partial class LocalisedText : Allors.ObjectBase , Searchable, UserInterfaceable, Localised
	{
		public static readonly LocalisedTextMeta Meta = LocalisedTextMeta.Instance;

		public LocalisedText(Allors.IStrategy allors) : base(allors) {}

		public static LocalisedText Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (LocalisedText) allorsSession.Instantiate(allorsObjectId);		
		}

		internal override void OnBuild(global::Allors.IObjectBuilder objectBuilder)
		{
			var builder = (LocalisedTextBuilder)objectBuilder;

			this.Text = builder.Text;
		

			this.DisplayName = builder.DisplayName;
		

			this.SearchData = builder.SearchData;


			if(builder.DeniedPermissions!=null)
			{
				this.DeniedPermissions = builder.DeniedPermissions.ToArray();
			}

			if(builder.SecurityTokens!=null)
			{
				this.SecurityTokens = builder.SecurityTokens.ToArray();
			}


			this.Locale = builder.Locale;


		}




		virtual public global::System.String Text 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(Meta.Text);
			}
			set
			{
				Strategy.SetUnitRole(Meta.Text, value);
			}
		}

		virtual public bool ExistText{
			get
			{
				return Strategy.ExistUnitRole(Meta.Text);
			}
		}

		virtual public void RemoveText()
		{
			Strategy.RemoveUnitRole(Meta.Text);
		}


		virtual public SearchData SearchData
		{ 
			get
			{
				return (SearchData) Strategy.GetCompositeRole(Meta.SearchData);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.SearchData ,value);
			}
		}

		virtual public bool ExistSearchData
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.SearchData);
			}
		}

		virtual public void RemoveSearchData()
		{
			Strategy.RemoveCompositeRole(Meta.SearchData);
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


		virtual public Locale Locale
		{ 
			get
			{
				return (Locale) Strategy.GetCompositeRole(Meta.Locale);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.Locale ,value);
			}
		}

		virtual public bool ExistLocale
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.Locale);
			}
		}

		virtual public void RemoveLocale()
		{
			Strategy.RemoveCompositeRole(Meta.Locale);
		}



		virtual public Language LanguageWhereLocalisedName
		{ 
			get
			{
				return (Language) Strategy.GetCompositeAssociation(Meta.LanguageWhereLocalisedName);
			}
		} 

		virtual public bool ExistLanguageWhereLocalisedName
		{
			get
			{
				return Strategy.ExistCompositeAssociation(Meta.LanguageWhereLocalisedName);
			}
		}


		virtual public Enumeration EnumerationWhereLocalisedName
		{ 
			get
			{
				return (Enumeration) Strategy.GetCompositeAssociation(Meta.EnumerationWhereLocalisedName);
			}
		} 

		virtual public bool ExistEnumerationWhereLocalisedName
		{
			get
			{
				return Strategy.ExistCompositeAssociation(Meta.EnumerationWhereLocalisedName);
			}
		}


		virtual public Country CountryWhereLocalisedName
		{ 
			get
			{
				return (Country) Strategy.GetCompositeAssociation(Meta.CountryWhereLocalisedName);
			}
		} 

		virtual public bool ExistCountryWhereLocalisedName
		{
			get
			{
				return Strategy.ExistCompositeAssociation(Meta.CountryWhereLocalisedName);
			}
		}


		virtual public Currency CurrencyWhereLocalisedName
		{ 
			get
			{
				return (Currency) Strategy.GetCompositeAssociation(Meta.CurrencyWhereLocalisedName);
			}
		} 

		virtual public bool ExistCurrencyWhereLocalisedName
		{
			get
			{
				return Strategy.ExistCompositeAssociation(Meta.CurrencyWhereLocalisedName);
			}
		}

	}

	public class LocalisedTextMeta
	{
		public static readonly LocalisedTextMeta Instance = new LocalisedTextMeta();

		public global::Allors.Meta.Class ObjectType = global::Allors.Meta.Classes.LocalisedText;

		public global::Allors.Meta.RoleType Text 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.LocalisedTextText;
			}
		} 
		public global::Allors.Meta.RoleType SearchData 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.SearchableSearchData;
			}
		} 
		public global::Allors.Meta.RoleType DisplayName 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.UserInterfaceableDisplayName;
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
		public global::Allors.Meta.RoleType Locale 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.LocalisedLocale;
			}
		} 

		public global::Allors.Meta.AssociationType LanguageWhereLocalisedName 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.LanguageLocalisedName;
			}
		} 
		public global::Allors.Meta.AssociationType EnumerationWhereLocalisedName 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.EnumerationLocalisedName;
			}
		} 
		public global::Allors.Meta.AssociationType CountryWhereLocalisedName 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.CountryLocalisedName;
			}
		} 
		public global::Allors.Meta.AssociationType CurrencyWhereLocalisedName 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.CurrencyLocalisedName;
			}
		} 

	}

	public partial class LocalisedTextBuilder : Allors.ObjectBuilder<LocalisedText> , SearchableBuilder, UserInterfaceableBuilder, LocalisedBuilder, global::System.IDisposable
	{		
		public LocalisedTextBuilder(Allors.ISession session) : base(session)
	    {
	    }

				public global::System.String Text {get; set;}

				/// <exclude/>
				public LocalisedTextBuilder WithText(global::System.String value)
		        {
				    if(this.Text!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.Text = value;
		            return this;
		        }	

				public SearchData SearchData {get; set;}

				/// <exclude/>
				public LocalisedTextBuilder WithSearchData(SearchData value)
		        {
		            if(this.SearchData!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.SearchData = value;
		            return this;
		        }		

				
				public global::System.String DisplayName {get; set;}

				/// <exclude/>
				public LocalisedTextBuilder WithDisplayName(global::System.String value)
		        {
				    if(this.DisplayName!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.DisplayName = value;
		            return this;
		        }	

				public global::System.Collections.Generic.List<Permission> DeniedPermissions {get; set;}	

				/// <exclude/>
				public LocalisedTextBuilder WithDeniedPermission(Permission value)
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
				public LocalisedTextBuilder WithSecurityToken(SecurityToken value)
		        {
					if(this.SecurityTokens == null)
					{
						this.SecurityTokens = new global::System.Collections.Generic.List<SecurityToken>(); 
					}
		            this.SecurityTokens.Add(value);
		            return this;
		        }		

				
				public Locale Locale {get; set;}

				/// <exclude/>
				public LocalisedTextBuilder WithLocale(Locale value)
		        {
		            if(this.Locale!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.Locale = value;
		            return this;
		        }		

				

	}

	public partial class LocalisedTexts : global::Allors.ObjectsBase<LocalisedText>
	{
		public static readonly LocalisedTextMeta Meta = LocalisedTextMeta.Instance;

		public LocalisedTexts(Allors.ISession session) : base(session)
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