// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	public partial interface SecurityTokenOwner : Allors.IObject
	{


		SecurityToken OwnerSecurityToken
		{ 
			get;
			set;
		}

		bool ExistOwnerSecurityToken
		{
			get;
		}

		void RemoveOwnerSecurityToken();

	}

	public class SecurityTokenOwnerMeta
	{
		public static readonly SecurityTokenOwnerMeta Instance = new SecurityTokenOwnerMeta();

		public global::Allors.Meta.Interface ObjectType = global::Allors.Meta.Interfaces.SecurityTokenOwner;

		public global::Allors.Meta.RoleType OwnerSecurityToken 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.SecurityTokenOwnerOwnerSecurityToken;
			}
		} 

	}
}