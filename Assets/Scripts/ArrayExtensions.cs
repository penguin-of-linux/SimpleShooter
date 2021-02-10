using UnityEngine;

namespace DefaultNamespace
{
    public static class ArrayExtensions
    {
        public static T ValueAt<T>(this T[,] array, Vector2Int vec)
        {
            return array[vec.x, vec.y];
        }
    }
}