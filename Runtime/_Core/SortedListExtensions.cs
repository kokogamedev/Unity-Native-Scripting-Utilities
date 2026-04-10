using System.Collections.Generic;

namespace PsigenVision.Utilities.Collection
{
    public static class SortedListExtensions 
    {
        #region Remove Methods

        /// <summary>
        /// Remove a key-value pair in the sorted list of lists.
        /// This method assumes that the key exists in the sorted list and the value is not null. 
        /// This method does not delete the key in the sorted list if the value is an empty list. 
        /// This method assumes the pair has been verified to exist prior to being called.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        public static void Remove<TKey, TValue>(this SortedList<TKey, List<TValue>> list, TKey key, TValue value) =>  list[key].Remove(value);

        /// <summary>
        /// Remove a key-value pair in the sorted list of lists.
        /// This method assumes that the key exists in the sorted list and the value is not null.
        /// This method deletes the key in the sorted list if the value is an empty list. 
        /// This method assumes the pair has been verified to exist prior to being called.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        public static void ThoroughRemove<TKey, TValue>(this SortedList<TKey, List<TValue>> list, TKey key,
            TValue value)
        {
            var values = list[key];
            values.Remove(value);
            if (values.Count == 0) list.Remove(key);
        }
        
        /// <summary>
        /// Try and remove a key-value pair in a sorted list of lists.
        /// This method DOES assume the lists associated with the keys will never be null.
        /// This method DOES NOT ensure a value's list is removed if it is empty.
        /// This method will return a boolean based on its success in this operation. 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static bool TryRemove<TKey, TValue>(this SortedList<TKey, List<TValue>> list, TKey key, TValue value)
        {
            if (list.TryGetValue(key, out var values))
                return values.Remove(value);
            return false;
        }
        
        /// <summary>
        /// Try and remove a key-value pair in a sorted list of lists.
        /// This method DOES assume the lists associated with the keys will never be null.
        /// This method DOES ensure that a value's list is not left in the collection if it is empty. 
        /// This method will return a boolean based on its success in this operation. 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static bool TryThoroughRemove<TKey, TValue>(this SortedList<TKey, List<TValue>> list, TKey key, TValue value)
        {
            if (list.TryGetValue(key, out var values))
            {
                bool removed = values.Remove(value);
                //If the list is empty after removal, remove the key as well
                if (values.Count == 0)
                {
                    list.Remove(key);
                }

                return removed;
            }

            return false;
        }
        /// <summary>
        /// Try and remove a key-value pair in a sorted list of lists.
        /// This method DOES NOT assume the lists associated with the keys will never be null.
        /// This method DOES NOT ensure that a value's list is not left in the collection if it is empty. 
        /// This method will return a boolean based on its success in this operation. 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static bool TrySafeRemove<TKey, TValue>(this SortedList<TKey, List<TValue>> list, TKey key, TValue value)
        {
            if (list.TryGetValue(key, out var values))
            {
                if (values != null)
                    return values.Remove(value);
            }
            return false;
        }
        
        /// <summary>
        /// Try and remove a key-value pair in a sorted list of lists.
        /// This method DOES NOT assume the lists associated with the keys will never be null. 
        /// This method DOES ensure that a value's list is not left in the collection if it is empty. 
        /// This method will return a boolean based on its success in this operation. 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static bool TryThoroughSafeRemove<TKey, TValue>(this SortedList<TKey, List<TValue>> list, TKey key, TValue value)
        {
            if (list.TryGetValue(key, out var values))
            { 
                if (values != null)
                {
                    bool removed = values.Remove(value);
                    //If the list is empty after removal, remove the key as well (thorough)
                    if (values.Count == 0)
                    {
                        list.Remove(key);
                    }

                    return removed;
                }
                else
                {
                    list.Remove(key); //Remove the null list from the sorted list by removing its key entirely (thorough)
                }
            }
            return false;
        }

        #endregion

        #region Add methods
        /// <summary>
        /// Adds a new key-value pair or appends a values to an existing key's list. 
        /// This extension does assume that the existing key's list is not null. 
        /// </summary>
        /// <param name="sortedList"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        public static void AddOrAppend<TKey, TValue>(this SortedList<TKey, List<TValue>> sortedList, TKey key, TValue value)
        {
            if (sortedList.TryGetValue(key, out var values))
            {
                values.Add(value);
            }
            else
            {
                sortedList.Add(key, new List<TValue>(){value});
            }
        }
        /// <summary>
        /// Adds a new key-value pair or appends a values to an existing key's list. 
        /// This extension does NOT assume that the existing key's list is not null. 
        /// </summary>
        /// <param name="sortedList"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        public static void SafeAddOrAppend<TKey, TValue>(this SortedList<TKey, List<TValue>> sortedList, TKey key, TValue value)
        {
            if (sortedList.TryGetValue(key, out var values))
            {
                if (values != null)
                {
                    values.Add(value);
                }
                else
                {
                    sortedList[key] = new List<TValue>() { value };
                }
            }
            else
            {
                sortedList.Add(key, new List<TValue>() {value});
            }
        }

