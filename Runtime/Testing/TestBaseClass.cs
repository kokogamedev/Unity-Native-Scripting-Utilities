using System;
using System.Collections.Generic;
using UnityEngine;

namespace PsigenVision.Utilities.Testing
{
    /// <summary>
    /// An abstract base class providing a framework for testing simulation workflows.
    /// </summary>
    /// <remarks>
    /// This class leverages Unity's MonoBehaviour lifecycle methods (Start and Update)
    /// to manage the execution flow of simulation tests, including initializing data,
    /// storing and restoring cache, handling simulation steps, and managing simulation states.
    /// It is intended to be extended by concrete implementation classes to define specific behaviors.
    /// </remarks>
    public abstract class TestBaseClass : MonoBehaviour
    {
        /// <summary>
        /// A collection of simulation steps represented as actions to be executed in sequence.
        /// Each action defines a procedure or behavior to be performed as part of the simulation process.
        /// This list is initialized and populated during the setup phase and serves as the foundation
        /// for controlling the individual steps of the simulation.
        /// </summary>
        protected List<Action> simulationSteps = new List<Action>();

        /// <summary>
        /// Represents the key used to start the simulation in the application.
        /// This variable is configurable and can be assigned to a specific
        /// Unity KeyCode to trigger the start of the simulation sequence.
        /// </summary>
        [SerializeField] protected KeyCode startSimulation = KeyCode.S;

        /// <summary>
        /// Represents the key binding used to trigger the execution of the next step
        /// in the simulation process.
        /// </summary>
        /// <remarks>
        /// This key is checked during the Update loop to determine if the user wants to
        /// proceed to the next step of the simulation. When pressed, the simulation will
        /// move forward by invoking the next step in the ordered list of simulation steps
        /// defined in the derived class.
        /// </remarks>
        [SerializeField] protected KeyCode executeNextStepInSimulation = KeyCode.N;

        /// <summary>
        /// Indicates whether the simulation has started or not.
        /// </summary>
        /// <remarks>
        /// This boolean variable is used to control the flow of the simulation.
        /// It is set to true when the simulation is initiated and false when the simulation ends or is reset.
        /// The variable is updated in methods such as StartSimulation, EndSimulation, and ResetSimulation.
        /// </remarks>
        protected bool started = false;

        /// <summary>
        /// Indicates whether the simulation has finished executing all its steps.
        /// </summary>
        /// <remarks>
        /// This variable is set to true when the simulation completes all its steps
        /// or the simulation explicitly ends. It is reset to false when the simulation
        /// is restarted or reset.
        /// </remarks>
        protected bool finished = false;

        /// <summary>
        /// Represents the current index or position in the simulation workflow, tracking
        /// which step is to be executed next within the sequence of simulation steps.
        /// Incremented as each step in the simulation progresses.
        /// </summary>
        protected int step;

        // Start is called before the first frame update
        /// <summary>
        /// A Unity MonoBehaviour lifecycle method that is called before the first frame update.
        /// This method is typically used for initialization tasks such as setting up data,
        /// storing cached data, and defining the sequence of simulation steps.
        /// </summary>
        /// <remarks>
        /// This method is marked as `protected virtual`, meaning it is intended to be overridden
        /// in derived classes to customize additional initialization behavior, while still
        /// invoking the base class implementation.
        /// </remarks>
        protected virtual void Start()
        {
            InitializeData();
            StoreCachedData();
            InitializeSteps();
        }

        // Update is called once per frame
        /// <summary>
        /// Updates the component state on every frame. Handles user input to start, step through,
        /// reset, or restart a simulation. Also manages logic specific to the inherited
        /// behavior within derived classes.
        /// </summary>
        /// <remarks>
        /// This method calls the parent class Update method to execute common simulation control logic.
        /// Additionally, it includes specific handling for input checks, such as printing sorted lists
        /// or running data-specific tests. Derived classes can override this method
        /// to extend or modify the behavior.
        /// </remarks>
        /// <seealso cref="MonoBehaviour.Update"/>
        protected virtual void Update()
        {
            if (!started && Input.GetKeyDown(startSimulation))
            {
                if (!started) StartSimulation();
            }

            if (started && Input.GetKeyDown(executeNextStepInSimulation))
            {
                if (!finished) NextStep();
            }

            if (started && !finished && step != 0 && Input.GetKeyDown(startSimulation))
            {
                ResetSimulation();
                StartSimulation();
            }

            if (finished)
            {
                ResetSimulation();
            }
        }

        #region Simulation Execution Methods

