namespace RSLib.GE
{
    using System.Collections.Generic;
    using Godot;

    public static class IListExtensions
    {
        /// <summary>
        /// Shuffles the list.
        /// </summary>
        /// <param name="list">List to shuffle.</param>
        /// <param name="rng">Random number generator.</param>
        /// <returns>Shuffled list.</returns>
        public static void ShuffleDeterministic<T>(this IList<T> list, RandomNumberGenerator rng)
        {
            int n = list.Count;
            while (n > 1)
            {
                int k = rng.RandiRange(0, n - 1);
                (list[k], list[n - 1]) = (list[n - 1], list[k]);
                n--;
            }
        }
    }
}