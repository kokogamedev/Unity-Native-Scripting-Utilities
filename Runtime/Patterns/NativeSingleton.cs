using System;

namespace PsigenVision.Utilities.Singleton
{
    /// <summary>
    /// Provides a generic singleton implementation for pure C# types that require a single instance.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the singleton instance. Must be a class with a parameterless constructor.
    /// </typeparam>
    /// <remarks>
    /// This class ensures that only one instance of type <typeparamref name="T"/> can exist at any given time.
    /// If the type implements <see cref="IDisposable"/>, the instance will automatically be disposed when
    /// the application is quitting, provided it subscribes to the <see cref="LifecycleTracker.applicationQuitting"/> event.
    /// </remarks>
    /// <example>
    /// This class is intended to be inherited by a target class that requires singleton behavior.
    /// </example>
    public abstract class NativeSingleton<T> where T : class, new()
    {
        /// <summary>
        /// Represents the internal instance of the singleton class of type <typeparamref name="T"/>.
        /// </summary>
        /// <remarks>
        /// This variable is used to ensure that only one instance of the class exists throughout the application's lifecycle.
        /// The instance is lazily initialized when accessed through the <see cref="NativeSingleton{T}.Instance"/> property.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the singleton class. Must be a class with a parameterless constructor.
        /// </typeparam>
        protected static T instance;

        /// <summary>
        /// Indicates whether the singleton instance of type <typeparamref name="T"/> has been instantiated.
        /// </summary>
        /// <remarks>
        /// This variable is used to track the instantiation state of the singleton.
        /// It is set to <c>true</c> when the instance is first created and remains
        /// <c>true</c> for the lifetime of the application unless explicitly reset or disposed.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the singleton class associated with this variable. Must be a class with a parameterless constructor.
        /// </typeparam>
        protected static bool hasInstance = false;

        /// <summary>
        /// Indicates whether an instance of the singleton class of type <typeparamref name="T"/> has been created.
        /// </summary>
        /// <remarks>
        /// This property determines if the singleton instance has been instantiated during the application's lifecycle.
        /// The value is <c>true</c> if the instance has been created; otherwise, <c>false</c>.
        /// It ensures that initialization logic depending on the existence of the singleton instance can be conditionally executed.
        /// </remarks>
        /// <typeparam name="T">
        /// The type of the singleton class. Must be a class with a parameterless constructor.
        /// </typeparam>
        public static bool HasInstance => hasInstance;

        /// Gets the singleton instance of the specified type, ensuring that only one instance exists.
        /// If the instance does not already exist, it is created and hooked into a global cleanup mechanism.
        /// Thread-safe lazy initialization for retrieving or creating the instance of type <typeparamref name="T"/>.
        /// Once the instance is initialized, it will persist for the application's lifetime,
        /// or until explicitly disposed via the associated global cleanup mechanism.
        /// Throws:
        /// Throws no exceptions during normal usage. If an error occurs during instantiation of <typeparamref name="T"/>,
        /// an exception originating from the type's constructor might propagate.
        public static T Instance
        {
            get
            {
                if (!hasInstance)
                {
                    instance ??= new T();
                    // Optional: Hook into a global cleanup if needed
                    LifecycleTracker.applicationQuitting -= Dispose;
                    LifecycleTracker.applicationQuitting += Dispose;
                    hasInstance = instance != null;
                }
                return instance;
            }
        }
        
        /// <summary>
        /// Releases all resources and handles cleanup prior to the application quitting.
        /// This includes unhooking from the global cleanup event and disposing of
        /// the singleton instance if it implements <see cref="IDisposable"/>.
        /// </summary>
        public static void Dispose()
        {
            LifecycleTracker.applicationQuitting -= Dispose;
            (instance as IDisposable)?.Dispose(); // If your type is disposable
            instance = null;
        }
    }
}