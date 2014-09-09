// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
    using Allors.Meta;

    public partial interface Localised : Allors.IObject
	{


		Locale Locale
		{ 
			get;
			set;
		}

		bool ExistLocale
		{
			get;
		}

		void RemoveLocale();

	}

	public class LocalisedMeta
	{
		public static readonly LocalisedMeta Instance = new LocalisedMeta();

		public global::Allors.Meta.Interface ObjectType = global::Allors.Meta.Interfaces.Localised;

		public global::Allors.Meta.RoleType Locale 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.LocalisedLocale;
			}
		} 

	}

	public partial interface LocalisedBuilder :  global::System.IDisposable
	{	
		Locale Locale {get;}

		
	}

	public partial class Localiseds : global::Allors.ObjectsBase<Localised>
	{
		public static readonly LocalisedMeta Meta = LocalisedMeta.Instance;

		public Localiseds(Allors.ISession session) : base(session)
		{
		}

		public override Composite ObjectType
		{
			get
			{
				return Meta.ObjectType;
			}
		}
	}

}