namespace PsigenVision.Utilities
{
    public static class Hasher
    {
        // These constants are the "magic numbers" defined by the FNV creators.
        // They are specifically chosen because they help spread bits evenly.
        internal const uint FNV_OFFSET_BASIS = 2166136261;
        internal const uint FNV_PRIME = 16777619;
        
        /// <summary>
        /// Computes the FNV-1a hash for the input string.
        /// This follows the standard FNV-1a formula: Hash = (Hash XOR Data) * Prime. 
        /// The FNV-1a hash is a non-cryptographic hash function known for its speed and good distribution properties.
        /// Useful for creating Dictionary keys instead of using strings.
        /// https://en.wikipedia.org/wiki/Fowler%E2%80%93Noll%E2%80%93Vo_hash_function
        /// </summary>
        /// <param name="ch">The input char to hash.</param>
        /// <returns>An unsigned integer representing the FNV-1a hash of the input string.</returns>
        public static uint HashFNV1a(this char ch, uint seed = Hasher.FNV_OFFSET_BASIS) {
            // We start with the 'currentHash'. 
            // If this is the first thing being hashed, it uses the OFFSET_BASIS.
            // If we are continuing a hash, it uses the result of the previous step.
            uint hash = seed;
            //For each character:
            // 1. XOR the bottom 8-16 bits of the hash with our character
            // 2. Multiply by the prime. This "scrambles" the bits and 
            // moves them to the left, making room for the next character.
            return (hash ^ ch) * FNV_PRIME;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="seed"></param>
        /// <returns></returns>
        public static int HashIntFNV1a(this char ch, uint seed = FNV_OFFSET_BASIS)
        => unchecked((int)ch.HashFNV1a(seed));
        
        /// <summary>
        /// Computes the FNV-1a hash for the input string.
        /// This follows the standard FNV-1a formula: Hash = (Hash XOR Data) * Prime. 
        /// The FNV-1a hash is a non-cryptographic hash function known for its speed and good distribution properties.
        /// Useful for creating Dictionary keys instead of using strings.
        /// https://en.wikipedia.org/wiki/Fowler%E2%80%93Noll%E2%80%93Vo_hash_function
        /// </summary>
        /// <param name="str">The input string to hash.</param>
        /// <returns>An integer representing the FNV-1a hash of the input string.</returns>
        public static int HashIntFNV1a(this string str, uint seed = FNV_OFFSET_BASIS) {
            // We start with the 'currentHash'. 
            // If this is the first thing being hashed, it uses the OFFSET_BASIS.
            // If we are continuing a hash, it uses the result of the previous step.
            uint hash = seed;
            //For each character:
            // 1. XOR the bottom 8-16 bits of the hash with our character
            // 2. Multiply by the prime. This "scrambles" the bits and 
            // moves them to the left, making room for the next character.
            foreach (char c in str) hash = c.HashFNV1a(hash);//(hash ^ c) * Hasher.FNV_PRIME;
            return unchecked((int)hash);
        }
        
        /// <summary>
        /// Computes the FNV-1a hash for the input string.
        /// This follows the standard FNV-1a formula: Hash = (Hash XOR Data) * Prime. 
        /// The FNV-1a hash is a non-cryptographic hash function known for its speed and good distribution properties.
        /// Useful for creating Dictionary keys instead of using strings.
        /// https://en.wikipedia.org/wiki/Fowler%E2%80%93Noll%E2%80%93Vo_hash_function
        /// </summary>
        /// <param name="str">The input string to hash.</param>
        /// <returns>An unsigned integer representing the FNV-1a hash of the input string.</returns>
        public static uint HashFNV1a(this string str, uint seed = Hasher.FNV_OFFSET_BASIS) {
            // We start with the 'currentHash'. 
            // If this is the first thing being hashed, it uses the OFFSET_BASIS.
            // If we are continuing a hash, it uses the result of the previous step.
            uint hash = seed;
            //For each character:
            // 1. XOR the bottom 8-16 bits of the hash with our character
            // 2. Multiply by the prime. This "scrambles" the bits and 
            // moves them to the left, making room for the next character.
            foreach (char c in str) hash = (hash ^ c) * FNV_PRIME;
            return hash;
        }

        /// <summary>
        /// Combines an existing hash with a nested hash using the FNV-1a algorithm.
        /// Processes each byte of the nested hash individually to ensure a well-distributed result.
        /// This approach treats the nested hash as a collection of four individual bytes, minimizing collisions and maximizing mathematical distribution.
        /// </summary>
        /// <param name="currentHash">The current (outer) hash to be combined with the nested hash.</param>
        /// <param name="innerHash">The nested hash to be combined with the current hash.</param>
        /// <returns>A uint representing the combined hash value after applying the FNV-1a algorithm to the input hashes.</returns>
        public static uint HashMix(this uint currentHash, uint innerHash)
        {
            //STEP 2: The "Nested Hash Combiner".
            // Instead of turning an ID back into a string,
            // we treat the 4 bytes of the integer ID as 4 separate "ingredients".
            // This is mathematically superior to XORing the final results
            
            // An integer (uint) is 32 bits, which is 4 bytes. 
            // We will process each byte one-by-one to keep the "smoothie" consistent.
            
            //Byte 1: The lowest 8 bits
            currentHash = (currentHash ^ (innerHash & 0xFF)) * FNV_PRIME;
            //Byte 2: Shift right 8 bits, then grab the next 8
            currentHash = (currentHash ^ ((innerHash >> 8) & 0xFF)) * FNV_PRIME;
            //Byte 3: Shift right 16 bits...
            currentHash = (currentHash ^ ((innerHash >> 16) & 0xFF)) * FNV_PRIME;
            //Byte 4: The highest 8 bits
            return (currentHash ^ ((innerHash >> 24) & 0xFF)) * FNV_PRIME;
        }

        /// <summary>
        /// Combines an existing hash with an integer value using the FNV-1a algorithm.
        /// This method treats the integer value as a nested hash, processing its individual bytes
        /// to ensure a well-distributed and collision-resistant result.
        /// </summary>
        /// <param name="currentHash">The initial hash value to be combined with the integer value.</param>
        /// <param name="value">The integer value to be blended into the current hash.</param>
        /// <returns>A uint representing the combined hash value after applying the FNV-1a algorithm.</returns>
        public static uint HashMix(this uint currentHash, int value)
        {
            // STEP 3: The "Raw Value Blender".
            // Useful for members like 'int Count' or 'bool IsActive'.
            
            // We can just reuse the logic from HashNestedId
            return HashMix(currentHash, (uint)value);
        }
        
        /* Example of Computing Nested Hash IDs 
        public int GetID() 
        {
            // Start the assembly line
            uint hash = TargetType.FullName.HashFNV1a();

            // Add the nested struct's ID to the mix
            hash = hash.HashMixFNV1a(Signature.ID);

            // Add a raw integer member
            hash = hash.HashMixFNV1a(Priority);

            // Return the final result as a signed int (as C# expects)
            return unchecked((int)hash);
        }
         */
    }
}