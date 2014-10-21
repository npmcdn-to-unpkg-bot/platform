// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	public partial class C4 : Allors.ObjectBase , I4, I34
	{
		public static readonly C4Meta Meta = C4Meta.Instance;

		public C4(Allors.IStrategy allors) : base(allors) {}

		public static C4 Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (C4) allorsSession.Instantiate(allorsObjectId);		
		}




		virtual public global::System.String C4AllorsString 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(Meta.C4AllorsString);
			}
			set
			{
				Strategy.SetUnitRole(Meta.C4AllorsString, value);
			}
		}

		virtual public bool ExistC4AllorsString{
			get
			{
				return Strategy.ExistUnitRole(Meta.C4AllorsString);
			}
		}

		virtual public void RemoveC4AllorsString()
		{
			Strategy.RemoveUnitRole(Meta.C4AllorsString);
		}



		virtual public global::System.Double? S1234AllorsFloat 
		{
			get
			{
				return (global::System.Double?) Strategy.GetUnitRole(Meta.S1234AllorsFloat);
			}
			set
			{
				Strategy.SetUnitRole(Meta.S1234AllorsFloat, value);
			}
		}

		virtual public bool ExistS1234AllorsFloat{
			get
			{
				return Strategy.ExistUnitRole(Meta.S1234AllorsFloat);
			}
		}

		virtual public void RemoveS1234AllorsFloat()
		{
			Strategy.RemoveUnitRole(Meta.S1234AllorsFloat);
		}



		virtual public global::System.Decimal? S1234AllorsDecimal 
		{
			get
			{
				return (global::System.Decimal?) Strategy.GetUnitRole(Meta.S1234AllorsDecimal);
			}
			set
			{
				Strategy.SetUnitRole(Meta.S1234AllorsDecimal, value);
			}
		}

		virtual public bool ExistS1234AllorsDecimal{
			get
			{
				return Strategy.ExistUnitRole(Meta.S1234AllorsDecimal);
			}
		}

		virtual public void RemoveS1234AllorsDecimal()
		{
			Strategy.RemoveUnitRole(Meta.S1234AllorsDecimal);
		}



		virtual public global::System.Int32? S1234AllorsInteger 
		{
			get
			{
				return (global::System.Int32?) Strategy.GetUnitRole(Meta.S1234AllorsInteger);
			}
			set
			{
				Strategy.SetUnitRole(Meta.S1234AllorsInteger, value);
			}
		}

		virtual public bool ExistS1234AllorsInteger{
			get
			{
				return Strategy.ExistUnitRole(Meta.S1234AllorsInteger);
			}
		}

		virtual public void RemoveS1234AllorsInteger()
		{
			Strategy.RemoveUnitRole(Meta.S1234AllorsInteger);
		}


		virtual public S1234 S1234many2one
		{ 
			get
			{
				return (S1234) Strategy.GetCompositeRole(Meta.S1234many2one);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.S1234many2one ,value);
			}
		}

		virtual public bool ExistS1234many2one
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.S1234many2one);
			}
		}

		virtual public void RemoveS1234many2one()
		{
			Strategy.RemoveCompositeRole(Meta.S1234many2one);
		}


		virtual public C2 S1234C2one2one
		{ 
			get
			{
				return (C2) Strategy.GetCompositeRole(Meta.S1234C2one2one);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.S1234C2one2one ,value);
			}
		}

		virtual public bool ExistS1234C2one2one
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.S1234C2one2one);
			}
		}

		virtual public void RemoveS1234C2one2one()
		{
			Strategy.RemoveCompositeRole(Meta.S1234C2one2one);
		}


		virtual public global::Allors.Extent<C2> S1234C2many2manies
		{ 
			get
			{
				return Strategy.GetCompositeRoles(Meta.S1234C2many2many);
			}
			set
			{
				Strategy.SetCompositeRoles(Meta.S1234C2many2many, value);
			}
		}

		virtual public void AddS1234C2many2many (C2 value)
		{
			Strategy.AddCompositeRole(Meta.S1234C2many2many, value);
		}

		virtual public void RemoveS1234C2many2many (C2 value)
		{
			Strategy.RemoveCompositeRole(Meta.S1234C2many2many,value);
		}

		virtual public bool ExistS1234C2many2manies
		{
			get
			{
				return Strategy.ExistCompositeRoles(Meta.S1234C2many2many);
			}
		}

		virtual public void RemoveS1234C2many2manies()
		{
			Strategy.RemoveCompositeRoles(Meta.S1234C2many2many);
		}


		virtual public global::Allors.Extent<S1234> S1234one2manies
		{ 
			get
			{
				return Strategy.GetCompositeRoles(Meta.S1234one2many);
			}
			set
			{
				Strategy.SetCompositeRoles(Meta.S1234one2many, value);
			}
		}

		virtual public void AddS1234one2many (S1234 value)
		{
			Strategy.AddCompositeRole(Meta.S1234one2many, value);
		}

		virtual public void RemoveS1234one2many (S1234 value)
		{
			Strategy.RemoveCompositeRole(Meta.S1234one2many,value);
		}

		virtual public bool ExistS1234one2manies
		{
			get
			{
				return Strategy.ExistCompositeRoles(Meta.S1234one2many);
			}
		}

		virtual public void RemoveS1234one2manies()
		{
			Strategy.RemoveCompositeRoles(Meta.S1234one2many);
		}


		virtual public global::Allors.Extent<C2> S1234C2one2manies
		{ 
			get
			{
				return Strategy.GetCompositeRoles(Meta.S1234C2one2many);
			}
			set
			{
				Strategy.SetCompositeRoles(Meta.S1234C2one2many, value);
			}
		}

		virtual public void AddS1234C2one2many (C2 value)
		{
			Strategy.AddCompositeRole(Meta.S1234C2one2many, value);
		}

		virtual public void RemoveS1234C2one2many (C2 value)
		{
			Strategy.RemoveCompositeRole(Meta.S1234C2one2many,value);
		}

		virtual public bool ExistS1234C2one2manies
		{
			get
			{
				return Strategy.ExistCompositeRoles(Meta.S1234C2one2many);
			}
		}

		virtual public void RemoveS1234C2one2manies()
		{
			Strategy.RemoveCompositeRoles(Meta.S1234C2one2many);
		}


		virtual public global::Allors.Extent<S1234> S1234many2manies
		{ 
			get
			{
				return Strategy.GetCompositeRoles(Meta.S1234many2many);
			}
			set
			{
				Strategy.SetCompositeRoles(Meta.S1234many2many, value);
			}
		}

		virtual public void AddS1234many2many (S1234 value)
		{
			Strategy.AddCompositeRole(Meta.S1234many2many, value);
		}

		virtual public void RemoveS1234many2many (S1234 value)
		{
			Strategy.RemoveCompositeRole(Meta.S1234many2many,value);
		}

		virtual public bool ExistS1234many2manies
		{
			get
			{
				return Strategy.ExistCompositeRoles(Meta.S1234many2many);
			}
		}

		virtual public void RemoveS1234many2manies()
		{
			Strategy.RemoveCompositeRoles(Meta.S1234many2many);
		}



		virtual public global::System.String ClassName 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(Meta.ClassName);
			}
			set
			{
				Strategy.SetUnitRole(Meta.ClassName, value);
			}
		}

		virtual public bool ExistClassName{
			get
			{
				return Strategy.ExistUnitRole(Meta.ClassName);
			}
		}

		virtual public void RemoveClassName()
		{
			Strategy.RemoveUnitRole(Meta.ClassName);
		}



		virtual public global::System.DateTime? S1234AllorsDate 
		{
			get
			{
				return (global::System.DateTime?) Strategy.GetUnitRole(Meta.S1234AllorsDate);
			}
			set
			{
				Strategy.SetUnitRole(Meta.S1234AllorsDate, value);
			}
		}

		virtual public bool ExistS1234AllorsDate{
			get
			{
				return Strategy.ExistUnitRole(Meta.S1234AllorsDate);
			}
		}

		virtual public void RemoveS1234AllorsDate()
		{
			Strategy.RemoveUnitRole(Meta.S1234AllorsDate);
		}


		virtual public S1234 S1234one2one
		{ 
			get
			{
				return (S1234) Strategy.GetCompositeRole(Meta.S1234one2one);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.S1234one2one ,value);
			}
		}

		virtual public bool ExistS1234one2one
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.S1234one2one);
			}
		}

		virtual public void RemoveS1234one2one()
		{
			Strategy.RemoveCompositeRole(Meta.S1234one2one);
		}


		virtual public C2 S1234C2many2one
		{ 
			get
			{
				return (C2) Strategy.GetCompositeRole(Meta.S1234C2many2one);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.S1234C2many2one ,value);
			}
		}

		virtual public bool ExistS1234C2many2one
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.S1234C2many2one);
			}
		}

		virtual public void RemoveS1234C2many2one()
		{
			Strategy.RemoveCompositeRole(Meta.S1234C2many2one);
		}



		virtual public global::System.String S1234AllorsString 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(Meta.S1234AllorsString);
			}
			set
			{
				Strategy.SetUnitRole(Meta.S1234AllorsString, value);
			}
		}

		virtual public bool ExistS1234AllorsString{
			get
			{
				return Strategy.ExistUnitRole(Meta.S1234AllorsString);
			}
		}

		virtual public void RemoveS1234AllorsString()
		{
			Strategy.RemoveUnitRole(Meta.S1234AllorsString);
		}



		virtual public global::System.Boolean? S1234AllorsBoolean 
		{
			get
			{
				return (global::System.Boolean?) Strategy.GetUnitRole(Meta.S1234AllorsBoolean);
			}
			set
			{
				Strategy.SetUnitRole(Meta.S1234AllorsBoolean, value);
			}
		}

		virtual public bool ExistS1234AllorsBoolean{
			get
			{
				return Strategy.ExistUnitRole(Meta.S1234AllorsBoolean);
			}
		}

		virtual public void RemoveS1234AllorsBoolean()
		{
			Strategy.RemoveUnitRole(Meta.S1234AllorsBoolean);
		}



		virtual public global::System.Decimal? I34AllorsDecimal 
		{
			get
			{
				return (global::System.Decimal?) Strategy.GetUnitRole(Meta.I34AllorsDecimal);
			}
			set
			{
				Strategy.SetUnitRole(Meta.I34AllorsDecimal, value);
			}
		}

		virtual public bool ExistI34AllorsDecimal{
			get
			{
				return Strategy.ExistUnitRole(Meta.I34AllorsDecimal);
			}
		}

		virtual public void RemoveI34AllorsDecimal()
		{
			Strategy.RemoveUnitRole(Meta.I34AllorsDecimal);
		}



		virtual public global::System.Boolean? I34AllorsBoolean 
		{
			get
			{
				return (global::System.Boolean?) Strategy.GetUnitRole(Meta.I34AllorsBoolean);
			}
			set
			{
				Strategy.SetUnitRole(Meta.I34AllorsBoolean, value);
			}
		}

		virtual public bool ExistI34AllorsBoolean{
			get
			{
				return Strategy.ExistUnitRole(Meta.I34AllorsBoolean);
			}
		}

		virtual public void RemoveI34AllorsBoolean()
		{
			Strategy.RemoveUnitRole(Meta.I34AllorsBoolean);
		}



		virtual public global::System.Double? I34AllorsFloat 
		{
			get
			{
				return (global::System.Double?) Strategy.GetUnitRole(Meta.I34AllorsFloat);
			}
			set
			{
				Strategy.SetUnitRole(Meta.I34AllorsFloat, value);
			}
		}

		virtual public bool ExistI34AllorsFloat{
			get
			{
				return Strategy.ExistUnitRole(Meta.I34AllorsFloat);
			}
		}

		virtual public void RemoveI34AllorsFloat()
		{
			Strategy.RemoveUnitRole(Meta.I34AllorsFloat);
		}



		virtual public global::System.Int32? I34AllorsInteger 
		{
			get
			{
				return (global::System.Int32?) Strategy.GetUnitRole(Meta.I34AllorsInteger);
			}
			set
			{
				Strategy.SetUnitRole(Meta.I34AllorsInteger, value);
			}
		}

		virtual public bool ExistI34AllorsInteger{
			get
			{
				return Strategy.ExistUnitRole(Meta.I34AllorsInteger);
			}
		}

		virtual public void RemoveI34AllorsInteger()
		{
			Strategy.RemoveUnitRole(Meta.I34AllorsInteger);
		}



		virtual public global::System.String I34AllorsString 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(Meta.I34AllorsString);
			}
			set
			{
				Strategy.SetUnitRole(Meta.I34AllorsString, value);
			}
		}

		virtual public bool ExistI34AllorsString{
			get
			{
				return Strategy.ExistUnitRole(Meta.I34AllorsString);
			}
		}

		virtual public void RemoveI34AllorsString()
		{
			Strategy.RemoveUnitRole(Meta.I34AllorsString);
		}



		virtual public global::Allors.Extent<C3> C3sWhereC4many2one
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.C3sWhereC4many2one);
			}
		}

		virtual public bool ExistC3sWhereC4many2one
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.C3sWhereC4many2one);
			}
		}


		virtual public global::Allors.Extent<C3> C3sWhereC4many2many
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.C3sWhereC4many2many);
			}
		}

		virtual public bool ExistC3sWhereC4many2many
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.C3sWhereC4many2many);
			}
		}


		virtual public C3 C3WhereC4one2many
		{ 
			get
			{
				return (C3) Strategy.GetCompositeAssociation(Meta.C3WhereC4one2many);
			}
		} 

		virtual public bool ExistC3WhereC4one2many
		{
			get
			{
				return Strategy.ExistCompositeAssociation(Meta.C3WhereC4one2many);
			}
		}


		virtual public C3 C3WhereC4one2one
		{ 
			get
			{
				return (C3) Strategy.GetCompositeAssociation(Meta.C3WhereC4one2one);
			}
		} 

		virtual public bool ExistC3WhereC4one2one
		{
			get
			{
				return Strategy.ExistCompositeAssociation(Meta.C3WhereC4one2one);
			}
		}


		virtual public global::Allors.Extent<I3> I3sWhereC4many2many
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.I3sWhereC4many2many);
			}
		}

		virtual public bool ExistI3sWhereC4many2many
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.I3sWhereC4many2many);
			}
		}


		virtual public I3 I3WhereC4one2many
		{ 
			get
			{
				return (I3) Strategy.GetCompositeAssociation(Meta.I3WhereC4one2many);
			}
		} 

		virtual public bool ExistI3WhereC4one2many
		{
			get
			{
				return Strategy.ExistCompositeAssociation(Meta.I3WhereC4one2many);
			}
		}


		virtual public I3 I3WhereC4one2one
		{ 
			get
			{
				return (I3) Strategy.GetCompositeAssociation(Meta.I3WhereC4one2one);
			}
		} 

		virtual public bool ExistI3WhereC4one2one
		{
			get
			{
				return Strategy.ExistCompositeAssociation(Meta.I3WhereC4one2one);
			}
		}


		virtual public global::Allors.Extent<I3> I3sWhereC4many2one
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.I3sWhereC4many2one);
			}
		}

		virtual public bool ExistI3sWhereC4many2one
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.I3sWhereC4many2one);
			}
		}


		virtual public C3 C3WhereI4one2one
		{ 
			get
			{
				return (C3) Strategy.GetCompositeAssociation(Meta.C3WhereI4one2one);
			}
		} 

		virtual public bool ExistC3WhereI4one2one
		{
			get
			{
				return Strategy.ExistCompositeAssociation(Meta.C3WhereI4one2one);
			}
		}


		virtual public global::Allors.Extent<C3> C3sWhereI4many2many
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.C3sWhereI4many2many);
			}
		}

		virtual public bool ExistC3sWhereI4many2many
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.C3sWhereI4many2many);
			}
		}


		virtual public global::Allors.Extent<C3> C3sWhereI4many2one
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.C3sWhereI4many2one);
			}
		}

		virtual public bool ExistC3sWhereI4many2one
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.C3sWhereI4many2one);
			}
		}


		virtual public C3 C3WhereI4one2many
		{ 
			get
			{
				return (C3) Strategy.GetCompositeAssociation(Meta.C3WhereI4one2many);
			}
		} 

		virtual public bool ExistC3WhereI4one2many
		{
			get
			{
				return Strategy.ExistCompositeAssociation(Meta.C3WhereI4one2many);
			}
		}


		virtual public I3 I3WhereI4one2many
		{ 
			get
			{
				return (I3) Strategy.GetCompositeAssociation(Meta.I3WhereI4one2many);
			}
		} 

		virtual public bool ExistI3WhereI4one2many
		{
			get
			{
				return Strategy.ExistCompositeAssociation(Meta.I3WhereI4one2many);
			}
		}


		virtual public global::Allors.Extent<I3> I3sWhereI4many2many
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.I3sWhereI4many2many);
			}
		}

		virtual public bool ExistI3sWhereI4many2many
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.I3sWhereI4many2many);
			}
		}


		virtual public global::Allors.Extent<I3> I3sWhereI4many2one
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.I3sWhereI4many2one);
			}
		}

		virtual public bool ExistI3sWhereI4many2one
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.I3sWhereI4many2one);
			}
		}


		virtual public I3 I3WhereI4one2one
		{ 
			get
			{
				return (I3) Strategy.GetCompositeAssociation(Meta.I3WhereI4one2one);
			}
		} 

		virtual public bool ExistI3WhereI4one2one
		{
			get
			{
				return Strategy.ExistCompositeAssociation(Meta.I3WhereI4one2one);
			}
		}


		virtual public global::Allors.Extent<S1234> S1234sWhereS1234many2one
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.S1234sWhereS1234many2one);
			}
		}

		virtual public bool ExistS1234sWhereS1234many2one
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.S1234sWhereS1234many2one);
			}
		}


		virtual public S1234 S1234WhereS1234one2many
		{ 
			get
			{
				return (S1234) Strategy.GetCompositeAssociation(Meta.S1234WhereS1234one2many);
			}
		} 

		virtual public bool ExistS1234WhereS1234one2many
		{
			get
			{
				return Strategy.ExistCompositeAssociation(Meta.S1234WhereS1234one2many);
			}
		}


		virtual public global::Allors.Extent<S1234> S1234sWhereS1234many2many
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.S1234sWhereS1234many2many);
			}
		}

		virtual public bool ExistS1234sWhereS1234many2many
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.S1234sWhereS1234many2many);
			}
		}


		virtual public S1234 S1234WhereS1234one2one
		{ 
			get
			{
				return (S1234) Strategy.GetCompositeAssociation(Meta.S1234WhereS1234one2one);
			}
		} 

		virtual public bool ExistS1234WhereS1234one2one
		{
			get
			{
				return Strategy.ExistCompositeAssociation(Meta.S1234WhereS1234one2one);
			}
		}


		virtual public I12 I12WhereI34one2many
		{ 
			get
			{
				return (I12) Strategy.GetCompositeAssociation(Meta.I12WhereI34one2many);
			}
		} 

		virtual public bool ExistI12WhereI34one2many
		{
			get
			{
				return Strategy.ExistCompositeAssociation(Meta.I12WhereI34one2many);
			}
		}


		virtual public global::Allors.Extent<I12> I12sWhereI34many2one
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.I12sWhereI34many2one);
			}
		}

		virtual public bool ExistI12sWhereI34many2one
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.I12sWhereI34many2one);
			}
		}


		virtual public global::Allors.Extent<I12> I12sWhereI34many2many
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.I12sWhereI34many2many);
			}
		}

		virtual public bool ExistI12sWhereI34many2many
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.I12sWhereI34many2many);
			}
		}


		virtual public I12 I12WhereI34one2one
		{ 
			get
			{
				return (I12) Strategy.GetCompositeAssociation(Meta.I12WhereI34one2one);
			}
		} 

		virtual public bool ExistI12WhereI34one2one
		{
			get
			{
				return Strategy.ExistCompositeAssociation(Meta.I12WhereI34one2one);
			}
		}


		virtual public I1 I1WhereI34one2many
		{ 
			get
			{
				return (I1) Strategy.GetCompositeAssociation(Meta.I1WhereI34one2many);
			}
		} 

		virtual public bool ExistI1WhereI34one2many
		{
			get
			{
				return Strategy.ExistCompositeAssociation(Meta.I1WhereI34one2many);
			}
		}


		virtual public global::Allors.Extent<I1> I1sWhereI34many2one
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.I1sWhereI34many2one);
			}
		}

		virtual public bool ExistI1sWhereI34many2one
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.I1sWhereI34many2one);
			}
		}


		virtual public global::Allors.Extent<I1> I1sWhereI34many2many
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.I1sWhereI34many2many);
			}
		}

		virtual public bool ExistI1sWhereI34many2many
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.I1sWhereI34many2many);
			}
		}


		virtual public I1 I1WhereI34one2one
		{ 
			get
			{
				return (I1) Strategy.GetCompositeAssociation(Meta.I1WhereI34one2one);
			}
		} 

		virtual public bool ExistI1WhereI34one2one
		{
			get
			{
				return Strategy.ExistCompositeAssociation(Meta.I1WhereI34one2one);
			}
		}

	}

	public class C4Meta
	{
		public static readonly C4Meta Instance = new C4Meta();

		public global::Allors.Meta.Class ObjectType = global::Allors.Meta.Classes.C4;

		public global::Allors.Meta.RoleType C4AllorsString 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.C4AllorsString;
			}
		} 
		public global::Allors.Meta.RoleType S1234AllorsFloat 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.S1234AllorsFloat;
			}
		} 
		public global::Allors.Meta.RoleType S1234AllorsDecimal 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.S1234AllorsDecimal;
			}
		} 
		public global::Allors.Meta.RoleType S1234AllorsInteger 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.S1234AllorsInteger;
			}
		} 
		public global::Allors.Meta.RoleType S1234many2one 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.S1234S1234many2one;
			}
		} 
		public global::Allors.Meta.RoleType S1234C2one2one 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.S1234C2one2one;
			}
		} 
		public global::Allors.Meta.RoleType S1234C2many2many 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.S1234C2many2many;
			}
		} 
		public global::Allors.Meta.RoleType S1234one2many 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.S1234S1234one2many;
			}
		} 
		public global::Allors.Meta.RoleType S1234C2one2many 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.S1234C2one2many;
			}
		} 
		public global::Allors.Meta.RoleType S1234many2many 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.S1234S1234many2many;
			}
		} 
		public global::Allors.Meta.RoleType ClassName 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.S1234ClassName;
			}
		} 
		public global::Allors.Meta.RoleType S1234AllorsDate 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.S1234AllorsDate;
			}
		} 
		public global::Allors.Meta.RoleType S1234one2one 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.S1234S1234one2one;
			}
		} 
		public global::Allors.Meta.RoleType S1234C2many2one 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.S1234C2many2one;
			}
		} 
		public global::Allors.Meta.RoleType S1234AllorsString 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.S1234AllorsString;
			}
		} 
		public global::Allors.Meta.RoleType S1234AllorsBoolean 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.S1234AllorsBoolean;
			}
		} 
		public global::Allors.Meta.RoleType I34AllorsDecimal 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.I34AllorsDecimal;
			}
		} 
		public global::Allors.Meta.RoleType I34AllorsBoolean 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.I34AllorsBoolean;
			}
		} 
		public global::Allors.Meta.RoleType I34AllorsFloat 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.I34AllorsFloat;
			}
		} 
		public global::Allors.Meta.RoleType I34AllorsInteger 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.I34AllorsInteger;
			}
		} 
		public global::Allors.Meta.RoleType I34AllorsString 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.I34AllorsString;
			}
		} 

		public global::Allors.Meta.AssociationType C3sWhereC4many2one 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.C3C4many2one;
			}
		} 
		public global::Allors.Meta.AssociationType C3sWhereC4many2many 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.C3C4many2many;
			}
		} 
		public global::Allors.Meta.AssociationType C3WhereC4one2many 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.C3C4one2many;
			}
		} 
		public global::Allors.Meta.AssociationType C3WhereC4one2one 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.C3C4one2one;
			}
		} 
		public global::Allors.Meta.AssociationType I3sWhereC4many2many 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.I3C4many2many;
			}
		} 
		public global::Allors.Meta.AssociationType I3WhereC4one2many 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.I3C4one2many;
			}
		} 
		public global::Allors.Meta.AssociationType I3WhereC4one2one 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.I3C4one2one;
			}
		} 
		public global::Allors.Meta.AssociationType I3sWhereC4many2one 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.I3C4many2one;
			}
		} 
		public global::Allors.Meta.AssociationType C3WhereI4one2one 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.C3I4one2one;
			}
		} 
		public global::Allors.Meta.AssociationType C3sWhereI4many2many 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.C3I4many2many;
			}
		} 
		public global::Allors.Meta.AssociationType C3sWhereI4many2one 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.C3I4many2one;
			}
		} 
		public global::Allors.Meta.AssociationType C3WhereI4one2many 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.C3I4one2many;
			}
		} 
		public global::Allors.Meta.AssociationType I3WhereI4one2many 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.I3I4one2many;
			}
		} 
		public global::Allors.Meta.AssociationType I3sWhereI4many2many 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.I3I4many2many;
			}
		} 
		public global::Allors.Meta.AssociationType I3sWhereI4many2one 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.I3I4many2one;
			}
		} 
		public global::Allors.Meta.AssociationType I3WhereI4one2one 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.I3I4one2one;
			}
		} 
		public global::Allors.Meta.AssociationType S1234sWhereS1234many2one 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.S1234S1234many2one;
			}
		} 
		public global::Allors.Meta.AssociationType S1234WhereS1234one2many 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.S1234S1234one2many;
			}
		} 
		public global::Allors.Meta.AssociationType S1234sWhereS1234many2many 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.S1234S1234many2many;
			}
		} 
		public global::Allors.Meta.AssociationType S1234WhereS1234one2one 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.S1234S1234one2one;
			}
		} 
		public global::Allors.Meta.AssociationType I12WhereI34one2many 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.I12I34one2many;
			}
		} 
		public global::Allors.Meta.AssociationType I12sWhereI34many2one 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.I12I34many2one;
			}
		} 
		public global::Allors.Meta.AssociationType I12sWhereI34many2many 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.I12I34many2many;
			}
		} 
		public global::Allors.Meta.AssociationType I12WhereI34one2one 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.I12I34one2one;
			}
		} 
		public global::Allors.Meta.AssociationType I1WhereI34one2many 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.I1I34one2many;
			}
		} 
		public global::Allors.Meta.AssociationType I1sWhereI34many2one 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.I1I34many2one;
			}
		} 
		public global::Allors.Meta.AssociationType I1sWhereI34many2many 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.I1I34many2many;
			}
		} 
		public global::Allors.Meta.AssociationType I1WhereI34one2one 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.I1I34one2one;
			}
		} 

	}
}