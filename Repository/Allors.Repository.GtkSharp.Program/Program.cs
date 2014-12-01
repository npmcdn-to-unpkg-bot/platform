//------------------------------------------------------------------------------------------------- 
// <copyright file="MainClass.cs" company="Allors bvba">
// Copyright 2002-2009 Allors bvba.
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
//-------------------------------------------------------------------------------------------------

namespace Allors.Meta.GtkSharp.Exe
{
    using System;
    using System.IO;

    using Gtk;

    using Mono.Addins;

    using MonoDevelop.Core.Instrumentation;

    internal class MainClass
    {
        public static void Main(string[] args)
        {
            AddinManager.Initialize();
            AddinManager.Registry.Update(null);

            try
            {
                // Triggers registration
                InstrumentationService.CreateCounter("allors");
            }
            catch{}
            finally
            {
                InstrumentationService.Enabled = false;
            }

            Application.Init();
            var win = new MainWindow(args);
            win.Show();
            Application.Run();
        }
    }
}