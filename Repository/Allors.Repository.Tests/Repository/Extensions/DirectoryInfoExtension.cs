// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DirectoryInfoExtension.cs" company="Allors bvba">
//   Copyright 2002-2009 Allors bvba.
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

namespace Allors.Meta
{
    using System.IO;
    using System.Threading;

    public static class DirectoryInfoExtension
    {
        public static void DeleteRecursive(this DirectoryInfo directoryInfo)
        {
            directoryInfo.Refresh();
            if (directoryInfo.Exists)
            {
                try
                {
                    directoryInfo.Delete(true);
                }
                catch
                {
                    Thread.Sleep(100);
                    
                    directoryInfo.Refresh();
                    directoryInfo.Delete(true);
                }
            }
        }
    }
}