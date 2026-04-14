using UnityEngine;

namespace PsigenVision.Utilities.Patterns.Command
{
    /// <summary>
    /// A generic abstract base class for implementing the Flyweight Command Pattern. representing a ScriptableObject designed to execute a command
    /// on a specific target type with associated data.
    /// </summary>
    /// <typeparam name="On">The type of the target on which the command operates.</typeparam>
    /// <typeparam name="With">The type of the data associated with the command.</typeparam>
    public abstract class CommandActionSO<On, With>: ScriptableObject
    {
        /// <summary>
        /// Executes a command using the provided object and additional data.
        /// </summary>
        /// <typeparam name="On">The type of object the command is executed upon.</typeparam>
        /// <typeparam name="With">The type of data passed to the command.</typeparam>
        /// <param name="obj">The object on which the command is executed.</param>
        /// <param name="data">The additional data used/required during the execution of the command.</param>
        public abstract void Execute(On obj, With data);
    }

    /// <summary>
    /// A generic abstract base class for implementing the Flyweight Command Pattern,
    /// representing a ScriptableObject designed to execute a command on a specific target type without requiring additional data.
    /// </summary>
    /// <typeparam name="On">The type of the target on which the command operates.</typeparam>
    public abstract class CommandActionSO<On>: ScriptableObject
    {
        /// <summary>
        /// Executes a command on the specified object.
        /// </summary>
        /// <typeparam name="On">The type of object the command is executed upon.</typeparam>
        /// <param name="obj">The object on which the command is executed.</param>
        public abstract void Execute(On obj);
    }

    /// <summary>
    /// A generic abstract base class for implementing the Flyweight Command Pattern, representing a ScriptableObject designed to execute a command
    /// on a specific target type with two associated data types.
    /// </summary>
    /// <typeparam name="On">The type of the target on which the command operates.</typeparam>
    /// <typeparam name="WithA">The first type of data associated with the command.</typeparam>
    /// <typeparam name="WithB">The second type of data associated with the command.</typeparam>
    public abstract class CommandActionMultiSO<On, WithA, WithB> : ScriptableObject
    {
        /// <summary>
        /// Executes a command on the specified target with the provided data types.
        /// </summary>
        /// <typeparam name="On">The type of the target on which the command is executed.</typeparam>
        /// <typeparam name="WithA">The first type of the data used during execution.</typeparam>
        /// <typeparam name="WithB">The second type of the data used during execution.</typeparam>
        /// <param name="obj">The target object on which the command operates.</param>
        /// <param name="dataA">The first piece of data associated with the command execution.</param>
        /// <param name="dataB">The second piece of data associated with the command execution.</param>
        public abstract void Execute(On obj, WithA dataA, WithB dataB);
    }

    public class BlackBoardPointer<T>
    {
        T value;
    }
    /// <summary>
    /// A generic abstract base class for implementing the Flyweight Command Pattern, representing a ScriptableObject designed to execute a command
    /// on a specific target type with an array of associated data.
    /// </summary>
    /// <typeparam name="On">The type of the target on which the command operates.</typeparam>
    /// <typeparam name="With">The type of the data associated with the command, provided as an array.</typeparam>
    public abstract class CommandActionParamsSO<On, With>: ScriptableObject
    {
        /// <summary>
        /// Executes a command on the provided object with an array of additional data.
        /// </summary>
        /// <typeparam name="On">The type of the object on which the command is executed.</typeparam>
        /// <typeparam name="With">The type of the data passed to the command.</typeparam>
        /// <param name="obj">The object on which the command is executed.</param>
        /// <param name="data">An array of additional data used or required during the execution.</param>
        public abstract void Execute(On obj, params With[] data);
    }
}