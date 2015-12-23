namespace Allors.Repository.Domain
{
    using System;

    #region Allors
    [Id("3a5dcec7-308f-48c7-afee-35d38415aa0b")]
    #endregion
    public partial class Organisation : Object, UniquelyIdentifiable, AccessControlledObject 
    {
        public Guid UniqueId { get; set; }


        public Permission[] DeniedPermissions { get; set; }


        public SecurityToken[] SecurityTokens { get; set; }

        #region Allors
        [Id("73f23588-1444-416d-b43c-b3384ca87bfc")]
        [Indexed]
        #endregion
        public Address[] Addresses { get; set; }
        #region Allors
        [Id("2cfea5d4-e893-4264-a966-a68716839acd")]
        [Size(-1)]
        #endregion
        public string Description { get; set; }
        #region Allors
        [Id("49b96f79-c33d-4847-8c64-d50a6adb4985")]
        [Group("Workspace")]
        #endregion
        public Person[] Employees { get; set; }
        #region Allors
        [Id("17e55fcd-2c82-462b-8e31-b4a515acdaa9")]
        [Indexed]
        #endregion
        public Image[] Images { get; set; }
        #region Allors
        [Id("5fa25b53-e2a7-44c8-b6ff-f9575abb911d")]
        #endregion
        public bool Incorporated { get; set; }
        #region Allors
        [Id("7046c2b4-d458-4343-8446-d23d9c837c84")]
        #endregion
        public DateTime IncorporationDate { get; set; }
        #region Allors
        [Id("01dd273f-cbca-4ee7-8c2d-827808aba481")]
        [Indexed]
        [Size(-1)]
        #endregion
        public string Information { get; set; }
        #region Allors
        [Id("68c61cea-4e6e-4ed5-819b-7ec794a10870")]
        #endregion
        public bool IsSupplier { get; set; }
        #region Allors
        [Id("b201d2a0-2335-47a1-aa8d-8416e89a9fec")]
        [Indexed]
        #endregion
        public Image Logo { get; set; }
        #region Allors
        [Id("ddcea177-0ed9-4247-93d3-2090496c130c")]
        [Indexed]
        #endregion
        public Address MainAddress { get; set; }
        #region Allors
        [Id("dbef262d-7184-4b98-8f1f-cf04e884bb92")]
        [Multiplicity(Multiplicity.OneToOne)]
        [Indexed]
        [Group("Workspace")]
        #endregion
        public Person Manager { get; set; }
        #region Allors
        [Id("2cc74901-cda5-4185-bcd8-d51c745a8437")]
        [Indexed]
        [Size(256)]
        [Group("Workspace")]
        #endregion
        public string Name { get; set; }
        #region Allors
        [Id("845ff004-516f-4ad5-9870-3d0e966a9f7d")]
        [Multiplicity(Multiplicity.OneToOne)]
        [Indexed]
        [Group("Workspace")]
        #endregion
        public Person Owner { get; set; }
        #region Allors
        [Id("15f33fa4-c878-45a0-b40c-c5214bce350b")]
        [Multiplicity(Multiplicity.ManyToMany)]
        [Indexed]
        [Group("Workspace")]
        #endregion
        public Person[] Shareholders { get; set; }
        #region Allors
        [Id("bac702b8-7874-45c3-a410-102e1caea4a7")]
        [Size(256)]
        #endregion
        public string Size { get; set; }


        public void OnBuild(){}
        public void OnPostBuild(){}
        public void OnPreDerive(){}
        public void OnDerive(){}
        public void OnPostDerive(){}



        #region Allors
        [Id("cbf9121e-a5e5-45c6-99fe-52fa80dc3220")]
        #endregion
        public void JustDoIt(){}
    }
}