# Core Utilities

The **Core Utilities** module provides foundational tools and helpers for a variety of use cases ranging from hash computation and data structure manipulation to core type definitions and low-level operations. These utilities are designed to promote robustness and reusability across diverse parts of your project.

---

## 1. `PsigenVision` Namespace

### Classes/Interfaces:
- **`IHaveGuid<T>`** *(Immutable GUID-based objects)*
- **`IHaveMutableGuid<T>`** *(Mutable GUID-based objects)*
- **`IHaveID<T>`** *(Immutable ID-based objects)*
- **`IHaveMutableID<T>`** *(Mutable ID-based objects)*
- **`IUnityComponent`**

---

### `IHaveGuid<T>`

#### Overview
The **`IHaveGuid<T>`** interface provides a contract for immutable GUID identifiers, ensuring uniquely identifiable objects. As the GUID is immutable, it prevents changes after being set (e.g., via a constructor or factory).

#### Members
- **Properties**:
    - `Guid ID`: The unique identifier for the object.
- **Implementation**:
    - The implementer must define the `ID` property initialized with an immutable GUID (via constructor/factory).
    - Equality comparison (via `IEquatable<T>`) ensures that objects of the same type have the same GUID.

#### Example Usage
```c#
public class ExampleClass : IHaveGuid<ExampleClass>
{
    public Guid ID { get; }

    public ExampleClass()
    {
        ID = Guid.NewGuid(); // GUID is immutable
    }

    public bool Equals(ExampleClass other) => other?.ID == this.ID;

    public override int GetHashCode() => ID.GetHashCode();
}
```

---

### `IHaveMutableGuid<T>`

#### Overview
The **`IHaveMutableGuid<T>`** interface supplements `IHaveGuid<T>` by introducing methods to explicitly change (`generate`) the GUID when necessary (e.g., for new instances or dynamic settings).

#### Members
- **Properties**:
    - `Guid ID`: The current identifier.
- **Methods**:
    - `Guid GenerateID()`: Regenerates the GUID and returns the new identifier.

#### Example Usage
```c#
public class ExampleClass : IHaveMutableGuid<ExampleClass>
{
    public Guid ID { get; private set; }

    public ExampleClass() { GenerateID(); }

    public Guid GenerateID()
    {
        ID = Guid.NewGuid();
        return ID;
    }

    public bool Equals(ExampleClass other) => other?.ID == this.ID;

    public override int GetHashCode() => ID.GetHashCode();
}
```

---

### `IHaveID<T>`

#### Overview
The **`IHaveID<T>`** interface defines immutable integer-based unique identifiers. IDs are typically generated using deterministic hashing functions like **FNV-1a** from a given string input.

#### Members
- **Properties**:
    - `int ID`: The unique identifier for the object.
- **Implementation**:
    - The implementer must ensure the ID is immutable.

#### Example Usage
```c#
public class ExampleClass : IHaveID<ExampleClass>
{
    public int ID { get; }

    public ExampleClass(string name)
    {
        ID = name.ComputeFNV1aHash(); // Generates immutable ID
    }

    public bool Equals(ExampleClass other) => other?.ID == this.ID;

    public override int GetHashCode() => ID.GetHashCode();
}
```

---

### `IHaveMutableID<T>`

#### Overview
The **`IHaveMutableID<T>`** extends `IHaveID<T>` by offering mechanisms to both **define an initial identifier based on input** and modify the identifier dynamically.

#### Members
- **Properties**:
    - `int ID`: The current identifier.
- **Methods**:
    - `int GenerateID(string name)`: Computes the ID based on the given string name and assigns it.
    - `int GenerateID()`: Overload to regenerate the ID without passing the name (if cached internally).

#### Example Usage
```c#
public class ExampleClass : IHaveMutableID<ExampleClass>
{
    public int ID { get; private set; }
    private string _cachedName;

    public ExampleClass(string name)
    {
        _cachedName = name;
        GenerateID(name);
    }

    public int GenerateID(string name)
    {
        ID = name.ComputeFNV1aHash();
        return ID;
    }

    public int GenerateID()
    {
        ID = _cachedName.ComputeFNV1aHash(); // Uses previously cached name
        return ID;
    }

    public bool Equals(ExampleClass other) => other?.ID == this.ID;

    public override int GetHashCode() => ID.GetHashCode();
}
```

---

### `IUnityComponent`

