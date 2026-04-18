# PsigenVision's Unity Native C# Utilities

**Package:** `com.psigenvision.utilities.native`  
**Version:** 0.9.0  
**Namespace:** `PsigenVision.Utilities`

Welcome to **PsigenVision's Unity Native C# Utilities**, a growing suite of utility libraries designed to boost productivity and facilitate common tasks in Unity (and beyond). Whether you're shuffling lists, generating random values, or working with Unity's `Bounds`, these extensions are here to simplify your workflow.

**Disclaimer**:
> The code within this library spans quite a bit of time and evolved with my own skill level. As such, many scripts will most certainly have potential for improvement. I truly welcome advice and collaboration—it's how I learn! So, feel free to reach out! :)

**Check Out the Documentation!**
> I won't elaborate too much on the contents of this package within this README—rather, I recommend checking out the various files under _Documentation_, which detail each part of the package according to its use case and features!

---

## Features

Here’s a peek into what this library offers:

### 1. **Core Utilities**
- Unified interfaces for immutable **and mutable** `Guid` and ID-based unique identifiers.
- Handy methods for working with strings, vectors, and collections.
- New methods for validating C# member names and generating unique names with ease.
- Extended support for collection manipulation, including dynamic resizing of arrays.
- A lightweight interface for structured data retrieval.

### 2. **Randomization Utilities**
- Retrieve random elements from collections or Unity's `Bounds`.
- Shuffle lists using the Fisher-Yates algorithm (`DurstenShuffle`).
- Generate random long and double values within custom ranges.

### 3. **Extensive Collection Utilities**
- Extensions for `SortedList<TKey, List<TValue>>` for streamlined removal, cleanup, and operations.

### 4. **Animation Utilities**
- Tools to smoothly normalize and map values for animations.
- Integrate `AnimationCurve` for non-linear interpolation.
- Debug and validate animated values at runtime.

### 5. **Math Utilities**
- Normalize angles to `-180°` to `180°` or Euler rotations for consistent rotational logic.
- Simplify handling of animation curves (e.g., inversion, linear detection).

### 6. **Interpolation Utilities**
- Functions for smooth animations and transitions.

    - **DecayTowards**: Smoothly decay any value toward a target.
    - **Easing Functions**: Built-in easing styles like Quad, Cubic, Sine, Bounce, and Back.
  > _Disclaimer_: Easing functions have not yet been tested! This section is still under active development.

### 7. **Combinatorics Utilities**
- Advanced operations for collection comparisons and processing.

    - **PrepareForUniqueComparison**: Merge collections into unique combined arrays.
    - **PrepareForUniqueComparisonBetween**: Split collections into unique and shared subsets.
    - **MakeUnique**: Remove duplicates from lists or arrays.
    - **ProcessUniquePairsRoutine**: Coroutine for iterating unique pairs within a collection.
    - **ProcessBipartiteSlice**: Coroutine for handling pairs across two collections.

### 8. **Low-Level Utilities**
- Essential components for Unity's player loop and custom processing pipelines.
  > _Inspired by git-amend's own [Player Loop tutorial](https://youtu.be/ilvmOQtl57c)! Check it out!_

### 9. **IO Utilities**
- Simple file and directory solutions for Unity and general-purpose IO tasks.
  > _Disclaimer_: This section was written early in my experience and is in need of refactoring. Updates in progress!

### 10. **Rendering Utilities**
- Focused on materials and renderers.
  > _Disclaimer_: Still very early in development!

### 11. **Testing Utilities**
- Debug utilities for validating early logic and concepts during development.
  > _Disclaimer_: Absolutely still in the early stages! This is a pretty bare bones (but still useful) utility. It complements, but **does not replace**, unit testing!

### 12. **Programming Patterns**

#### Submodule: Commands
- Implements the **Flyweight Command Pattern** using Unity's `ScriptableObject`.
- Provides reusable, decoupled, state-independent commands:
  - Commands with target and single input data (`CommandActionSO<On, With>`).
  - Commands operating only on a target object (`CommandActionSO<On>`).
  - Commands with target and variadic input data (`CommandActionMultiSO<On, WithA, WithB>`, `CommandActionParamsSO<On, With>`).
- Ideal for reusable design and clean code organization.


---

## Installation

To use the utilities in your Unity project:

1. Install ["com.psigenvision.utilities.native"](https://github.com/kokogamedev/Unity-Native-Scripting-Utilities.git) via Git URL or a local Unity folder.
2. Start using the utilities by including the required namespaces in your scripts. For example:
   ```c#
   using PsigenVision.Utilities.Randomization;
   ```

---

## Contributing

1. Fork the repository.
2. Create a new branch.
3. Submit a pull request with detailed information about your changes.

---

## Issues or Feedback?

If you encounter any bugs or have suggestions for improvements, please [open an issue](https://github.com/kokogamedev/Unity-Native-Scripting-Utilities/issues). I'd love your feedback!

---

## License

This project is licensed under the **MIT License**. See the [LICENSE](LICENSE) file for details.

---

## Final Notes

Thank you for checking out **PsigenVision Utilities**!

This library is a culmination of features and functions that grew with me on my learning journey. Hopefully, it will help with yours and make development in Unity easier and more fun. 🚀

---