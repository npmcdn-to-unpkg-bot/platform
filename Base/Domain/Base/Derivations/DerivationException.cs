// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DerivationException.cs" company="Allors bvba">
//   Copyright 2002-2016 Allors bvba.
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
    using System.Text;

    public class DerivationException : Exception
    {
        private readonly DerivationLog derivationLog;

        public DerivationException(DerivationLog derivationLog)
        {
            this.derivationLog = derivationLog;
        }

        public DerivationLog DerivationLog
        {
            get
            {
                return this.derivationLog;
            }
        }

        public override string Message
        {
            get
            {
                var message = new StringBuilder();
                foreach (var error in this.derivationLog.Errors)
                {
                    message.Append(error.Message + "\n");
                }

                return message.ToString();
            }
        }
    }
}