#### Overview
The **`IUnityComponent`** interface captures GameObject and Transform properties for Unity-specific entities without requiring inheritance from `Component`.

#### Members
- **Properties**:
    - `GameObject gameObject`: Reference to the GameObject containing this component.
    - `Transform transform`: Reference to the Transform of the GameObject.

#### Example Usage
```c#
public class ExampleComponent : MonoBehaviour, IUnityComponent
{
    public GameObject gameObject => base.gameObject;
    public Transform transform => base.transform;
}
```

---

## 2. `PsigenVision.Utilities` Namespace

### Classes:
- **`StringExtensions`**
- **`VectorMath`**
- **`VectorExtensions`**

---

### `StringExtensions`

#### Overview
The **`StringExtensions`** class provides utility methods for working with strings, including the efficient computation of hash values.

#### Methods

##### 1. `ComputeFNV1aHash`
```c#
public static int ComputeFNV1aHash(this string str)
```

- **Description**:
  Computes the **FNV-1a hash** for the provided string, which is a fast, non-cryptographic hash function with good distribution properties.

- **Parameters**:
	- `string str`: The input string to hash.

- **Returns**:
	- `int`: The computed hash value of the input string.

- **Usage**:
  ```c#
  string name = "ExampleName";
  int hash = name.ComputeFNV1aHash();
  Debug.Log($"Hash: {hash}");
  ```

- **Applications**:
	- Useful for creating identifiers or dictionary keys without relying on full strings, which can reduce memory usage and improve lookup performance.

---

### VectorMath Helper Class

The `PsigenVision.Utilities.VectorMath` class provides utility methods to work with vectors in Unity, offering advanced vector manipulation functionalities.

---

#### **GetProjectedVector/GetProjectedVectorUnsafe Method**

##### Definition:
```csharp
public static Vector3 GetProjectedVector(Vector3 vector, Vector3 direction)
public static Vector3 GetProjectedVectorUnsafe(Vector3 vector, Vector3 direction) 
```

##### Summary:
Projects a given vector onto a specified direction vector.

##### Parameters:
- `Vector3 vector`: The vector to be projected.
- `Vector3 direction`: The direction vector onto which the projection will occur.

##### Returns:
- `Vector3`: The projected vector in the specified direction.

##### Difference Between Safe and Unsafe
- The passed in `direction` vector must be normalized before using the unsafe method, whereas the safe method normalizes the passed in `direction` vector automatically.

---

#### **RemoveProjection/RemoveProjectionUnsafe Method**

##### Definition:
```csharp
public static Vector3 RemoveProjection(Vector3 vector, Vector3 direction)
public static Vector3 RemoveProjectionUnsafe(Vector3 vector, Vector3 direction)
```

##### Summary:
Removes the component of a vector that is projected onto a specified direction vector.

##### Parameters:
- `Vector3 vector`: The original vector to adjust.
- `Vector3 direction`: The direction vector representing the projection to remove. This vector does not need to be normalized; it will be normalized internally.

##### Returns:
- `Vector3`: A `Vector3` that excludes any contribution of the original vector in the specified direction. This resultant vector is orthogonal to the direction.

##### Difference Between Safe and Unsafe
- The passed in `direction` vector must be normalized before using the unsafe method, whereas the safe method normalizes the passed in `direction` vector automatically.

---

##### Example:
```csharp
using UnityEngine;
using PsigenVision.Utilities;

public class Example : MonoBehaviour
{
    void Start()
    {
        Vector3 movementVelocity = new Vector3(12, 0, 5);
        Vector3 momentumDirection = new Vector3(0, 0, 1); // Horizontal momentum along the Z-axis

        // Remove the contribution of horizontal momentum
        Vector3 adjusted = VectorMath.RemoveProjection(movementVelocity, momentumDirection);

        Debug.Log($"Resultant Vector: {adjusted}"); // Logs the velocity orthogonal to the momentum direction
    }
}
```

##### Remarks:
- Use this method to cleanly isolate portions of movement or velocity orthogonal to a given direction.
- Common applications include removing horizontal momentum from character movement in games.

---

#### **GetProjectedMagnitude/GetProjectedMagnitudeUnsafe Method**

##### Definition:
```csharp
public static float GetProjectedMagnitude(Vector3 vector, Vector3 direction)
public static float GetProjectedMagnitudeUnsafe(Vector3 vector, Vector3 direction)
```

