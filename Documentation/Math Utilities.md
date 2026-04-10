# Math Utilities

The **Math Utilities** package provides a collection of helper tools and methods to simplify mathematical operations commonly used in Unity projects. This module focuses on enhancing workflow efficiency when working with angles, animation curves, and related mathematical constructs.

---

## Overview

The **Math Utilities** module currently contains the following classes:
- **AngleHelper**: A static utility class for working with angles and Euler rotations.
- **AnimationCurveHelper**: A helper class designed to perform transformations and checks on Unity’s `AnimationCurve`.

---

## Classes

### 1. `AngleHelper`

#### Overview
The **`AngleHelper`** class is a collection of static methods designed to simplify common tasks involving angles and rotations, such as normalizing angles and clamping them to a specific range.

#### Key Features
- Normalize angles to the range `-180°` to `180°` for consistent handling.
- Normalize Euler rotations (`Vector3`) for Unity’s `Transform` system.

---

### `AngleHelper`

#### Methods

##### 1. `WrapAngle`
```c#
public static float WrapAngle(float angle)
```

- **Description**:
  Ensures that any given angle in degrees falls within the range `-180°` to `180°` by wrapping it appropriately. Useful for keeping rotations consistent and avoiding issues with large angle values.

- **Parameters**:
	- `float angle`: The input angle in degrees.

- **Returns**:
	- `float`: The normalized angle in the range `-180°` to `180°`.

- **Details**:
	- If the input angle is already within the range `-180°` to `180°`, it is returned as-is.
	- For angles outside this range, the method uses a modular arithmetic technique to normalize the value:
		1. Adjusts the angle into the `[0°, 360°]` range.
		2. Wraps it back into the `[-180°, 180°]` range.

- **Usage**:
  ```c#
  float normalizedAngle = AngleHelper.WrapAngle(270f); // Output: -90
  float normalizedAngle2 = AngleHelper.WrapAngle(-450f); // Output: -90
  ```

- **Best Practices**:
	- Use this method when working with angles in feedback loops or rotational interpolation, where consistency is critical.
	- Common use cases include normalizing player rotation, camera angles, or performing angle comparisons.

---

##### 2. `NormalizeEulerAngles`
```c#
public static Vector3 NormalizeEulerAngles(this Vector3 eulerAngles)
```

- **Description**:
  Normalizes the components of a `Vector3` representing Euler angles, ensuring each angle falls within the range `-180°` to `180°`.

- **Parameters**:
	- `Vector3 eulerAngles`: The input Euler angles to normalize.

- **Returns**:
	- `Vector3`: A new `Vector3` with each angle component normalized to `-180°` to `180°`.

- **Details**:
	- The method internally calls `WrapAngle` for each component of the input `eulerAngles` (x, y, and z).
	- Ideal for cleaning up Euler rotations retrieved from Unity’s `Transform.eulerAngles`.

- **Usage**:
  ```c#
  Vector3 normalizedEulerAngles = new Vector3(720f, -450f, 810f).NormalizeEulerAngles();
  Debug.Log(normalizedEulerAngles); // Output: (0, -90, 90)
  ```

- **Best Practices**:
	- Avoid direct manipulation of Unity’s `transform.eulerAngles` as it is expressed in `[0°, 360°]`. Instead, pass the rotation through this method before applying custom logic.
	- Use normalized Euler angles when performing rotation-based calculations or comparisons to avoid jittering or inconsistencies.

---

### Example Applications

1. **Rotational Feedback**
   Normalize angles to ensure smooth rotational blending between two angles:
   ```c#
   float playerHeading = 450f;
   float smoothedHeading = Mathf.LerpAngle(transform.eulerAngles.y, AngleHelper.WrapAngle(playerHeading), 0.1f);
   transform.rotation = Quaternion.Euler(0, smoothedHeading, 0);
   ```

