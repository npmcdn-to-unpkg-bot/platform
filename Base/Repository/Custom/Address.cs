namespace Allors.Repository.Domain
{
    using System;

    #region Allors
    [Id("130aa2ff-4f14-4ad7-8a27-f80e8aebfa00")]
    #endregion
    public partial interface Address : Object 
    {
        #region Allors
        [Id("36e7d935-a9c7-484d-8551-9bdc5bdeab68")]
        [Indexed]
        #endregion
        Place Place { get; set; }

    }
}