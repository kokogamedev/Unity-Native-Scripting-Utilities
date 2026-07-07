using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Collections;

namespace PsigenVision
{
    /// <summary>
    /// Provides functionality for tracking the status of scenes in a Unity application.
    /// </summary>
    /// <remarks>
    /// The <see cref="SceneTracker"/> class is a static utility responsible for managing and tracking the
    /// lifecycle status of scenes. It maintains scene status information, and it offers methods and events
    /// for querying and monitoring changes in scene states.
    /// </remarks>
    public static class SceneTracker
    {
        /// <summary>
        /// An event that is triggered whenever the status of a scene changes.
        /// </summary>
        /// <remarks>
        /// The status change occurs when a scene is loaded or unloaded. The event provides the build index of the scene
        /// and its new status as parameters. This allows subscribers to respond dynamically to scene lifecycle changes.
        /// Possible statuses are defined in the <see cref="SceneTracker.Status"/> enumeration:
        /// - <see cref="SceneTracker.Status.Loaded"/>: Indicates that the scene has been successfully loaded.
        /// - <see cref="SceneTracker.Status.Unloaded"/>: Indicates that the scene has been unloaded.
        /// The event may be null if no listeners are subscribed. It is important to add appropriate null checks or ensure
        /// subscribers are managed properly to avoid <see cref="System.NullReferenceException"/>.
        /// </remarks>
        public static event Action<int, Status> OnStatusChanged;

        /// <summary>
        /// A read-only property that provides the current status of all scenes in the build settings.
        /// </summary>
        /// <remarks>
        /// The property returns a <see cref="Unity.Collections.NativeArray{T}"/> where each element corresponds to the status of a scene.
        /// The index of the array matches the build index of the scene in the Unity build settings, and the value represents the scene's status.
        /// Possible statuses are defined in the <see cref="SceneTracker.Status"/> enumeration:
        /// - <see cref="SceneTracker.Status.Invalid"/> (0): Indicates an invalid status or uninitialized state.
        /// - <see cref="SceneTracker.Status.Unloaded"/> (1): Indicates that the scene is currently unloaded.
        /// - <see cref="SceneTracker.Status.Loaded"/> (2): Indicates that the scene is currently loaded.
        /// </remarks>
        public static NativeArray<int> SceneStatus => sceneStatus;
        private static NativeArray<int> sceneStatus;

        /// <summary>
        /// Initializes the SceneTracker by setting up the necessary data structures, default values,
        /// and event subscriptions to track the status of scenes in the application.
        /// </summary>
        /// <remarks>
        /// This method is automatically invoked before the first scene is loaded in the application.
        /// It allocates persistent memory for scene status tracking, sets default statuses for all
        /// scenes, subscribes to scene load/unload events, and ensures proper resource cleanup upon application exit.
        /// </remarks>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            //Clean up any potential leftover native memory from editor domain reloads
            if (SceneStatus.IsCreated)
                sceneStatus.Dispose();
            
            //Allocate persistant memory for the scene status tracking array
            sceneStatus = new NativeArray<int>(SceneManager.sceneCountInBuildSettings, Allocator.Persistent);
            
            // Default all scenes to Unloaded (1) instead of Invalid (0)
            for (int i = 0; i < SceneStatus.Length; i++)
            {
                sceneStatus[i] = (int)Status.Unloaded;
            }
            
            //Subscribe to scene events
            SceneManager.sceneLoaded -= OnSceneLoaded; //Protect against multiple subscriptions
            SceneManager.sceneLoaded += OnSceneLoaded;
            
            SceneManager.sceneUnloaded -= OnSceneUnloaded; //Protect against multiple subscriptions
            SceneManager.sceneUnloaded += OnSceneUnloaded;
            
            //Subscribe to application quit to safely dispose memory
            LifecycleTracker.applicationQuitting -= Dispose; //Protect against multiple subscriptions
            LifecycleTracker.applicationQuitting += Dispose;
        }

