/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Data;
using System.Data.Common;
using NHibernate;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;

namespace Bifrost.NHibernate.UserTypes
{
    /// <summary>
    /// A User type that wraps up transforming between a .NET type of Guid and the RAW(16) data type in Oracle
    /// </summary>
    public class OracleGuidUserType : IUserType
    {
        static SqlType[] _types = new [] { new SqlType(DbType.Binary) };
        static Type _type = typeof (Guid);
#pragma warning disable 1591
        public object Assemble(object cached, object owner)
        {
            return DeepCopy(cached);
        }

        public new bool Equals(object x, object y)
        {
            return (x != null && x.Equals(y));
        }

        public object DeepCopy(object value)
        {
            return value;
        }

        public object Disassemble(object value)
        {
            return DeepCopy(value);
        }

        public int GetHashCode(object x)
        {
            return x.GetHashCode();
        }

        public bool IsMutable
        {
            get { return true; }
        }

        public object NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner)
        {
            var result = Guid.Empty;
            var buffer = (byte[])NHibernateUtil.Binary.NullSafeGet(rs, names[0], session);
            if (buffer == null)
                return result;

            result = new Guid(buffer);
            return result;
        }

        public void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
        {
            var type = (Type)value;
            NHibernateUtil.String.NullSafeSet(cmd, type.AssemblyQualifiedName, index, session);
        }

        public object Replace(object original, object target, object owner)
        {
            return original;
        }

        public Type ReturnedType
        {
            get { return _type; }
        }

        public SqlType[] SqlTypes
        {
            get { return _types; }
        }
#pragma warning restore 1591
    }
}
