namespace Allors.Repository.Domain
{
    using System;

    #region Allors
    [Id("4e501cd6-807c-4f10-b60b-acd1d80042cd")]
    #endregion
    public partial class Unit : Object, AccessControlledObject 
    {
        public Permission[] DeniedPermissions { get; set; }
        
        public SecurityToken[] SecurityTokens { get; set; }
        
        #region Allors
        [Id("24771d5b-f920-4820-aff7-ea6391b4a45c")]
        [Size(-1)]
        #endregion
        public byte[] AllorsBinary { get; set; }
        #region Allors
        [Id("4d6a80f5-0fa7-4867-91f8-37aa92b6707b")]
        #endregion
        public DateTime AllorsDateTime { get; set; }
        #region Allors
        [Id("5a788ebe-65e9-4d5e-853a-91bb4addabb5")]
        #endregion
        public bool AllorsBoolean { get; set; }
        #region Allors
        [Id("74a35820-ef8c-4373-9447-6215ee8279c0")]
        #endregion
        public double AllorsDouble { get; set; }
        #region Allors
        [Id("b817ba76-876e-44ea-8e5a-51d552d4045e")]
        #endregion
        public int AllorsInteger { get; set; }
        #region Allors
        [Id("c724c733-972a-411c-aecb-e865c2628a90")]
        [Size(256)]
        #endregion
        public string AllorsString { get; set; }
        #region Allors
        [Id("ed58ae4c-24e0-4dd1-8b1c-0909df1e0fcd")]
        #endregion
        public Guid AllorsUnique { get; set; }
        #region Allors
        [Id("f746da51-ea2d-4e22-9ecb-46d4dbc1b084")]
        [Precision(19)]
        [Scale(2)]
        #endregion
        public decimal AllorsDecimal { get; set; }
        
        public void OnBuild(){}

        public void OnPostBuild(){}

        public void OnPreDerive(){}

        public void OnDerive(){}

        public void OnPostDerive(){}
    }
}