##### Summary:
Calculates the signed scalar magnitude (scalar projection) of a given vector onto a specified direction vector.

##### Parameters:
- `Vector3 vector`: The vector to be projected.
- `Vector3 direction`: The direction vector onto which the projection will occur. This vector does not need to be normalized; it is normalized internally.

##### Returns:
- `float`: The signed scalar projection of the `vector` onto the `direction`.
	- **Positive Value**: Indicates alignment with the `direction`.
	- **Negative Value**: Indicates opposition to the `direction`.

##### Difference Between Safe and Unsafe
- The passed in `direction` vector must be normalized before using the unsafe method, whereas the safe method normalizes the passed in `direction` vector automatically.

---

#### **Map2DTo3D Method**

##### Definition:
```csharp
public static Vector3 Map2DTo3D(Vector2 vector2, Vector3 xAxis3D, Vector3 yAxis3D)
```

##### Summary:
Maps a 2D vector into a 3D vector by projecting it along two custom 3D axis directions (`xAxis3D` and `yAxis3D`).

##### Parameters:
- `Vector2 vector2`: The 2D vector to map into 3D space.
- `Vector3 xAxis3D`: The 3D vector representing the X-axis direction in the target 3D space.
- `Vector3 yAxis3D`: The 3D vector representing the Y-axis direction in the target 3D space.

##### Returns:
- `Vector3`: The resulting 3D vector, aligned to the specified axes.

##### Example:
```csharp
using UnityEngine;
using PsigenVision.Utilities;

public class Example : MonoBehaviour
{
    void Start()
    {
        Vector2 input = new Vector2(2, 5);
        Vector3 xAxis3D = Vector3.right;  // X-axis in 3D
        Vector3 yAxis3D = Vector3.up;     // Y-axis in 3D

        Vector3 result = VectorMath.Map2DTo3D(input, xAxis3D, yAxis3D);

        Debug.Log($"Mapped Vector: {result}");
    }
}
```

---

#### **Project2DToPlane Method**

##### Definition:
```csharp
public static Vector3 Project2DToPlane(Vector2 vector2, Vector2 xAxis2D, Vector2 yAxis2D, Vector3 planeNormal)
```

##### Summary:
Projects a 2D vector onto a 3D plane defined by specified 2D axis orientations (`xAxis2D` and `yAxis2D`) and the plane's normal vector (`planeNormal`).

##### Parameters:
- `Vector2 vector2`: The 2D vector to project onto the plane.
- `Vector2 xAxis2D`: The 2D x-axis direction defining the alignment of the plane in 3D.
- `Vector2 yAxis2D`: The 2D y-axis direction defining the alignment of the plane in 3D.
- `Vector3 planeNormal`: The normal vector of the target plane.

##### Returns:
- `Vector3`: The resulting 3D vector after projecting the 2D vector onto the specified 3D plane.

##### Remarks:
Before projection, the method normalizes the corresponding 3D vectors derived from the 2D axis directions.

##### Example:
```csharp
using UnityEngine;
using PsigenVision.Utilities;

public class Example : MonoBehaviour
{
    void Start()
    {
        Vector2 input = new Vector2(3, -1);
        Vector2 xAxis2D = new Vector2(1, 0); // X direction (2D)
        Vector2 yAxis2D = new Vector2(0, 1); // Y direction (2D)
        Vector3 planeNormal = Vector3.forward; // Normal vector of the target plane

        Vector3 projectedResult = VectorMath.Project2DToPlane(input, xAxis2D, yAxis2D, planeNormal);

        Debug.Log($"Projected Vector: {projectedResult}");
    }
}
```

---

#### Examples

##### Using GetProjectedVector:
```csharp
using UnityEngine;
using PsigenVision.Utilities;

public class Example : MonoBehaviour
{
    void Start()
    {
        Vector3 velocity = new Vector3(10, 5, 0);
        Vector3 direction = new Vector3(1, 0, 0); // Project onto the X-Axis

        Vector3 projected = VectorMath.GetProjectedVector(velocity, direction);

        Debug.Log($"Projected Vector: {projected}");
    }
}
```

