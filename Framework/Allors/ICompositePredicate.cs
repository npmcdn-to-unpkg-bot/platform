//------------------------------------------------------------------------------------------------- 
// <copyright file="ICompositePredicate.cs" company="Allors bvba">
// Copyright 2002-2016 Allors bvba.
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
// <summary>Defines the ICompositePredicate type.</summary>
//-------------------------------------------------------------------------------------------------
namespace Allors
{
    using System.Collections.Generic;

    using Allors.Meta;

    /// <summary>
    /// <para> A Predicate is an expression that either returns true, false or unknown (Three Value Logic).
    /// A CompositePredicate is a Predicate that can contain other Predicates.</para>
    /// <para>CompositePredicates are applied to other predicates (And and Or) or to a single other predicate (Not).
    /// Non-CompositePredicates are applied to objects (InstanceOf) or to
    /// relations of those objects (InstanceOf,Exists,NotExists,Contains,Equals,Like,LessThan,GreaterThan and Between).</para>
    /// <para> Adding a CompositePredicate returns the newly added CompositePredicate,
    /// adding a Non-CompositePredicate returns the composing CompositePredicate to which the Non-CompositePredicate was added.
    /// This allows for chained method invocations, e.g predicate.AddEquals(...).AddEquals(...)</para>
    /// </summary>
    public interface ICompositePredicate
    {
        /// <summary>
        /// Adds a CompositePredicate that evaluates to true if all of its composed predicates evaluate to true.
        /// This predicate is ignored when there are no composed predicates.
        /// </summary>
        /// <returns>the newly added CompositePredicate</returns>
        ICompositePredicate AddAnd();

        /// <summary>
        /// Adds a Predicate that evaluates to true if the role of the object under evaluation is between the first and the second object.
        /// </summary>
        /// <param name="role">The role .</param>
        /// <param name="firstValue">The first object.</param>
        /// <param name="secondValue">The second object.</param>
        /// <returns>the composing CompositePredicate</returns>
        ICompositePredicate AddBetween(IRoleType role, object firstValue, object secondValue);

        /// <summary>
        /// Adds a Predicate that evaluates to true if any object of the role of the object under evaluation is contained in the containingExtent.
        /// </summary>
        /// <param name="role">The role .</param>
        /// <param name="containingExtent">The extent.</param>
        /// <returns>this CompositePredicate</returns>
        ICompositePredicate AddContainedIn(IRoleType role, Extent containingExtent);

        /// <summary>
        /// Adds a Predicate that evaluates to true if any object of the role of the object under evaluation is contained in the containingExtent.
        /// </summary>
        /// <param name="role">The role .</param>
        /// <param name="containingEnumerable">The enumerable.</param>
        /// <returns>This CompositePredicate. </returns>
        ICompositePredicate AddContainedIn(IRoleType role, IEnumerable<IObject> containingEnumerable);

        /// <summary>
        /// Adds a Predicate that evaluates to true if any object of the association of the object under evaluation is contained in the containingExtent.
        /// </summary>
        /// <param name="association">The association.</param>
        /// <param name="containingExtent">The extent.</param>
        /// <returns>the composing CompositePredicate</returns>
        ICompositePredicate AddContainedIn(IAssociationType association, Extent containingExtent);

        /// <summary>
        /// Adds a Predicate that evaluates to true if any object of the role of the object under evaluation is contained in the containingExtent.
        /// </summary>
        /// <param name="association">The association.</param>
        /// <param name="containingEnumerable">The enumerable.</param>
        /// <returns>This CompositePredicate. </returns>
        ICompositePredicate AddContainedIn(IAssociationType association, IEnumerable<IObject> containingEnumerable);

        /// <summary>
        /// Adds a Predicate that evaluates to true if the role of the object under evaluation contains the allorsObject.
        /// </summary>
        /// <param name="role">The role .</param>
        /// <param name="containedObject">The allors object.</param>
        /// <returns>the composing CompositePredicate</returns>
        ICompositePredicate AddContains(IRoleType role, IObject containedObject);

        /// <summary>
        /// Adds a Predicate that evaluates to true if the association of the object under evaluation contains the allorsObject.
        /// </summary>
        /// <param name="association">The association.</param>
        /// <param name="containedObject">The allors object.</param>
        /// <returns>the composing CompositePredicate</returns>
        ICompositePredicate AddContains(IAssociationType association, IObject containedObject);

