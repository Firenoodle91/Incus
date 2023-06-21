using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Utils.Class
{
    public class DataParam : Dictionary<string, object>
    {
        public object GetValue(string key)
        {
            object val;
            if (base.TryGetValue(key, out val))
                return val;
            else
                return null;
        }

        public void SetValue(string key, object value)
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
