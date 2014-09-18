// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	public partial interface I12Builder : UserInterfaceableBuilder ,AccessControlledObjectBuilder ,SearchableBuilder , global::System.IDisposable
	{	

		global::System.Byte[] I12AllorsBinary {get;}

		
		C2 I12C2One2One {get;}

		

		global::System.Double? I12AllorsDouble {get;}

		
		I1 I12I1Many2One {get;}

		

		global::System.String I12AllorsString {get;}

		

		global::System.Collections.Generic.List<I12> I12I12Many2Many {get;}		

		

		global::System.Decimal? I12AllorsDecimal {get;}

		

		global::System.Collections.Generic.List<I2> I12I2Many2Many {get;}		

		

		global::System.Collections.Generic.List<C2> I12C2Many2Many {get;}		

		

		global::System.Collections.Generic.List<I1> I12I1Many2Many {get;}		

		

		global::System.Collections.Generic.List<I12> I12I12One2Many {get;}		

		

		global::System.String Name {get;}

		

		global::System.Collections.Generic.List<C1> I12C1Many2Many {get;}		

		
		I2 I12I2Many2One {get;}

		

		global::System.Guid? I12AllorsUnique {get;}

		

		global::System.Int32? I12AllorsInteger {get;}

		

		global::System.Collections.Generic.List<I1> I12I1One2Many {get;}		

		
		C1 I12C1One2One {get;}

		

		global::System.Int64? I12AllorsLong {get;}

		
		I12 I12I12One2One {get;}

		
		I2 I12I2One2One {get;}

		

		global::System.Collections.Generic.List<I12> Dependency {get;}		

		

		global::System.Collections.Generic.List<I2> I12I2One2Many {get;}		

		
		C2 I12C2Many2One {get;}

		
		I12 I12I12Many2One {get;}

		

		global::System.Boolean? I12AllorsBoolean {get;}

		
		I1 I12I1One2One {get;}

		

		global::System.DateTime? I12AllorsDateTime {get;}

		

		global::System.Collections.Generic.List<C1> I12C1One2Many {get;}		

		
		C1 I12C1Many2One {get;}

		
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