# Combinator Utilities

The **Combinator Utilities** package provides a set of tools to perform advanced operations on collections, focusing on comparison, uniqueness checks, and pair processing. These utilities are highly performant and designed to handle a variety of use cases while being extendable for future additions.

---

## Overview

### Purpose
The **Combinator Utilities** module simplifies and accelerates operations involving comparisons and combinations of lists or arrays. It includes robust solutions for:
- Preparing collections for unique comparisons.
- Processing all unique pairs within or across lists/arrays.
- Efficient computation using coroutines with time-budget constraints.

### Key Features
- **Unique Comparison**: Prepare collections for unique comparisons with multiple overloads for flexibility.
- **Coroutine-Friendly**: Use coroutines for processing large datasets with a time budget to avoid frame stalling.
- **Pair Processing**: Handle combinations and pairs within or across collections for custom logic.

---

## Classes

### 1. `Combinator`

The `Combinator` class provides a wide variety of comparison and pair-processing methods for arrays and lists.

---

### `Combinator`

#### Overview

The `Combinator` class is the backbone of the **Combinator Utilities** package. It includes methods for:
- Preparing collections for unique comparisons and processing.
- Finding unique subsets across two lists/arrays.
- Processing pairs within collections or between collections using coroutines.

Its methods handle both synchronous and coroutine-based workflows, allowing efficient computation in Unity runtime environments.

---

#### Methods

##### 1. `PrepareForUniqueComparison<T>`

##### Overloads:
```c#
void PrepareForUniqueComparison<T>(List<T> list1, List<T> list2, out T[] masterArray)
void PrepareForUniqueComparison<T>(T[] array1, T[] array2, out T[] masterArray)
void PrepareForUniqueComparison<T, U>(List<T> listA, List<U> listB, out T[] arrayA, out U[] arrayB, bool ensureUniqueness = false)
```

- **Description**:
  Combines elements from two collections (arrays or lists) into a unique comparison array or prepares two separate arrays or lists for comparison.

- **Parameters**:
	- `List<T> list1` or `List<T> listA`: The first list.
	- `List<T> list2` or `List<U> listB`: The second list.
	- `T[] array1` or `T[] arrayA`: The first array.
	- `T[] array2` or `T[] arrayB`: The second array.
	- `out T[] masterArray`: The combined unique array.
	- `out T[] arrayA, T[] arrayB`: Two arrays prepared for comparison (in the `T, U` overload).
	- `bool ensureUniqueness`: If `true`, removes duplicate elements in both arrays (default is `false`).

- **Usage**:
  ```c#
  List<int> list1 = new List<int> { 1, 2, 3 };
  List<int> list2 = new List<int> { 3, 4, 5 };

  Combinator.PrepareForUniqueComparison(list1, list2, out int[] masterArray);

  foreach (var item in masterArray)
  {
      Debug.Log(item); // Outputs: 1, 2, 3, 4, 5
  }
  ```

---

##### 2. `PrepareForUniqueComparisonBetween<T>`

##### Overloads:
```c#
void PrepareForUniqueComparisonBetween<T>(List<T> listA, List<T> listB,
    out T[] onlyA, out T[] onlyB, out T[] both)
void PrepareForUniqueComparisonBetween<T>(T[] arrayA, T[] arrayB,
    out T[] onlyA, out T[] onlyB, out T[] both)
IEnumerator PrepareForUniqueComparisonBetweenCoroutine<T>(List<T> listA, List<T> listB, 
    Action<(T[] onlyA, T[] onlyB, T[] both)> callback)
```

- **Description**:
  Prepares three subsets of elements across two collections: elements unique to the first collection (`onlyA`), elements unique to the second collection (`onlyB`), and elements common to both (`both`). For large datasets, a coroutine version is available.

- **Parameters**:
	- `List<T> listA` or `T[] arrayA`: The first collection.
	- `List<T> listB` or `T[] arrayB`: The second collection.
	- `out T[] onlyA`: Array of elements unique to the first collection.
	- `out T[] onlyB`: Array of elements unique to the second collection.
	- `out T[] both`: Array of elements shared between both collections.
	- `callback`: A callback providing `(onlyA, onlyB, both)` for coroutine-based usage.

- **Usage**:
  Synchronous:
  ```c#
  List<string> listA = new List<string> { "apple", "orange", "banana" };
  List<string> listB = new List<string> { "banana", "kiwi", "apple" };

  Combinator.PrepareForUniqueComparisonBetween(listA, listB, out string[] onlyA, out string[] onlyB, out string[] both);

  // Outputs:
  Debug.Log(string.Join(", ", onlyA)); // orange
  Debug.Log(string.Join(", ", onlyB)); // kiwi
  Debug.Log(string.Join(", ", both));  // apple, banana
  ```

  Coroutine:
  ```c#
  IEnumerator ExampleCoroutine()
  {
      List<int> listA = new List<int> { 1, 2, 3 };
      List<int> listB = new List<int> { 2, 3, 4 };

      yield return Combinator.PrepareForUniqueComparisonBetweenCoroutine(listA, listB, result =>
      {
          Debug.Log(string.Join(", ", result.onlyA)); // 1
          Debug.Log(string.Join(", ", result.onlyB)); // 4
          Debug.Log(string.Join(", ", result.both));  // 2, 3
      });
  }
  ```

---

##### 3. `MakeUnique<T>`

##### Overloads:
```c#
void MakeUnique<T>(ref T[] array)
void MakeUnique<T>(ref List<T> list)
```

- **Description**:
  Removes duplicate elements from a collection (array or list).

- **Parameters**:
	- `ref T[] array`: The array to make unique.
	- `ref List<T> list`: The list to make unique.

- **Usage**:
  ```c#
  int[] array = new int[] { 1, 1, 2, 3, 3 };
  Combinator.MakeUnique(ref array);

  foreach (var item in array)
  {
      Debug.Log(item); // Outputs: 1, 2, 3
  }
  ```

---

##### More Methods Overview
Other methods like `ProcessUniquePairsRoutine` and `ProcessBipartiteSlice` perform advanced operations on pairs within or between collections. The highlights include:
- **Processing with Time Budgets**: Use coroutines to batch process pairs without exceeding a time limit (e.g., `msBudget`).

---

## Best Practices

- **Choose the Right Overload**:
  Use the most appropriate overload based on the data type (e.g., `List<T>` vs. `T[]`).

- **Leverage Coroutines for Large Datasets**:
  For large data sets, favor coroutine variants of the methods (`PrepareForUniqueComparisonBetweenCoroutine`) to avoid freezing frames while processing.

- **Optimize Pair Processing**:
  When processing all pairs in a collection, consider `ProcessUniquePairsRoutine` for better performance and frame responsiveness.

---

## Notes for Contributors
Any future expansions to the class should include:
- Detailed breakdowns of method overloads.
- Usage examples for both synchronous and coroutine-based workflows.
- Clear explanations of performance considerations.

---

## Namespace
All related utilities are available under:
```c#
PsigenVision.Utilities.Combinatorics
```