        /// <summary>
        /// Adds a new key-value pair or appends a values to an existing key's list, AS LONG AS it does not already exist in that list. 
        /// This extension assumes that the existing key's list is not null.
        /// If you wish to replace a value in the key's list, you can indicate this by passing in true to the replace parameter.
        /// </summary>
        /// <param name="sortedList"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="replace"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        public static void AddOrAppendUnique<TKey, TValue>(this SortedList<TKey, List<TValue>> sortedList, TKey key,
            TValue value, bool replace = false)
        {
            if (sortedList.TryGetValue(key, out List<TValue> values))
            {
                int index = values.IndexOf(value);
                if (index >= 0) //A value was found in the list
                {
                    if (replace) 
                    {
                        values[index] = value; //Replace the existing value if found
                    }
                    // If not replacing (replace = false), do nothing and exit
                }
                else
                {
                    values.Add(value);
                }
            }
            else
            {
                sortedList.Add(key, new List<TValue>() {value});
            }
        }

        /// <summary>
        /// Adds a new key-value pair or appends a values to an existing key's list, AS LONG AS it does not already exist in that list. 
        /// This extension does NOT assume that the existing key's list is not null.
        /// If you wish to replace a value in the key's list, you can indicate this by passing in true to the replace parameter. 
        /// </summary>
        /// <param name="sortedList"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="replace"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        public static void SafeAddOrAppendUnique<TKey, TValue>(this SortedList<TKey, List<TValue>> sortedList, TKey key,
            TValue value, bool replace = false)
        {
            if (sortedList.TryGetValue(key, out List<TValue> values))
            {
                if (values == null)
                {
                    sortedList[key] = values = new List<TValue>();
                }

                int index = values.IndexOf(value);
                if (index >= 0) //A value was found in the list
                {
                    if (replace) 
                    {
                        values[index] = value; //Replace the existing value if found
                    }
                    // If not replacing (replace = false), do nothing and exit
                }
                else
                {
                    values.Add(value);
                }
            }
            else
            {
                sortedList.Add(key, new List<TValue>() {value});
            }
        }

        #endregion
        
        #region Contains methods

        /// <summary>
        /// Checks if a specific key-value pair exists where the value is contained within the list associated with the key.
        /// This extension assumes that the list associated with the key is NOT null.
        /// </summary>
        /// <param name="sortedList"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static bool Contains<TKey, TValue>(this SortedList<TKey, List<TValue>> sortedList, TKey key, TValue value)
        {
            if (sortedList.TryGetValue(key, out var values))
            {
                return values.Contains(value);
            }
            return false;
        }

        /// <summary>
        /// Checks if a specific key-value pair exists where the value is contained within the list associated with the key.
        /// This extension does NOT assume that the list associated with the key is NOT null.
        /// </summary>
        /// <param name="sortedList"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static bool SafeContains<TKey, TValue>(this SortedList<TKey, List<TValue>> sortedList, TKey key,
            TValue value)
        {
            if (sortedList.TryGetValue(key, out var values) && values != null)
                return values.Contains(value);
            return false;
        }
        
        #endregion

        #region Formatting Lists

        public static void Format<TKey, TValue>(this SortedList<TKey, List<TValue>> sortedList, bool removeNullLists, bool removeEmptyLists)
        {
            var keysToRemove = new List<TKey>();
            foreach (var keyValuePair in sortedList)
            {
                if (keyValuePair.Value != null)
                {
                    //Remove the list if it is empty if the user has requested to remove empty lists
                    if (removeEmptyLists && keyValuePair.Value.Count == 0) keysToRemove.Add(keyValuePair.Key);
                }
                else
                {
                    if (removeNullLists) keysToRemove.Add(keyValuePair.Key);
                }
            }
            for (int i = 0; i < keysToRemove.Count; i++)
            {
                //Remove all keys that were slated to be removed
                sortedList.Remove(keysToRemove[i]);
            }
        }

        #endregion
    }
}
