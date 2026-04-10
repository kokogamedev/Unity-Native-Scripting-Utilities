# Rendering Utilities

The **Rendering Utilities** package provides tools to simplify and streamline operations related to rendering in Unity. It focuses on improving workflows for modifying renderer properties at runtime, ensuring efficiency and flexibility without modifying shared materials.

---

## Overview

### Purpose
The **Rendering Utilities** package contains utility classes and extensions that make rendering-related operations easier, safer, and more performant.

### Key Features
- Efficient updates to `Renderer` properties using `MaterialPropertyBlock`.
- Non-destructive runtime modifications to avoid material instantiation overhead.
- Modular structure designed to support future additions.

---

## Classes

### 1. `RendererExtensions`

The `RendererExtensions` class is a static class that provides extension methods for Unity's `Renderer` component, enabling safer runtime modifications using `MaterialPropertyBlock`.

---

### `RendererExtensions`

#### Overview
The `RendererExtensions` class simplifies runtime modifications to renderer properties without directly modifying materials. This prevents overhead caused by instantiating materials and ensures better batching performance.

---

#### Methods

##### `SetColorViaPropertyBlock`

```c#
public static void SetColorViaPropertyBlock(this Renderer renderer, Color color, MaterialPropertyBlock propertyBlock)
```

- **Description**:
  Updates the `_Color` property of the given `Renderer`'s material using a `MaterialPropertyBlock`. This ensures efficient runtime changes without modifying shared materials.

- **Parameters**:
	- `Renderer renderer`: The target renderer whose color property is being updated.
	- `Color color`: The new color to apply to the `_Color` property.
	- `MaterialPropertyBlock propertyBlock`: A `MaterialPropertyBlock` instance used to set the dynamic property.

- **Usage**:
  ```c#
  targetRenderer.SetColorViaPropertyBlock(Color.red, propertyBlock);
  ```

- **Returns**: `void`

---

#### Example Usage

Here’s an example of how to use the `SetColorViaPropertyBlock` method to dynamically update the color property of a `Renderer`:

```c#
using UnityEngine;
using PsigenVision.Utilities.Rendering;

public class Example : MonoBehaviour
{
    [SerializeField] private Renderer targetRenderer;

    private MaterialPropertyBlock propertyBlock;

    private void Start()
    {
        // Initialize the MaterialPropertyBlock
        propertyBlock = new MaterialPropertyBlock();

        // Set the color of the renderer using the extension method
        targetRenderer.SetColorViaPropertyBlock(Color.green, propertyBlock);
    }

    private void ChangeColor(Color newColor)
    {
        // Re-use the same property block for better efficiency
        targetRenderer.SetColorViaPropertyBlock(newColor, propertyBlock);
    }
}
```

---

## Best Practices

- **Reuse MaterialPropertyBlock Instances**:
  Avoid creating a new `MaterialPropertyBlock` every time. Create a shared instance and reuse it to reduce memory allocation overhead.

- **Dynamic Updates**:
  Use this method for dynamic rendering effects like state indicators, damage flashes, or highlighting objects.

- **Avoid Direct Modifications**:
  Avoid using `Renderer.material` for runtime modifications, as it generates unique instances and increases memory usage.

---

## Expansion Roadmap

### Future Additions
The **Rendering Utilities** module has room to grow! Here are some potential additions to the utility:
1. Additional extension methods for handling other `MaterialPropertyBlock` properties like:
	- `float` (e.g., modifying shader parameters dynamically).
	- `Vector4` (e.g., setting shader vectors like `MainTex_ST`).
	- `Texture` (e.g., applying runtime textures dynamically).
2. Utility classes for managing batch color changes across multiple renderers.
3. Utilities for working with Unity’s **Shader Graph**.

When a new class or method is added, it should follow this same structure with its own sub-section under **Classes**.

---

## Limitations

- **Method `SetColorViaPropertyBlock`**:
	- Specifically targets the `_Color` property of a material. Expanding these utilities to work with other properties will require additional methods.

- **MaterialPropertyBlock Scope**:
  These changes are applied at runtime and do not persist in the material asset itself. Any modification only affects the renderer during runtime execution.

---

## Notes for Contributors
- Follow the class-per-section modular structure (like the `RendererExtensions` structure above).
- Include examples for each method wherever relevant.
- Clearly state the purpose of each utility method and its parameters.

---

## Namespace
All related utilities are available under the namespace:
```c#
PsigenVision.Utilities.Rendering
```