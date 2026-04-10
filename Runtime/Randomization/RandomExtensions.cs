using System;

namespace PsigenVision.Utilities.Randomization
{
    public static class RandomExtensions
    {
        /// <summary>
        /// This is an extension method to generate a random long value from min (inclusive) to max (exclusive).  
        /// Taken from https://stackoverflow.com/questions/6651554/random-number-in-long-range-is-this-the-way
        /// </summary>
        /// <param name="random"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static long NextLong(this Random random, long min = 0, long max = long.MaxValue)
        {
            if (max <= min)
                throw new ArgumentOutOfRangeException("max", "max must be > min!");

            //Working with ulong so that modulo works correctly with values > long.MaxValue
            ulong uRange = (ulong)(max - min);

            //Prevent a modolo bias; see https://stackoverflow.com/a/10984975/238419
            //for more information.
            //In the worst case, the expected number of calls is 2 (though usually it's
            //much closer to 1) so this loop doesn't really hurt performance at all.
            ulong ulongRand;
            do
            {
                byte[] buf = new byte[8];
                random.NextBytes(buf);
                ulongRand = (ulong)BitConverter.ToInt64(buf, 0);
            } while (ulongRand > ulong.MaxValue - ((ulong.MaxValue % uRange) + 1) % uRange);

            return (long)(ulongRand % uRange) + min;
        }

        /// <summary>
        /// Generate a pseudorandom double between the lowerBound and upperBound specified. 
        /// Resource: https://code-maze.com/csharp-random-double-range/
        /// </summary>
        /// <param name="random"></param>
        /// <param name="lowerBound"></param>
        /// <param name="upperBound"></param>
        /// <returns></returns>
        public static double NextDouble(this Random random, double lowerBound, double upperBound)
        {
            if (upperBound <= lowerBound)
                throw new ArgumentOutOfRangeException("upperBound", "upperBound must be > lowerBound!");
            return random.NextDouble() * (upperBound - lowerBound) + lowerBound;
        }
    }
}