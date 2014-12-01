//------------------------------------------------------------------------------------------------- 
// <copyright file="RepositoryMetaObjectChangedEventArgs.cs" company="Allors bvba">
// Copyright 2002-2012 Allors bvba.
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
// <summary>Defines the MetaObjectChangedEventArgs type.</summary>
//-------------------------------------------------------------------------------------------------
namespace Allors.Meta.Events
{
    using Allors.Meta;

    /// <summary>
    /// The domain changed event arguments.
    /// </summary>
    public class RepositoryMetaObjectChangedEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryMetaObjectChangedEventArgs"/> class.
        /// </summary>
        /// <param name="repository">
        /// The repository.
        /// </param>
        /// <param name="metaObject">
        /// The changed meta object.
        /// </param>
        public RepositoryMetaObjectChangedEventArgs(XmlRepository repository, MetaObject metaObject)
        {
            this.MetaObject = metaObject;
            this.Repository = repository;
        }

        public MetaObject MetaObject { get; private set; }

        public XmlRepository Repository { get; private set; }
    }
}