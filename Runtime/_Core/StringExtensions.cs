using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PsigenVision.Utilities
{
    public static class StringExtensions
    {
        /// <summary>
        /// Computes the FNV-1a hash for the input string. 
        /// The FNV-1a hash is a non-cryptographic hash function known for its speed and good distribution properties.
        /// Useful for creating Dictionary keys instead of using strings.
        /// https://en.wikipedia.org/wiki/Fowler%E2%80%93Noll%E2%80%93Vo_hash_function
        /// </summary>
        /// <param name="str">The input string to hash.</param>
        /// <returns>An integer representing the FNV-1a hash of the input string.</returns>
        public static int ComputeFNV1aHash(this string str) {
            uint hash = 2166136261;
            foreach (char c in str) hash = (hash ^ c) * 16777619;
            return unchecked((int)hash);
        }

        /// <summary>
        /// Validates whether the provided string is a valid C# member name.
        /// A valid member name must start with a letter or underscore, can be followed
        /// by letters, digits, or underscores, and must not match any reserved C# keywords.
        /// </summary>
        /// <param name="variableName">The string to validate as a potential C# member name.</param>
        /// <returns>True if the string is a valid C# member name; otherwise, false.</returns>
        public static bool IsValidMemberName(this string variableName)
        {
            /* Breakdown of the Pattern
             ^ ... $: Anchors the search to the start and end of the string to ensure the entire input is validated.
            (?!(?:keyword1|keyword2|...)$): This is a negative lookahead. It checks if the input matches any C# reserved keyword exactly and fails the match if it does.
            [a-zA-Z_]: Ensures the first character is either a letter or an underscore, which is a requirement for C# identifiers.
            [a-zA-Z0-9_]*: Allows the rest of the string to contain letters, numbers, or underscores.
             */
            string pattern = @"^(?!(?:abstract|as|base|bool|break|byte|case|catch|char|checked|class|const|continue|decimal|default|delegate|do|double|else|enum|event|explicit|extern|false|finally|fixed|float|for|foreach|goto|if|implicit|in|int|interface|internal|is|lock|long|namespace|new|null|object|operator|out|override|params|private|protected|public|readonly|ref|return|sbyte|sealed|short|sizeof|stackalloc|static|string|struct|switch|this|throw|true|try|typeof|uint|ulong|unchecked|unsafe|ushort|using|virtual|void|volatile|while)$)[a-zA-Z_][a-zA-Z0-9_]*$";
            return Regex.IsMatch(variableName, pattern);
        }

        /// <summary>
        /// Determines whether the provided string is a valid dot-separated path.
        /// A valid dot-separated path consists of segments separated by dots, where each segment
        /// must be a valid C# member name as per C# identifier naming rules.
        /// </summary>
        /// <param name="dotSeparatedPath">The string to validate as a dot-separated path.</param>
        /// <returns>True if the string is a valid dot-separated path; otherwise, false.</returns>
        public static bool IsValidDotSeparatedPath(this string dotSeparatedPath)
        {
            if (string.IsNullOrEmpty(dotSeparatedPath)) return false;
            var parts = dotSeparatedPath.Split('.');
            foreach (var part in parts)
                if (part.Length == 0 || !part.IsValidMemberName()) return false;

            return true;
        }

        /// <summary>
        /// Appends a unique numeric suffix to the original name if it conflicts with existing names.
        /// Ensures that the resulting name is unique within the provided set of names and optionally adds it to the set.
        /// </summary>
        /// <param name="originalName">The base name to modify if it already exists in the collection.</param>
        /// <param name="existingNames">A reference to a set containing names to check for uniqueness.</param>
        /// <param name="addToHashset">A boolean flag indicating whether the generated unique name should be added to the set.</param>
        /// <returns>A unique name. If no conflict exists, returns the original name; otherwise, returns the modified unique name.</returns>
        public static string AppendDigitForUniqueName(this string originalName, ref HashSet<string> existingNames,
            bool withUnderscore = false, bool addToHashset = true)
        {
            if (!addToHashset)
                return GenerateUniqueName(originalName, existingNames, withUnderscore);

            string uniqueName = GenerateUniqueName(originalName, existingNames, withUnderscore);
            existingNames.Add(uniqueName);
            return uniqueName;
        }
        
        
        private static string GenerateUniqueName(string originalName, HashSet<string> existingNames, bool withUnderscore)
        {
            int counter = 1;
            string formattedName = originalName;

            while (existingNames.Contains(formattedName))
            {
                formattedName = (withUnderscore) ? $"{originalName}_{counter++}" : $"{originalName}{counter++}";
            }

            return formattedName;
        }
    }
}