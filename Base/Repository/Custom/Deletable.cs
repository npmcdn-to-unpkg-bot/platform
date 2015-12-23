namespace Allors.Repository.Domain
{
    using System;

    public partial interface Deletable :  Object 
    {


        void Delete();
    }
}