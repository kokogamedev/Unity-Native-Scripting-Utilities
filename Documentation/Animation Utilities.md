# Animation Utilities

The **Animation Utilities** package provides tools to simplify and streamline animation-related workflows in Unity. It includes functionality for synchronizing, normalizing, and manipulating animation properties, allowing developers to easily manage complex animations and dependencies.

---

## Overview

### Purpose
The **Animation Utilities** module focuses on solving common challenges in animation synchronization, property normalization, and runtime data management. These tools are designed to extend Unity's animation systems and improve workflow efficiency.

### Key Features
- **Synced Animation**: Synchronize float properties across animations seamlessly.
- **Normalization and Range Mapping**: Normalize input values and map them to defined ranges or curves.
- **Framework for Growth**: Built to house additional animation utilities and functionality as features are developed.

---

## Classes

### 1. `DeriveSyncedAnimatedFloat`

The **DeriveSyncedAnimatedFloat** class is a MonoBehaviour that facilitates synchronizing one animated float property with another. It is particularly useful in cases where animations need to stay in sync within coordinated systems.

---

### `DeriveSyncedAnimatedFloat`

#### Overview
The `DeriveSyncedAnimatedFloat` class allows developers to synchronize one animated float with another while providing tools for normalization and proportional scaling. It ensures smooth value transitions for complex animations by defining ranges or applying optional animation curves.

---

#### Fields

##### `animatedFloatToSyncWith`
```c#
[SerializeField] public AnimatedPropertyData animatedFloatToSyncWith
```
- **Description**: The source animated float property whose parameters (range, normalization, etc.) are used as the basis for synchronization.
- **Type**: `AnimatedPropertyData`
- **Default Value**: `AnimatedPropertyData(0f, 1f)`
- **Tooltip**: `"The animation parameters for animated float to which we are syncing"`

##### `animatedFloatToSync`
```c#
[SerializeField] public AnimatedPropertyData animatedFloatToSync
```
- **Description**: The target animated float that is being synchronized with the source. Its values are scaled based on the source property and its own defined range.
- **Type**: `AnimatedPropertyData`
- **Default Value**: `AnimatedPropertyData(0f, 1f)`
- **Tooltip**: `"The animation parameters for animated float we bringing into sync"`

##### `isInitialized`
```c#
private bool isInitialized
```
- **Description**: A flag indicating whether the class has been properly initialized. Checks are made during `Awake()` to ensure valid ranges.

##### `lastValueToSyncWith`
```c#
private float lastValueToSyncWith
```
- **Description**: A cached value of the last synchronized float to prevent redundant recalculations.

---

#### Methods

##### 1. `GetSyncedFloatValue`

```c#
public bool GetSyncedFloatValue(float valueToSyncWith, out float syncedValue)
```

- **Description**:
  Retrieves a synchronized float value based on the provided input value. If the input value has not changed since the last call, no synchronization occurs, reducing unnecessary computation.

- **Parameters**:
    - `float valueToSyncWith`: The input float value to synchronize with.
    - `out float syncedValue`: The resulting synchronized float value.

- **Returns**:
    - `bool`: Returns `true` if the synchronized value was updated; otherwise, returns `false` if the input value remained unchanged.

- **Details**:
    1. The input value is normalized using `animatedFloatToSyncWith.NormalizePropertyValue`.
    2. The normalized value is applied to the range defined in `animatedFloatToSync.GetAbsolutePropertyValue`.
    3. The last `valueToSyncWith` is updated for the next call.

- **Usage**:
  ```c#
  public class ExampleUsage : MonoBehaviour
  {
      [SerializeField] private DeriveSyncedAnimatedFloat syncedFloatBehavior;
      [SerializeField] private float inputValue;
      private float outputValue;

      void Update()
      {
          if (syncedFloatBehavior.GetSyncedFloatValue(inputValue, out outputValue))
          {
              Debug.Log($"Synchronized float value: {outputValue}");
          }
      }
  }
  ```

---

#### Lifecycle Methods

##### `Awake`
```c#
void Awake()
```

- **Description**:
  Performs validation checks on the minimum and maximum ranges of `animatedFloatToSyncWith` and `animatedFloatToSync`. If invalid ranges are found, initialization fails, and errors are logged.

- **Details**:
    - Ensures `maxValue > minValue` for both source and target animated floats.
    - Logs descriptive errors in Unity's console if initialization conditions are not met.

---

## Example Usage in Unity

Below is an example setup for synchronizing animated floats.

1. Add the `DeriveSyncedAnimatedFloat` MonoBehaviour to a GameObject.
2. Configure the `animatedFloatToSyncWith` and `animatedFloatToSync` fields in the Inspector.
3. Call `GetSyncedFloatValue` at runtime to obtain synchronized values.