2. **Consistent Euler Rotations**
   Use `NormalizeEulerAngles` to sanitize a GameObject's rotation:
   ```c#
   Vector3 cleanedRotation = transform.eulerAngles.NormalizeEulerAngles();
   Debug.Log($"Cleaned rotation: {cleanedRotation}");
   ```

---

### Best Practices for `AngleHelper`

1. **Modular Arithmetic Insight**:
   The `WrapAngle` method applies modular arithmetic to handle edge cases where the input angle is far outside the normal bounds (e.g., `10000°`). This is critical in physics simulations and feedback loops.

2. **Avoid Manual Euler Manipulation**:
   Since Unity handles rotations internally as quaternions, avoid manual manipulation of `transform.eulerAngles` unless necessary. Use `NormalizeEulerAngles` for situations where Euler angle calculations are needed.

3. **Performance Considerations**:
	- Both `WrapAngle` and `NormalizeEulerAngles` are lightweight and suitable for use in updates or tight loops.
	- When normalizing bulk rotations (e.g., hundreds of objects), consider caching normalized values where possible.

---


### Expansion Roadmap

Potential improvements and extensions for **`AngleHelper`** include:
- **Angle Comparison Utilities**:
	- Add methods for calculating angular differences or clamped interpolation between two angles.
- **Support for Quaternion Angles**:
	- Introduce methods to directly normalize quaternions or extract consistent Euler angles.


---

### 2. `AnimationCurveHelper`

#### Overview
The `AnimationCurveHelper` class provides static methods for working with Unity’s `AnimationCurve`. It supports tasks such as inverting curves and checking their linearity, which is helpful in smoothing animations, converting easing curves, and performance optimization.

#### Key Features
- Generate approximate inverses for `AnimationCurve` objects.
- Verify if an `AnimationCurve` is approximately linear for performance-sensitive workflows.

#### Disclaimer
Some of the methods in `AnimationCurveHelper` (e.g., `InvertCurve`) require rigorous testing in complex use cases. Ensure to test these methods in your specific scenarios before deploying them in production!

---

### `AnimationCurveHelper`

#### Methods

##### 1. `InvertCurve`
```c#
public static AnimationCurve InvertCurve(AnimationCurve curve, int resolution = 256)
```

- **Description**:
  Generates an approximate inverse of a specified `AnimationCurve`. The resulting curve maps the original curve's output values (`y-values`) back to their corresponding input values (`x-values`). This can be useful for reversing curve-space progress or converting eased values back to linear values.

- **Parameters**:
	- `AnimationCurve curve`: The original curve to invert. Must be monotonic (strictly increasing or decreasing) for meaningful results.
	- `int resolution`: The number of samples used to approximate the inverse mapping. Higher resolution produces smoother results but increases memory and evaluation costs. Default is `256`.

- **Returns**:
	- `AnimationCurve`: A new curve such that `invertedCurve.Evaluate(curve.Evaluate(t)) ≈ t` for inputs `t` in `[0,1]`.

- **Details**:
	- If the curve is `null` or empty, an identity curve is returned: `(0,0) → (1,1)`.
	- The method samples the input curve at `resolution` evenly spaced points in `[0,1]`, swaps their `(x,y)` coordinates, and uses them to construct the inverse curve.
	- Linear tangents are applied to the inverse curve using Unity’s `AnimationUtility`.

- **Usage**:
  ```c#
  AnimationCurve easingCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
  AnimationCurve inverseCurve = AnimationCurveHelper.InvertCurve(easingCurve);

  float easedValue = easingCurve.Evaluate(0.75f);  // Example: eased value
  float linearT = inverseCurve.Evaluate(easedValue); // Approximate linear progress
  Debug.Log($"Eased t: {easedValue}, Linear t: {linearT}");
  ```

- **Best Practices**:
	- Ensure the input curve is monotonic before inverting, as non-monotonic curves will produce ambiguous results.
	- Use a `resolution` that balances performance and accuracy (e.g., `128–512` for most cases).

