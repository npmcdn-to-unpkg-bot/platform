// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	public partial interface ILT32Unit : Allors.IObjectBase
	{


		global::System.String AllorsString1 
		{
			get;
			set;
		}

		bool ExistAllorsString1{get;}

		void RemoveAllorsString1();


		global::System.String AllorsString3 
		{
			get;
			set;
		}

		bool ExistAllorsString3{get;}

		void RemoveAllorsString3();


		global::System.String AllorsString2 
		{
			get;
			set;
		}

		bool ExistAllorsString2{get;}

		void RemoveAllorsString2();

	}

	public class ILT32UnitMeta
	{
		public static readonly ILT32UnitMeta Instance = new ILT32UnitMeta();

		public global::Allors.Meta.Interface ObjectType = global::Allors.Meta.Interfaces.ILT32Unit;

		public global::Allors.Meta.IRoleType AllorsString1 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.ILT32UnitAllorsString1;
			}
		} 
		public global::Allors.Meta.IRoleType AllorsString3 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.ILT32UnitAllorsString3;
			}
		} 
		public global::Allors.Meta.IRoleType AllorsString2 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.ILT32UnitAllorsString2;
			}
		} 

	}
}