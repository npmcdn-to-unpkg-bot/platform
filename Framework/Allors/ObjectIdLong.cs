//------------------------------------------------------------------------------------------------- 
// <copyright file="ObjectIdLong.cs" company="Allors bvba">
// Copyright 2002-2013 Allors bvba.
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
// <summary>Defines the ObjectIdLong type.</summary>
//-------------------------------------------------------------------------------------------------
namespace Allors
{
    using System;
    using System.Globalization;

    /// <summary>
    /// A 64 bit <see cref="ObjectId"/>.
    /// </summary>
    [Serializable]
    public sealed class ObjectIdLong : ObjectId, IComparable<ObjectIdLong>
    {
        /// <summary>
        /// The object id.
        /// </summary>
        private readonly long id;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectIdLong"/> class.
        /// </summary>
        /// <param name="id">The object id.</param>
        public ObjectIdLong(long id)
        {
            this.id = id;
        }

        /// <summary>
        /// Gets the value of the Id.
        /// </summary>
        /// <value>The value.</value>
        public override object Value
        {
            get { return this.id; }
        }

        /// <summary>
        /// Gets the value64.
        /// </summary>
        /// <value>The value64.</value>
        public long ValueLong
        {
            get { return this.id; }
        }

        /// <summary>
        /// Compares the current instance with another object of the same type.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the objects being compared. 
        /// The return value has these meanings: Value Meaning Less than zero This instance is less than <paramref name="obj"/>. 
        /// Zero This instance is equal to <paramref name="obj"/>. Greater than zero This instance is greater than <paramref name="obj"/>.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="obj"/> is not the same type as this instance. </exception>
        public override int CompareTo(object obj)
        {
            return this.CompareTo(obj as ObjectIdLong);
        }

        /// <summary>
        /// Compares the current instance with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the objects being compared. 
        /// The return value has these meanings: Value Meaning Less than zero This instance is less than <paramref name="other"/>. 
        /// Zero This instance is equal to <paramref name="other"/>. Greater than zero This instance is greater than <paramref name="other"/>.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="other"/> is not the same type as this instance. </exception>
        public int CompareTo(ObjectIdLong other)
        {
            return other == null ? -1 : this.id.CompareTo(other.id);
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <param name="other">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.</param>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.NullReferenceException">The <paramref name="other"/> parameter is null.</exception>
        public override bool Equals(object other)
        {
            return this.Equals(other as ObjectIdLong);
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <param name="other">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.</param>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.NullReferenceException">The <paramref name="other"/> parameter is null.</exception>
        public bool Equals(ObjectIdLong other)
        {
            return other != null && this.id == other.id;
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        public override int GetHashCode()
        {
            return this.id.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return this.id.ToString(CultureInfo.InvariantCulture);
        }
    }
}