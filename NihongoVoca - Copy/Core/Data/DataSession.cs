using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ivs.Core.Data
{
    public static class DataSession
    {
        public static IDictionary<string, int> VocaSets = new Dictionary<string, int>();
        public static IDictionary<string, int> VocaCates = new Dictionary<string, int>();
    }
}