
namespace NET.S._2019.Dremliug._04
{
    /// <summary>
    /// Provides extension method for doubles.
    /// </summary>
    public static class DoubleExtensions
    {
        /// <summary>
        /// Converts the binary representation of a double number to a string.
        /// </summary>
        /// <param name="number"> A double number. </param>
        /// <returns> A string created from the binary representation of a given double number. </returns>
        public static string ToBinaryString(this double number)
        {

            char[] bitsAsChar = new char[64];

            ulong rawBits;
            unsafe
            {
                rawBits = *(ulong*)&number;
            }

            for (int i = 0; i < 64; i++)
            {
                // Check each bit state and set the corresponding char. The bits remain in their original order.
                bitsAsChar[i] = (rawBits & ((ulong)1 << (63 - i))) == 0 ? '0' : '1';
            }

            return new string(bitsAsChar);
        }
    }
}
