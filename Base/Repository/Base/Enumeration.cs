namespace Allors.Repository.Domain
{
    #region Allors
    [Id("b7bcc22f-03f0-46fd-b738-4e035921d445")]
    #endregion
    public partial interface Enumeration : AccessControlledObject, UniquelyIdentifiable 
    {
        #region Allors
        [Id("07e034f1-246a-4115-9662-4c798f31343f")]
        [AssociationId("bcf428fd-0263-488c-b9ac-963ceca1c972")]
        [RoleId("919fdad7-830e-4b12-b23c-f433951236af")]
        [Multiplicity(Multiplicity.OneToMany)]
        [Indexed]
        #endregion
        LocalisedText[] LocalisedNames { get; set; }
        
        #region Allors
        [Id("3d3ae4d0-bac6-4645-8a53-3e9f7f9af086")]
        [AssociationId("004cc333-b8ae-4952-ae13-f2ab80eb018c")]
        [RoleId("5850860d-c772-402f-815b-7634c9a1e697")]
        [Indexed]
        [Size(256)]
        #endregion
        [Required]
        string Name { get; set; }
        
        #region Allors
        [Id("f57bb62e-77a8-4519-81e6-539d54b71cb7")]
        [AssociationId("a8993304-52c0-4b53-9982-6caa5675467a")]
        [RoleId("0c6faf5a-eac9-454c-bd53-3b8409e56d34")]
        [Indexed]
        #endregion
        [Required]
        bool IsActive { get; set; }
    }
}