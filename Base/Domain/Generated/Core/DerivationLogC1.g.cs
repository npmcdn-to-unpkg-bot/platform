// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	public partial class DerivationLogC1 : Allors.ObjectBase , DerivationLogI12
	{
		public static readonly DerivationLogC1Meta Meta = DerivationLogC1Meta.Instance;

		public DerivationLogC1(Allors.IStrategy allors) : base(allors) {}

		public static DerivationLogC1 Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (DerivationLogC1) allorsSession.Instantiate(allorsObjectId);		
		}




		virtual public global::System.Guid UniqueId 
		{
			get
			{
				return (global::System.Guid) Strategy.GetUnitRole(Meta.UniqueId);
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

	}

	public class DerivationLogC1Meta
	{
		public static readonly DerivationLogC1Meta Instance = new DerivationLogC1Meta();

		public global::Allors.Meta.Class ObjectType = global::Allors.Meta.Classes.DerivationLogC1;

		public global::Allors.Meta.RoleType UniqueId 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.DerivationLogI12UniqueId;
			}
		} 

	}
}