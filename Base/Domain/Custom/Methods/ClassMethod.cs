//------------------------------------------------------------------------------------------------- 
// <copyright file="C1ClassMethod.cs" company="Allors bvba">
// Copyright 2002-2016 Allors bvba.
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
// <summary>Defines the Person type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Domain
{
    public partial class C1
    {
        public void CoreClassMethod(C1ClassMethod method)
        {
            method.Value += "C1Core";
        }

        public void BaseClassMethod(C1ClassMethod method)
        {
            method.Value += "C1Base";
        }

        public void CustomClassMethod(C1ClassMethod method)
        {
            method.Value += "C1Custom";
        }
    }

    public partial class C1ClassMethod
    {
        public string Value { get; set; }
    }
}
