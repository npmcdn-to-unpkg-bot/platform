namespace Allors.Repository.Domain
{
    using System;

    #region Allors
    [Id("9279e337-c658-4086-946d-03c75cdb1ad3")]
    #endregion
	public partial interface Deletable :  Object 
    {


        void Delete();
    }
}