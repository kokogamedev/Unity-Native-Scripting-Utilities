# Testing Utilities

The **Testing Utilities** package provides an abstract base class for creating simulation-based tests within Unity. This utility simplifies the process of scripting and executing testing scenarios by breaking them down into simulation steps. It is especially useful for prototyping and gameplay logic testing.

---

## Overview

### Purpose
The `TestBaseClass` is an abstract class designed to help developers simulate, test, and debug functionalities in Unity projects. The class provides:
- A structured simulation flow with lifecycle stages: **StartSimulation**, **NextStep**, and **EndSimulation**.
- Easy-to-trigger simulation stages using customizable key bindings.
- Functionality for caching/restoring data for repeatable simulations.
- Extendable hooks for customized behavior.

### Key Features
- **Simulation Steps**: Define step-by-step testing logic using a list of `Action` delegates.
- **Keyboard Shortcuts**: Trigger key simulation actions (`StartSimulation` and `NextStep`) through configurable Unity `KeyCode` bindings.
- **Hooks for Customization**: Override methods to define data initialization, simulation logic, and simulation stage event reporting.
- **Reset and Rerun**: Restore cached data and reset state to rerun simulations iteratively.

---

## Configuration and Usage

### Namespace
All related utilities are available under:
```c#
PsigenVision.Utilities.Testing
```

### Base Class
The `TestBaseClass` is abstract and must be inherited to create concrete test classes. Subclasses are required to define key simulation hooks for initialization, data handling, and reporting.

---

## Example

Here’s an example of how you can use the `TestBaseClass` to create a basic test for a simulation.

```c#
using System;
using UnityEngine;
using PsigenVision.Utilities.Testing;

public class MySimulationTest : TestBaseClass
{
    private int testData;
    private int cachedData;

    protected override void InitializeData()
    {
        testData = 0; // Initialize default test data.
        Debug.Log("Data Initialized");
    }

    protected override void StoreCachedData()
    {
        cachedData = testData; // Cache the initial state.
        Debug.Log("Cached Data Stored");
    }

    protected override void RestoreDataFromCache()
    {
        testData = cachedData; // Restore cached state.
        Debug.Log("Data Restored to Cached State");
    }

    protected override void InitializeSteps()
    {
        simulationSteps.Add(Step1);
        simulationSteps.Add(Step2);
        simulationSteps.Add(Step3);
    }

    protected override void ReportSimulationStart()
    {
        Debug.Log("Simulation Started");
    }

    protected override void ReportSimulationReset()
    {
        Debug.Log("Simulation Reset");
    }

    protected override void ReportSimulationEnd()
    {
        Debug.Log("Simulation Ended");
    }

    private void Step1()
    {
        testData++;
        Debug.Log($"Step 1 completed. Test Data: {testData}");
    }

    private void Step2()
    {
        testData *= 2;
        Debug.Log($"Step 2 completed. Test Data: {testData}");
    }

    private void Step3()
    {
        testData -= 3;
        Debug.Log($"Step 3 completed. Test Data: {testData}");
    }
}
```

### How It Works
1. **Key Bindings**:
	- Press `S` to start the simulation.
	- Press `N` to execute the next step in the simulation.
	- Press `S` during the simulation to reset and restart it.
	- All key bindings are configurable via the `startSimulation` and `executeNextStepInSimulation` properties.

2. **Step Lifecycle**:
	- The simulation execution steps (`Step1`, `Step2`, etc.) are defined by overriding the `InitializeSteps` method and adding actions to the `simulationSteps` list.
	- Each step is executed sequentially when the "Next" key is pressed.

3. **Reset and Rerun**:
	- When the simulation ends or is reset, the data is restored to its initial state using the cached values.

---

## Key Components

### Public/Protected Properties
| Property                   | Description                                                                                   |
|----------------------------|-----------------------------------------------------------------------------------------------|
| `simulationSteps`          | A list of `Action` delegates representing each step in the simulation.                        |
| `startSimulation`          | A `KeyCode` defining the key to start the simulation. Default is `KeyCode.S`.                   |
| `executeNextStepInSimulation` | A `KeyCode` defining the key to execute the next simulation step. Default is `KeyCode.N`.       |

### Public/Protected Methods
| Method                        | Description                                                                                       |
|-------------------------------|---------------------------------------------------------------------------------------------------|
| `StartSimulation`             | Initiates the simulation and sets the state to "started".                                         |
| `NextStep`                    | Executes the next step in the simulation.                                                        |
| `EndSimulation`               | Marks the simulation as "finished".                                                              |
| `ResetSimulation`             | Resets the simulation to its initial state by invoking `RestoreDataFromCache`.                    |
| Abstract: `ReportSimulationStart` | Customizable hook for reporting when the simulation starts.                                     |
| Abstract: `ReportSimulationReset` | Customizable hook for reporting when the simulation is reset.                                  |
| Abstract: `ReportSimulationEnd`   | Customizable hook for reporting when the simulation ends.                                     |
| Abstract: `InitializeData`        | Define logic for initializing simulation-specific data.                                        |
| Abstract: `StoreCachedData`       | Save initial state for restoring during resets.                                               |
| Abstract: `RestoreDataFromCache`  | Restore cached state when resetting the simulation.                                           |
| Abstract: `InitializeSteps`       | Define simulation steps by populating the `simulationSteps` list with `Action` delegates.     |

---

## Best Practices

1. **Modularity**: Keep the implementation of each step (`Action`) small and focused on a single operation.
2. **Testing Scenarios**: Use the `ReportSimulation*` hooks to gather logs or data during specific stages of the simulation.
3. **Extensibility**: Extend the base class to create reusable test cases without touching the base logic.

---

## Limitations
- This utility class is not designed as a replacement for modern test frameworks (e.g., NUnit or Unity Test Framework).
- Simulations primarily rely on manual key inputs for execution, making them less suited for fully automated tests.

---