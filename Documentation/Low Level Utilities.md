# Low-Level Utilities

The **Low-Level Utilities** module provides tools to interact with core Unity systems at a lower level, offering advanced flexibility for custom systems. These utilities can be leveraged for tasks such as modifying Unity’s Player Loop System, enabling refined control over update cycles, and creating performance-optimized workflows.

---

## Classes

### 1. `PlayerLoopUtils`

#### Overview
The **`PlayerLoopUtils`** class is a set of static methods for modifying and working with Unity’s **Player Loop System**. The Player Loop represents Unity's update cycle, where systems like physics, rendering, and input processing are executed.

**Key Features**:
- Insert custom systems into Unity’s Player Loop at runtime.
- Traverse and manipulate the Player Loop hierarchy using recursive graph traversal.

**Disclaimer**:
This utility was inspired by a tutorial from git-amend (link: [PlayerLoop Tutorial](https://youtu.be/ilvmOQtl57c)) and, while functional, may require further optimization for broader use cases.

---

### `PlayerLoopUtils`

#### Methods

##### 1. `InsertSystem`
```c#
public static bool InsertSystem<T>(ref PlayerLoopSystem loop, in PlayerLoopSystem systemToInsert, int index)
```

- **Description**:
  Inserts a **PlayerLoopSystem** into Unity’s Player Loop graph under the hierarchy of a specified subsystem type (`T`). Ideal for inserting custom logic during core engine update cycles (e.g., custom simulation or cleanup routines).

- **Parameters**:
	- `PlayerLoopSystem loop`: The Player Loop graph to modify.
	- `PlayerLoopSystem systemToInsert`: The custom system to be inserted.
	- `int index`: The target position within the subsystem list of `T`.

- **Returns**:
	- `bool`: Returns `true` if the insertion succeeds; otherwise, `false`.

- **Details**:
	- Performs **recursive traversal** of the Player Loop graph to find the desired subsystem of type `T`.
	- Upon identifying the target system, it caches its existing subsystems into a list, inserts the new system at the specified index, and reassigns the updated list back to the `PlayerLoopSystem`.

- **Usage**:
  ```c#
  // Define a new PlayerLoopSystem
  PlayerLoopSystem customSystem = new PlayerLoopSystem
  {
      type = typeof(CustomSystem),
      updateDelegate = () => Debug.Log("Custom update logic executed!")
  };

  // Retrieve the current Player Loop
  var currentLoop = PlayerLoop.GetCurrentPlayerLoop();

  // Insert the custom system under PreLateUpdate at index 0
  if (PlayerLoopUtils.InsertSystem<UnityEngine.PlayerLoop.PreLateUpdate>(ref currentLoop, in customSystem, 0))
  {
      PlayerLoop.SetPlayerLoop(currentLoop);
      Debug.Log("Custom system inserted successfully!");
  }
  ```

- **Best Practices**:
	- Carefully select the insertion point to avoid disrupting Unity’s critical systems.
	- Test thoroughly to ensure custom systems align well with the engine’s update order.

---

##### 2. `HandleSubSystemLoop`
```c#
private static bool HandleSubSystemLoop<T>(ref PlayerLoopSystem loop, in PlayerLoopSystem systemToInsert, int index)
```

- **Description**:
  A **private helper method** used by `InsertSystem` to traverse the Player Loop hierarchy and locate the target **subsystem** of the specified type (`T`).

- **Parameters**:
	- `PlayerLoopSystem loop`: The current Player Loop node to traverse.
	- `PlayerLoopSystem systemToInsert`: The custom system to insert.
	- `int index`: The position to insert the system in the subsystem list.

- **Returns**:
	- `bool`: Returns `true` if the insertion succeeds; otherwise, `false`.

- **Details**:
	- Checks whether the current `PlayerLoopSystem` node has subsystems (`subSystemList`).
	- Iterates over subsystems recursively. If a match is found, attempts insertion using `InsertSystem`.

- **Notes**:
	- This method operates **recursively**, which may lead to performance concerns for extremely deep or complex Player Loop hierarchies.

---

### Best Practices for `PlayerLoopUtils`

1. **Performance and Debugging**:
	- **Debug Logging**: Use debug logs (like in the commented `PrintPlayerLoop` method) to analyze the current Player Loop structure during development.
	- Protect against edge cases like null subsystems, invalid indices, or overly deep recursion.

2. **Subsystem Placement**:
	- Insert custom systems at strategic points without impacting Unity’s built-in workflows. For example:
		- **Before or after rendering** for visual effects.
		- **During PreLateUpdate** for physics callbacks.

3. **Backup Original Player Loop**:
	- Always store a copy of the original Player Loop before applying changes to allow for reverting to Unity’s default behavior if needed.

---

### Expansion Roadmap

1. **Enhanced Subsystem Management**:
	- Build higher-level utilities for common tasks, such as removing or replacing subsystems.
	- Provide overloaded variants of `InsertSystem` for simpler use cases (e.g., automatic position calculation).

2. **Graph Inspector**:
	- Develop a debugging tool to visualize the Player Loop hierarchy in the Unity Editor. This would complement and simplify the recursive graph traversal approach.

3. **Error Handling**:
	- Add exception handling or fail-safe mechanisms to warn developers about invalid operations (e.g., trying to overwrite critical systems).

4. **Performance Optimizations**:
	- Explore ways to improve recursion efficiency, especially for production environments with high dependency on Player Loop customization.

5. **Runtime Flexibility**:
	- Test and adapt Player Loop modifications for runtime platforms to ensure compatibility across different builds (e.g., WebGL, mobile).

---

### Example Applications

1. **Custom Gameplay Loop**:
   Implement custom logic to be called before Unity updates its physics system:
   ```c#
   PlayerLoopSystem customLoop = new PlayerLoopSystem
   {
       type = typeof(MyCustomUpdate),
       updateDelegate = () => Debug.Log("Custom gameplay logic executed.")
   };

   // Update Player Loop with the custom system
   var loop = PlayerLoop.GetCurrentPlayerLoop();
   PlayerLoopUtils.InsertSystem<UnityEngine.PlayerLoop.FixedUpdate>(ref loop, in customLoop, 0);
   PlayerLoop.SetPlayerLoop(loop);
   ```

2. **Custom Cleanup**:
   Add a system for end-of-frame cleanup, such as memory allocation tracking:
   ```c#
   PlayerLoopSystem cleanupSystem = new PlayerLoopSystem
   {
       type = typeof(EndOfFrameSystem),
       updateDelegate = () => Debug.Log("End of frame cleanup executed!")
   };

   // Insert the cleanup system into PostLateUpdate phase
   var loop = PlayerLoop.GetCurrentPlayerLoop();
   PlayerLoopUtils.InsertSystem<UnityEngine.PlayerLoop.PostLateUpdate>(ref loop, in cleanupSystem, 1);
   PlayerLoop.SetPlayerLoop(loop);
   ```

---

## Namespace
All related utilities are available under:
```c#
PsigenVision.Utilities.LowLevel
```
---

---

Let me know if the documentation needs further refinement or extra details! I’m happy to tweak or expand on any part if necessary. 😊