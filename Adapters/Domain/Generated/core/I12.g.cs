// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	public partial interface I12Builder : S12Builder , global::System.IDisposable
	{	

		global::System.Boolean? I12AllorsBoolean {get;}

		

		global::System.Int32? I12AllorsInteger {get;}

		

		global::System.Collections.Generic.List<I34> I12I34one2many {get;}		

		
		C3 C3many2one {get;}

		
		C2 I12C2many2one {get;}

		

		global::System.Double? I12AllorsDouble {get;}

		
		I34 I12I34many2one {get;}

		

		global::System.Collections.Generic.List<I34> I12I34many2many {get;}		

		
		C3 I12C3one2one {get;}

		

		global::System.Collections.Generic.List<C2> I12C2many2many {get;}		

		

		global::System.Int64? I12AllorsLong {get;}

		

		global::System.Decimal? I12AllorsDecimal {get;}

		
		C2 I12C2one2one {get;}

		

		global::System.Collections.Generic.List<C3> I12C3one2many {get;}		

		

		global::System.Collections.Generic.List<C3> I12C3many2many {get;}		

		

		global::System.String PrefetchTest {get;}

		

		global::System.DateTime? I12AllorsDateTime {get;}

		

		global::System.String I12AllorsString {get;}

		
		I34 I12I34one2one {get;}

		

		global::System.Collections.Generic.List<C2> I12C2one2many {get;}		

		
	}

	public partial class I12s : global::Allors.ObjectsBase<I12>
	{
		public static readonly I12Meta Meta = I12Meta.Instance;

		public I12s(Allors.ISession session) : base(session)
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