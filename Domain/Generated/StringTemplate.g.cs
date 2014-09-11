// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	[System.Diagnostics.DebuggerNonUserCode]
	public partial class StringTemplate : Allors.ObjectBase , UniquelyIdentifiable, Localised
	{
		public static readonly StringTemplateMeta Meta = StringTemplateMeta.Instance;

		public StringTemplate(Allors.IStrategy allors) : base(allors) {}

		public static StringTemplate Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (StringTemplate) allorsSession.Instantiate(allorsObjectId);		
		}




		virtual public global::System.String Body 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(Meta.Body);
			}
			set
			{
				Strategy.SetUnitRole(Meta.Body, value);
			}
		}

		virtual public bool ExistBody{
			get
			{
				return Strategy.ExistUnitRole(Meta.Body);
			}
		}

		virtual public void RemoveBody()
		{
			Strategy.RemoveUnitRole(Meta.Body);
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



		virtual public global::Allors.Extent<Singleton> SingletonsWherePersonTemplate
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.SingletonsWherePersonTemplate);
			}
		}

		virtual public bool ExistSingletonsWherePersonTemplate
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.SingletonsWherePersonTemplate);
			}
		}

	}

	public class StringTemplateMeta
	{
		public static readonly StringTemplateMeta Instance = new StringTemplateMeta();

		public global::Allors.Meta.Class ObjectType = global::Allors.Meta.Classes.StringTemplate;

		public global::Allors.Meta.RoleType Body 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.StringTemplateBody;
			}
		} 
		public global::Allors.Meta.RoleType Name 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.StringTemplateName;
			}
		} 
		public global::Allors.Meta.RoleType UniqueId 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.UniquelyIdentifiableUniqueId;
			}
		} 
		public global::Allors.Meta.RoleType Locale 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.LocalisedLocale;
			}
		} 

		public global::Allors.Meta.AssociationType SingletonsWherePersonTemplate 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.SingletonPersonTemplate;
			}
		} 

	}


	public partial class StringTemplateBuilder : Allors.ObjectBuilder<StringTemplate> , UniquelyIdentifiableBuilder, LocalisedBuilder, global::System.IDisposable
	{		
		public StringTemplateBuilder(Allors.ISession session) : base(session)
	    {
	    }

				public global::System.String Body {get; set;}

				/// <exclude/>
				public StringTemplateBuilder WithBody(global::System.String value)
		        {
				    if(this.Body!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.Body = value;
		            return this;
		        }	

				public global::System.String Name {get; set;}

				/// <exclude/>
				public StringTemplateBuilder WithName(global::System.String value)
		        {
				    if(this.Name!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.Name = value;
		            return this;
		        }	

				public global::System.Guid? UniqueId {get; set;}

				/// <exclude/>
				public StringTemplateBuilder WithUniqueId(global::System.Guid? value)
		        {
				    if(this.UniqueId!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.UniqueId = value;
		            return this;
		        }	

				public Locale Locale {get; set;}

				/// <exclude/>
				public StringTemplateBuilder WithLocale(Locale value)
		        {
		            if(this.Locale!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.Locale = value;
		            return this;
		        }		

				

	}

	public partial class StringTemplates : global::Allors.ObjectsBase<StringTemplate>
	{
		public static readonly StringTemplateMeta Meta = StringTemplateMeta.Instance;

		public StringTemplates(Allors.ISession session) : base(session)
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