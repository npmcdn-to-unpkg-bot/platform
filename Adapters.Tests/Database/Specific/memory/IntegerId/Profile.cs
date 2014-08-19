// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Profile.cs" company="Allors bvba">
//   Copyright 2002-2012 Allors bvba.
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
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.R1.Adapters.Special.Memory.IntegerId
{
    using System;
    using System.Collections.Generic;

    public class Profile : Special.Profile
    {
        public override Action[] Markers
        {
            get
            {
                var markers = new List<Action> 
                { 
                    () => { }, 
                    () => this.Session.Commit() 
                };

                if (Settings.ExtraMarkers)
                {
                    markers.Add(
                        () =>
                        {
                            this.Session.Commit();
                            this.Session.Checkpoint();
                        });
                }

                return markers.ToArray();
            }
        }

        public override IDatabase CreateDatabase()
        {
            return new Database.Memory.IntegerId.Database(new Database.Memory.IntegerId.Configuration { ObjectFactory = this.ObjectFactory });
        }

        public override IWorkspace CreateWorkspace(IDatabase database)
        {
            var configuration = new Workspace.Memory.IntegerId.Configuration { Database = database };
            return new Workspace.Memory.IntegerId.Workspace(configuration);
        }
    }
}