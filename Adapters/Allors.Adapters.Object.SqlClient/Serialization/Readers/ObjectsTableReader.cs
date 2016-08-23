// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectsTableReader.cs" company="Allors bvba">
//   Copyright 2002-2016 Allors bvba.
// Dual Licensed under
//   a) the Lesser General Public Licence v3 (LGPL)
//   b) the Allors License
// The LGPL License is included in the file lgpl.txt.
// The Allors License is an addendum to your contract.
// Allors Platform is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// For more information visit http://www.allors.com/legal
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Allors.Meta;

namespace Allors.Adapters.Object.SqlClient
{
    internal class ObjectsTableReader : IDataReader
    {
        private readonly Dictionary<long, long> objectVersionByObjectId;
        private readonly IEnumerator<KeyValuePair<long,IObjectType>>  enumerator;

        internal ObjectsTableReader(Dictionary<long, IObjectType> objectTypeByObjectId, Dictionary<long, long> objectVersionByObjectId)
        {
            this.objectVersionByObjectId = objectVersionByObjectId;
            this.enumerator = objectTypeByObjectId.GetEnumerator();
        }
        
        public int FieldCount => 3;

        public bool Read()
        {
            var result = this.enumerator.MoveNext();
            return result;
        }

        public string GetName(int i)
        {
            switch (i)
            {
                case 0:
                    return Mapping.ColumnNameForObject;
                case 1:
                    return Mapping.ColumnNameForClass;
                case 2:
                    return Mapping.ColumnNameForVersion;
                default:
                    return default(string);
            }
        }

        public int GetOrdinal(string name)
        {
            switch (name)
            {
                case Mapping.ColumnNameForObject:
                    return 0;
                case Mapping.ColumnNameForClass:
                    return 1;
                case Mapping.ColumnNameForVersion:
                    return 2;
                default:
                    return -1;
            }
        }

        public object GetValue(int i)
        {
            var current = this.enumerator.Current;

            switch (i)
            {
                case 0:
                    return current.Key;
                case 1:
                    return current.Value.Id;
                case 2:
                    return this.objectVersionByObjectId[current.Key];
                default:
                    return null;
            }
        }

        #region Not Supported
        public void Close()
        {
            throw new NotSupportedException();
        }

        public int Depth
        {
            get { throw new NotSupportedException(); }
        }

        public DataTable GetSchemaTable()
        {
            throw new NotSupportedException();
        }

        public bool IsClosed
        {
            get { throw new NotSupportedException(); }
        }

        public bool NextResult()
        {
            throw new NotSupportedException();
        }

        public int RecordsAffected
        {
            get { throw new NotSupportedException(); }
        }

        public void Dispose()
        {
            throw new NotSupportedException();
        }

        public bool GetBoolean(int i)
        {
            throw new NotSupportedException();
        }

        public byte GetByte(int i)
        {
            throw new NotSupportedException();
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotSupportedException();
        }

        public char GetChar(int i)
        {
            throw new NotSupportedException();
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotSupportedException();
        }

        public IDataReader GetData(int i)
        {
            throw new NotSupportedException();
        }

        public string GetDataTypeName(int i)
        {
            throw new NotSupportedException();
        }

        public DateTime GetDateTime(int i)
        {
            throw new NotSupportedException();
        }

        public decimal GetDecimal(int i)
        {
            throw new NotSupportedException();
        }

        public double GetDouble(int i)
        {
            throw new NotSupportedException();
        }

        public Type GetFieldType(int i)
        {
            throw new NotSupportedException();
        }

        public float GetFloat(int i)
        {
            throw new NotSupportedException();
        }

        public Guid GetGuid(int i)
        {
            throw new NotSupportedException();
        }

        public short GetInt16(int i)
        {
            throw new NotSupportedException();
        }

        public int GetInt32(int i)
        {
            throw new NotSupportedException();
        }

        public long GetInt64(int i)
        {
            throw new NotSupportedException();
        }

        public string GetString(int i)
        {
            throw new NotSupportedException();
        }

        public int GetValues(object[] values)
        {
            throw new NotSupportedException();
        }

        public bool IsDBNull(int i)
        {
            throw new NotSupportedException();
        }

        public object this[string name]
        {
            get { throw new NotSupportedException(); }
        }

        public object this[int i]
        {
            get { throw new NotSupportedException(); }
        }
        #endregion
    }
}