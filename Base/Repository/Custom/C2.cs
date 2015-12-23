namespace Allors.Repository.Domain
{
    using System;

    #region Allors
    [Id("72c07e8a-03f5-4da8-ab37-236333d4f74e")]
    #endregion
    public partial class C2 : Object, I2 
    {
        public I2 I2I2Many2One { get; set; }


        public C1 I2C1Many2One { get; set; }


        public I12 I2I12Many2One { get; set; }


        public bool I2AllorsBoolean { get; set; }


        public C1[] I2C1One2Manies { get; set; }


        public C1 I2C1One2One { get; set; }


        public decimal I2AllorsDecimal { get; set; }


        public I2[] I2I2Many2Manies { get; set; }


        public byte[] I2AllorsBinary { get; set; }


        public Guid I2AllorsUnique { get; set; }


        public I1 I2I1Many2One { get; set; }


        public DateTime I2AllorsDateTime { get; set; }


        public I12[] I2I12One2Manies { get; set; }


        public I12 I2I12One2One { get; set; }


        public C2[] I2C2Many2Manies { get; set; }


        public I1[] I2I1Many2Manies { get; set; }


        public C2 I2C2Many2One { get; set; }


        public string I2AllorsString { get; set; }


        public C2[] I2C2One2Manies { get; set; }


        public I1 I2I1One2One { get; set; }


        public I1[] I2I1One2Manies { get; set; }


        public I12[] I2I12Many2Manies { get; set; }


        public I2 I2I2One2One { get; set; }


        public int I2AllorsInteger { get; set; }


        public I2[] I2I2One2Manies { get; set; }


        public C1[] I2C1Many2Manies { get; set; }


        public C2 I2C2One2One { get; set; }


        public double I2AllorsDouble { get; set; }


        public byte[] I12AllorsBinary { get; set; }


        public C2 I12C2One2One { get; set; }


        public double I12AllorsDouble { get; set; }


        public I1 I12I1Many2One { get; set; }


        public string I12AllorsString { get; set; }


        public I12[] I12I12Many2Manies { get; set; }


        public decimal I12AllorsDecimal { get; set; }


        public I2[] I12I2Many2Manies { get; set; }


        public C2[] I12C2Many2Manies { get; set; }


        public I1[] I12I1Many2Manies { get; set; }


        public I12[] I12I12One2Manies { get; set; }


        public string Name { get; set; }


        public C1[] I12C1Many2Manies { get; set; }


        public I2 I12I2Many2One { get; set; }


        public Guid I12AllorsUnique { get; set; }


        public int I12AllorsInteger { get; set; }


        public I1[] I12I1One2Manies { get; set; }


        public C1 I12C1One2One { get; set; }


        public I12 I12I12One2One { get; set; }


        public I2 I12I2One2One { get; set; }


        public I12[] Dependencies { get; set; }


        public I2[] I12I2One2Manies { get; set; }


        public C2 I12C2Many2One { get; set; }


        public I12 I12I12Many2One { get; set; }


        public bool I12AllorsBoolean { get; set; }


        public I1 I12I1One2One { get; set; }


        public C1[] I12C1One2Manies { get; set; }


        public C1 I12C1Many2One { get; set; }


        public DateTime I12AllorsDateTime { get; set; }


        #region Allors
        [Id("07eaa992-322a-40e9-bf2c-aa33b69f54cd")]
        [Precision(19)]
        [Scale(2)]
        #endregion
        public decimal C2AllorsDecimal { get; set; }
        #region Allors
        [Id("0c8209e3-b2fc-4c7a-acd2-6b5b8ac89bf4")]
        [Multiplicity(Multiplicity.OneToOne)]
        [Indexed]
        #endregion
        public C1 C2C1One2One { get; set; }
        #region Allors
        [Id("12896fc2-c9e9-4a89-b875-0aeb92e298e5")]
        [Indexed]
        #endregion
        public C2 C2C2Many2One { get; set; }
        #region Allors
        [Id("1444d919-6ca1-4642-8d18-9d955c817581")]
        #endregion
        public Guid C2AllorsUnique { get; set; }
        #region Allors
        [Id("165cc83e-935d-4d0d-aec7-5da155300086")]
        [Indexed]
        #endregion
        public I12 C2I12Many2One { get; set; }
        #region Allors
        [Id("1d0c57c9-a3d1-4134-bc7d-7bb587d8250f")]
        [Multiplicity(Multiplicity.OneToOne)]
        [Indexed]
        #endregion
        public I12 C2I12One2One { get; set; }
        #region Allors
        [Id("1d98eda7-6dba-43f1-a5ce-44f7ed104cf9")]
        [Multiplicity(Multiplicity.ManyToMany)]
        [Indexed]
        #endregion
        public I1[] C2I1Many2Manies { get; set; }
        #region Allors
        [Id("262ad367-a52c-4d8b-94e2-b477bb098423")]
        #endregion
        public double C2AllorsDouble { get; set; }
        #region Allors
        [Id("2ac55066-c748-4f90-9d0f-1090fe02cc76")]
        [Indexed]
        #endregion
        public I1[] C2I1One2Manies { get; set; }
        #region Allors
        [Id("38063edc-271a-410d-b857-807a9100c7b5")]
        [Multiplicity(Multiplicity.OneToOne)]
        [Indexed]
        #endregion
        public I2 C2I2One2One { get; set; }
        #region Allors
        [Id("42f9f4b6-3b35-4168-93cb-35171dbf83f4")]
        #endregion
        public int C2AllorsInteger { get; set; }
        #region Allors
        [Id("4a963639-72c3-4e9f-9058-bcfc8fe0bc9e")]
        [Multiplicity(Multiplicity.ManyToMany)]
        [Indexed]
        #endregion
        public I2[] C2I2Many2Manies { get; set; }
        #region Allors
        [Id("50300577-b5fb-4c16-9ac5-41151543f958")]
        [Multiplicity(Multiplicity.ManyToMany)]
        [Indexed]
        #endregion
        public I12[] C2I12Many2Manies { get; set; }
        #region Allors
        [Id("60680366-4790-4443-a941-b30cd4bd3848")]
        [Indexed]
        #endregion
        public C2[] C2C2One2Manies { get; set; }
        #region Allors
        [Id("61daaaae-dd22-405e-aa98-6321d2f8af04")]
        #endregion
        public bool C2AllorsBoolean { get; set; }
        #region Allors
        [Id("65a246a7-cd78-45eb-90db-39f542e7c6cf")]
        [Indexed]
        #endregion
        public I1 C2I1Many2One { get; set; }
        #region Allors
        [Id("67780894-fa62-48ba-8f47-7f54106090cd")]
        [Multiplicity(Multiplicity.OneToOne)]
        [Indexed]
        #endregion
        public I1 C2I1One2One { get; set; }
        #region Allors
        [Id("70600f67-7b18-4b5c-b11e-2ed180c5b2d6")]
        [Multiplicity(Multiplicity.ManyToMany)]
        [Indexed]
        #endregion
        public C1[] C2C1Many2Manies { get; set; }
        #region Allors
        [Id("770eb33c-c8ef-4629-a3a0-20decd92ff62")]
        [Indexed]
        #endregion
        public I12[] C2I12One2Manies { get; set; }
        #region Allors
        [Id("7a9129c9-7b6d-4bdd-a630-cfd1392549b7")]
        [Indexed]
        #endregion
        public I2[] C2I2One2Manies { get; set; }
        #region Allors
        [Id("86ad371b-0afd-420b-a855-38ebb3f39f38")]
        [Multiplicity(Multiplicity.OneToOne)]
        [Indexed]
        #endregion
        public C2 C2C2One2One { get; set; }
        #region Allors
        [Id("9c7cde3f-9b61-4c79-a5d7-afe1067262ce")]
        [Size(256)]
        #endregion
        public string C2AllorsString { get; set; }
        #region Allors
        [Id("a5315151-aa0f-42a3-9d5b-2c7f2cb92560")]
        [Indexed]
        #endregion
        public C1 C2C1Many2One { get; set; }
        #region Allors
        [Id("bc6c7fe0-6501-428c-a929-da87a9f4b885")]
        [Multiplicity(Multiplicity.ManyToMany)]
        [Indexed]
        #endregion
        public C2[] C2C2Many2Manies { get; set; }
        #region Allors
        [Id("ce23482d-3a22-4202-98e7-5934fd9abd2d")]
        #endregion
        public DateTime C2AllorsDateTime { get; set; }
        #region Allors
        [Id("e08d75a9-9b67-4d20-a476-757f8fb17376")]
        [Indexed]
        #endregion
        public I2 C2I2Many2One { get; set; }
        #region Allors
        [Id("f748949e-de5a-4f2e-85e2-e15516d9bf24")]
        [Indexed]
        #endregion
        public C1[] C2C1One2Manies { get; set; }
        #region Allors
        [Id("fa8ad982-9953-47dd-9905-81cc4572300e")]
        [Size(-1)]
        #endregion
        public byte[] C2AllorsBinary { get; set; }


        public void OnBuild(){}
        public void OnPostBuild(){}
        public void OnPreDerive(){}
        public void OnDerive(){}
        public void OnPostDerive(){}



    }
}