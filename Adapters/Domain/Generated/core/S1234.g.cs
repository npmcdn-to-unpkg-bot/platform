// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	public partial interface S1234Builder :  global::System.IDisposable
	{	

		global::System.Double? S1234AllorsDouble {get;}

		

		global::System.Decimal? S1234AllorsDecimal {get;}

		

		global::System.Int32? S1234AllorsInteger {get;}

		
		S1234 S1234many2one {get;}

		
		C2 S1234C2one2one {get;}

		

		global::System.Collections.Generic.List<C2> S1234C2many2many {get;}		

		

		global::System.Collections.Generic.List<S1234> S1234one2many {get;}		

		

		global::System.Collections.Generic.List<C2> S1234C2one2many {get;}		

		

		global::System.Collections.Generic.List<S1234> S1234many2many {get;}		

		

		global::System.Int64? S1234AllorsLong {get;}

		

		global::System.String ClassName {get;}

		

		global::System.DateTime? S1234AllorsDateTime {get;}

		
		S1234 S1234one2one {get;}

		
		C2 S1234C2many2one {get;}

		

		global::System.String S1234AllorsString {get;}

		

		global::System.Boolean? S1234AllorsBoolean {get;}

		
	}

	public partial class S1234s : global::Allors.ObjectsBase<S1234>
	{
		public static readonly S1234Meta Meta = S1234Meta.Instance;

		public S1234s(Allors.ISession session) : base(session)
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