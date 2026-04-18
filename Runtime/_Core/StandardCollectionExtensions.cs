using System;

namespace PsigenVision.Utilities.Collection
{
    public static class StandardCollectionExtensions
    {
        public static T[] LengthenBy<T>(this T[] array, int by, bool reversed = false)
        {
            if (by <= 0)
                throw new ArgumentOutOfRangeException(nameof(by), "Length increase must be greater than zero.");
            
            //Increase the collection length and copy it over
            var lengthenedArray = new T[array.Length + by];
            //if reversed, the end of the last collection must be at the end of the new one, which is lengthed by 1 (so the original array must be inserted at index 1 of the shifted array)
            array.CopyTo(lengthenedArray, reversed ? by : 0);
            return lengthenedArray;
        }
    }
}