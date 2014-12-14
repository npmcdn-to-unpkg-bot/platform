// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	public partial interface Commentable : Allors.IObjectBase
	{


		global::System.String Comment 
		{
			get;
			set;
		}

		bool ExistComment{get;}

		void RemoveComment();

	}

	public class CommentableMeta
	{
		public static readonly CommentableMeta Instance = new CommentableMeta();

		public global::Allors.Meta.Interface interface = global::Allors.Meta.Interfaces.Commentable;

		public global::Allors.Meta.RoleType Comment 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.CommentableComment;
			}
		} 

	}

	public partial interface CommentableBuilder :  global::System.IDisposable
	{	

		global::System.String Comment {get;}

		
	}

	public partial class Commentables : global::Allors.ObjectsBase<Commentable>
	{
		public static readonly CommentableMeta Meta = CommentableMeta.Instance;

		public Commentables(Allors.ISession session) : base(session)
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