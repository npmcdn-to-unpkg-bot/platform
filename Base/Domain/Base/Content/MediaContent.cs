// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Media.cs" company="Allors bvba">
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

namespace Allors.Domain
{
    using System;
    using System.Security.Cryptography;

    public partial class MediaContent
    {
        // TODO: Value should be write-once

        public void BaseDerive(ObjectDerive method)
        {
            var derivation = method.Derivation;

            if (!this.ExistHash && this.ExistValue)
            {
                var sha1CryptoServiceProvider = new SHA1CryptoServiceProvider();
                var binarySha1 = sha1CryptoServiceProvider.ComputeHash(this.Value);
                var sha1 = BitConverter.ToString(binarySha1);
                sha1 = sha1.Replace("-", string.Empty);
                sha1 = sha1.ToUpper();

                this.Hash = sha1;
            }
        }
    }
}