### Scenario Example:
Imagine an interactive UI where an alpha transparency animation of one element (`animatedFloatToSyncWith`) controls the size of another element (`animatedFloatToSync`).

#### Setup:
```c#
[SerializeField] private DeriveSyncedAnimatedFloat alphaSizeSynchronizer;

void Update()
{
    float alpha = Mathf.PingPong(Time.time, 1f); // Example: Generate alpha dynamically
    if (alphaSizeSynchronizer.GetSyncedFloatValue(alpha, out float size))
    {
        targetTransform.localScale = Vector3.one * size;
    }
}
```

### 2. `AnimatedPropertyData`

#### Overview
The `AnimatedPropertyData` struct is a foundational type used by the `DeriveSyncedAnimatedFloat` class to define the parameters of animated float properties. It specifies value ranges, supports normalization, and facilitates proportional mapping from one range to another.

This struct encapsulates all necessary data for managing and processing animated float properties effectively.

---

#### Fields

##### `minValue`
```c#
public float minValue
```
- **Description**: The minimum value of the animated property.
- **Usage**: Used to calculate the normalized level and define the property's range.

##### `maxValue`
```c#
public float maxValue
```
- **Description**: The maximum value of the animated property.
- **Usage**: Used to calculate the normalized level and define the property's range.

##### `animationCurve`
```c#
public AnimationCurve animationCurve
```
- **Description**: An optional animation curve applied during value mapping. If not set, linear interpolation is used for mapping normalized values to absolute values.
- **Usage**: Enables non-linear transitions and animations.

---

#### Methods

##### 1. `NormalizePropertyValue`
```c#
public float NormalizePropertyValue(float rawValue)
```
- **Description**:
  Converts a raw float value to a normalized level (0.0 to 1.0) based on the defined `minValue` and `maxValue`.

- **Parameters**:
  - `float rawValue`: The input value to normalize.

- **Returns**:
  - `float`: The normalized value in the range [0, 1].

- **Details**:
  If `rawValue` is below `minValue`, it clamps to `0`. If it exceeds `maxValue`, it clamps to `1`.

- **Example Usage**:
  ```c#
  AnimatedPropertyData propertyData = new AnimatedPropertyData(0f, 100f);
  float normalizedValue = propertyData.NormalizePropertyValue(50f); // Output: 0.5
  ```

---

##### 2. `GetAbsolutePropertyValue`
```c#
public float GetAbsolutePropertyValue(float normalizedValue)
```
- **Description**:
  Converts a normalized value back to an absolute value within the range defined by `minValue` and `maxValue`.
- **Parameters**:
  - `float normalizedValue`: A normalized float value (0.0 to 1.0).
- **Returns**:
  - `float`: The absolute float value within the defined range.

- **Details**:
  When an `animationCurve` is applied, it modifies the interpolation to reflect the curve's shape.

- **Example Usage**:
  ```c#
  AnimatedPropertyData propertyData = new AnimatedPropertyData(0f, 100f);
  float absoluteValue = propertyData.GetAbsolutePropertyValue(0.5f); // Output: 50f
  ```

---


## Best Practices

1. **Validate Values in Inspector**:
   When assigning values to `animatedFloatToSyncWith` and `animatedFloatToSync`, ensure that `maxValue > minValue` to prevent runtime errors during synchronization.

2. **Reuse MonoBehaviour Across Animations**:
   You can use multiple instances of `DeriveSyncedAnimatedFloat` to handle different animations, as long as each instance is synchronized with independent float properties.

3. **Use Debug.Log for Validation**:
   Leverage the logged warnings in `Awake` to debug issues with incorrectly defined ranges.

4. **Leverage `animationCurve` for Custom Transitions**:
   Use the `animationCurve` field in `AnimatedPropertyData` to create non-linear transitions and animations.

5. **Validate Ranges During Setup**:
   Ensure each `AnimatedPropertyData` instance has `maxValue > minValue` to avoid runtime errors.

6. **Batch Process Synchronization**:
   If multiple synchronized animations are needed, consider grouping them logically and using multiple instances of `DeriveSyncedAnimatedFloat`.

---

## Expansion Roadmap

As **Animation Utilities** grows, here are some potential additions:
- **New Tools**:
    - Utilities for handling multiple synchronized properties.
    - Support for Vector properties (`Vector2`, `Vector3`) within animations.
- **Event-Driven Updates**:
    - Add support for invoking UnityEvents when a synchronized value changes.
- **Support for Other Data Types**:
  - Expand `AnimatedPropertyData` to handle `Vector2` and `Vector3` ranges for more complex animations.
- **Global Animation Controllers**:
  - Introduce a global controller to manage and synchronize multiple animations across objects.
- **Weighted Animation Blending**:
  - Add functionality to blend synchronized animations based on weighted parameters.


---

## Namespace
All related utilities are available under:
```c#
PsigenVision.Animation
```