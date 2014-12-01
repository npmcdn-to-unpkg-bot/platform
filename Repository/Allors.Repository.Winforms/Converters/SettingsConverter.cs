// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingsConverter.cs" company="Allors bvba">
//   Copyright 2002-2009 Allors bvba.
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

namespace Allors.Meta.WinForms.Converters
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    using Allors.Meta.WinForms.Objects;

    public class SettingsConverter : TypeConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destType)
        {
            if (destType == typeof(string) && value is Setting[])
            {
                var settings = (Setting[])value;
                if (settings.Length == 1)
                {
                    return "1 setting";
                }

                return settings.Length + " settings";
            }

            return base.ConvertTo(context, culture, value, destType);
        }
    }
}