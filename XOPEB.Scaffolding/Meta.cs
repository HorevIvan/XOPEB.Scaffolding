using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace XOPEB.Scaffolding
{
    public class Meta : Dictionary<string, object>
    {
        protected void Set(object value, [CallerMemberName] string propertyName = null)
        {
            this[propertyName] = value;
        }

        protected T Get<T>([CallerMemberName] string propertyName = null)
        {
            return ContainsKey(propertyName) ? (T)this[propertyName] : default(T);
        }
    }
}
