namespace Allors.Repository.Domain
{
    using System;

    public partial interface Object : 
    {


        void OnBuild();

        void OnPostBuild();

        void OnPreDerive();

        void OnDerive();

        void OnPostDerive();
    }
}