        /// <summary>
        /// Retrieves the status of a scene based on its build index.
        /// </summary>
        /// <param name="buildIndex">The build index of the scene whose status is being queried.</param>
        /// <returns>The status of the scene as a <see cref="Status"/> enum value. Returns <see cref="Status.Invalid"/> if the build index is invalid or out of range.</returns>
        public static Status GetStatus(int buildIndex)
        {
            if (buildIndex < 0)
            {
                Debug.LogError("Cannot get status of a scene with a negative buildIndex");
                return Status.Invalid;
            }
            
            if (buildIndex >= sceneStatus.Length)
            {
                Debug.LogError("Cannot get status of a scene whose buildIndex exceeds the number in the build settings");
                return Status.Invalid;
            }
            
            return (Status)sceneStatus[buildIndex];
        }

        /// <summary>
        /// Retrieves the status of a scene based on a given Scene object.
        /// </summary>
        /// <param name="scene">The Scene object whose status is being queried.</param>
        /// <returns>The status of the scene as a <see cref="SceneTracker.Status"/> enum value. Returns <see cref="SceneTracker.Status.Invalid"/> if the scene's build index is invalid or not included in the build settings.</returns>
        public static Status GetStatus(Scene scene)
        {
            if (scene.buildIndex < 0)
            {
                Debug.LogError("Cannot get status of a scene that is not contained in the build settings");
                return Status.Invalid;
            }
            
            return (Status)sceneStatus[scene.buildIndex];
        }

        /// <summary>
        /// Handles the event when a scene is unloaded, updating its status and notifying subscribers.
        /// </summary>
        /// <param name="scene">The scene that has been unloaded.</param>
        private static void OnSceneUnloaded(Scene scene)
        {
            if (scene.buildIndex < 0) return;
            sceneStatus[scene.buildIndex] = 1;
            OnStatusChanged?.Invoke(scene.buildIndex, Status.Unloaded);
        }

        /// <summary>
        /// Handles the event triggered when a scene is loaded and updates the scene status accordingly.
        /// </summary>
        /// <param name="scene">The scene that was loaded.</param>
        /// <param name="mode">The mode in which the scene was loaded.</param>
        private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.buildIndex < 0) return;
            sceneStatus[scene.buildIndex] = 2;
            OnStatusChanged?.Invoke(scene.buildIndex, Status.Loaded);
        }

        /// <summary>
        /// Disposes of allocated resources and unsubscribes from relevant events to ensure proper cleanup.
        /// Should be invoked when the application is quitting to release persistent native memory used for scene status tracking.
        /// </summary>
        private static void Dispose()
        {
            LifecycleTracker.applicationQuitting -= Dispose;
            Cleanup();
        }

        /// <summary>
        /// Cleans up resources and listeners used by the SceneTracker, ensuring proper management of memory and preventing memory leaks.
        /// </summary>
        /// <remarks>
        /// This method removes event subscriptions, disposes of the <see cref="SceneStatus"/> NativeArray, and resets the <see cref="OnStatusChanged"/> delegate to null.
        /// It is primarily called during the application shutdown or when SceneTracker is no longer needed.
        /// </remarks>
        private static void Cleanup()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
            OnStatusChanged = null;
            
            //Dispose NativeArray to prevent native memory leaks
            if (SceneStatus.IsCreated) sceneStatus.Dispose();
        }

        /// <summary>
        /// Represents the status of a scene in the build settings or runtime environment.
        /// </summary>
        /// <remarks>
        /// This enum is used to track and manage scenes within the application. Each scene can
        /// have one of the following statuses:
        /// - <see cref="Invalid"/>: The scene index is invalid or does not correspond to a valid scene.
        /// - <see cref="Unloaded"/>: The scene is currently not loaded in memory.
        /// - <see cref="Loaded"/>: The scene is currently loaded and active in memory.
        /// </remarks>
        public enum Status
        {
            Invalid = 0,
            Unloaded = 1,
            Loaded = 2
        }
    }
}