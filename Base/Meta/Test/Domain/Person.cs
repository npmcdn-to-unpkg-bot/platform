namespace Allors.Meta
{
    [Inherit(typeof(PrintableInterface))]
    [Inherit(typeof(DeletableInterface))]
    public partial class PersonClass : Class
    {
        #region Allors

        [Id("e9e7c874-4d94-42ff-a4c9-414d05ff9533")]
        [AssociationId("da5e0427-79f7-4259-8a68-0071aa4c6273")]
        [RoleId("c922b44f-6c6f-4e8b-901d-6558e79bb558")]

        #endregion
        [Indexed]
        [Type(typeof(AddressInterface))]
        [Plural("Addresses")]
        [Multiplicity(Multiplicity.ManyToMany)]
        public RelationType Address;

        #region Allors

        [Id("2a25125f-3545-4209-afc6-523eb0d8851e")]
        [AssociationId("94b038b3-2dd6-42a8-9cd6-800ddbef104c")]
        [RoleId("fb6dcca2-14a6-4b00-bd3e-81acf59fbbe2")]

        #endregion
        [Type(typeof(AllorsIntegerUnit))]
        public RelationType Age;

        #region Allors

        [Id("adf83a86-878d-4148-a9fc-152f56697136")]
        [AssociationId("b9da077d-bfc7-4b4e-be62-03aded6da22e")]
        [RoleId("0ffd9c62-efc6-4438-aaa3-755e4c637c21")]

        #endregion
        [Type(typeof(AllorsDateTimeUnit))]
        [Group(Groups.Workspace)]
        public RelationType BirthDate;

        #region Allors

        [Id("6cc34453-ac7a-4004-8380-033f92324e99")]
        [AssociationId("5a99b822-8c51-4cf6-82e9-ee4ca311216a")]
        [RoleId("cc14daec-604d-4ca6-9908-a57c10ba1403")]

        #endregion
        [Type(typeof(AllorsStringUnit))]
        [Size(-1)]
        public RelationType CKEditorText;

        #region Allors

        [Id("688ebeb9-8a53-4e8d-b284-3faa0a01ef7c")]
        [AssociationId("8a181cec-7bae-4248-8e24-8abc7e01eea2")]
        [RoleId("e431d53c-37ed-4fde-86a9-755f354c1d75")]

        #endregion
        [Derived]
        [Type(typeof(AllorsStringUnit))]
        [Size(256)]
        public RelationType FullName;

        #region Allors

        [Id("654f6c84-62f2-4c0a-9d68-532ed3f39447")]
        [AssociationId("5ec6caf4-4752-4a89-92ec-13fd69b444f2")]
        [RoleId("34704a90-d513-4fe2-a1ed-ad6d89399c73")]

        #endregion
        [Indexed]
        [Type(typeof(GenderClass))]
        [Multiplicity(Multiplicity.ManyToOne)]
        public RelationType Gender;

        #region Allors

        [Id("a8a3b4b8-c4f2-4054-ab2a-2eac6fd058e4")]
        [AssociationId("0fdeacf1-35bd-473d-88a9-acd65803f731")]
        [RoleId("656f11e4-7652-4b4d-9dda-28cfe16333ec")]

        #endregion
        [Type(typeof(AllorsBooleanUnit))]
        [Plural("AreMarried")]
        public RelationType IsMarried;

        #region Allors

        [Id("54f11f06-8d3f-4d58-bcdc-d40e6820fdad")]
        [AssociationId("03a7ffcc-4291-4ae1-a2ab-69f7257fbf04")]
        [RoleId("abd2a4b3-4b17-48d4-b465-0ffcb5a2664d")]

        #endregion
        [Type(typeof(AllorsBooleanUnit))]
        [Plural("AreStudent")]
        public RelationType IsStudent;

        #region Allors

        [Id("6340de2a-c3b1-4893-a7f3-cb924b82fa0e")]
        [AssociationId("b6ea4ac5-088a-4773-8410-6813d0185d7c")]
        [RoleId("5a472c98-481f-407c-b53e-eaaa7e7a5340")]

        #endregion
        [Indexed]
        [Type(typeof(MailboxAddressClass))]
        [Plural("MailboxAddresses")]
        [Multiplicity(Multiplicity.ManyToOne)]
        public RelationType MailboxAddress;

        #region Allors

        [Id("0375a3d3-1a1b-4cbb-b735-1fe508bcc672")]
        [AssociationId("ebaedf39-1af9-42b7-83dc-8945450cebf2")]
        [RoleId("86685c44-5196-46dd-9260-e40a434e9a52")]

        #endregion
        [Indexed]
        [Type(typeof(AddressInterface))]
        [Plural("MainAddresses")]
        [Multiplicity(Multiplicity.ManyToOne)]
        public RelationType MainAddress;

        #region Allors

        [Id("b3ddd2df-8a5a-4747-bd4f-1f1eb37386b3")]
        [AssociationId("912b48f5-215e-4cc0-a83b-56b74d986608")]
        [RoleId("f6624fac-db8e-4fb2-9e86-18021b59d31d")]

        #endregion
        [Indexed]
        [Type(typeof(MediaClass))]
        [Multiplicity(Multiplicity.ManyToOne)]
        [Group(Groups.Workspace)]
        public RelationType Photo;

        #region Allors

        [Id("6b626ba5-0c45-48c7-8b6b-5ea85e002d90")]
        [AssociationId("520bb966-6e8a-46a4-a3c0-18422af13cba")]
        [RoleId("66e20063-ab51-417a-8ce4-135bb6e115c1")]

        #endregion
        [Type(typeof(AllorsIntegerUnit))]
        public RelationType ShirtSize;

        #region Allors

        [Id("1b057406-3343-426b-ab5b-ceb93ba02446")]
        [AssociationId("91d44bdd-7b17-4fa7-aeb7-625571b252b9")]
        [RoleId("93d01c4a-0aa3-4d7c-a6d8-139b8ed1ffcc")]

        #endregion
        [Type(typeof(AllorsStringUnit))]
        [Size(-1)]
        public RelationType Text;

        #region Allors

        [Id("15de4e58-c5ef-4ebb-9bf6-5ab06a02c5a4")]
        [AssociationId("be22968c-a450-418f-8f2e-f6140a56589c")]
        [RoleId("ad249eb0-6cf2-4bcb-b3d1-3ff1282cd2f9")]

        #endregion
        [Type(typeof(AllorsStringUnit))]
        [Size(-1)]
        public RelationType TinyMCEText;

        #region Allors

        [Id("afc32e62-c310-421b-8c1d-6f2b0bb88b54")]
        [AssociationId("c21ebc52-6b32-4af7-847e-d3d7e1c4defe")]
        [RoleId("0aab73c3-f997-4dd9-885a-2c1c892adb0e")]

        #endregion
        [Type(typeof(AllorsDecimalUnit))]
        [Precision(19)]
        [Scale(2)]
        [Group(Groups.Workspace)]
        public RelationType Weight;

        #region Allors
        [Id("FC091EAC-05DC-4C09-8B9A-BB142C196708")]
        #endregion
        [Group(Groups.Workspace)]
        public MethodType Method;

        public Tree AngularHome { get; private set; }

        internal override void TestExtend()
        {
            var person = this;

            this.FirstName.AddGroup(Groups.Workspace);
            this.LastName.AddGroup(Groups.Workspace);
            this.MiddleName.AddGroup(Groups.Workspace);

            this.AngularHome = new Tree(person)
                    .Add(person.Photo);
        }
    }
}