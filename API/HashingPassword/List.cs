using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.HashingPassword
{
    public static class List
    {
         public static T[] Append<T>(this T[] array, T item)
    {
        return new List<T>(array) { item }.ToArray();
    }
}
}