##### Using GetProjectedMagnitude:
```csharp
using UnityEngine;
using PsigenVision.Utilities;

public class Example : MonoBehaviour
{
    void Start()
    {
        Vector3 force = new Vector3(5, -10, 0);
        Vector3 upwardDirection = new Vector3(0, 1, 0); // Upward direction (Y-Axis)

        float projectedMagnitude = VectorMath.GetProjectedMagnitude(force, upwardDirection);

        if (projectedMagnitude < 0)
        {
            Debug.Log("The force has a downward component (opposed to the upward direction).");
        }
        else
        {
            Debug.Log($"The upward component of the force is: {projectedMagnitude}");
        }
    }
}
```

##### Using Map2DTo3D:
```csharp
using UnityEngine;
using PsigenVision.Utilities;

public class Example : MonoBehaviour
{
    void Start()
    {
        Vector2 input = new Vector2(-1, 4);
        Vector3 xDirection = Vector3.right;
        Vector3 yDirection = Vector3.up;

        Vector3 mapped = VectorMath.Map2DTo3D(input, xDirection, yDirection);

        Debug.Log($"Mapped 3D Vector: {mapped}");
    }
}
```

##### Using Project2DToPlane:
```csharp
using UnityEngine;
using PsigenVision.Utilities;

public class Example : MonoBehaviour
{
    void Start()
    {
        Vector2 input = new Vector2(1, -2);
        Vector2 xAxis2D = new Vector2(1, 0);
        Vector2 yAxis2D = new Vector2(0, 1);
        Vector3 planeNormal = Vector3.up;

        Vector3 planeProjection = VectorMath.Project2DToPlane(input, xAxis2D, yAxis2D, planeNormal);

        Debug.Log($"Projected Vector3 onto Plane: {planeProjection}");
    }
}
```
---

##### **Project/ProjectUnsafe (High-Performance Variant)**

##### Definition:
```csharp
public static void Project(ref Vector3 vector, Vector3 direction)
public static void ProjectUnsafe(ref Vector3 vector, Vector3 direction)
```

##### Summary:
Projects a vector onto a specified direction by modifying the original vector in-place to reduce memory allocations in performance-critical situations.

##### Parameters:
- `ref Vector3 vector`: The original vector to be projected. This vector is modified directly.
- `Vector3 direction`: The direction vector representing the projection target. This vector will be normalized internally.

##### Returns:
None (the modification is done in-place).

##### Difference Between Safe and Unsafe
- The passed in `direction` vector must be normalized before using the unsafe method, whereas the safe method normalizes the passed in `direction` vector automatically.

##### Example:
```csharp
using UnityEngine;
using PsigenVision.Utilities;

public class Example : MonoBehaviour
{
    void Start()
    {
        Vector3 velocity = new Vector3(10, 5, 0);
        Vector3 direction = Vector3.right;

        VectorMath.Project(ref velocity, direction);

        Debug.Log($"Projected Velocity (in-place): {velocity}");
    }
}
```

##### Remarks:
This method is designed for scenarios where performance is critical, such as during physics updates or tight game loops.

---

##### **RemoveProjection/RemoveProjectionUnsafe (High-Performance Variant)**

##### Definition:
```csharp
public static void RemoveProjection(ref Vector3 vector, Vector3 direction)
public static void RemoveProjectionUnsafe(ref Vector3 vector, Vector3 direction)
```

##### Summary:
Removes the component of a vector aligned to a direction, modifying the original vector in-place for performance-critical use.

##### Parameters:
- `ref Vector3 vector`: The original vector to adjust. This vector is modified directly.
- `Vector3 direction`: The direction vector representing the projection to remove. This vector will be normalized internally.

##### Returns:
None (the modification is done in-place).

##### Difference Between Safe and Unsafe
- The passed in `direction` vector must be normalized before using the unsafe method, whereas the safe method normalizes the passed in `direction` vector automatically.

##### Example:
```csharp
using UnityEngine;
using PsigenVision.Utilities;

public class Example : MonoBehaviour
{
    void Start()
    {
        Vector3 velocity = new Vector3(10, 5, 0);
        Vector3 direction = Vector3.right;

        VectorMath.RemoveProjection(ref velocity, direction);

        Debug.Log($"Vector after removing projection (in-place): {velocity}");
    }
}
```

##### Remarks:
This method is optimized for removing unwanted components during frequent calculations in performance-critical code.

---

#### **ChangeMagnitude**

##### Definition:
```csharp
public static Vector3 ChangeMagnitude(Vector3 vector3, float magnitude)
```

