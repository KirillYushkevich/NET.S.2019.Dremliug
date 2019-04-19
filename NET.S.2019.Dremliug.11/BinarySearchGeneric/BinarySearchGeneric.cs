using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.S._2019.Dremliug._11
{
    public static class BinarySearch
    {
        #region Simple implementation
        /// <summary>
        /// Searches the sorted <see cref="IList{T}"/> for an element using the specified of default comparer and returns zero-based index of the element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="value"></param>
        /// <param name="comparer"></param>
        /// <returns> The zero-based index of <paramref name="value"> in the sorted <see cref="IList{T}"/>, if <paramref name="value"> is found; otherwise, a negative number that is the bitwise complement of the index of the next element that is larger than <paramref name="value"> or, if there is no larger element, the bitwise complement of <see cref="IList{T}.Count"/>; <see cref="null"/> if <paramref name="collection"> is <see cref="null"/> </returns>
        public static int? SimpleImplementation<T>(this IList<T> collection, T value, IComparer<T> comparer = null)
            => collection?.ToList().BinarySearch(value, comparer);
        #endregion

        #region Real implementation
        /// <summary>
        /// Searches the sorted <see cref="IList{T}"/> for an element using the specified of default <paramref name="comparer"/> and returns zero-based index of the element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="value"></param>
        /// <param name="comparer"></param>
        /// <returns> The zero-based index of value in the sorted <see cref="IList{T}"/>, if <paramref name="value"> is found; otherwise, a negative number that is the bitwise complement of the index of the next element that is larger than <paramref name="value"> or, if there is no larger element, the bitwise complement of <see cref="IList{T}.Count"/> </returns>
        /// <exception cref="ArgumentNullException"><paramref name="collection"/> is <see cref="null"/></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static int Search<T>(this IList<T> collection, T value, IComparer<T> comparer = null)
        {
            if (collection is null || value == null)
            {
                throw new ArgumentNullException($"{((collection is null) ? nameof(collection) : nameof(value))}");
            }

            if (comparer is null && (!(typeof(IComparable<T>).IsAssignableFrom(typeof(T)) || typeof(IComparable).IsAssignableFrom(typeof(T)))))
            {
                throw new InvalidOperationException($"Cannot compare instances of {typeof(T)}");
            }

            comparer = comparer ?? Comparer<T>.Default;

            int lowBound = 0;
            int highBound = collection.Count - 1;
            while (lowBound <= highBound)
            {
                int median = lowBound + ((highBound - lowBound) >> 1);
                int order = comparer.Compare(collection[median], value);

                if (order == 0)
                {
                    return median;
                }

                if (order < 0)
                {
                    lowBound = median + 1;
                }
                else
                {
                    highBound = median - 1;
                }
            }

            return ~lowBound;
        } 
        #endregion
    }
}