        /// <summary>
        /// Adds a Predicate that evaluates to true if the object under evaluation equals the allorsObject.
        /// </summary>
        /// <param name="allorsObject">The allors object.</param>
        /// <returns>the composing CompositePredicate</returns>
        ICompositePredicate AddEquals(IObject allorsObject);

        /// <summary>
        /// Adds a Predicate that evaluates to true if the role of the object under evaluation equals the object (unit or composite).
        /// </summary>
        /// <param name="role">The role .</param>
        /// <param name="valueOrAllorsObject">The object.</param>
        /// <returns>the composing CompositePredicate</returns>
        ICompositePredicate AddEquals(IRoleType role, object valueOrAllorsObject);

        /// <summary>
        /// Adds a Predicate that evaluates to true if the association of the object under evaluation equals the allorsObject.
        /// </summary>
        /// <param name="association">The association.</param>
        /// <param name="allorsObject">The allors object.</param>
        /// <returns>the composing CompositePredicate</returns>
        ICompositePredicate AddEquals(IAssociationType association, IObject allorsObject);

        /// <summary>
        /// Adds a Predicate that evaluates to true if the role of the object under evaluation exists.
        /// </summary>
        /// <param name="role">The role .</param>
        /// <returns>the composing CompositePredicate</returns>
        ICompositePredicate AddExists(IRoleType role);

        /// <summary>
        /// Adds a Predicate that evaluates to true if the association of the object under evaluation exists.
        /// </summary>
        /// <param name="assocation">The assocation.</param>
        /// <returns>the composing CompositePredicate</returns>
        ICompositePredicate AddExists(IAssociationType assocation);

        /// <summary>
        /// Adds a Predicate that evaluates to true if the role of the object under evaluation is greater than the object.
        /// </summary>
        /// <param name="role">The role .</param>
        /// <param name="value">The object.</param>
        /// <returns>the composing CompositePredicate</returns>
        ICompositePredicate AddGreaterThan(IRoleType role, object value);

        /// <summary>
        /// Adds a Predicate that evaluates to true if the object under evaluation is an instance of the IObjectType.
        /// </summary>
        /// <param name="objectType">the IObjectType.</param>
        /// <returns>the composing CompositePredicate</returns>
        ICompositePredicate AddInstanceof(IComposite objectType);

        /// <summary>
        /// Adds a Predicate that evaluates to true if the role of the object under evaluation is an instance of the IObjectType.
        /// </summary>
        /// <param name="role">the RoleType .</param>
        /// <param name="objectType">the IObjectType.</param>
        /// <returns>the composing CompositePredicate</returns>
        ICompositePredicate AddInstanceof(IRoleType role, IComposite objectType);

        /// <summary>
        /// Adds a Predicate that evaluates to true if the association of the object under evaluation is an instance of the IObjectType.
        /// </summary>
        /// <param name="association">the AssociationType.</param>
        /// <param name="objectType">the IObjectType.</param>
        /// <returns>the composing CompositePredicate</returns>
        ICompositePredicate AddInstanceof(IAssociationType association, IComposite objectType);

        /// <summary>
        /// Adds a Predicate that evaluates to true if the role of the object under evaluation is less than the object.
        /// </summary>
        /// <param name="role">The role .</param>
        /// <param name="value">The object.</param>
        /// <returns>the composing CompositePredicate</returns>
        ICompositePredicate AddLessThan(IRoleType role, object value);

        /// <summary>
        /// Adds a Predicate that evaluates to true if the role of the object under evaluation is like the string (Sql like).
        /// </summary>
        /// <param name="role">The role .</param>
        /// <param name="value">The string.</param>
        /// <returns>the composing CompositePredicate</returns>
        ICompositePredicate AddLike(IRoleType role, string value);

        /// <summary>
        /// Adds a CompositePredicate that evaluates to true if its composed predicate evaluates to false.
        /// This predicate is ignored when there are no composed predicates.
        /// </summary>
        /// <returns>the newly added CompositePredicate</returns>
        ICompositePredicate AddNot();

        /// <summary>
        /// Adds a CompositePredicate that evaluates to true if any of its composed predicates evaluate to true.
        /// This predicate is ignored when there are no composed predicates.
        /// </summary>
        /// <returns>the newly added CompositePredicate</returns>
        ICompositePredicate AddOr();
    }
}