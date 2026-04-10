using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PsigenVision.Utilities.Combinatorics
{
	public class Combinator
	{
		/// <summary>
		/// Prepares a master array containing unique elements from the provided collections.
		/// CAUTION: Will be created assuming combinations of elements within the same collection is expected.
		/// </summary>
		/// <typeparam name="T">The type of elements in the collections.</typeparam>
		/// <param name="list1">The first list to process for unique elements.</param>
		/// <param name="list2">The second list to process for unique elements.</param>
		/// <param name="masterArray">The resulting array containing unique elements from both lists.</param>
		public static void PrepareForUniqueComparison<T>(List<T> list1, List<T> list2, out T[] masterArray)
		{
			// 1. Create a HashSet with the total possible capacity to avoid resizing
			var uniqueSet = new HashSet<T>(list1.Count + list2.Count);

			// 2. Add both lists. Items appearing in both will naturally only exist once.
			uniqueSet.UnionWith(list1);
			uniqueSet.UnionWith(list2);

			// 3. Bake into a fixed-size array for raw memory access
			masterArray = uniqueSet.ToArray();
		}


		/// <summary>
		/// Prepares separate arrays representing unique elements found exclusively in each list
		/// and elements common to both lists.
		/// </summary>
		/// <typeparam name="T">The type of elements in the lists.</typeparam>
		/// <param name="listA">The first list to process.</param>
		/// <param name="listB">The second list to process.</param>
		/// <param name="onlyA">The resulting array containing elements unique to listA.</param>
		/// <param name="onlyB">The resulting array containing elements unique to listB.</param>
		/// <param name="both">The resulting array containing elements common to both lists.</param>
		public static void PrepareForUniqueComparisonBetween<T>(List<T> listA, List<T> listB,
			out T[] onlyA, out T[] onlyB, out T[] both)
		{
			var set1 = new HashSet<T>(listA);
			var set2 = new HashSet<T>(listB);

			// Filter items into high-performance arrays
			onlyA = listA.Where(x => !set2.Contains(x)).ToArray();
			onlyB = listB.Where(x => !set1.Contains(x)).ToArray();
			both = listA.Where(x => set2.Contains(x)).ToArray();
		}

		/// <summary>
		/// Prepares separate arrays representing unique elements found exclusively in each list
		/// and elements common to both lists.
		/// </summary>
		/// <typeparam name="T">The type of elements in the lists.</typeparam>
		/// <param name="listA">The first list to process.</param>
		/// <param name="listB">The second list to process.</param>
		/// <param name="callback">Allows user to work with the results of the coroutine</param>
		/// /// <param name="onlyA">The resulting array containing elements unique to listA.</param>
		/// <param name="onlyB">The resulting array containing elements unique to listB.</param>
		/// <param name="both">The resulting array containing elements common to both lists.</param>
		public static IEnumerator PrepareForUniqueComparisonBetweenCoroutine<T>(List<T> listA, List<T> listB, Action<(T[] onlyA, T[] onlyB, T[] both)> callback)
		{
			var set1 = new HashSet<T>(listA);
			yield return null;
			var set2 = new HashSet<T>(listB);
			yield return null;

			// Filter items into high-performance arrays
			var onlyA = listA.Where(x => !set2.Contains(x)).ToArray();
			yield return null;
			var onlyB = listB.Where(x => !set1.Contains(x)).ToArray();
			yield return null;
			var both = listA.Where(x => set2.Contains(x)).ToArray();
			yield return null;
			callback((onlyA, onlyB, both));
		}
		
		/// <summary>
		/// Prepares separate arrays representing unique elements found exclusively in each list
		/// and elements common to both lists.
		/// </summary>
		/// <typeparam name="T">The type of elements in the lists.</typeparam>
		/// <param name="arrayA">The first array to process.</param>
		/// <param name="arrayB">The second array to process.</param>
		/// <param name="onlyA">The resulting array containing elements unique to listA.</param>
		/// <param name="onlyB">The resulting array containing elements unique to listB.</param>
		/// <param name="both">The resulting array containing elements common to both lists.</param>
		public static void PrepareForUniqueComparisonBetween<T>(T[] arrayA, T[] arrayB,
			out T[] onlyA, out T[] onlyB, out T[] both)
		{
			var set1 = new HashSet<T>(arrayA);
			var set2 = new HashSet<T>(arrayB);

			// Filter items into high-performance arrays
			onlyA = arrayA.Where(x => !set2.Contains(x)).ToArray();
			onlyB = arrayB.Where(x => !set1.Contains(x)).ToArray();
			both = arrayA.Where(x => set2.Contains(x)).ToArray();
		}
		
		/// <summary>
		/// Prepares a master array containing unique elements from the provided arrays.
		/// CAUTION: Will be created assuming combinations of elements within the same collection is expected.
		/// </summary>
		/// <typeparam name="T">The type of elements in the arrays.</typeparam>
		/// <param name="array1">The first array to process for unique elements.</param>
		/// <param name="array2">The second array to process for unique elements.</param>
		/// <param name="masterArray">The resulting array containing unique elements from both arrays.</param>
		public static void PrepareForUniqueComparison<T>(T[] array1, T[] array2, out T[] masterArray)
		{
			// 1. Create a HashSet with the total possible capacity to avoid resizing
			var uniqueSet = new HashSet<T>(array1.Length + array2.Length);

			// 2. Add both lists. Items appearing in both will naturally only exist once.
			uniqueSet.UnionWith(array1);
			uniqueSet.UnionWith(array2);

			// 3. Bake into a fixed-size array for raw memory access
			masterArray = uniqueSet.ToArray();
		}

		/// <summary>
		/// Prepares a master array containing unique elements from the provided collections.
		/// CAUTION: Will be created assuming combinations of elements within the same collection is expected.
		/// </summary>
		/// <typeparam name="T">The type of elements in the collections.</typeparam>
		/// <param name="listA">The first list to process.</param>
		/// <param name="listB">The second list to process.</param>
		/// <param name="arrayA">The resulting array containing elements from the first list.</param>
		/// <param name="arrayB">The resulting array containing elements from the second list.</param>
		/// <param name="ensureUniqueness">Specifies whether the resulting arrays should only contain unique elements.</param>
		public static void PrepareForUniqueComparison<T, U>(List<T> listA, List<U> listB, out T[] arrayA,
			out U[] arrayB, bool ensureUniqueness = false)
		{
			if (ensureUniqueness)
			{
				arrayA = listA.ToHashSet().ToArray();
				arrayB = listB.ToHashSet().ToArray();
				return;
			}

			// Convert to arrays once to get contiguous memory and avoid List overhead
			arrayA = listA.ToArray();
			arrayB = listB.ToArray();
		}

		/// <summary>
		/// Removes duplicate elements from the provided array, ensuring elements are unique.
		/// </summary>
		/// <typeparam name="T">The type of elements in the array.</typeparam>
		/// <param name="array">The array to process and modify to ensure uniqueness.</param>
		public static void MakeUnique<T>(ref T[] array) => array = array.ToHashSet().ToArray();

		/// <summary>
		/// Removes duplicate elements from the provided list, ensuring all elements are unique.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="list">The reference to the list to process. The list will be updated to contain only unique elements.</param>
		public static void MakeUnique<T>(ref List<T> list) => list = list.ToHashSet().ToList();

		/// <summary>
		/// Processes unique pairs of elements within a preprocessed array using the given pair processor, while adhering to a specified time budget.
		/// CAUTION: MasterUniqueArray will have been created assuming combinations of elements within the same collection is expected.
		/// </summary>
		/// <typeparam name="T">The type of elements in the unique array.</typeparam>
		/// <param name="masterUniqueArray">An array containing preprocessed unique elements.</param>
		/// <param name="pairProcessor">A delegate to process each pair of unique elements.</param>
		/// <param name="msBudget">The maximum time budget, in milliseconds, allowed for processing in each frame.</param>
		/// <returns>An enumerator that supports yielding between processing iterations.</returns>
		public static IEnumerator ProcessUniquePairsRoutine<T>(T[] masterUniqueArray, Action<T, T> pairProcessor,
			float msBudget = 2.0f)
		{
			//This method expects the passed in array to have been preprocessed such that it only contain unique elements
			/*
			 * Notice j is carefully managed in the loop. By setting j = i + 1 only when starting a new i, you maintain the "Triangle" logic across multiple frames. 
			 */
			var i = 0;
			var j = 1;
			
			var sw = System.Diagnostics.Stopwatch.StartNew();

			while (i < masterUniqueArray.Length)
			{
				// RECREATE Span locally at the start of each frame
				// This is extremely cheap and solves the "yield" error
				Span<T> span = masterUniqueArray.AsSpan();

				for (; i < span.Length; i++)
				{
					// Ensure j always starts at least at i + 1 for unique pairs
					if (j <= i) j = i + 1;

					for (; j < span.Length; j++)
					{
						pairProcessor.Invoke(span[i], span[j]);

						// Time-Slicing Check
						if (sw.Elapsed.TotalMilliseconds > msBudget)
						{
							yield return null; // Pauses here; span is "dropped" and recreated next frame
							sw.Restart();
							span = masterUniqueArray.AsSpan(); // Re-fetch the span after resuming
						}
					}
					j = 0; // Reset j for the next i-iteration
				}
			}
		}

		/// <summary>
		/// Iterates through unique pairs formed from the elements of the given array and processes them using the given pair processor, while adhering to a specified time budget.
		/// CAUTION: Will also compare unique combinations of elements within the same collection.
		/// </summary>
		/// <typeparam name="T">The type of elements in the array.</typeparam>
		/// <param name="masterUniqueArray">An array containing preprocessed unique elements to form pairs.</param>
		/// <param name="pairProcessor">The action to perform on each unique pair of elements.</param>
		/// <param name="msBudget">The maximum permissible time, in milliseconds, to execute per frame before yielding. Defaults to 2.0.</param>
		/// <returns>An enumerator that performs pair processing over multiple frames if necessary for time-slicing.</returns>
		public static IEnumerator ProcessUniquePairsRoutine<T, U>(T[] preparedArrayA, U[] preparedArrayB,
			Action<T, U> pairProcessor, float msBudget = 2.0f)
		{
			//This method expects the passed in arrays to have been preprocessed such that they only contain unique elements (individually, not when compared with each other)
			int _currentIndexA = 0;
			int _currentIndexB = 0;
			// Access memory directly for maximum performance
			Span<T> spanA = preparedArrayA.AsSpan();
			Span<U> spanB = preparedArrayB.AsSpan();
			var sw = System.Diagnostics.Stopwatch.StartNew();

			for (; _currentIndexA < spanA.Length; _currentIndexA++)
			{
				// Resume inner loop where we left off last frame
				for (; _currentIndexB < spanB.Length; _currentIndexB++)
				{
					pairProcessor.Invoke(spanA[_currentIndexA], spanB[_currentIndexB]);
					// Time-Slicing Check
					if (sw.Elapsed.TotalMilliseconds > msBudget)
					{
						yield return null; // Pauses here; span is "dropped" and recreated next frame
						sw.Restart();
						spanA = preparedArrayA.AsSpan();
						spanB = preparedArrayB.AsSpan(); // Re-fetch the span after resuming
					}
				}
				_currentIndexB = 0; // Reset inner index for the next outer item
			}
		}
		
		public static IEnumerator ProcessAllUniquePairsBetween<T>(T[] preparedArrayA, T[] preparedArrayB, T[] preparedArrayBoth,
			Action<T, T> pairProcessor, float msBudget = 2.0f)
		{
			var sw = System.Diagnostics.Stopwatch.StartNew();

			// 1. Only in A paired with Only in B
			yield return ProcessBipartiteSlice(preparedArrayA, preparedArrayB, pairProcessor, sw, msBudget);
    
			// 2. Only in A paired with items in Both
			yield return ProcessBipartiteSlice(preparedArrayA, preparedArrayBoth, pairProcessor, sw, msBudget);
    
			// 3. Only in B paired with items in Both
			yield return ProcessBipartiteSlice(preparedArrayB, preparedArrayBoth, pairProcessor, sw, msBudget);
    
			// Note: We skip Both-with-Both because that would be internal pairing!
		}

		public static IEnumerator ProcessBipartiteSlice<T>(T[] arr1, T[] arr2, Action<T, T> pairProcessor, System.Diagnostics.Stopwatch sw, float budget)
		{
			if (arr1.Length == 0 || arr2.Length == 0) yield break;

			for (int i = 0; i < arr1.Length; i++)
			{
				// Re-acquire span every frame to satisfy safety and speed
				ReadOnlySpan<T> span1 = arr1.AsSpan();
				ReadOnlySpan<T> span2 = arr2.AsSpan();

				for (int j = 0; j < span2.Length; j++)
				{
					pairProcessor(span1[i], span2[j]);

					if (sw.Elapsed.TotalMilliseconds > budget)
					{
						yield return null;
						sw.Restart();
						// Refresh spans after returning from yield
						span1 = arr1.AsSpan();
						span2 = arr2.AsSpan();
					}
				}
			}
		}

		
		/*public IEnumerator ProcessInSlices<T>(T[] masterArray, Action<T,T> pairProcessor, float msBudget = 2.0f)
		{
			var span = masterArray.AsSpan();
			var sw = System.Diagnostics.Stopwatch.StartNew();

			for (int i = 0; i < span.Length; i++)
			{
				for (int j = i + 1; j < span.Length; j++)
				{
					pairProcessor(span[i], span[j]);

					// If we've worked for more than our 2ms budget, 
					// pause and finish the rest in the next frame.
					if (sw.Elapsed.TotalMilliseconds > msBudget)
					{
						yield return null; 
						sw.Restart();
					}
				}
			}
		}*/
	}
}