##### Summary:
Changes the magnitude of the given vector to the specified value while maintaining its direction.

##### Parameters:
- `Vector3 vector3`: The vector whose magnitude is to be changed. The direction of this vector remains unchanged.
- `float magnitude`: The new magnitude to assign to the vector.

##### Returns:
- `Vector3`: A new vector with the specified magnitude and the same direction as the input vector.

##### Example:
```csharp
using UnityEngine;

public class Example : MonoBehaviour
{
    void Start()
    {
        Vector3 original = new Vector3(1, -1, 2);
        Vector3 result = VectorMath.ChangeMagnitude(original, 5f);

        Debug.Log($"Changed Magnitude: {result}");
    }
}
```

---

#### **ChangeMagnitude (ref Vector3)**

##### Definition:
```csharp
public static void ChangeMagnitude(ref Vector3 vector3, float magnitude)
```

##### Summary:
Changes the magnitude of the given vector to the specified value while maintaining its direction. This method modifies the original vector in-place.

##### Parameters:
- `ref Vector3 vector3`: The vector whose magnitude is to be updated. This vector is modified in-place.
- `float magnitude`: The new magnitude to assign to the vector.

##### Returns:
- `(void)`: No return value. The target vector is modified in-place.

##### Example:
```csharp
using UnityEngine;

public class Example : MonoBehaviour
{
    void Start()
    {
        Vector3 vector = new Vector3(2, 3, 6);
        VectorMath.ChangeMagnitude(ref vector, 10f);

        Debug.Log($"Modified Vector: {vector}");
    }
}
```

---

#### Use Cases

##### GetProjectedVector
- Determine how much of a vector lies in a particular direction.
- Useful in aligning objects, force vectors, or movement directions.

##### GetProjectedMagnitude
- Analyze the relative direction of forces or movement (e.g., detecting upward or downward components).
- Detect opposing forces or misalignments in momentum.

##### Map2DTo3D
- Transform 2D vectors into 3D space using custom axis mappings.
- Use for motion systems that require translating 2D input into a 3D environment.

##### Project2DToPlane
- Project 2D data onto a 3D surface while aligning with custom axes.
- Useful in rendering, procedural generation, or handling 2D UI transforms on a 3D plane.

##### Notes on Usage
- **`VectorExtensions`** methods are user-friendly and return a new `Vector3`. They are suitable when readability and flexibility are more important than performance.
- **`VectorMath`** methods (`ref` versions) are designed for high-performance applications where frequent vector operations in tight loops (e.g., physics systems) demand minimal memory allocation.

---

#### **MoveMeTowards**

##### Definition:
```csharp
public static void MoveMeTowards(ref Vector3 me, Vector3 towards, float maxDistanceDelta)
```

##### Summary:
Moves a vector towards a specified target position in-place by a maximum distance delta.

##### Parameters:
- `ref Vector3 me`: The current vector position to be moved. This vector is modified in-place.
- `Vector3 towards`: The target vector position to move towards.
- `float maxDistanceDelta`: The maximum distance the vector can move in this step.

##### Returns:
- `(void)`: No return value. The target vector is modified in-place.

##### Example:
```csharp
using UnityEngine;

public class Example : MonoBehaviour
{
    void Start()
    {
        Vector3 currentPos = new Vector3(0, 0, 0);
        Vector3 targetPos = new Vector3(10, 0, 0);

        VectorMath.MoveMeTowards(ref currentPos, targetPos, 2f);

        Debug.Log($"New Position: {currentPos}");
    }
}
```
---

#### **MoveTowardsZero**

##### Definition:
```csharp
public static void MoveTowardsZero(ref Vector3 vector, float maxDistanceDelta)
```

##### Summary:
Gradually moves a vector toward the origin (zero) in-place by a maximum distance delta.

##### Parameters:
- `ref Vector3 vector`: The vector to be moved. This is modified in-place.
- `float maxDistanceDelta`: The maximum distance the vector can move toward zero in this operation.

##### Returns:
- `(void)`: No return value. The vector is updated in-place.

##### Example:
```csharp
using UnityEngine;

public class Example : MonoBehaviour
{
    void Start()
    {
        Vector3 vector = new Vector3(12, -8, 5);

        VectorMath.MoveTowardsZero(ref vector, 4f);

        Debug.Log($"Updated Vector: {vector}");
        // Logs the new vector position closer to zero but within the delta.
    }
}
```

