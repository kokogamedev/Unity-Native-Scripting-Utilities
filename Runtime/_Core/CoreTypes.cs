using System;
using PsigenVision.Utilities;
using UnityEngine;

namespace PsigenVision
{
    /// <summary>
    /// Represents an interface that defines a contract for objects with a mutable globally unique identifier (GUID).
    /// <remarks>
    /// This interface extends the capability of having a GUID by allowing the identifier to be modified or regenerated
    /// through specific operations. Implementing types are expected to support both retrieval and reassignment
    /// of the GUID property while ensuring unique identification.
    /// </remarks>
    /// </summary>
    /// <typeparam name="T">
    /// Specifies the type of the identifier that uniquely represents the object.
    /// </typeparam>
    public interface IHaveMutableGuid<T> : IHaveGuid<T> //Used to identify an implementer as having an MUTABLE Guid with methods for generating/regenerating them
    {
        Guid ID { get; }
        Guid GenerateID(); //Call this on initialization - ID = System.Guid.NewGuid();

        //Implement the following:
        /*
        public bool Equals(T other) => other.ID == this.ID;
        public override bool Equals(object obj) => obj is T other && Equals(other);
        public override int GetHashCode() => ID;
        */

    }

    /// <summary>
    /// Represents an interface that defines a contract for objects that have a globally unique identifier (GUID).
    /// <remarks>
    /// This interface ensures that implementing types are equipped with a GUID property, which uniquely identifies
    /// the entity, and also supports equality comparison operations.
    /// </remarks>
    /// </summary>
    /// <typeparam name="T">
    /// Specifies the type of the identifier that uniquely represents the object.
    /// </typeparam>
    public interface IHaveGuid<T> : IEquatable<T> //Used to identify an implementer as having an IMMUTABLE Guid
    {
        //Generate ID via constructor using ID = name.ComputeFNV1aHash() or some other hash generating function
        //Optionally cache the name as a member variable if desired
        Guid ID { get; }
        
        //Implement the following:
        /*
        public bool Equals(T other) => other.ID == this.ID;
        public override bool Equals(object obj) => obj is T other && Equals(other);
        public override int GetHashCode() => ID;
        */
    }

    /// <summary>
    /// Represents an interface that defines a contract for objects capable of generating and maintaining a mutable identifier (ID).
    /// <remarks>
    /// This interface extends the functionality of static ID representations by providing mechanisms to dynamically generate
    /// or update the ID value, potentially based on specific input parameters or internal state.
    /// Implementing types are expected to generate and manage their ID in alignment with the defined contract.
    /// </remarks>
    /// </summary>
    /// <typeparam name="T">
    /// Specifies the type of the identifier that uniquely represents the object.
    /// </typeparam>
    public interface IHaveMutableID<T> : IHaveID<T> //Used to identify an implementer as having an MUTABLE ID with methods for generating/regenerating them
    {
        //Call GenerateID method on initialization - ID = name.ComputeFNV1aHash(); or some other hash generating function
        //Optionally cache the name as a member variable if desired
        int GenerateID(string name);
        //optional overload without a string parameter (in the event a string name has already been cached by the implementer internally)
        int GenerateID(); 

        //Implement the following:
        /*
        public bool Equals(T other) => other.ID == this.ID;
        public override bool Equals(object obj) => obj is T other && Equals(other);
        public override int GetHashCode() => ID;
        */
    }

    /// <summary>
    /// Defines a contract for objects with a unique identifier of type <typeparamref name="T"/>.
    /// <remarks>
    /// This interface ensures that implementing types include a mechanism to retrieve an immutable identifier.
    /// The identifier serves as a fundamental attribute for uniquely identifying instances
    /// and can be used in comparisons or hash-based collections.
    /// </remarks>
    /// </summary>
    /// <typeparam name="T">
    /// Specifies the type of the identifier that uniquely represents the object.
    /// </typeparam>
    public interface IHaveID<T> : IEquatable<T> //Used to identify an implementer as having an IMMUTABLE ID
    {
        //Generate ID via constructor using ID = name.ComputeFNV1aHash() or some other hash generating function
        //Optionally cache the name as a member variable if desired
        int ID { get; }

        //Implement the following:
        /*
        public bool Equals(T other) => other.ID == this.ID;
        public override bool Equals(object obj) => obj is T other && Equals(other);
        public override int GetHashCode() => ID;
        */
    }

    /// <summary>
    /// Represents a Unity-specific component interface that provides access
    /// to GameObject and Transform properties commonly associated with Unity components.
    /// <remarks>Component also serves as a convenient way to differentiate scene-entities from other object types without relying on a Component base class</remarks>
    /// </summary>
    public interface IUnityComponent
    {
        public GameObject gameObject { get; }
        public Transform transform { get; }
    }
}