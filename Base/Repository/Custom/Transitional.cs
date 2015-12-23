namespace Allors.Repository.Domain
{
    using System;

    public partial interface Transitional :  Object, AccessControlledObject 
    {


        #region Allors
        [Id("6e27b0e8-2ac1-4ec8-90fe-6a38b7a2f690")]
        [AssociationId("52f880b3-09af-4842-bb44-e86cfd937e14")]
        [RoleId("2ff12d5c-2042-4b55-ba4b-1b6389a066c6")]
        [Multiplicity(Multiplicity.ManyToOne)]
        [Derived]
        [Indexed]
        #endregion
        ObjectState PreviousObjectState { get; set; }


        #region Allors
        [Id("f0d9a21f-0570-4dca-9555-ccd8aabbb8d8")]
        [AssociationId("1e2268c1-badf-49bd-81fe-c6122cbd1f81")]
        [RoleId("d9c8465b-3f59-4985-9ff3-c04be6a242de")]
        [Multiplicity(Multiplicity.ManyToOne)]
        [Derived]
        [Indexed]
        #endregion
        ObjectState LastObjectState { get; set; }

    }
}