        /// <summary>
        /// Starts the simulation by setting the necessary flags to indicate the simulation
        /// has begun. Resets the 'finished' flag to false and sets the 'started' flag to true.
        /// Also triggers the simulation start reporting mechanism. This method is intended to
        /// be overridden in derived classes to include additional behavior when the simulation begins.
        /// </summary>
        protected virtual void StartSimulation()
        {
            started = true;
            finished = false;
            ReportSimulationStart();
        }

        /// <summary>
        /// Executes the next step in the simulation sequence.
        /// </summary>
        /// <remarks>
        /// This method is called during the simulation process to advance to the next predefined action
        /// in the sequence of simulation steps. If all steps have been completed, the simulation will
        /// end and the corresponding end logic will be executed. This function ensures the correct
        /// invocation of actions stored in the simulationSteps list and increments the step counter.
        /// </remarks>
        private void NextStep()
        {
            if (step == simulationSteps.Count)
            {
                EndSimulation();
                return;
            }

            simulationSteps[step].Invoke();
            step++;
        }

        /// <summary>
        /// Marks the simulation as finished and performs necessary actions to handle the end of the simulation.
        /// </summary>
        /// <remarks>
        /// This method sets the simulation state to finished and invokes any required operations or
        /// notifications to signal the end of the simulation process. It is typically called once
        /// all simulation steps have been executed.
        /// </remarks>
        protected virtual void EndSimulation()
        {
            finished = true;
            ReportSimulationEnd();
        }

        /// <summary>
        /// Resets the simulation state to its initial condition, clearing any progress or changes made during the simulation.
        /// This method restores data from the cached state, resets control flags (such as `started` and `finished`),
        /// and sets the step counter back to zero. Additionally, it invokes the process to report the reset of the simulation.
        /// </summary>
        protected virtual void ResetSimulation()
        {
            RestoreDataFromCache();
            finished = false;
            started = false;
            step = 0;
            ReportSimulationReset();
        }
        
        #endregion

        #region Simulation Stage Reporting

        /// <summary>
        /// Reports the start of the simulation process.
        /// This method is invoked when a simulation begins, and it serves
        /// as a notification mechanism to log or perform actions indicating
        /// the commencement of a simulation sequence.
        /// </summary>
        /// <remarks>
        /// This method must be implemented by derived classes to define
        /// specific behaviors or logging mechanisms triggered at the onset
        /// of simulation. It is part of the abstract contract in the
        /// TestBaseClass for handling simulation lifecycle events.
        /// </remarks>
        protected abstract void ReportSimulationStart();

        /// <summary>
        /// This method is responsible for reporting when the simulation has been reset.
        /// It is an abstract method to be implemented by derived classes, allowing customization
        /// of the behavior or message displayed/logged when a simulation reset occurs.
        /// Implementations can include logging indicators, resetting UI elements, or other reset-related operations.
        /// </summary>
        protected abstract void ReportSimulationReset();

        /// <summary>
        /// Reports the end of a simulation process. This method is intended to be overridden
        /// in derived classes to define specific behavior or logging when the simulation ends.
        /// </summary>
        protected abstract void ReportSimulationEnd();
        #endregion

        #region Data Handling

        /// <summary>
        /// Initializes the data required for the simulation or testing process.
        /// This method is meant to be implemented by derived classes to set up
        /// any necessary data or preconditions specific to the simulation or test.
        /// </summary>
        protected abstract void InitializeData();

        /// <summary>
        /// This method is responsible for storing cached data to allow for restoration during simulation resets or other operations.
        /// Implementations of this method should define the logic required to save relevant runtime data into a desired cache structure
        /// to facilitate data recovery when needed.
        /// </summary>
        protected abstract void StoreCachedData();

        /// <summary>
        /// Restores data to its state from the cache. Typically called to reset or reinitialize
        /// critical components or data structures during simulation.
        /// </summary>
        /// <remarks>
        /// This method is abstract in the base class and requires an implementation in derived classes.
        /// It is typically used within the context of a testing or simulation framework.
        /// Derived implementations vary based on the needs of each simulation subclass.
        /// </remarks>
        protected abstract void RestoreDataFromCache();
        
        #endregion
        
        #region Steps

        /// <summary>
        /// Abstract method to define the initialization of simulation steps.
        /// Subclasses must override this method to populate the list of simulation steps with specific test actions.
        /// Each step represents a unit of simulation logic to be executed.
        /// </summary>
        protected abstract void InitializeSteps();

        #endregion
    }
}