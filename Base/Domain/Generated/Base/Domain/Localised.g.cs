// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
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

		public override Allors.Meta.Composite ObjectType
		{
			get
			{
				return Meta.ObjectType;
			}
		}
	}

}