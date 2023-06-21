using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Utils.Class
{
    public static class AttributeExtention
    {
        public static T GetAttributeFrom<T>(this object instance, string propertyName) where T : Attribute
        {
            var attrType = typeof(T);
            var property = instance.GetType().GetProperty(propertyName);
            if (property == null) return null;
            object[] obj = property.GetCustomAttributes(attrType, false);
            if (obj.Length > 0)
                return (T)property.GetCustomAttributes(attrType, false).First();
            else
                return null;

        }
        public static T GetAttributeFrom<T>(this Type type, string propertyName) where T : Attribute
        {
            var attrType = typeof(T);
            var property = type.GetProperty(propertyName);
            object[] obj = property.GetCustomAttributes(attrType, false);
            if (obj.Length > 0)
                return (T)property.GetCustomAttributes(attrType, false).First();
            else
                return null;
        }
    }
}