- **Notes**:
	- Tangent manipulation depends on UnityEditor’s `AnimationUtility` class, which may not work correctly in non-Editor environments (e.g., runtime builds).

---

##### 2. `IsCurveApproximatelyLinear`
```c#
public static bool IsCurveApproximatelyLinear(AnimationCurve curve, float tolerance = 0.01f)
```

- **Description**:
  Checks whether a given `AnimationCurve` is nearly linear between `(0,0)` and `(1,1)`. This is useful for detecting when a curve does not meaningfully alter interpolation, allowing you to bypass it for performance optimizations.

- **Parameters**:
	- `AnimationCurve curve`: The curve to test.
	- `float tolerance`: The maximum deviation allowed from a perfectly linear curve. A smaller value (e.g., `0.001`) enforces stricter checking, while a larger value (e.g., `0.05`) is more forgiving. Default is `0.01`.

- **Returns**:
	- `bool`: Returns `true` if the curve is approximately linear; otherwise, `false`.

- **Details**:
	- The method evaluates the curve at `32` evenly spaced points in `[0,1]`.
	- For each point, it compares the curve's actual value with a straight line's expected value and checks if the deviation exceeds the `tolerance`.

- **Usage**:
  ```c#
  AnimationCurve testCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
  bool isLinear = AnimationCurveHelper.IsCurveApproximatelyLinear(testCurve);

  Debug.Log($"Is curve linear? {isLinear}"); // Output: true
  ```

- **Best Practices**:
	- Use stricter `tolerance` values for critical systems requiring high precision.
	- Apply this method only when necessary, as iterating over the curve may slightly impact performance.

- **Examples**:
	- Detect unnecessary animation curves in optimization workflows:
	  ```c#
      if (AnimationCurveHelper.IsCurveApproximatelyLinear(customCurve, 0.01f))
      {
          Debug.Log("Curve is linear. It can likely be optimized or skipped.");
      }
      ```

---

### Example Applications

1. **Inverse Mapping with Easing Curves**:
   Convert eased values back to linear values for reverse animation calculations:
   ```c#
   AnimationCurve easingCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
   AnimationCurve inverseEasing = AnimationCurveHelper.InvertCurve(easingCurve);
   
   float easedValue = easingCurve.Evaluate(0.6f);
   float linearVal = inverseEasing.Evaluate(easedValue);

   Debug.Log($"Linear value: {linearVal}");
   ```

2. **Check Curve Linearity**:
   Efficiently determine if a curve is redundant (i.e., approximately linear):
   ```c#
   if (AnimationCurveHelper.IsCurveApproximatelyLinear(someCurve, 0.005f))
   {
       Debug.Log("Curve is essentially linear. No need for special handling.");
   }
   ```

---

### Best Practices for `AnimationCurveHelper`

1. **Monotonicity Requirement**:
	- For `InvertCurve`, monotonically increasing or decreasing input curves are essential for meaningful results.

2. **Editor Dependency**:
	- Be cautious when using `InvertCurve` in non-Editor environments because it relies on UnityEditor’s `AnimationUtility` class. Consider precomputing inverted curves in the Editor.

3. **Resolution Tuning**:
	- Adjust the `resolution` parameter in `InvertCurve` based on your desired accuracy and performance needs. The default value (`256`) is suited for most common cases.

---

### Expansion Roadmap

Future improvements and extensions for **`AnimationCurveHelper`** might include:
- **Custom Curve Tools**:
	- Add utilities for blending multiple curves.
	- Create a curve simplification function to reduce keyframes while preserving the shape.
- **Runtime Compatibility**:
	- Replace `AnimationUtility` dependencies for better runtime support.
- **Advanced Curve Comparison**:
	- Introduce methods for determining similarity between two curves.
---

## Namespace
All related utilities are available under:
```c#
PsigenVision.Utilities.Math
```