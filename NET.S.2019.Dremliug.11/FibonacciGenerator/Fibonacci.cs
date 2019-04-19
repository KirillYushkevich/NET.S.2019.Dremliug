using System;
using System.Collections.Generic;

namespace NET.S._2019.Dremliug._11
{
    public class Fibonacci
    {
        public static IEnumerable<long> Sequence()
        {
            long first = 1;
            long second = 1;

            yield return first;

            while (true)
            {
                yield return second;
                (first, second) = (second, first + second);
            }
        }
    }
}