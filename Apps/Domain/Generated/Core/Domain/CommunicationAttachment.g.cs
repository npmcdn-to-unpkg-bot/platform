// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	public partial interface CommunicationAttachment :  UserInterfaceable, Allors.IObjectBase
	{
	}

	public class CommunicationAttachmentMeta
	{
		public static readonly CommunicationAttachmentMeta Instance = new CommunicationAttachmentMeta();

		public global::Allors.Meta.Interface ObjectType = global::Allors.Meta.Interfaces.CommunicationAttachment;

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

	}
}