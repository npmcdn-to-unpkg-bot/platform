// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReferenceOne2ManyTest.cs" company="Allors bvba">
//   Copyright 2002-2010 Allors bvba.
// 
// Dual Licensed under
//   a) the Lesser General Public Licence v3 (LGPL)
//   b) the Allors License
// 
// The LGPL License is included in the file lgpl.txt.
// The Allors License is an addendum to your contract.
// 
// Allors Platform is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// For more information visit http://www.allors.com/legal
// </copyright>
// <summary>
//   Defines the ReferenceOne2ManyTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Allors.Meta;

namespace Tests.Repositories.General.MySqlClient.Connected.IntegerId
{
    using Allors;
    using Allors.Repositories;

    using NUnit.Framework;

    using Domain = Allors.Meta.Domain;

    [TestFixture]
    public class ReferenceOne2ManyTest : General.ReferenceOne2ManyTest
    {
        private Profile profile = new Profile();
        [SetUp]
        protected void Init()
        {
            profile.Init();
        }
        [TearDown]
        protected void Dispose()
        {
            profile.Dispose();
        }
        public override IObject[] CreateArray(ObjectType objectType, int count)
        {
            return profile.CreateArray(objectType, count);
        }
        public override IRepository CreateMemoryPopulation()
        {
            return profile.CreateMemoryPopulation();
        }
        public override Domain GetMetaDomain()
        {
            return profile.GetMetaDomain();
        }
        public override Domain GetMetaDomain2()
        {
            return profile.GetMetaDomain2();
        }
        public override IRepository GetPopulation()
        {
            return profile.GetPopulation();
        }
        public override IRepository GetPopulation2()
        {
            return profile.GetPopulation2();
        }
        public override ISession GetSession()
        {
            return profile.GetSession();
        }
        public override IRepositorySession GetSession2()
        {
            return profile.GetSession2();
        }
        public override bool IsRollbackSupported()
        {
            return profile.IsRollbackSupported();
        }
    }
}