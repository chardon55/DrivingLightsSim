using System;
using System.Collections.Generic;
using System.Text;

namespace DrivingLightsSim.Utils
{
    public static class GenericCollectionExtensions
    {
        public static T[] ToArray<T>(this ICollection<T> @this)
        {
            T[] arr = new T[@this.Count];
            @this.CopyTo(arr, 0);
            return arr;
        }
    }
}
