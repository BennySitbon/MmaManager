using System.Collections.Generic;

namespace Domain.Extensions
{
    public static class ListExtensions
    {
        public static HashSet<T> ToHashSet<T>(this List<T> list) where T : class
        {
            return new HashSet<T>(list);
        }
    }
}
