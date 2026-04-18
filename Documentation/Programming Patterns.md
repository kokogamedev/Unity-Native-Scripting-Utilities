# Programming Patterns

The **Programming Patterns** module contains reusable abstractions and implementations for common and advanced design patterns, tailored for Unity projects. These patterns are developed to accelerate workflows while maintaining best practices for clean, scalable code.

---

## Submodules

### 1. `Commands`

#### Overview
The **`Commands`** submodule provides an implementation of the **Flyweight Command Pattern** using Unity's `ScriptableObject`. This framework allows commands to be serialized, reusable, and decoupled from the logic or objects they act upon.

#### Key Features
- Reusable, stateless commands for objects and game logic.
- Support for multiple variations of commands:
  - **`CommandActionSO<On, With>`**: Operates on a target object with associated data.
  - **`CommandActionSO<On>`**: Operates on a target object without requiring additional data.
  - **`CommandActionMultiSO<On, WithA, WithB>`**: Operates on a target object with two separate inputs for data of potentially different types.
  - **`CommandActionParamsSO<On, With>`**: Operates on a target object with a dynamic array of associated data.
- Unity-ready design through `ScriptableObject` for easy integration into serialization workflows and inspector tooling.

---

## Classes

### `CommandActionSO<On, With>`

#### Overview
This class is the base abstraction for commands that operate on a target object (`On`) with associated input data (`With`). It simplifies the creation of reusable logic that can be applied dynamically at runtime.

#### Methods

##### 1. `Execute(On obj, With data)`
```c#
public abstract void Execute(On obj, With data);
```

- **Description**:
  Executes the command on the specified target object with the provided input data.

- **Parameters**:
  - `On obj`: The object to operate upon.
  - `With data`: The associated data required for the command to execute.

---

##### 2. `Execute(On obj, ref With data)`
```c#
public abstract void Execute(On obj, ref With data);
```

- **Description**:
  Executes the command on the specified target object with the provided input data. Unlike the previous overload, this method uses a `ref` parameter, allowing in-place modifications to the provided data.

- **Parameters**:
  - `On obj`: The object to operate upon.
  - `With data`: The associated data used and/or modified during the command execution.

- **Usage Example**:
  ```c#
  [CreateAssetMenu(menuName = "Commands/ModifyStats")]
  public class ModifyStatsCommandSO : CommandActionSO<Player, float>
  {
      public override void Execute(Player player, ref float healthMultiplier)
      {
          player.Health *= healthMultiplier;
          healthMultiplier -= 0.1f;  // Example modification
      }
  }
  ```

---

### `CommandActionSO<On>`

#### Overview
This abstraction supports commands that require **only a target object** to execute. It eliminates the need for additional input data, making it ideal for simpler operations.

#### Methods

##### 1. `Execute(On obj)`
```c#
public abstract void Execute(On obj);
```

- **Description**:
  Executes the command on the specified target object.

- **Parameters**:
  - `On obj`: The object to operate upon.

- **Usage Example**:
  ```c#
  [CreateAssetMenu(menuName = "Commands/DisableGameObject")]
  public class DisableGameObjectCommandSO : CommandActionSO<GameObject>
  {
      public override void Execute(GameObject obj)
      {
          obj.SetActive(false);
      }
  }
  ```

---

### `CommandActionMultiSO<On, WithA, WithB>`

#### Overview
The `CommandActionMultiSO` class provides an implementation where commands operate on a target object with **two data inputs of potentially differing types**. This is particularly useful for scenarios requiring two data inputs. An example usage might be passing in TransactionData and a pointer to the BlackboardModule on which that data must being applied.

#### Methods

##### 1. `Execute(On obj, WithA dataA, WithB dataB)`
```c#
public abstract void Execute(On obj, WithA dataA, WithB dataB);
```

- **Description**:
  Executes the command on the specified target object with the provided input data.

---

##### 2. `Execute(On obj, ref WithA dataA, WithB dataB)`
```c#
public abstract void Execute(On obj, ref WithA dataA, WithB dataB);
```

- **Description**:
  Executes the command using the specified target and input data. The first data parameter (`dataA`) is passed by reference, allowing in-place modifications during execution.

---

##### 3. `Execute(On obj, WithA dataA, ref WithB dataB)`
```c#
public abstract void Execute(On obj, WithA dataA, ref WithB dataB);
```

- **Description**:
  Executes the command on the specified target with input data. The second data parameter (`dataB`) is passed by reference, allowing in-place modifications during execution.

---

##### 4. `Execute(On obj, ref WithA dataA, ref WithB dataB)`
```c#
public abstract void Execute(On obj, ref WithA dataA, ref WithB dataB);
```

- **Description**:
  Executes the command using the specified target and input data, with both data parameters (`dataA` and `dataB`) passed by reference for in-place modifications.

- **Usage Example**:
  ```c#
  [CreateAssetMenu(menuName = "Commands/AdjustStats")]
  public class AdjustStatsCommandSO : CommandActionMultiSO<Player, float, float>
  {
      public override void Execute(Player player, ref float healthMultiplier, ref float staminaMultiplier)
      {
          player.Health *= healthMultiplier;
          player.Stamina *= staminaMultiplier;
          healthMultiplier -= 0.1f;
          staminaMultiplier -= 0.2f;
      }
  }
  ```
---
### `CommandActionParamsSO<On, With>`

#### Overview
The `CommandActionParamsSO` class provides an implementation where commands operate on a target object with a **dynamic array of data**. This is particularly useful for scenarios involving multiple inputs, such as applying a sequence of instructions or combining data dynamically.

#### Methods

##### 1. `Execute(On obj, params With[] data)`
```c#
public abstract void Execute(On obj, params With[] data);
```

- **Description**:
  Executes the command on the specified target object with a variadic array of input data.

- **Parameters**:
  - `On obj`: The object to operate upon.
  - `params With[] data`: A dynamic array of the associated data.

- **Usage Example**:
  ```c#
  [CreateAssetMenu(menuName = "Commands/Rigidbody/AddImpulses")]
  public class AddImpulsesCommandSO : CommandActionParamsSO<Rigidbody, Vector3>
  {
      public override void Execute(Rigidbody obj, params Vector3[] forces)
      {
          foreach (var force in forces)
          {
              obj.AddForce(force, ForceMode.Impulse);
          }
      }
  }
  ```
---

### Usage Notes
1. Combine these reusable commands with UnityEvents, custom scriptable architectures, or Event Systems to design clean, modular systems.
2. Place concrete implementations in **well-defined folders** organized by their purpose (e.g., Movement, Interaction, GameObject Lifecycle).

---

## Namespace
All `CommandSO` classes and their variations are part of:
```c#
PsigenVision.Utilities.Patterns.Commands
```
---