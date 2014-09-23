// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	[System.Diagnostics.DebuggerNonUserCode]
	public partial class MediaContent : Allors.ObjectBase 
	{
		public static readonly MediaContentMeta Meta = MediaContentMeta.Instance;

		public MediaContent(Allors.IStrategy allors) : base(allors) {}

		public static MediaContent Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (MediaContent) allorsSession.Instantiate(allorsObjectId);		
		}




		virtual public global::System.Byte[] Value 
		{
			get
			{
				return (global::System.Byte[]) Strategy.GetUnitRole(Meta.Value);
			}
			set
			{
				Strategy.SetUnitRole(Meta.Value, value);
			}
		}

		virtual public bool ExistValue{
			get
			{
				return Strategy.ExistUnitRole(Meta.Value);
			}
		}

		virtual public void RemoveValue()
		{
			Strategy.RemoveUnitRole(Meta.Value);
		}



		virtual public global::System.String Hash 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(Meta.Hash);
			}
			set
			{
				Strategy.SetUnitRole(Meta.Hash, value);
			}
		}

		virtual public bool ExistHash{
			get
			{
				return Strategy.ExistUnitRole(Meta.Hash);
			}
		}

		virtual public void RemoveHash()
		{
			Strategy.RemoveUnitRole(Meta.Hash);
		}



		virtual public global::Allors.Extent<Media> MediasWhereMediaContent
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.MediasWhereMediaContent);
			}
		}

		virtual public bool ExistMediasWhereMediaContent
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.MediasWhereMediaContent);
			}
		}

	}

	public class MediaContentMeta
	{
		public static readonly MediaContentMeta Instance = new MediaContentMeta();

		public global::Allors.Meta.Class ObjectType = global::Allors.Meta.Classes.MediaContent;

		public global::Allors.Meta.RoleType Value 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.MediaContentValue;
			}
		} 
		public global::Allors.Meta.RoleType Hash 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.MediaContentHash;
			}
		} 

		public global::Allors.Meta.AssociationType MediasWhereMediaContent 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.MediaMediaContent;
			}
		} 

	}
}