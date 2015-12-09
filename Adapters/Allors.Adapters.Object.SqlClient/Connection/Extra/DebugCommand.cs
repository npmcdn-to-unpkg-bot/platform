// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DebugCommand.cs" company="Allors bvba">
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

namespace Allors.Adapters.Object.SqlClient.Debug
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class DebugCommand : Command
    {

        public DebugCommand(Mapping mapping, SqlCommand command)
            : base(mapping, command)
        {
        }

        public List<DateTime> ExecutingTimeStamps { get; } = new List<DateTime>();

        public List<DateTime> ExecutedTimeStamps { get; } = new List<DateTime>();

        public override string ToString()
        {
            return $"[{this.ExecutedTimeStamps.Count}x] {this.SqlCommand.CommandText}";
        }

        protected override void OnExecuting()
        {
            this.ExecutingTimeStamps.Add(DateTime.Now);
        }

        protected override void OnExecuted()
        {
            this.ExecutedTimeStamps.Add(DateTime.Now);
        }
    }
}
