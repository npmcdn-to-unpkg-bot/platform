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

namespace Allors.Databases.Memory.LongId
{
    using System;
    using System.Collections.Generic;

    using Allors.Workspaces.Memory.LongId;
    using Allors.Populations;

    public class Profile : Databases.Profile
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
            return new Databases.Memory.LongId.Database(new Databases.Memory.LongId.Configuration { ObjectFactory = this.ObjectFactory });
        }

        public override IWorkspace CreateWorkspace(IDatabase database)
        {
            var configuration = new Workspaces.Memory.LongId.Configuration { Database = database };
            return new Workspace(configuration);
        }
    }
}