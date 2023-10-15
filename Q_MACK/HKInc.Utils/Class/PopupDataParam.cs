using System;
using System.Collections.Generic;

namespace HKInc.Utils.Class
{
    public class PopupDataParam : Dictionary<Enum.PopupParameter, object>
    {
        public object GetValue(Enum.PopupParameter key)
        {
            object val;
            if (base.TryGetValue(key, out val))
                return val;
            else
                return null;
        }

        public void SetValue(Enum.PopupParameter key, object value)
        {
            if (this.ContainsKey(key))
            {
                this[key] = value;
            }
            else
            {
                this.Add(key, value);
            }
        }
    }
}