---

##### Definition:
```csharp
public static Vector3 MoveTowardsZero(Vector3 vector, float maxDistanceDelta)
```

##### Summary:
Gradually moves a vector toward the origin (zero) by a specified maximum distance delta, returning the updated vector.

##### Parameters:
- `Vector3 vector`: The vector to be moved toward zero.
- `float maxDistanceDelta`: The maximum distance the vector can move toward zero in this operation.

##### Returns:
- `Vector3`: The updated vector after moving toward zero by the specified distance delta.

##### Example:
```csharp
using UnityEngine;

public class Example : MonoBehaviour
{
    void Start()
    {
        Vector3 result = VectorMath.MoveTowardsZero(new Vector3(7, 3, -2), 2f);

        Debug.Log($"Moved Vector: {result}");
        // Logs the updated vector closer to zero by up to a delta of 2f.
    }
}
```
---
Happy coding with `VectorMath`!

---

### **VectorExtensions Class**

---

#### **Project/ProjectUnsafe (Extension Method)**

##### Definition:
```csharp
public static Vector3 Project(this Vector3 vector, Vector3 direction)
public static Vector3 ProjectUnsafe(this Vector3 vector, Vector3 direction)
```

##### Summary:
Projects a vector onto a specified direction vector, returning the resulting projection as a new `Vector3`.

##### Parameters:
- `Vector3 vector`: The original `Vector3` to project.
- `Vector3 direction`: The direction vector representing where the projection will occur.

##### Returns:
- `Vector3`: A new vector representing the projection of the original vector onto the specified direction.

##### Difference Between Safe and Unsafe
- The passed in `direction` vector must be normalized before using the unsafe method, whereas the safe method normalizes the passed in `direction` vector automatically.

##### Example:
```csharp
using UnityEngine;
using PsigenVision.Utilities;

public class Example : MonoBehaviour
{
    void Start()
    {
        Vector3 velocity = new Vector3(10, 5, 0);
        Vector3 direction = Vector3.right;

        Vector3 projected = velocity.Project(direction);

        Debug.Log($"Projected Velocity: {projected}");
    }
}
```

---

#### **RemoveProjection/RemoveProjectionUnsafe (Extension Method)**

##### Definition:
```csharp
public static Vector3 RemoveProjection(this Vector3 vector, Vector3 direction)
public static Vector3 RemoveProjectionUnsafe(this Vector3 vector, Vector3 direction)
```

##### Summary:
Removes the component of a vector projected onto a direction, returning a new vector that is orthogonal to the direction.

##### Parameters:
- `Vector3 vector`: The original vector to adjust.
- `Vector3 direction`: The direction vector onto which the projection is performed.

##### Returns:
- `Vector3`: A new vector representing the original vector with its projected component removed.

##### Difference Between Safe and Unsafe
- The passed in `direction` vector must be normalized before using the unsafe method, whereas the safe method normalizes the passed in `direction` vector automatically.

##### Example:
```csharp
using UnityEngine;
using PsigenVision.Utilities;

public class Example : MonoBehaviour
{
    void Start()
    {
        Vector3 velocity = new Vector3(10, 5, 0);
        Vector3 direction = Vector3.right;

        Vector3 adjusted = velocity.RemoveProjection(direction);

        Debug.Log($"Adjusted Velocity: {adjusted}");
    }
}
```

---

#### **ProjectMagnitude/ProjectMagnitudeUnsafe (Extension Method)**

##### Definition:
```csharp
public static float ProjectMagnitude(this Vector3 vector, Vector3 direction)
public static float ProjectMagnitudeUnsafe(this Vector3 vector, Vector3 direction)
```

##### Summary:
Calculates the signed scalar magnitude (scalar projection) of a given vector onto a specified direction vector.

##### Parameters:
- `Vector3 vector`: The original vector.
- `Vector3 direction`: The direction vector onto which the scalar projection is performed.

##### Returns:
- `float`: The signed scalar magnitude of the projected vector.

##### Difference Between Safe and Unsafe
- The passed in `direction` vector must be normalized before using the unsafe method, whereas the safe method normalizes the passed in `direction` vector automatically.

