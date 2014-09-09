// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
    using Allors.Meta;

    public partial interface Period : Allors.IObject
	{


		global::System.DateTime? PeriodFromDate 
		{
			get;
			set;
		}

		bool ExistPeriodFromDate{get;}

		void RemovePeriodFromDate();


		global::System.DateTime? PeriodThroughDate 
		{
			get;
			set;
		}

		bool ExistPeriodThroughDate{get;}

		void RemovePeriodThroughDate();

	}

	public class PeriodMeta
	{
		public static readonly PeriodMeta Instance = new PeriodMeta();

		public global::Allors.Meta.Interface ObjectType = global::Allors.Meta.Interfaces.Period;

		public global::Allors.Meta.RoleType PeriodFromDate 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.PeriodFromDate;
			}
		} 
		public global::Allors.Meta.RoleType PeriodThroughDate 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.PeriodThroughDate;
			}
		} 

	}

	public partial interface PeriodBuilder :  global::System.IDisposable
	{	

		global::System.DateTime? PeriodFromDate {get;}

		

		global::System.DateTime? PeriodThroughDate {get;}

		
	}

	public partial class Periods : global::Allors.ObjectsBase<Period>
	{
		public static readonly PeriodMeta Meta = PeriodMeta.Instance;

		public Periods(Allors.ISession session) : base(session)
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