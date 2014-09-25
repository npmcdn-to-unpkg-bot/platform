// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Singleton.cs" company="Allors bvba">
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
    using Allors;

    public partial class Singleton
    {
        private static readonly string SessionKey = "Allors.Cache." + typeof(Singleton);

        public static Singleton Instance(ISession session)
        {
            var instance = (Singleton)session[SessionKey];
            if (instance == null)
            {
                if (session is IDatabaseSession)
                {
                    instance = (Singleton)session.Extent<Singleton>().First;
                }
                else
                {
                    var workspaceSession = (IWorkspaceSession)session;
                    instance = Instance(workspaceSession.DatabaseSession);
                }
            }

            session[SessionKey] = instance;
            return instance;
        }

        protected override void BaseDerive(IDerivation derivation)
        {
            base.BaseDerive(derivation);

            this.DisplayName = "Singleton";
        }
    }
}