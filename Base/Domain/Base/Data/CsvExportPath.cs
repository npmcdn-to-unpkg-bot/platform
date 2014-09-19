// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvExportPath.cs" company="Allors bvba">
//   Copyright 2002-2011 Allors bvba.
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
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

    using Allors.Domain;
    using Allors.Meta;
    using Allors;
    using Allors.Meta;

    using ObjectBase = ObjectBase;

    public class CsvExportPath : CsvExportColumn
    {
        public CsvExportPath(PropertyType propertyType)
            : this(new Path { PropertyType = propertyType })
        {
        }

        public CsvExportPath(Path path)
        {
            this.Path = path;
            this.Header = path.End.PropertyType.DisplayName;
        }

        public Path Path { get; private set; }

        public object EmptyValue { get; set; }

        public string FormatString { get; set; }

        public IFormatProvider FormatProvider { get; set; }

        public string SubfieldSeparator { get; set; }

        public override void Write(CsvExport file, CultureInfo cultureInfo, StringBuilder stringBuilder, IObject obj, IAccessControlListFactory aclFactory)
        {
            var value = this.Path.Get((ObjectBase)obj, aclFactory) ?? this.EmptyValue;

            if (value != null)
            {
                var actualFormatProvider = this.FormatProvider ?? cultureInfo;

                var set = value as ISet<object>;
                if (set != null)
                {
                    stringBuilder.Append("\"");

                    var actualSubfieldSeparator = this.SubfieldSeparator ?? file.SubfieldSeparator ?? cultureInfo.TextInfo.ListSeparator;

                    var first = true;
                    foreach (var item in set)
                    {
                        if (first)
                        {
                            first = false;
                        }
                        else
                        {
                            stringBuilder.Append(actualSubfieldSeparator);
                        }

                        var escapedStringValue = this.GetEscapedStringValue(actualFormatProvider, item);
                        stringBuilder.Append(escapedStringValue);
                    }

                    stringBuilder.Append("\"");
                }
                else
                {
                    stringBuilder.Append("\"");

                    var escapedStringValue = this.GetEscapedStringValue(actualFormatProvider, value);
                    stringBuilder.Append(escapedStringValue);

                    stringBuilder.Append("\"");
                }
            }
        }

        private string GetEscapedStringValue(IFormatProvider formatProvider, object value)
        {
            string stringValue;
            if (this.FormatString != null)
            {
                stringValue = string.Format(formatProvider, this.FormatString, value);
            }
            else
            {
                var objectType = this.Path.GetObjectType();
                if (objectType.IsUnit)
                {
                    var unit = (Meta.Unit)objectType;
                    switch ((UnitTags)unit.UnitTag)
                    {
                        case UnitTags.AllorsString:
                            stringValue = (string)value;
                            break;

                        case UnitTags.AllorsInteger:
                            stringValue = ((int)value).ToString(formatProvider);
                            break;

                        case UnitTags.AllorsLong:
                            stringValue = ((long)value).ToString(formatProvider);
                            break;

                        case UnitTags.AllorsDecimal:
                            stringValue = ((decimal)value).ToString(formatProvider);
                            break;

                        case UnitTags.AllorsDouble:
                            stringValue = ((double)value).ToString(formatProvider);
                            break;

                        case UnitTags.AllorsBoolean:
                            stringValue = ((bool)value).ToString(formatProvider);
                            break;

                        case UnitTags.AllorsDateTime:
                            stringValue = ((DateTime)value).ToString(formatProvider);
                            break;

                        case UnitTags.AllorsUnique:
                            stringValue = ((Guid)value).ToString();
                            break;

                        case UnitTags.AllorsBinary:
                            stringValue = Convert.ToBase64String((byte[])value);
                            break;

                        default:
                            throw new ArgumentException("Unknown unit ObjectType: " + objectType);
                    }
                }
                else
                {
                    throw new Exception("ObjectType of an ExportColumn should be a Unit.");
                }
            }

            return stringValue.Replace("\"", "\"\"");
        }
    }
}