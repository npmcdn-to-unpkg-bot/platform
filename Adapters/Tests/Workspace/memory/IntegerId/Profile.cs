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

namespace Allors.Workspaces.Memory.IntegerId
{
    using System;
    using System.Collections.Generic;

    using Allors;
    using Allors.Databases.Memory.IntegerId;
    using Allors.Populations;

    public class Profile : Workspaces.Profile
    {
        public virtual string PopulationName
        {
            get { return "ServerIntegerId"; }
        }

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
                                ((IWorkspaceSession)this.Session).Sync();
                                this.Session.Commit();
                            });
                }

                return markers.ToArray();
            }
        }

        public override Action[] Inits
        {
            get
            {
                return new Action[] { () => this.Init() };
            }
        }

        public override IPopulation CreatePopulation()
        {
            var database = new Database(new Databases.Memory.IntegerId.Configuration { ObjectFactory = this.ObjectFactory });
            return new Workspace(new Configuration { Database = database });
        }
    }
}