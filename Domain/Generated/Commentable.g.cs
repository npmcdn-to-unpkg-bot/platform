// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Domain
{
	public partial interface Commentable : Allors.IObject
	{
	}

	public static class CommentableMeta
	{
		public static readonly global::Allors.Meta.Interface ObjectType = (Allors.Meta.Interface)global::Allors.Meta.Repository.MetaPopulation.Find( new System.Guid("fdd52472-e863-4e91-bb01-1dada2acc8f6") );

		public static readonly global::Allors.Meta.RoleType CommentableComment = ((Allors.Meta.RelationType)global::Allors.Meta.Repository.MetaPopulation.Find( new System.Guid("d800f9a2-fadd-45f1-8731-4dac177c6b1b"))).RoleType;

	}
}