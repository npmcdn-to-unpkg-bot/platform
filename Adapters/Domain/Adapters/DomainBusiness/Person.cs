// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Person.cs" company="Allors bvba">
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

namespace Allors.Domain
{
    using Allors;

    public partial class Person
    {
        public static Person Create(IDatabaseSession session)
        {
            return (Person)session.Create(Meta.ObjectType);
        }

        public static Person[] Create(IDatabaseSession session, int count)
        {
            return (Person[])session.Create(Meta.ObjectType, count);
        }

        public static Person[] Instantiate(IDatabaseSession session, string[] ids)
        {
            return (Person[])session.Instantiate(ids);
        }

        public static Person[] Extent(IDatabaseSession session)
        {
            return (Person[])session.Extent(Meta.ObjectType).ToArray();
        }

        public static Person Create(IDatabaseSession session, string name)
        {
            Person person = Create(session);
            person.Name = name;
            return person;
        }

        public static Person Create(IDatabaseSession session, string name, int index)
        {
            Person person = Create(session);
            person.Name = name;
            person.Index = index;
            return person;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}