using System.Collections.Generic;

namespace Ivs.Core.Data
{
    public static class ApplicationState
    {
        private static Dictionary<string, object> _values =
                   new Dictionary<string, object>();

        public static void SetValue(string key, object value)
        {
            if (_values.ContainsKey(key))
            {
                _values.Remove(key);
            }
            _values.Add(key, value);
        }

        public static T GetValue<T>(string key)
        {
            if (_values.ContainsKey(key))
            {
                return (T)_values[key];
            }
            else
            {
                return default(T);
            }
        }

        public static void Clear()
        {
            _values.Clear();
        }

        public static bool Contain(string key)
        {
            if (_values.ContainsKey(key))
            {
                return true;
            }
            return false;
        }
    }
}