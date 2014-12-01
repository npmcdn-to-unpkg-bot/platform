// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectTypeTag.cs" company="Allors bvba">
// Copyright 2002-2012 Allors bvba.
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
namespace Allors.Meta.WinForms
{
    using Allors.Meta;

    public sealed class ObjectTypeTag : Tag
    {
        public ObjectTypeTag(XmlRepository repository, ObjectType objectType)
        {
            this.Repository = repository;
            this.ObjectType = objectType;
        }

        public XmlRepository Repository { get; private set; }

        public ObjectType ObjectType { get; private set; }

        public override bool Equals(object obj)
        {
            var that = obj as ObjectTypeTag;
            return that != null && this.Repository.Equals(that.Repository) && this.ObjectType.Equals(that.ObjectType);
        }

        public override int GetHashCode()
        {
            return this.Repository.GetHashCode() ^ this.ObjectType.GetHashCode();
        }
    }
}