using System.Collections.Generic;
using System.Linq;
namespace Chatlyst.Runtime.Util
{
    public static class ListExtend
    {
        public static bool AreSimilar<T>(this List<T> expected, List<T> actual)
        {
            return expected.Count == actual.Count && actual.All(expected.Contains);
        }
    }
}
