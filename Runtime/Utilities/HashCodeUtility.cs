using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BeardPhantom.UCL.Utility
{
    /// <summary>
    /// Utility functions for hashcode operations.
    /// </summary>
    public static class HashCodeUtility
    {
        #region Methods

        /// <summary>
        /// Combines two hashcodes to produce a third.
        /// </summary>
        public static int CombineHashes(int hashA, int hashB)
        {
            unchecked
            {
                return ((hashA << 5) + hashA) ^ hashB;
            }
        }

        /// <summary>
        /// Combines a given hashcode with an object's hashcode to produce a third.
        /// </summary>
        public static int CombineWithObjectHashCode<T>(this int hash, T obj) where T : class
        {
            return CombineHashes(hash, obj.GetHashCodeSafe());
        }

        /// <summary>
        /// Combines a given hashcode with an object's hashcode to produce a third.
        /// </summary>
        public static int CombineWithStructHashCode<T>(this int hash, T obj) where T : struct
        {
            return CombineHashes(hash, obj.GetHashCode());
        }

        /// <summary>
        /// Combines a given hashcode with a collection of objects' hashcode to produce a new hashcode.
        /// </summary>
        public static int CombineWithObjectHashCodes<T>(this int hash, IEnumerable<T> collection) where T : class
        {
            foreach (var obj in collection)
            {
                hash = CombineHashes(hash, obj.GetHashCodeSafe());
            }

            return hash;
        }

        /// <summary>
        /// Combines a given hashcode with a collection of objects' hashcode to produce a new hashcode.
        /// </summary>
        public static int CombineWithStructHashCodes<T>(this int hash, IEnumerable<T> collection) where T : struct
        {
            foreach (var obj in collection)
            {
                hash = CombineHashes(hash, obj.GetHashCode());
            }

            return hash;
        }

        /// <summary>
        /// Safely returns a hashcode for a reference type object.
        /// </summary>
        [SuppressMessage("ReSharper", "MergeConditionalExpression")]
        public static int GetHashCodeSafe<T>(this T obj) where T : class
        {
            return obj == null
                ? 0
                : obj.GetHashCode();
        }

        #endregion
    }
}