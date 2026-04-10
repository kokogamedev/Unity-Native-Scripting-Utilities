using System;
using PsigenVision.Utilities;
using UnityEngine;

namespace PsigenVision
{
    public interface IHaveGuid<T>: IEquatable<T>
    {
        Guid ID { get; }
        void GenerateID(); //Call this on initialization - ID = System.Guid.NewGuid();

        //Implement the following:
        /*
        public bool Equals(T other) => other.ID == this.ID;

        public override int GetHashCode() => ID.GetHashCode();
        */

    }
    
    public interface IHaveID<T>: IEquatable<T>
    {
        int ID { get; }
        //Call this on initialization - ID = name.ComputeFNV1aHash(); or some other hash generating function
        //Optionally cache the name as a member variable if desired
        void GenerateID(string name); 

        //Implement the following:
        /*
        public bool Equals(T other) => other.ID == this.ID;

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