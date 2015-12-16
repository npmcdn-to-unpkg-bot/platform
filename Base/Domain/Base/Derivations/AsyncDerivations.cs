// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AsyncDerivations.cs" company="Allors bvba">
//   Copyright 2002-2013 Allors bvba.
//
// Dual Licensed under
//   a) the General Public Licence v3 (GPL)
//   b) the Allors License
//
// The GPL License is included in the file gpl.txt.
// The Allors License is an addendum to your contract.
//
// Allors Applications is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// For more information visit http://www.allors.com/legal
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Domain
{
    using System.Linq;

    public partial class AsyncDerivations
    {
        public void AsyncDerive()
        {
            this.Session.Rollback();

            while (true)
            {
                var asyncDerivationsByAsyncDerivable = this.Extent()
                    .GroupBy(v => v.AsyncDerivable)
                    .ToDictionary(g => g.Key, g => g.ToList());

                var asyncDerivables = asyncDerivationsByAsyncDerivable.Keys.ToList();

                foreach (var asyncDerivable in asyncDerivables)
                {
                    var asyncDerivations = asyncDerivationsByAsyncDerivable[asyncDerivable];

                    try
                    {
                        asyncDerivable.AsyncDerive();

                        asyncDerivations.ForEach(asyncDerivation => asyncDerivation.Delete());
                        this.Session.Commit();
                    }
                    catch
                    {
                        this.Session.Rollback();

                        asyncDerivations.ForEach(asyncDerivation => asyncDerivation.Delete());
                        this.Session.Commit();
                    }
                    finally
                    {
                        this.Session.Rollback();
                    }
                }
            }
        }
    }
}