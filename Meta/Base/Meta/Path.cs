// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Path.cs" company="Allors bvba">
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

namespace Allors.Meta
{
    using System.Text;
    
    public class Path
    {
        public Path(params PropertyType[] propertyTypes)
        {
            if (propertyTypes.Length > 0)
            {
                var previous = this;
                previous.PropertyType = propertyTypes[0];

                for (var i = 1; i < propertyTypes.Length; i++)
                {
                    previous.Next = new Path { PropertyType = propertyTypes[i] };
                    previous = previous.Next;
                }
            }
        }

        public Path()
        {
        }

        public bool ExistPropertyType
        {
            get
            {
                return this.PropertyType != null;
            }
        }

        public PropertyType PropertyType { get; set; }

        public bool ExistNext
        {
            get
            {
                return this.Next != null;
            }
        }

        public Path Next { get; set; }

        public Path End
        {
            get
            {
                return this.ExistNext ? this.Next.End : this;
            }
        }

        public string Name
        {
            get
            {
                var name = new StringBuilder();
                name.Append(this.PropertyType.Name);
                if (this.ExistNext)
                {
                    this.Next.AppendToName(name);
                }

                return name.ToString();
            }
        }

        public override string ToString()
        {
            return this.Name;
        }

        public ObjectType GetObjectType()
        {
            if (this.ExistNext)
            {
                return this.Next.GetObjectType();
            }

            return this.PropertyType.GetObjectType();
        }

        private void AppendToName(StringBuilder name)
        {
            name.Append("." + this.PropertyType.Name);

            if (this.ExistNext)
            {
                this.Next.AppendToName(name);
            }
        }
    }
}