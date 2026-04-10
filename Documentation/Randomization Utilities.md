# Randomization Utilities

The **Randomization Utilities** module provides a variety of extension methods to introduce randomness into different types of data structures, such as `Bounds`, `IEnumerable`, lists, and custom random number logic. These utilities simplify generating random values while improving code readability and reusability throughout the project.

---

## 1. `PsigenVision.Utilities.Randomization` Namespace

This namespace provides randomization-related extension methods for Unity's `Bounds`, generic collections like `IEnumerable`, and lists, as well as additional randomness helpers for the `Random` class.

---

### Classes:
- **`BoundsRandomExtensions`**
- **`EnumerableRandomExtensions`**
- **`ListRandomExtensions`**
- **`RandomExtensions`**

---

### `BoundsRandomExtensions`

#### Overview
The **`BoundsRandomExtensions`** class allows random sampling of points within a Unity `Bounds` object.

#### Methods

##### 1. `GetRandomPointWithin`
```c#
public static Vector3 GetRandomPointWithin(this Bounds input)
```

- **Description**:
  Retrieves a random point within the given `Bounds`. The point is calculated by randomly sampling within each axis's extents (x, y, z).

- **Parameters**:
    - `Bounds input`: The input bounds to sample from.

- **Returns**:
    - `Vector3`: A random point within the given `Bounds`.

- **Usage**:
  ```c#
  Bounds myBounds = new Bounds(Vector3.zero, new Vector3(10, 10, 10));
  Vector3 randomPoint = myBounds.GetRandomPointWithin();
  Debug.Log($"Random Point: {randomPoint}");
  ```

---

### `EnumerableRandomExtensions`

#### Overview
The **`EnumerableRandomExtensions`** class provides functionality to retrieve random elements from an `IEnumerable` collection.

#### Methods

##### 1. `Random<T>`
```c#
public static T Random<T>(this IEnumerable<T> input)
```

- **Description**:
  Retrieves a random element from an `IEnumerable`. Internally, it uses a helper class with a pseudo-random generator.

- **Parameters**:
    - `IEnumerable<T> input`: The collection to retrieve a random element from.

- **Returns**:
    - `T`: A randomly selected element from the collection.

- **Usage**:
  ```c#
  var colors = new List<string> { "Red", "Green", "Blue" };
  string randomColor = colors.Random();
  Debug.Log($"Random Color: {randomColor}");
  ```

---

### `ListRandomExtensions`

#### Overview
The **`ListRandomExtensions`** class introduces methods to randomize the order of elements in a list.

#### Methods

##### 1. `DurstenShuffle<T>`
```c#
public static IList<T> DurstenShuffle<T>(this IList<T> list)
```

- **Description**:
  Shuffles the elements of a list in random order using the **Durstenfeld implementation** of the Fisher-Yates shuffle algorithm. This method modifies the input list in-place and returns it for fluency.

- **Reference**: [Fisher-Yates Shuffle](https://en.wikipedia.org/wiki/Fisher-Yates_shuffle)

- **Parameters**:
    - `IList<T> list`: The list to shuffle.

- **Returns**:
    - `IList<T>`: The input list after being shuffled.

- **Usage**:
  ```c#
  var numbers = new List<int> { 1, 2, 3, 4, 5 };
  numbers.DurstenShuffle();
  Debug.Log(string.Join(", ", numbers));
  ```

---

### `RandomExtensions`

#### Overview
The **`RandomExtensions`** class extends `System.Random` with additional methods for generating random numbers in various ranges and data types.

#### Methods

##### 1. `NextLong`
```c#
public static long NextLong(this Random random, long min = 0, long max = long.MaxValue)
```

- **Description**:
  Generates a random 64-bit integer (`long`) value between a specified minimum and maximum range. Supports values greater than `long.MaxValue` by using `ulong` internally to avoid overflow issues.

- **Parameters**:
    - `Random random`: The instance of `System.Random`.
    - `long min`: The minimum value (inclusive).
    - `long max`: The maximum value (exclusive).

- **Returns**:
    - `long`: A random long value within the specified range.

- **Usage**:
  ```c#
  Random rng = new Random();
  long randomLong = rng.NextLong(100, 1000);
  Debug.Log($"Random Long: {randomLong}");
  ```

---

##### 2. `NextDouble`
```c#
public static double NextDouble(this Random random, double lowerBound, double upperBound)
```

- **Description**:
  Generates a pseudo-random `double` value between the `lowerBound` and `upperBound`.

- **Parameters**:
    - `Random random`: The instance of `System.Random`.
    - `double lowerBound`: The lower bound (inclusive).
    - `double upperBound`: The upper bound (exclusive).

- **Returns**:
    - `double`: A random double value within the specified range.

- **Usage**:
  ```c#
  Random rng = new Random();
  double randomDouble = rng.NextDouble(1.5, 3.5);
  Debug.Log($"Random Double: {randomDouble}");
  ```

---

## Final Notes

The **Randomization Utilities** greatly simplify common randomization tasks across different data types and structures in Unity and C#. Here's a quick breakdown of when to use each class:

1. **`BoundsRandomExtensions`**: For random point sampling within Unity's `Bounds`.
2. **`EnumerableRandomExtensions`**: To pick random elements from any `IEnumerable` collection.
3. **`ListRandomExtensions`**: Use for shuffling lists in an unbiased manner.
4. **`RandomExtensions`**: Extend `System.Random` for generating random `long` or `double` in specific ranges.

This set of utilities can easily support game-related randomization tasks or general randomness requirements in the project. 