##### Example:
```csharp
using UnityEngine;
using PsigenVision.Utilities;

public class Example : MonoBehaviour
{
    void Start()
    {
        Vector3 force = new Vector3(10, 5, 0);
        Vector3 direction = Vector3.up;

        float projectedMagnitude = force.ProjectMagnitude(direction);

        Debug.Log($"Projected Magnitude: {projectedMagnitude}");
    }
}
```

---

#### **MapTo3D (Extension Method)**

##### Definition:
```csharp
public static Vector3 MapTo3D(this Vector2 vector2, Vector3 xAxis3D, Vector3 yAxis3D)
```

##### Summary:
Maps a 2D vector to a 3D space using specified 3D axes. The `Vector2` is translated into a `Vector3` with user-defined directions.

##### Parameters:
- `Vector2 vector2`: The original vector in 2D space.
- `Vector3 xAxis3D`: The 3D axis representing the X-axis in the 2D plane.
- `Vector3 yAxis3D`: The 3D axis representing the Y-axis in the 2D plane.

##### Returns:
- `Vector3`: A new vector in 3D space.

##### Example:
```csharp
using UnityEngine;
using PsigenVision.Utilities;

public class Example : MonoBehaviour
{
    void Start()
    {
        Vector2 input2D = new Vector2(1, 3);
        Vector3 mapped = input2D.MapTo3D(Vector3.right, Vector3.up);

        Debug.Log($"Mapped Vector: {mapped}");
    }
}
```

---

#### **ProjectToPlane (Extension Method)**

##### Definition:
```csharp
public static Vector3 ProjectToPlane(this Vector2 vector2, Vector3 normal3D, Vector3 xAxis3D, Vector3 yAxis3D)
```

##### Summary:
Projects a 2D vector onto a 3D plane defined by the given plane's normal vector and user-defined 3D axes. The result is a `Vector3` projection.

##### Parameters:
- `Vector2 vector2`: The original vector in 2D space.
- `Vector3 normal3D`: The normal vector of the plane for projection.
- `Vector3 xAxis3D`: The 3D axis representing the X-axis in the 2D plane.
- `Vector3 yAxis3D`: The 3D axis representing the Y-axis in the 2D plane.

##### Returns:
- `Vector3`: A new vector projected onto the 3D plane.

##### Example:
```csharp
using UnityEngine;
using PsigenVision.Utilities;

public class Example : MonoBehaviour
{
    void Start()
    {
        Vector2 vec2 = new Vector2(4, 2);
        Vector3 projectedOnPlane = vec2.ProjectToPlane(Vector3.forward, Vector3.right, Vector3.up);

        Debug.Log($"Projected on Plane: {projectedOnPlane}");
    }
}
```

---

### **VectorExtensions.ChangeMagnitude (Extension Method)**

#### Definition:
```csharp
public static Vector3 ChangeMagnitude(this Vector3 vector3, float magnitude)
```

#### Summary:
An extension method that changes the magnitude of the given vector to the specified value while maintaining its direction.

#### Parameters:
- `Vector3 vector3`: The vector whose magnitude is to be changed. The direction of this vector remains unchanged.
- `float magnitude`: The new magnitude to assign to the vector.

#### Returns:
- `Vector3`: A new vector with the specified magnitude and the same direction as the input vector.

#### Example:
```csharp
using UnityEngine;

public class Example : MonoBehaviour
{
    void Start()
    {
        Vector3 original = new Vector3(3, 4, 5);
        Vector3 result = original.ChangeMagnitude(8f);

        Debug.Log($"Changed Magnitude (Extension): {result}");
    }
}
```
---

### **MoveMeTowards (Extension Method)**

#### Definition:
```csharp
public static Vector3 MoveMeTowards(this Vector3 me, Vector3 towards, float maxDistanceDelta)
```

#### Summary:
Moves a vector towards a specified target position by a maximum distance delta.

#### Parameters:
- `Vector3 me`: The current vector position to be moved.
- `Vector3 towards`: The target vector position to move towards.
- `float maxDistanceDelta`: The maximum distance the vector can move in this step.

#### Returns:
- `Vector3`: A new vector position after moving towards the target position.

#### Example:
```csharp
using UnityEngine;

public class Example : MonoBehaviour
{
    void Start()
    {
        Vector3 currentPos = new Vector3(0, 0, 0);
        Vector3 targetPos = new Vector3(10, 0, 0);

        Vector3 result = currentPos.MoveMeTowards(targetPos, 2f);
        Debug.Log($"New Position: {result}");
    }
}
```

---

