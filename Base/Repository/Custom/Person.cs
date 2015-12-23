namespace Allors.Repository.Domain
{
    using System;

    public partial class Person : Printable, Deletable
    {
        public string PrintContent { get; set; }
        
        #region Allors
        [Id("e9e7c874-4d94-42ff-a4c9-414d05ff9533")]
        [Multiplicity(Multiplicity.ManyToMany)]
        [Indexed]
        #endregion
        public Address[] Addresses { get; set; }

        #region Allors
        [Id("2a25125f-3545-4209-afc6-523eb0d8851e")]
        #endregion
        public int Age { get; set; }
        
        #region Allors
        [Id("adf83a86-878d-4148-a9fc-152f56697136")]
        [Group("Workspace")]
        #endregion
        public DateTime BirthDate { get; set; }
        
        #region Allors
        [Id("6cc34453-ac7a-4004-8380-033f92324e99")]
        [Size(-1)]
        #endregion
        public string CKEditorText { get; set; }
        
        #region Allors
        [Id("688ebeb9-8a53-4e8d-b284-3faa0a01ef7c")]
        [Derived]
        [Size(256)]
        #endregion
        public string FullName { get; set; }
        
        #region Allors
        [Id("654f6c84-62f2-4c0a-9d68-532ed3f39447")]
        [Indexed]
        #endregion
        public Gender Gender { get; set; }
        
        #region Allors
        [Id("a8a3b4b8-c4f2-4054-ab2a-2eac6fd058e4")]
        #endregion
        public bool IsMarried { get; set; }
        
        #region Allors
        [Id("54f11f06-8d3f-4d58-bcdc-d40e6820fdad")]
        [Group("Workspace")]
        #endregion
        public bool IsStudent { get; set; }
        
        #region Allors
        [Id("6340de2a-c3b1-4893-a7f3-cb924b82fa0e")]
        [Indexed]
        #endregion
        public MailboxAddress MailboxAddress { get; set; }
        
        #region Allors
        [Id("0375a3d3-1a1b-4cbb-b735-1fe508bcc672")]
        [Indexed]
        #endregion
        public Address MainAddress { get; set; }
        
        #region Allors
        [Id("b3ddd2df-8a5a-4747-bd4f-1f1eb37386b3")]
        [Indexed]
        [Group("Workspace")]
        #endregion
        public Media Photo { get; set; }
        
        #region Allors
        [Id("6b626ba5-0c45-48c7-8b6b-5ea85e002d90")]
        #endregion
        public int ShirtSize { get; set; }
        
        #region Allors
        [Id("1b057406-3343-426b-ab5b-ceb93ba02446")]
        [Size(-1)]
        #endregion
        public string Text { get; set; }
        
        #region Allors
        [Id("15de4e58-c5ef-4ebb-9bf6-5ab06a02c5a4")]
        [Size(-1)]
        #endregion
        public string TinyMCEText { get; set; }
        
        #region Allors
        [Id("afc32e62-c310-421b-8c1d-6f2b0bb88b54")]
        [Precision(19)]
        [Scale(2)]
        [Group("Workspace")]
        #endregion
        public decimal Weight { get; set; }

        #region Allors

        [Id("fc091eac-05dc-4c09-8b9a-bb142c196708")]

        #endregion
        public void Method()
        {
        }

        public void Delete()
        {
        }
    }
}