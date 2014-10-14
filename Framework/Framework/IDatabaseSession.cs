// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDatabaseSession.cs" company="Allors bvba">
//   Copyright 2002-2013 Allors bvba.
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

namespace Allors
{
    using Allors.Meta;

    /// <summary>
    /// Extends the <see cref="ISession"/> with database capabilities.
    /// </summary>
    public interface IDatabaseSession : ISession
    {
        /// <summary>
        /// Gets the database.
        /// </summary>
        IDatabase Database { get; }

        /// <summary>
        /// Inserts an Allors Object with the specified id.
        /// </summary>
        /// <param name="objectType">The IObjectType.</param>
        /// <param name="objectId">The object id.</param>
        /// <returns>The inserted object.</returns>
        IObject Insert(Class objectType, string objectId);

        /// <summary>
        /// Inserts an Allors Object with the specified id.
        /// </summary>
        /// <param name="objectType">The <see cref="IObjectType"/>.</param>
        /// <param name="objectId">The object id.</param>
        /// <returns>The inserted object.</returns>
        IObject Insert(Class objectType, ObjectId objectId);

        /// <summary>
        /// Instantiate a strategy.
        /// This method is primarily used by <see cref="IWorkspace"/>s.
        /// </summary>
        /// <param name="objectId">
        /// The object id.
        /// </param>
        /// <returns>
        /// The <see cref="IStrategy"/>.
        /// </returns>
        IStrategy InstantiateStrategy(ObjectId objectId);
    }
}