// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	public partial interface S12Builder :  global::System.IDisposable
	{	

		global::System.String S12AllorsString {get;}

		

		global::System.DateTime? S12AllorsDateTime {get;}

		

		global::System.Collections.Generic.List<C2> S12C2many2many {get;}		

		
		C2 S12C2many2one {get;}

		
		C2 S12C2one2one {get;}

		

		global::System.Collections.Generic.List<C2> S12C2one2many {get;}		

		

		global::System.Boolean? S12AllorsBoolean {get;}

		

		global::System.Double? S12AllorsDouble {get;}

		

		global::System.Int32? S12AllorsInteger {get;}

		

		global::System.Decimal? S12AllorsDecimal {get;}

		
	}

	public partial class S12s : global::Allors.ObjectsBase<S12>
	{
		public static readonly S12Meta Meta = S12Meta.Instance;

		public S12s(Allors.ISession session) : base(session)
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