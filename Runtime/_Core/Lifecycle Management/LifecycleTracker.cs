using System;
using UnityEngine;

namespace PsigenVision
{
    /// <summary>
    /// Provides lifecycle management utilities for handling application quitting events.
    /// This static class is designed to facilitate the registration and deregistration of
    /// callbacks related to application lifecycle events, specifically when the application is quitting.
    /// Note: This must account for differences between runtime and edit-mode environments,
    /// such as Unity-specific behavior where the editor's scripting environment is not reset on recompilation.
    /// </summary>
    public static class LifecycleTracker
    {
        //In the Editor, scripts recompile while the Editor stays open.
        //For this reason, Application.quitting must not be used for edit-mode - instead EditorApplication quitting must be used, necessitating two quitting callbacks
        /// <summary>
        /// Event triggered when the application is in the process of quitting.
        /// This event allows dependent systems or modules to perform necessary cleanup tasks
        /// before the application shuts down.
        /// </summary>
        public static event Action applicationQuitting;

        /// <summary>
        /// Initializes the LifecycleTracker by setting up appropriate shutdown hooks for the application's lifecycle.
        /// Depending on the runtime context (Editor or runtime build), it subscribes to the proper application quitting event:
        /// UnityEditor.EditorApplication.quitting for the Editor and Application.quitting for runtime builds.
        /// This ensures cleanup operations are triggered when the application closes.
        /// </summary>
        /// <remarks>
        /// In Unity Editor, scripts can recompile without closing the Editor. For this reason,
        /// UnityEditor.EditorApplication's quitting event is required to handle cleanup during edit-mode.
        /// In runtime builds, Application.quitting is used instead.
        /// This method is marked to execute automatically during runtime initialization using RuntimeInitializeOnLoadMethod
        /// and, for the Editor, using UnityEditor.InitializeOnLoadMethod.
        /// </remarks>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
#endif
        static void Initialize()
        {
            // Subscribe to the quitting event
#if UNITY_EDITOR
            // Editor-specific shutdown hook
            UnityEditor.EditorApplication.quitting -= OnEditorQuit;
            UnityEditor.EditorApplication.quitting += OnEditorQuit;
#else
            // Runtime-specific shutdown hook
            Application.quitting -= OnRuntimeQuit;
            Application.quitting += OnRuntimeQuit;
#endif
        }

#if UNITY_EDITOR
        /// <summary>
        /// Handles cleanup operations when the Unity Editor is quitting.
        /// This method is invoked during the Editor's quitting sequence, ensuring proper
        /// resource cleanup and event unsubscription specific to editor-only scenarios.
        /// </summary>
        static void OnEditorQuit()
        {
            UnityEditor.EditorApplication.quitting -= OnEditorQuit;
            ExecuteCleanup();
        }
#endif

        /// <summary>
        /// Handles cleanup and related tasks when the application runtime is about to quit.
        /// This method is invoked as part of the application quitting process to ensure that
        /// allocated resources and processes are appropriately released and finalized.
        /// Removes itself from the Application.quitting event and invokes internal cleanup logic
        /// via the `ExecuteCleanup` method.
        /// </summary>
        static void OnRuntimeQuit()
        {
            Application.quitting -= OnRuntimeQuit;
            ExecuteCleanup();
        }

        /// <summary>
        /// Executes the necessary cleanup operations when the application is quitting,
        /// either in runtime or editor mode.
        /// This method handles logging, invokes the <c>applicationQuitting</c> event,
        /// and ensures proper memory management by clearing the event handlers.
        /// </summary>
        static void ExecuteCleanup()
        {
            Debug.Log("Application Closing (Editor Stop or Build Exit)");
            applicationQuitting?.Invoke();
            applicationQuitting = null; // Hard clear to prevent memory leaks
        }
    }
}