### **MoveTowardsZero (Extension Method)**

#### Definition:
```csharp
public static Vector3 MoveTowardsZero(this Vector3 vector, float maxDistanceDelta)
```

#### Summary:
Gradually moves a vector closer to the origin (zero) by a maximum distance delta, returning the updated vector.

#### Parameters:
- `Vector3 vector`: The starting vector to move toward the origin.
- `float maxDistanceDelta`: The maximum distance the vector can move toward the origin.

#### Returns:
- `Vector3`: The updated vector position after moving closer to zero.

#### Example:
```csharp
using UnityEngine;

public class Example : MonoBehaviour
{
    void Start()
    {
        Vector3 current = new Vector3(-10, 15, 7);

        Vector3 result = current.MoveTowardsZero(5f);

        Debug.Log($"New Vector: {result}");
        // Logs the vector after moving 5f closer to zero.
    }
}
```
---

### `CommandSO<On, With>`

#### Overview
The **`CommandSO<On, With>`** class is a generalized implementation of the Flyweight Command Pattern. It allows defining stateless, reusable logic encapsulated in a `ScriptableObject` for execution on a target object (`On`) with some data (`With`).

#### Members
- **Methods**:
  - `abstract void Execute(On obj, With data)`: Executes the command with the specified target object and data.

#### Example Usage

- **Specialized Command**:
  ```csharp
  [CreateAssetMenu(menuName = "Commands/TransformCommand")]
  public class TransformCommand : CommandSO<Transform, Vector3>
  {
      public override void Execute(Transform target, Vector3 data)
      {
          target.position = data;
      }
  }
  ```

- **Executing a Command**:
  ```csharp
  TransformCommand moveCommand = ...;
  moveCommand.Execute(playerTransform, new Vector3(5, 5, 5));
  ```

---

## 3. `PsigenVision.Utilities.Collection` Namespace

### Classes:
- **`SortedListExtensions`**

---

### `SortedListExtensions`

#### Overview
The **`SortedListExtensions`** class provides helper methods to simplify operations on **SortedList<TKey, List<TValue>>** structures. These enhancements enable quicker manipulation and management of complex collections.

#### Methods

##### 1. `Remove<TKey, TValue>`
```c#
public static void Remove<TKey, TValue>(this SortedList<TKey, List<TValue>> list, TKey key, TValue value)
```

- **Description**:
  Removes a value from the list of values for a specific key in a SortedList. This does **not** remove the key if the list becomes empty.

- **Usage**:
  ```c#
  sortedList.Remove("Category1", "ValueA");
  ```

---

##### 2. `ThoroughRemove<TKey, TValue>`
```c#
public static void ThoroughRemove<TKey, TValue>(this SortedList<TKey, List<TValue>> list, TKey key, TValue value)
```

- **Description**:
  Removes a value from the list of values for a specific key and additionally removes the key if its list becomes empty.

- **Usage**:
  ```c#
  sortedList.ThoroughRemove("Category1", "ValueA");
  ```

---

##### 3. `TryRemove<TKey, TValue>`
```c#
public static bool TryRemove<TKey, TValue>(this SortedList<TKey, List<TValue>> list, TKey key, TValue value)
```

- **Description**:
  Attempts to remove a value from the list linked to the given key. Returns a boolean indicating success or failure.

---

##### 4. `TryThoroughRemove<TKey, TValue>`
```c#
public static bool TryThoroughRemove<TKey, TValue>(this SortedList<TKey, List<TValue>> list, TKey key, TValue value)
```

- **Description**:
  Similar to `TryRemove`, but also removes the key from the collection if the list becomes empty.

- **Usage**:
  ```c#
  bool success = sortedList.TryThoroughRemove("Category1", "ValueA");
  ```

---

### Refactoring Notes for `SortedListExtensions`

- **Null Safety**:
  Ensure the integrity of the `SortedList` methods by adding null-checks to prevent exceptions or undefined behaviors.

- **Generic Improvements**:
  Extend support for additional data structures, integrating with `SortedDictionary` or similar collections.

---

## Final Notes

This modular breakdown organizes the **Core Utilities** by namespace, focusing on their respective scopes:
1. **`PsigenVision`**: Interfaces and core abstractions.
2. **`PsigenVision.Utilities`**: Common utilities to streamline development.
3. **`PsigenVision.Utilities.Collection`**: Specialized collection operations.