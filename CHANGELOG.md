# Changelog

All notable changes to this project will be documented in this file.

The format adheres to [Keep a Changelog](https://keepachangelog.com/en/1.0.0/), and this project adheres to [Semantic Versioning](https://semver.org/).

---

## [0.9.0] - 2026-04-09

### Added
- Moved native C#/Unity utilities from the original personal mega-package into their respective categories.
- Organized and documented features across `Core`, `Randomization`, `Collection`, `Animation`, `Math`, `Interpolation`, `Combinatorics`, `IO`, `Rendering`, and `Testing` utilities.
- Added README and feature summaries for each category.
- Wrote fresh examples in Documentation for major components.
- Package restructured to follow Unity's package format for project modularity.
> This marks the first formal release of the utilities—in use in other packages, but with planned improvements, testing, completions, and refinements in future updates.
---

### Todo
- Refactor **IO Utilities** and improve compatibility with Unity workflows.
- Expand **Rendering Utilities** for handling custom shader properties.
- Add more **Testing Utilities** to ensure runtime stability and provide better debugging tools.
- Test **Easing** functions to ensure they are ready for use.
- Add more examples and test scenes that demonstrate the various utilities while implementing custom testing solution.

---

## [0.9.1] - 2026-04-10

### Changed
- Introduced new `IHaveMutableID<T>` and `IHaveMutableGuid<T>` interfaces to supplant the old `IHaveID<T>` and `IHaveGuid<T>` interfaces in order to support immutable/mutable IDs. Created new `IHaveID<T>` and `IHaveGuid<T>` interfaces that simply store IDs and implement `IEquatable<T>` (the immutable ID versions), having their mutable counterparts implement them.
- Adjusted the `GenerateID` methods for `IHaveMutableID<T>` and `IHaveMutableGuid<T>` interfaces to return the IDs they generate (`int` and `Guid` types, respectively).
- Added an overload for `IHaveMutableID<T>.GenerateID` method that accepts no parameters in the event the string name had been previously cached in place of passing an unnecessary string and incurring a performance impact.

### Chores
- Small refactoring of the FNV1a hash function implementation in `ComputeFNV1aHash` extension method - no logic impact.

---

## [0.9.2] - 2026-04-10

### Added **New Feature:** Programming Patterns.
- Introduced new submodule to the Programming Patterns feature to facilitate the implementation of the **Flyweight Command Pattern** while making use of Unity's `ScriptableObject` system for serialization and inspector support.
- Introduced abstract base classes for reusable commands in `PsigenVision.Utilities.Patterns.Commands`.
    - **`CommandActionSO<On, With>`**: Operates on a target object with associated input data.
    - **`CommandActionSO<On>`**: Operates on a target object without additional data.
    - **`CommandActionMultiSO<On, With>`**: Operates on a target object with a dynamic array of inputs.

---
## [0.9.3] - 2026-04-18

### Added to **Core Utilities**
- Introduced `IsValidMemberName` for validating C# member names against naming conventions and reserved keywords.
- Added `StringExtensions.IsValidDotSeparatedPath` to validate dot-separated paths (e.g., namespaces). Ensures each segment abides by C# member naming conventions.
- Added `AppendDigitForUniqueName` to effortlessly generate unique names using customizable numeric suffixes.
- Introduced `LengthenBy` for dynamically resizing arrays while maintaining original data, with optional reversed placement.
- Added `IDataProvider<T>` interface for lightweight and type-safe structured data retrieval.

### Added to **Programming Patterns**
- Introduced new `ref` overloads into the following methods to enhance flexibility and enable in-place data modifications:
  - `CommandActionSO<On, With>`:
    - `Execute(On obj, ref With data)`
  - `CommandActionMultiSO<On, WithA, WithB>`:
    - `Execute(On obj, ref WithA dataA, WithB dataB)`
    - `Execute(On obj, WithA dataA, ref WithB dataB)`
    - `Execute(On obj, ref WithA dataA, ref WithB dataB)`

These additions extend the Flyweight Command Pattern's support for scenarios where commands need to directly modify the data passed to them or reduce overhead for copying large structs, offering greater versatility for developers.

### Added to and Refactored **IO Utilities**
- Addition of `EnsureDirectory` extension method with the same implementation as the `EnsureDirectoryExists` helper method.
- Small refactoring of a few methods (if inversion to reduce nesting).

---

## [0.9.4] - 2026-04-19

### This is an integration of the former Editor Scripting Utils Package's Basic Extensions into Unity Native C# Scripting Extensions

### New Additions
#### Added
- **EscapeSpecialChars/EscapeSpecial**: A string helper method/extension method pair useful for processing an input string such that it escapes special characters. 

### The Previous Package's ChangeLog has been pasted below

### 2026-04-07
#### Added
- **EditorExtensions**:
  - `SetBoxedValue`: Allows updates of nested `SerializedProperty` values using a dot-separated path.
  - `GetValue` & `SetValue`: Reflection-based methods for retrieving and assigning serialized property values.
  - `GetSystemType`: Provides the `System.Type` for serialized fields.
  - `GetFieldViaPath`: Accesses `FieldInfo` for a field based on the serialized property's path.
- **ReflectionExtensions**:
  - `SetValueViaPath`: Updates nested object fields using a string-based dot-separated path.
  - `GetFieldMapsViaPath`: Resolves object and type stacks, supporting indexed collections.
  - `GetFieldViaPath`: Retrieves `FieldInfo` for nested fields using reflection.
  - `TryParseCollectionPart`: Parses and resolves indexed collection paths (e.g., "list[3]").
  - `IListMemberPointer`: Introspects and modifies list or array items for reflection operations.
- **EnumExtensions**:
  - `TryGetEnumByIndex<T>`: Dynamically retrieves an enum value based on its index order.
  - `TryGetEnumByIndex`: Resolves enum values dynamically using the field name and its definition order.

#### Notes
- This package is designed to simplify Unity editor scripting workflows by providing robust reflection-based utilities.
- The **first release** offers foundational support for serialized property handling, advanced reflection techniques, and dynamic enum manipulation.
- The package includes documentation for all key features.


### 2026-04-09

#### Included failure handling in setting of boxed methods, and refactored reflection extension methods

#### Changed
- `GetBoxedValueViaPath` renamed to `TrySetBoxedValueViaPath`, made to return the success-boolean value returned by editor extension method `SetValueViaPath`, internally sets the property's boxed value to the modified boxed value in the event of success, and outputs the modified property in a new out parameter.
- Removed redundant `SetBoxedValue` accepting a path parameter as it now performs the same function as `TrySetBoxedValueViaPath`
- Changed `Try`

#### Refactored
- `TryGetEnumByIndex` extension method off of `object` now accepts as an optional parameter a `BindingFlag`, and is, by default, set to find all non-public and public instance fields.
- `SetValueViaPath` now renamed to `TrySetValueViaPath`, and was internally refactored without modification of logic (code cleanup).


### 2026-04-17

#### Fixed Issue with Collections not being Identified for Serialized Properties

#### Fixed
- `GetFieldViaPath` was failing due to a quirk in how Unity handles collections when they are derived from `SerializedProperty`, namely using the convention "collectionName.Array.data[index]" rather than "collectionName.data[index]". A fix was enacted to detect and strip away this pattern if encountered.
- Fixed reversed safety check in `TryParseCollectionAccess` in which failure occurred when a collection index was successfully parsed.

---