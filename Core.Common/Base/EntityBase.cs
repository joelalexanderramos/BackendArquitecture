﻿using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Core.Common.Extensions;
using System;

namespace Core.Common.Base
{
    public abstract class EntityBase
    {

    }

    /// <summary>
    /// Base client applied to the Business Entities
    /// </summary>
    [DataContract]
    public abstract class EntityBase<T> : EntityBase, IExtensibleDataObject, IEquatable<T>
    {
        #region IExtensibleDataObject Members

        [IgnoreDataMember]
        [NotMapped]
        public ExtensionDataObject ExtensionData { get; set; }

        #endregion

        public override bool Equals(object obj)
        {
            if(obj.GetType() == typeof(T))
                return this.Equals((T)obj);

            return false;
        }

        public bool Equals(T x)
        {
            //create variables to store object values
            object value1 = null, value2 = null;

            PropertyInfo[] properties = x.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            //get all public properties of the object using reflection  
            foreach(PropertyInfo propertyInfo in properties)
            {
                //get the property values of both the objects
                value1 = propertyInfo.GetValue(x, null);
                value2 = propertyInfo.GetValue(this, null);

                if(Equals(value1, value2))
                    continue;
                else
                    return false;
            }

            return true;
        }

        public int GetHashCode(T obj)
        {
            int hashCode = this.GetHashCodeOnProperties();
            return hashCode;
        }

        public override int GetHashCode()
        {
            return this.GetHashCode((T)Convert.ChangeType(this, typeof(T)));
        }
    }

    [DataContract]
    public class StoredProcedureEntityBase : EntityBase<StoredProcedureEntityBase>
    {

    }

    [DataContract]
    public class FunctionEntityBase : EntityBase<FunctionEntityBase>
    {

    }
}
