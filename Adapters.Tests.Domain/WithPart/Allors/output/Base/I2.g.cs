// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Domain
{
	public partial interface I2 :  global::Domain.S1234,global::Domain.S2, Allors.R1.IObject
	{


		global::System.Boolean? I2AllorsBoolean 
		{
			get;
			set;
		}

		bool ExistI2AllorsBoolean{get;}

		void RemoveI2AllorsBoolean();


		global::System.Decimal? I2AllorsDecimal 
		{
			get;
			set;
		}

		bool ExistI2AllorsDecimal{get;}

		void RemoveI2AllorsDecimal();


		global::System.DateTime? I2AllorsDateTime 
		{
			get;
			set;
		}

		bool ExistI2AllorsDateTime{get;}

		void RemoveI2AllorsDateTime();


		global::System.String I2AllorsString 
		{
			get;
			set;
		}

		bool ExistI2AllorsString{get;}

		void RemoveI2AllorsString();


		global::System.Int64? I2AllorsLong 
		{
			get;
			set;
		}

		bool ExistI2AllorsLong{get;}

		void RemoveI2AllorsLong();


		global::System.Int32? I2AllorsInteger 
		{
			get;
			set;
		}

		bool ExistI2AllorsInteger{get;}

		void RemoveI2AllorsInteger();


		global::System.Double? I2AllorsDouble 
		{
			get;
			set;
		}

		bool ExistI2AllorsDouble{get;}

		void RemoveI2AllorsDouble();



		global::Domain.I1 I1WhereI2one2many
		{
			get;
		}

		bool ExistI1WhereI2one2many
		{
			get;
		}


		global::Domain.C1 C1WhereI2one2one
		{
			get;
		}

		bool ExistC1WhereI2one2one
		{
			get;
		}


		Allors.R1.Extent<global::Domain.I1> I1sWhereI2many2one
		{ 
			get;
		}

		bool ExistI1sWhereI2many2one
		{
			get;
		}


		Allors.R1.Extent<global::Domain.I1> I1sWhereI2many2many
		{ 
			get;
		}

		bool ExistI1sWhereI2many2many
		{
			get;
		}


		global::Domain.C1 C1WhereI2one2many
		{
			get;
		}

		bool ExistC1WhereI2one2many
		{
			get;
		}


		Allors.R1.Extent<global::Domain.C1> C1sWhereI2many2one
		{ 
			get;
		}

		bool ExistC1sWhereI2many2one
		{
			get;
		}


		global::Domain.I1 I1WhereI2one2one
		{
			get;
		}

		bool ExistI1WhereI2one2one
		{
			get;
		}


		Allors.R1.Extent<global::Domain.C1> C1sWhereI2many2many
		{ 
			get;
		}

		bool ExistC1sWhereI2many2many
		{
			get;
		}

	}

}