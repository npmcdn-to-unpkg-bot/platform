//------------------------------------------------------------------------------------------------- 
// <copyright file="RepositoryMetaObjectDeletedEventArgs.cs" company="Allors bvba">
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
// <summary>Defines the MetaObjectDeletedEventArgs type.</summary>
//-------------------------------------------------------------------------------------------------
namespace Allors.Meta.Events
{
    using System;

    using Allors.Meta;

    /// <summary>
    /// The domain changed event arguments.
    /// </summary>
    public class RepositoryMetaObjectDeletedEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryMetaObjectDeletedEventArgs"/> class.
        /// </summary>
        /// <param name="repository">
        /// The repository.
        /// </param>
        /// <param name="metaObjectId">
        /// The meta Object Id.
        /// </param>
        public RepositoryMetaObjectDeletedEventArgs(XmlRepository repository, Guid metaObjectId)
        {
            this.MetaObjectId = metaObjectId;
            this.Repository = repository;
        }

        public Guid MetaObjectId { get; private set; }

        public XmlRepository Repository { get; private set; }
    }
}