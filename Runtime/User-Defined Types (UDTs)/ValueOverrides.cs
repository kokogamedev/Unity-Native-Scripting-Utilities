using System;
using JetBrains.Annotations;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace PsigenVision.Utilities
{
    /// <summary>
    /// Represents a structure that conditionally overrides any value of type T.
    /// </summary>
    /// <remarks>
    /// This struct contains an override flag and a replacement value of type T.
    /// The replacement value is returned when the override is enabled;
    /// otherwise, the original value is used.
    /// </remarks>
    [Serializable]
    public struct ValueOverride<T>
    {
        /// <summary>
        /// Indicates whether the override value should be applied in the computations
        /// or if the original value should be retained.
        /// </summary>
        public bool doOverride;

        /// <summary>
        /// Represents an optional override value for a Vector3.
        /// If the override is active, this value will replace the original Vector3 value during evaluation.
        /// </summary>
        [SerializeField] public T overrideValue;

        /// <summary>
        /// Represents the retained value for the override that is used when the override is not active.
        /// </summary>
        [SerializeField] private T defaultValue;
        
        /// <summary>
        /// Retrieves the current value determined by whether the override is active or not.
        /// In the case of override, the override value is returned; otherwise, the original value is returned.
        /// </summary>
        public T Value => (doOverride) ? overrideValue : defaultValue;

        /// <summary>
        /// Retrieves the currently set default value for this value override.
        /// </summary>
        public T Default => defaultValue;
        
        /// <summary>
        /// The override value for this value override.
        /// </summary>
        public T Override
        {
            get => overrideValue; 
            set => overrideValue = value;
        }
        
        /// <summary>
        /// Switch between the default and override states, affecting the returned value.
        /// </summary>
        public void Toggle() => doOverride = !doOverride;
        
        /// <summary>
        /// Reset the currently cached default value to a new value.
        /// </summary>
        /// <param name="newDefaultValue"> The new default value to be cached. </param>
        public void ResetDefaultValue(T newDefaultValue) => defaultValue = newDefaultValue;

        /// <summary>
        /// A struct that optionally overrides a given Vector3 value with a specified override value.
        /// </summary>
        public ValueOverride(bool doOverride, T defaultValue, T overrideValue)
        {
            this.doOverride = doOverride;
            this.defaultValue = defaultValue;
            this.overrideValue = overrideValue;
        }

        public ValueOverride(bool doOverride, ValueOverride<T> value)
        {
            this.doOverride = doOverride;
            this.defaultValue = value.defaultValue;
            this.overrideValue = value.overrideValue;
        }
        
        /// <summary>
        /// Implicit conversion from the value override type to a value type according to its current override state. If being overriden, the override value is returned, otherwise the default value is returned.
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        public static implicit operator T(ValueOverride<T> valueOverride) => valueOverride.Value;
        
        /// <summary>
        /// Implicit conversion from a value type to the value override type. The value will be assigned as the default and override values, and the override state will be set to false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator ValueOverride<T>(T value) => new (false, value, value);

        /// <summary>
        /// Returns an equivalent value override with the override state switched/negated, thereby changing the current value (via the Value property). 
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        /// <remarks>
        /// This leverages the override value type's ability to operate as a binary value, in which the two states are the default value and override value.
        /// </remarks>
        public static ValueOverride<T> operator !(ValueOverride<T> valueOverride) => new(!valueOverride.doOverride, valueOverride);
    }
    
    /// <summary>
    /// Represents a structure that conditionally overrides a Vector3 value.
    /// </summary>
    /// <remarks>
    /// This struct contains an override flag and a replacement Vector3 value.
    /// The replacement value is returned when the override is enabled;
    /// otherwise, the original value is used.
    /// </remarks>
    [Serializable]
    public struct Vector3Override
    {
        /// <summary>
        /// Indicates whether the override value should be applied in the computations
        /// or if the original value should be retained.
        /// </summary>
        public bool doOverride;

        /// <summary>
        /// Represents an optional override value for a Vector3.
        /// If the override is active, this value will replace the original Vector3 value during evaluation.
        /// </summary>
        [SerializeField] public Vector3 overrideValue;

        /// <summary>
        /// Represents the retained value for the override that is used when the override is not active.
        /// </summary>
        [SerializeField] private Vector3 defaultValue;
        
        /// <summary>
        /// Retrieves the current value determined by whether the override is active or not.
        /// In the case of override, the override value is returned; otherwise, the original value is returned.
        /// </summary>
        public Vector3 Value => (doOverride) ? overrideValue : defaultValue;

        /// <summary>
        /// Retrieves the currently set default value for this value override.
        /// </summary>
        public Vector3 Default => defaultValue;
        
        /// <summary>
        /// The override value for this value override.
        /// </summary>
        public Vector3 Override
        {
            get => overrideValue; 
            set => overrideValue = value;
        }
        
        /// <summary>
        /// Switch between the default and override states, affecting the returned value.
        /// </summary>
        public void Toggle() => doOverride = !doOverride;
        
        /// <summary>
        /// Reset the currently cached default value to a new value.
        /// </summary>
        /// <param name="newDefaultValue"> The new default value to be cached. </param>
        public void ResetDefaultValue(Vector3 newDefaultValue) => defaultValue = newDefaultValue;

        /// <summary>
        /// A struct that optionally overrides a given Vector3 value with a specified override value.
        /// </summary>
        public Vector3Override(bool doOverride, Vector3 defaultValue, Vector3 overrideValue)
        {
            this.doOverride = doOverride;
            this.defaultValue = defaultValue;
            this.overrideValue = overrideValue;
        }

        public Vector3Override(bool doOverride, Vector3Override value)
        {
            this.doOverride = doOverride;
            this.defaultValue = value.defaultValue;
            this.overrideValue = value.overrideValue;
        }
        
        /// <summary>
        /// Implicit conversion from the value override type to a value type according to its current override state. If being overriden, the override value is returned, otherwise the default value is returned.
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        public static implicit operator Vector3(Vector3Override valueOverride) => valueOverride.Value;
        
        /// <summary>
        /// Implicit conversion from a value type to the value override type. The value will be assigned as the default and override values, and the override state will be set to false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator Vector3Override(Vector3 value) => new (false, value, value);

        /// <summary>
        /// Negation of a value override returns a copy of that value override with the override state switched/negated.
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        public static Vector3Override operator !(Vector3Override valueOverride) => new(!valueOverride.doOverride, valueOverride);
    }
    
    /// <summary>
    /// Represents a structure that conditionally overrides a Vector2 value.
    /// </summary>
    /// <remarks>
    /// This struct contains an override flag and a replacement Vector2 value.
    /// The replacement value is returned when the override is enabled;
    /// otherwise, the original value is used.
    /// </remarks>
    [Serializable]
    public struct Vector2Override
    {
        /// <summary>
        /// Indicates whether the override value should be applied in the computations
        /// or if the original value should be retained.
        /// </summary>
        public bool doOverride;

        /// <summary>
        /// Represents an optional override value for a Vector2.
        /// If the override is active, this value will replace the original Vector2 value during evaluation.
        /// </summary>
        [SerializeField] private Vector2 overrideValue;

        /// <summary>
        /// Represents the retained value for the override that is used when the override is not active.
        /// </summary>
        [SerializeField] private Vector2 defaultValue;
        
        /// <summary>
        /// Retrieves the current value determined by whether the override is active or not.
        /// In the case of override, the override value is returned; otherwise, the original value is returned.
        /// </summary>
        public Vector2 Value => (doOverride) ? overrideValue : defaultValue;

        /// <summary>
        /// Retrieves the currently set default value for this value override.
        /// </summary>
        public Vector2 Default => defaultValue;
        
        /// <summary>
        /// The override value for this value override.
        /// </summary>
        public Vector2 Override
        {
            get => overrideValue; 
            set => overrideValue = value;
        }
        
        /// <summary>
        /// Switch between the default and override states, affecting the returned value.
        /// </summary>
        public void Toggle() => doOverride = !doOverride;
        
        /// <summary>
        /// Reset the currently cached default value to a new value.
        /// </summary>
        /// <param name="newDefaultValue"> The new default value to be cached. </param>
        public void ResetDefaultValue(Vector2 newDefaultValue) => defaultValue = newDefaultValue;

        /// <summary>
        /// A struct that optionally overrides a given Vector2 value with a specified override value.
        /// </summary>
        public Vector2Override(bool doOverride, Vector2 defaultValue, Vector2 overrideValue)
        {
            this.doOverride = doOverride;
            this.defaultValue = defaultValue;
            this.overrideValue = overrideValue;
        }
        
        public Vector2Override(bool doOverride, Vector2Override value)
        {
            this.doOverride = doOverride;
            this.defaultValue = value.defaultValue;
            this.overrideValue = value.overrideValue;
        }
        
        /// <summary>
        /// Implicit conversion from the value override type to a value type according to its current override state. If being overriden, the override value is returned, otherwise the default value is returned.
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        public static implicit operator Vector2(Vector2Override valueOverride) => valueOverride.Value;
        
        /// <summary>
        /// Implicit conversion from a value type to the value override type. The value will be assigned as the default and override values, and the override state will be set to false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator Vector2Override(Vector2 value) => new (false, value, value);
        
        /// <summary>
        /// Returns an equivalent value override with the override state switched/negated, thereby changing the current value (via the Value property). 
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        /// <remarks>
        /// This leverages the override value type's ability to operate as a binary value, in which the two states are the default value and override value.
        /// </remarks>
        public static Vector2Override operator !(Vector2Override valueOverride) => new(!valueOverride.doOverride, valueOverride);
    }

    /// <summary>
    /// Represents a structure that conditionally overrides a Vector4 value.
    /// </summary>
    /// <remarks>
    /// This struct contains an override flag and a replacement Vector4 value.
    /// The replacement value is returned when the override is enabled;
    /// otherwise, the original value is used.
    /// </remarks>
    [Serializable]
    public struct Vector4Override
    {
        /// <summary>
        /// Indicates whether the override value should be applied in the computations
        /// or if the original value should be retained.
        /// </summary>
        public bool doOverride;

        /// <summary>
        /// Represents an optional override value for a Vector4.
        /// If the override is active, this value will replace the original Vector4 value during evaluation.
        /// </summary>
        [SerializeField] private Vector4 overrideValue;

        /// <summary>
        /// Represents the retained value for the override that is used when the override is not active.
        /// </summary>
        [SerializeField] private Vector4 defaultValue;
        
        /// <summary>
        /// Retrieves the current value determined by whether the override is active or not.
        /// In the case of override, the override value is returned; otherwise, the original value is returned.
        /// </summary>
        public Vector4 Value => (doOverride) ? overrideValue : defaultValue;

        /// <summary>
        /// Retrieves the currently set default value for this value override.
        /// </summary>
        public Vector4 Default => defaultValue;
        
        /// <summary>
        /// The override value for this value override.
        /// </summary>
        public Vector4 Override
        {
            get => overrideValue; 
            set => overrideValue = value;
        }
        
        /// <summary>
        /// Switch between the default and override states, affecting the returned value.
        /// </summary>
        public void Toggle() => doOverride = !doOverride;
        
        /// <summary>
        /// Reset the currently cached default value to a new value.
        /// </summary>
        /// <param name="newDefaultValue"> The new default value to be cached. </param>
        public void ResetDefaultValue(Vector4 newDefaultValue) => defaultValue = newDefaultValue;

        /// <summary>
        /// A struct that optionally overrides a given Vector4 value with a specified override value.
        /// </summary>
        public Vector4Override(bool doOverride, Vector4 defaultValue, Vector4 overrideValue)
        {
            this.doOverride = doOverride;
            this.defaultValue = defaultValue;
            this.overrideValue = overrideValue;
        }
        
        public Vector4Override(bool doOverride, Vector4Override value)
        {
            this.doOverride = doOverride;
            this.defaultValue = value.defaultValue;
            this.overrideValue = value.overrideValue;
        }
        
        /// <summary>
        /// Implicit conversion from the value override type to a value type according to its current override state. If being overriden, the override value is returned, otherwise the default value is returned.
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        public static implicit operator Vector4(Vector4Override valueOverride) => valueOverride.Value;
        
        /// <summary>
        /// Implicit conversion from a value type to the value override type. The value will be assigned as the default and override values, and the override state will be set to false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator Vector4Override(Vector4 value) => new (false, value, value);
        
        /// <summary>
        /// Returns an equivalent value override with the override state switched/negated, thereby changing the current value (via the Value property). 
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        /// <remarks>
        /// This leverages the override value type's ability to operate as a binary value, in which the two states are the default value and override value.
        /// </remarks>
        public static Vector4Override operator !(Vector4Override valueOverride) => new(!valueOverride.doOverride, valueOverride);
    }
    
    /// <summary>
    /// Represents a structure that conditionally overrides a Vector2Int value.
    /// </summary>
    /// <remarks>
    /// This struct contains an override flag and a replacement Vector2Int value.
    /// The replacement value is returned when the override is enabled;
    /// otherwise, the original value is used.
    /// </remarks>
    [Serializable]
    public struct Vector2IntOverride
    {
        /// <summary>
        /// Indicates whether the override value should be applied in the computations
        /// or if the original value should be retained.
        /// </summary>
        public bool doOverride;

        /// <summary>
        /// Represents an optional override value for a Vector2Int.
        /// If the override is active, this value will replace the original Vector2Int value during evaluation.
        /// </summary>
        [SerializeField] private Vector2Int overrideValue;

        /// <summary>
        /// Represents the retained value for the override that is used when the override is not active.
        /// </summary>
        [SerializeField] private Vector2Int defaultValue;
        
        /// <summary>
        /// Retrieves the current value determined by whether the override is active or not.
        /// In the case of override, the override value is returned; otherwise, the original value is returned.
        /// </summary>
        public Vector2Int Value => (doOverride) ? overrideValue : defaultValue;

        /// <summary>
        /// Retrieves the currently set default value for this value override.
        /// </summary>
        public Vector2Int Default => defaultValue;
        
        /// <summary>
        /// The override value for this value override.
        /// </summary>
        public Vector2Int Override
        {
            get => overrideValue; 
            set => overrideValue = value;
        }
        
        /// <summary>
        /// Switch between the default and override states, affecting the returned value.
        /// </summary>
        public void Toggle() => doOverride = !doOverride;
        
        /// <summary>
        /// Reset the currently cached default value to a new value.
        /// </summary>
        /// <param name="newDefaultValue"> The new default value to be cached. </param>
        public void ResetDefaultValue(Vector2Int newDefaultValue) => defaultValue = newDefaultValue;

        /// <summary>
        /// A struct that optionally overrides a given Vector2Int value with a specified override value.
        /// </summary>
        public Vector2IntOverride(bool doOverride, Vector2Int defaultValue, Vector2Int overrideValue)
        {
            this.doOverride = doOverride;
            this.defaultValue = defaultValue;
            this.overrideValue = overrideValue;
        }
        
        public Vector2IntOverride(bool doOverride, Vector2IntOverride value)
        {
            this.doOverride = doOverride;
            this.defaultValue = value.defaultValue;
            this.overrideValue = value.overrideValue;
        }
        
        /// <summary>
        /// Implicit conversion from the value override type to a value type according to its current override state. If being overriden, the override value is returned, otherwise the default value is returned.
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        public static implicit operator Vector2Int(Vector2IntOverride valueOverride) => valueOverride.Value;
        
        /// <summary>
        /// Implicit conversion from a value type to the value override type. The value will be assigned as the default and override values, and the override state will be set to false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator Vector2IntOverride(Vector2Int value) => new (false, value, value);
        
        /// <summary>
        /// Returns an equivalent value override with the override state switched/negated, thereby changing the current value (via the Value property). 
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        /// <remarks>
        /// This leverages the override value type's ability to operate as a binary value, in which the two states are the default value and override value.
        /// </remarks>
        public static Vector2IntOverride operator !(Vector2IntOverride valueOverride) => new(!valueOverride.doOverride, valueOverride);
    }
    
    /// <summary>
    /// Represents a structure that conditionally overrides a Vector3Int value.
    /// </summary>
    /// <remarks>
    /// This struct contains an override flag and a replacement Vector3Int value.
    /// The replacement value is returned when the override is enabled;
    /// otherwise, the original value is used.
    /// </remarks>
    [Serializable]
    public struct Vector3IntOverride
    {
        /// <summary>
        /// Indicates whether the override value should be applied in the computations
        /// or if the original value should be retained.
        /// </summary>
        public bool doOverride;

        /// <summary>
        /// Represents an optional override value for a Vector3Int.
        /// If the override is active, this value will replace the original Vector3Int value during evaluation.
        /// </summary>
        [SerializeField] private Vector3Int overrideValue;

        /// <summary>
        /// Represents the retained value for the override that is used when the override is not active.
        /// </summary>
        [SerializeField] private Vector3Int defaultValue;
        
        /// <summary>
        /// Retrieves the current value determined by whether the override is active or not.
        /// In the case of override, the override value is returned; otherwise, the original value is returned.
        /// </summary>
        public Vector3Int Value => (doOverride) ? overrideValue : defaultValue;

        /// <summary>
        /// Retrieves the currently set default value for this value override.
        /// </summary>
        public Vector3Int Default => defaultValue;
        
        /// <summary>
        /// The override value for this value override.
        /// </summary>
        public Vector3Int Override
        {
            get => overrideValue; 
            set => overrideValue = value;
        }
        
        /// <summary>
        /// Switch between the default and override states, affecting the returned value.
        /// </summary>
        public void Toggle() => doOverride = !doOverride;
        
        /// <summary>
        /// Reset the currently cached default value to a new value.
        /// </summary>
        /// <param name="newDefaultValue"> The new default value to be cached. </param>
        public void ResetDefaultValue(Vector3Int newDefaultValue) => defaultValue = newDefaultValue;

        /// <summary>
        /// A struct that optionally overrides a given Vector3Int value with a specified override value.
        /// </summary>
        public Vector3IntOverride(bool doOverride, Vector3Int defaultValue, Vector3Int overrideValue)
        {
            this.doOverride = doOverride;
            this.defaultValue = defaultValue;
            this.overrideValue = overrideValue;
        }
        
        public Vector3IntOverride(bool doOverride, Vector3IntOverride value)
        {
            this.doOverride = doOverride;
            this.defaultValue = value.defaultValue;
            this.overrideValue = value.overrideValue;
        }
        
        /// <summary>
        /// Implicit conversion from the value override type to a value type according to its current override state. If being overriden, the override value is returned, otherwise the default value is returned.
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        public static implicit operator Vector3Int(Vector3IntOverride valueOverride) => valueOverride.Value;
        
        /// <summary>
        /// Implicit conversion from a value type to the value override type. The value will be assigned as the default and override values, and the override state will be set to false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator Vector3IntOverride(Vector3Int value) => new (false, value, value);
        
        /// <summary>
        /// Returns an equivalent value override with the override state switched/negated, thereby changing the current value (via the Value property). 
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        /// <remarks>
        /// This leverages the override value type's ability to operate as a binary value, in which the two states are the default value and override value.
        /// </remarks>
        public static Vector3IntOverride operator !(Vector3IntOverride valueOverride) => new(!valueOverride.doOverride, valueOverride);
    }
    
    /// <summary>
    /// Represents a structure that conditionally overrides a Quaternion value.
    /// </summary>
    /// <remarks>
    /// This struct contains an override flag and a replacement Quaternion value.
    /// The replacement value is returned when the override is enabled;
    /// otherwise, the original value is used.
    /// </remarks>
    [Serializable]
    public struct QuaternionOverride
    {
        /// <summary>
        /// Indicates whether the override value should be applied in the computations
        /// or if the original value should be retained.
        /// </summary>
        public bool doOverride;

        /// <summary>
        /// Represents an optional override value for a Quaternion.
        /// If the override is active, this value will replace the original Quaternion value during evaluation.
        /// </summary>
        [SerializeField] private Quaternion overrideValue;

        /// <summary>
        /// Represents the retained value for the override that is used when the override is not active.
        /// </summary>
        [SerializeField] private Quaternion defaultValue;
        
        /// <summary>
        /// Retrieves the current value determined by whether the override is active or not.
        /// In the case of override, the override value is returned; otherwise, the original value is returned.
        /// </summary>
        public Quaternion Value => (doOverride) ? overrideValue : defaultValue;

        /// <summary>
        /// Retrieves the currently set default value for this value override.
        /// </summary>
        public Quaternion Default => defaultValue;
        
        /// <summary>
        /// The override value for this value override.
        /// </summary>
        public Quaternion Override
        {
            get => overrideValue; 
            set => overrideValue = value;
        }
        
        /// <summary>
        /// Switch between the default and override states, affecting the returned value.
        /// </summary>
        public void Toggle() => doOverride = !doOverride;
        
        /// <summary>
        /// Reset the currently cached default value to a new value.
        /// </summary>
        /// <param name="newDefaultValue"> The new default value to be cached. </param>
        public void ResetDefaultValue(Quaternion newDefaultValue) => defaultValue = newDefaultValue;

        /// <summary>
        /// A struct that optionally overrides a given Quaternion value with a specified override value.
        /// </summary>
        public QuaternionOverride(bool doOverride, Quaternion defaultValue, Quaternion overrideValue)
        {
            this.doOverride = doOverride;
            this.defaultValue = defaultValue;
            this.overrideValue = overrideValue;
        }
        
        public QuaternionOverride(bool doOverride, QuaternionOverride value)
        {
            this.doOverride = doOverride;
            this.defaultValue = value.defaultValue;
            this.overrideValue = value.overrideValue;
        }
        
        /// <summary>
        /// Implicit conversion from the value override type to a value type according to its current override state. If being overriden, the override value is returned, otherwise the default value is returned.
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        public static implicit operator Quaternion(QuaternionOverride valueOverride) => valueOverride.Value;
        
        /// <summary>
        /// Implicit conversion from a value type to the value override type. The value will be assigned as the default and override values, and the override state will be set to false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator QuaternionOverride(Quaternion value) => new (false, value, value);
        
        /// <summary>
        /// Returns an equivalent value override with the override state switched/negated, thereby changing the current value (via the Value property). 
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        /// <remarks>
        /// This leverages the override value type's ability to operate as a binary value, in which the two states are the default value and override value.
        /// </remarks>
        public static QuaternionOverride operator !(QuaternionOverride valueOverride) => new(!valueOverride.doOverride, valueOverride);
    }
    
    /// <summary>
    /// Represents a structure that conditionally overrides a LayerMask value.
    /// </summary>
    /// <remarks>
    /// This struct contains an override flag and a replacement LayerMask value.
    /// The replacement value is returned when the override is enabled;
    /// otherwise, the original value is used.
    /// </remarks>
    [Serializable]
    public struct LayerMaskOverride
    {
        /// <summary>
        /// Indicates whether the override value should be applied in the computations
        /// or if the original value should be retained.
        /// </summary>
        public bool doOverride;

        /// <summary>
        /// Represents an optional override value for a LayerMask.
        /// If the override is active, this value will replace the original LayerMask value during evaluation.
        /// </summary>
        [SerializeField] private LayerMask overrideValue;

        /// <summary>
        /// Represents the retained value for the override that is used when the override is not active.
        /// </summary>
        [SerializeField] private LayerMask defaultValue;
        
        /// <summary>
        /// Retrieves the current value determined by whether the override is active or not.
        /// In the case of override, the override value is returned; otherwise, the original value is returned.
        /// </summary>
        public LayerMask Value => (doOverride) ? overrideValue : defaultValue;

        /// <summary>
        /// Retrieves the currently set default value for this value override.
        /// </summary>
        public LayerMask Default => defaultValue;
        
        /// <summary>
        /// The override value for this value override.
        /// </summary>
        public LayerMask Override
        {
            get => overrideValue; 
            set => overrideValue = value;
        }
        
        /// <summary>
        /// Switch between the default and override states, affecting the returned value.
        /// </summary>
        public void Toggle() => doOverride = !doOverride;
        
        /// <summary>
        /// Reset the currently cached default value to a new value.
        /// </summary>
        /// <param name="newDefaultValue"> The new default value to be cached. </param>
        public void ResetDefaultValue(LayerMask newDefaultValue) => defaultValue = newDefaultValue;

        /// <summary>
        /// A struct that optionally overrides a given LayerMask value with a specified override value.
        /// </summary>
        public LayerMaskOverride(bool doOverride, LayerMask defaultValue, LayerMask overrideValue)
        {
            this.doOverride = doOverride;
            this.defaultValue = defaultValue;
            this.overrideValue = overrideValue;
        }
        
        public LayerMaskOverride(bool doOverride, LayerMaskOverride value)
        {
            this.doOverride = doOverride;
            this.defaultValue = value.defaultValue;
            this.overrideValue = value.overrideValue;
        }
        
        /// <summary>
        /// Implicit conversion from the value override type to a value type according to its current override state. If being overriden, the override value is returned, otherwise the default value is returned.
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        public static implicit operator LayerMask(LayerMaskOverride valueOverride) => valueOverride.Value;
        
        /// <summary>
        /// Implicit conversion from a value type to the value override type. The value will be assigned as the default and override values, and the override state will be set to false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator LayerMaskOverride(LayerMask value) => new (false, value, value);
        
        /// <summary>
        /// Returns an equivalent value override with the override state switched/negated, thereby changing the current value (via the Value property). 
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        /// <remarks>
        /// This leverages the override value type's ability to operate as a binary value, in which the two states are the default value and override value.
        /// </remarks>
        public static LayerMaskOverride operator !(LayerMaskOverride valueOverride) => new(!valueOverride.doOverride, valueOverride);
    }
    
    /// <summary>
    /// Represents a structure that conditionally overrides a Color value.
    /// </summary>
    /// <remarks>
    /// This struct contains an override flag and a replacement Color value.
    /// The replacement value is returned when the override is enabled;
    /// otherwise, the original value is used.
    /// </remarks>
    [Serializable]
    public struct ColorOverride
    {
        /// <summary>
        /// Indicates whether the override value should be applied in the computations
        /// or if the original value should be retained.
        /// </summary>
        public bool doOverride;

        /// <summary>
        /// Represents an optional override value for a Color.
        /// If the override is active, this value will replace the original Color value during evaluation.
        /// </summary>
        [SerializeField] private Color overrideValue;

        /// <summary>
        /// Represents the retained value for the override that is used when the override is not active.
        /// </summary>
        [SerializeField] private Color defaultValue;
        
        /// <summary>
        /// Retrieves the current value determined by whether the override is active or not.
        /// In the case of override, the override value is returned; otherwise, the original value is returned.
        /// </summary>
        public Color Value => (doOverride) ? overrideValue : defaultValue;

        /// <summary>
        /// Retrieves the currently set default value for this value override.
        /// </summary>
        public Color Default => defaultValue;
        
        /// <summary>
        /// The override value for this value override.
        /// </summary>
        public Color Override
        {
            get => overrideValue; 
            set => overrideValue = value;
        }
        
        /// <summary>
        /// Switch between the default and override states, affecting the returned value.
        /// </summary>
        public void Toggle() => doOverride = !doOverride;
        
        /// <summary>
        /// Reset the currently cached default value to a new value.
        /// </summary>
        /// <param name="newDefaultValue"> The new default value to be cached. </param>
        public void ResetDefaultValue(Color newDefaultValue) => defaultValue = newDefaultValue;

        /// <summary>
        /// A struct that optionally overrides a given Color value with a specified override value.
        /// </summary>
        public ColorOverride(bool doOverride, Color defaultValue, Color overrideValue)
        {
            this.doOverride = doOverride;
            this.defaultValue = defaultValue;
            this.overrideValue = overrideValue;
        }
        
        public ColorOverride(bool doOverride, ColorOverride value)
        {
            this.doOverride = doOverride;
            this.defaultValue = value.defaultValue;
            this.overrideValue = value.overrideValue;
        }
        
        /// <summary>
        /// Implicit conversion from the value override type to a value type according to its current override state. If being overriden, the override value is returned, otherwise the default value is returned.
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        public static implicit operator Color(ColorOverride valueOverride) => valueOverride.Value;
        
        /// <summary>
        /// Implicit conversion from a value type to the value override type. The value will be assigned as the default and override values, and the override state will be set to false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator ColorOverride(Color value) => new (false, value, value);
        
        /// <summary>
        /// Returns an equivalent value override with the override state switched/negated, thereby changing the current value (via the Value property). 
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        /// <remarks>
        /// This leverages the override value type's ability to operate as a binary value, in which the two states are the default value and override value.
        /// </remarks>
        public static ColorOverride operator !(ColorOverride valueOverride) => new(!valueOverride.doOverride, valueOverride);
    }
    
    /// <summary>
    /// Represents a structure that conditionally overrides a Rect value.
    /// </summary>
    /// <remarks>
    /// This struct contains an override flag and a replacement Rect value.
    /// The replacement value is returned when the override is enabled;
    /// otherwise, the original value is used.
    /// </remarks>
    [Serializable]
    public struct RectOverride
    {
        /// <summary>
        /// Indicates whether the override value should be applied in the computations
        /// or if the original value should be retained.
        /// </summary>
        public bool doOverride;

        /// <summary>
        /// Represents an optional override value for a Rect.
        /// If the override is active, this value will replace the original Rect value during evaluation.
        /// </summary>
        [SerializeField] private Rect overrideValue;

        /// <summary>
        /// Represents the retained value for the override that is used when the override is not active.
        /// </summary>
        [SerializeField] private Rect defaultValue;
        
        /// <summary>
        /// Retrieves the current value determined by whether the override is active or not.
        /// In the case of override, the override value is returned; otherwise, the original value is returned.
        /// </summary>
        public Rect Value => (doOverride) ? overrideValue : defaultValue;

        /// <summary>
        /// Retrieves the currently set default value for this value override.
        /// </summary>
        public Rect Default => defaultValue;
        
        /// <summary>
        /// The override value for this value override.
        /// </summary>
        public Rect Override
        {
            get => overrideValue; 
            set => overrideValue = value;
        }
        
        /// <summary>
        /// Switch between the default and override states, affecting the returned value.
        /// </summary>
        public void Toggle() => doOverride = !doOverride;
        
        /// <summary>
        /// Reset the currently cached default value to a new value.
        /// </summary>
        /// <param name="newDefaultValue"> The new default value to be cached. </param>
        public void ResetDefaultValue(Rect newDefaultValue) => defaultValue = newDefaultValue;

        /// <summary>
        /// A struct that optionally overrides a given Rect value with a specified override value.
        /// </summary>
        public RectOverride(bool doOverride, Rect defaultValue, Rect overrideValue)
        {
            this.doOverride = doOverride;
            this.defaultValue = defaultValue;
            this.overrideValue = overrideValue;
        }
        
        public RectOverride(bool doOverride, RectOverride value)
        {
            this.doOverride = doOverride;
            this.defaultValue = value.defaultValue;
            this.overrideValue = value.overrideValue;
        }
        
        /// <summary>
        /// Implicit conversion from the value override type to a value type according to its current override state. If being overriden, the override value is returned, otherwise the default value is returned.
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        public static implicit operator Rect(RectOverride valueOverride) => valueOverride.Value;
        
        /// <summary>
        /// Implicit conversion from a value type to the value override type. The value will be assigned as the default and override values, and the override state will be set to false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator RectOverride(Rect value) => new (false, value, value);
        
        /// <summary>
        /// Returns an equivalent value override with the override state switched/negated, thereby changing the current value (via the Value property). 
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        /// <remarks>
        /// This leverages the override value type's ability to operate as a binary value, in which the two states are the default value and override value.
        /// </remarks>
        public static RectOverride operator !(RectOverride valueOverride) => new(!valueOverride.doOverride, valueOverride);
    }
    
    /// <summary>
    /// Represents a structure that conditionally overrides a RectInt value.
    /// </summary>
    /// <remarks>
    /// This struct contains an override flag and a replacement RectInt value.
    /// The replacement value is returned when the override is enabled;
    /// otherwise, the original value is used.
    /// </remarks>
    [Serializable]
    public struct RectIntOverride
    {
        /// <summary>
        /// Indicates whether the override value should be applied in the computations
        /// or if the original value should be retained.
        /// </summary>
        public bool doOverride;

        /// <summary>
        /// Represents an optional override value for a RectInt.
        /// If the override is active, this value will replace the original RectInt value during evaluation.
        /// </summary>
        [SerializeField] private RectInt overrideValue;

        /// <summary>
        /// Represents the retained value for the override that is used when the override is not active.
        /// </summary>
        [SerializeField] private RectInt defaultValue;
        
        /// <summary>
        /// Retrieves the current value determined by whether the override is active or not.
        /// In the case of override, the override value is returned; otherwise, the original value is returned.
        /// </summary>
        public RectInt Value => (doOverride) ? overrideValue : defaultValue;

        /// <summary>
        /// Retrieves the currently set default value for this value override.
        /// </summary>
        public RectInt Default => defaultValue;
        
        /// <summary>
        /// The override value for this value override.
        /// </summary>
        public RectInt Override
        {
            get => overrideValue; 
            set => overrideValue = value;
        }
        
        /// <summary>
        /// Switch between the default and override states, affecting the returned value.
        /// </summary>
        public void Toggle() => doOverride = !doOverride;
        
        /// <summary>
        /// Reset the currently cached default value to a new value.
        /// </summary>
        /// <param name="newDefaultValue"> The new default value to be cached. </param>
        public void ResetDefaultValue(RectInt newDefaultValue) => defaultValue = newDefaultValue;

        /// <summary>
        /// A struct that optionally overrides a given RectInt value with a specified override value.
        /// </summary>
        public RectIntOverride(bool doOverride, RectInt defaultValue, RectInt overrideValue)
        {
            this.doOverride = doOverride;
            this.defaultValue = defaultValue;
            this.overrideValue = overrideValue;
        }
        
        public RectIntOverride(bool doOverride, RectIntOverride value)
        {
            this.doOverride = doOverride;
            this.defaultValue = value.defaultValue;
            this.overrideValue = value.overrideValue;
        }
        
        /// <summary>
        /// Implicit conversion from the value override type to a value type according to its current override state. If being overriden, the override value is returned, otherwise the default value is returned.
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        public static implicit operator RectInt(RectIntOverride valueOverride) => valueOverride.Value;
        
        /// <summary>
        /// Implicit conversion from a value type to the value override type. The value will be assigned as the default and override values, and the override state will be set to false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator RectIntOverride(RectInt value) => new (false, value, value);
        
        /// <summary>
        /// Returns an equivalent value override with the override state switched/negated, thereby changing the current value (via the Value property). 
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        /// <remarks>
        /// This leverages the override value type's ability to operate as a binary value, in which the two states are the default value and override value.
        /// </remarks>
        public static RectIntOverride operator !(RectIntOverride valueOverride) => new(!valueOverride.doOverride, valueOverride);
    }
    
    /// <summary>
    /// Represents a structure that conditionally overrides a AnimationCurve value.
    /// </summary>
    /// <remarks>
    /// This struct contains an override flag and a replacement AnimationCurve value.
    /// The replacement value is returned when the override is enabled;
    /// otherwise, the original value is used.
    /// </remarks>
    [Serializable]
    public struct AnimationCurveOverride
    {
        /// <summary>
        /// Indicates whether the override value should be applied in the computations
        /// or if the original value should be retained.
        /// </summary>
        public bool doOverride;

        /// <summary>
        /// Represents an optional override value for a AnimationCurve.
        /// If the override is active, this value will replace the original AnimationCurve value during evaluation.
        /// </summary>
        [SerializeField] private AnimationCurve overrideValue;

        /// <summary>
        /// Represents the retained value for the override that is used when the override is not active.
        /// </summary>
        [SerializeField] private AnimationCurve defaultValue;
        
        /// <summary>
        /// Retrieves the current value determined by whether the override is active or not.
        /// In the case of override, the override value is returned; otherwise, the original value is returned.
        /// </summary>
        public AnimationCurve Value => (doOverride) ? overrideValue : defaultValue;

        /// <summary>
        /// Retrieves the currently set default value for this value override.
        /// </summary>
        public AnimationCurve Default => defaultValue;
        
        /// <summary>
        /// The override value for this value override.
        /// </summary>
        public AnimationCurve Override
        {
            get => overrideValue; 
            set => overrideValue = value;
        }
        
        /// <summary>
        /// Switch between the default and override states, affecting the returned value.
        /// </summary>
        public void Toggle() => doOverride = !doOverride;
        
        /// <summary>
        /// Reset the currently cached default value to a new value.
        /// </summary>
        /// <param name="newDefaultValue"> The new default value to be cached. </param>
        public void ResetDefaultValue(AnimationCurve newDefaultValue) => defaultValue = newDefaultValue;

        /// <summary>
        /// A struct that optionally overrides a given AnimationCurve value with a specified override value.
        /// </summary>
        public AnimationCurveOverride(bool doOverride, AnimationCurve defaultValue, AnimationCurve overrideValue)
        {
            this.doOverride = doOverride;
            this.defaultValue = defaultValue;
            this.overrideValue = overrideValue;
        }
        
        public AnimationCurveOverride(bool doOverride, AnimationCurveOverride value)
        {
            this.doOverride = doOverride;
            this.defaultValue = value.defaultValue;
            this.overrideValue = value.overrideValue;
        }
        
        /// <summary>
        /// Implicit conversion from the value override type to a value type according to its current override state. If being overriden, the override value is returned, otherwise the default value is returned.
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        public static implicit operator AnimationCurve(AnimationCurveOverride valueOverride) => valueOverride.Value;
        
        /// <summary>
        /// Implicit conversion from a value type to the value override type. The value will be assigned as the default and override values, and the override state will be set to false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator AnimationCurveOverride(AnimationCurve value) => new (false, value, value);
        
        /// <summary>
        /// Returns an equivalent value override with the override state switched/negated, thereby changing the current value (via the Value property). 
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        /// <remarks>
        /// This leverages the override value type's ability to operate as a binary value, in which the two states are the default value and override value.
        /// </remarks>
        public static AnimationCurveOverride operator !(AnimationCurveOverride valueOverride) => new(!valueOverride.doOverride, valueOverride);
    }
        
    /// <summary>
    /// Represents a structure that conditionally overrides a Bounds value.
    /// </summary>
    /// <remarks>
    /// This struct contains an override flag and a replacement Bounds value.
    /// The replacement value is returned when the override is enabled;
    /// otherwise, the original value is used.
    /// </remarks>
    [Serializable]
    public struct BoundsOverride
    {
        /// <summary>
        /// Indicates whether the override value should be applied in the computations
        /// or if the original value should be retained.
        /// </summary>
        public bool doOverride;

        /// <summary>
        /// Represents an optional override value for a Bounds.
        /// If the override is active, this value will replace the original Bounds value during evaluation.
        /// </summary>
        [SerializeField] private Bounds overrideValue;

        /// <summary>
        /// Represents the retained value for the override that is used when the override is not active.
        /// </summary>
        [SerializeField] private Bounds defaultValue;
        
        /// <summary>
        /// Retrieves the current value determined by whether the override is active or not.
        /// In the case of override, the override value is returned; otherwise, the original value is returned.
        /// </summary>
        public Bounds Value => (doOverride) ? overrideValue : defaultValue;

        /// <summary>
        /// Retrieves the currently set default value for this value override.
        /// </summary>
        public Bounds Default => defaultValue;
        
        /// <summary>
        /// The override value for this value override.
        /// </summary>
        public Bounds Override
        {
            get => overrideValue; 
            set => overrideValue = value;
        }
        
        /// <summary>
        /// Switch between the default and override states, affecting the returned value.
        /// </summary>
        public void Toggle() => doOverride = !doOverride;
        
        /// <summary>
        /// Reset the currently cached default value to a new value.
        /// </summary>
        /// <param name="newDefaultValue"> The new default value to be cached. </param>
        public void ResetDefaultValue(Bounds newDefaultValue) => defaultValue = newDefaultValue;

        /// <summary>
        /// A struct that optionally overrides a given Bounds value with a specified override value.
        /// </summary>
        public BoundsOverride(bool doOverride, Bounds defaultValue, Bounds overrideValue)
        {
            this.doOverride = doOverride;
            this.defaultValue = defaultValue;
            this.overrideValue = overrideValue;
        }
        
        public BoundsOverride(bool doOverride, BoundsOverride value)
        {
            this.doOverride = doOverride;
            this.defaultValue = value.defaultValue;
            this.overrideValue = value.overrideValue;
        }
        
        /// <summary>
        /// Implicit conversion from the value override type to a value type according to its current override state. If being overriden, the override value is returned, otherwise the default value is returned.
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        public static implicit operator Bounds(BoundsOverride valueOverride) => valueOverride.Value;
        
        /// <summary>
        /// Implicit conversion from a value type to the value override type. The value will be assigned as the default and override values, and the override state will be set to false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator BoundsOverride(Bounds value) => new (false, value, value);
        
        /// <summary>
        /// Returns an equivalent value override with the override state switched/negated, thereby changing the current value (via the Value property). 
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        /// <remarks>
        /// This leverages the override value type's ability to operate as a binary value, in which the two states are the default value and override value.
        /// </remarks>
        public static BoundsOverride operator !(BoundsOverride valueOverride) => new(!valueOverride.doOverride, valueOverride);
    }
    
    /// <summary>
    /// Represents a structure that conditionally overrides a BoundsInt value.
    /// </summary>
    /// <remarks>
    /// This struct contains an override flag and a replacement BoundsInt value.
    /// The replacement value is returned when the override is enabled;
    /// otherwise, the original value is used.
    /// </remarks>
    [Serializable]
    public struct BoundsIntOverride
    {
        /// <summary>
        /// Indicates whether the override value should be applied in the computations
        /// or if the original value should be retained.
        /// </summary>
        public bool doOverride;

        /// <summary>
        /// Represents an optional override value for a BoundsInt.
        /// If the override is active, this value will replace the original BoundsInt value during evaluation.
        /// </summary>
        [SerializeField] private BoundsInt overrideValue;

        /// <summary>
        /// Represents the retained value for the override that is used when the override is not active.
        /// </summary>
        [SerializeField] private BoundsInt defaultValue;
        
        /// <summary>
        /// Retrieves the current value determined by whether the override is active or not.
        /// In the case of override, the override value is returned; otherwise, the original value is returned.
        /// </summary>
        public BoundsInt Value => (doOverride) ? overrideValue : defaultValue;

        /// <summary>
        /// Retrieves the currently set default value for this value override.
        /// </summary>
        public BoundsInt Default => defaultValue;
        
        /// <summary>
        /// The override value for this value override.
        /// </summary>
        public BoundsInt Override
        {
            get => overrideValue; 
            set => overrideValue = value;
        }
        
        /// <summary>
        /// Switch between the default and override states, affecting the returned value.
        /// </summary>
        public void Toggle() => doOverride = !doOverride;
        
        /// <summary>
        /// Reset the currently cached default value to a new value.
        /// </summary>
        /// <param name="newDefaultValue"> The new default value to be cached. </param>
        public void ResetDefaultValue(BoundsInt newDefaultValue) => defaultValue = newDefaultValue;

        /// <summary>
        /// A struct that optionally overrides a given BoundsInt value with a specified override value.
        /// </summary>
        public BoundsIntOverride(bool doOverride, BoundsInt defaultValue, BoundsInt overrideValue)
        {
            this.doOverride = doOverride;
            this.defaultValue = defaultValue;
            this.overrideValue = overrideValue;
        }
        
        public BoundsIntOverride(bool doOverride, BoundsIntOverride value)
        {
            this.doOverride = doOverride;
            this.defaultValue = value.defaultValue;
            this.overrideValue = value.overrideValue;
        }
        
        /// <summary>
        /// Implicit conversion from the value override type to a value type according to its current override state. If being overriden, the override value is returned, otherwise the default value is returned.
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        public static implicit operator BoundsInt(BoundsIntOverride valueOverride) => valueOverride.Value;
        
        /// <summary>
        /// Implicit conversion from a value type to the value override type. The value will be assigned as the default and override values, and the override state will be set to false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator BoundsIntOverride(BoundsInt value) => new (false, value, value);
        
        /// <summary>
        /// Returns an equivalent value override with the override state switched/negated, thereby changing the current value (via the Value property). 
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        /// <remarks>
        /// This leverages the override value type's ability to operate as a binary value, in which the two states are the default value and override value.
        /// </remarks>
        public static BoundsIntOverride operator !(BoundsIntOverride valueOverride) => new(!valueOverride.doOverride, valueOverride);
    }
    
    /// <summary>
    /// Represents a structure that conditionally overrides a Gradient value.
    /// </summary>
    /// <remarks>
    /// This struct contains an override flag and a replacement Gradient value.
    /// The replacement value is returned when the override is enabled;
    /// otherwise, the original value is used.
    /// </remarks>
    [Serializable]
    public struct GradientOverride
    {
        /// <summary>
        /// Indicates whether the override value should be applied in the computations
        /// or if the original value should be retained.
        /// </summary>
        public bool doOverride;

        /// <summary>
        /// Represents an optional override value for a Gradient.
        /// If the override is active, this value will replace the original Gradient value during evaluation.
        /// </summary>
        [SerializeField] private Gradient overrideValue;

        /// <summary>
        /// Represents the retained value for the override that is used when the override is not active.
        /// </summary>
        [SerializeField] private Gradient defaultValue;
        
        /// <summary>
        /// Retrieves the current value determined by whether the override is active or not.
        /// In the case of override, the override value is returned; otherwise, the original value is returned.
        /// </summary>
        public Gradient Value => (doOverride) ? overrideValue : defaultValue;

        /// <summary>
        /// Retrieves the currently set default value for this value override.
        /// </summary>
        public Gradient Default => defaultValue;
        
        /// <summary>
        /// The override value for this value override.
        /// </summary>
        public Gradient Override
        {
            get => overrideValue; 
            set => overrideValue = value;
        }
        
        /// <summary>
        /// Switch between the default and override states, affecting the returned value.
        /// </summary>
        public void Toggle() => doOverride = !doOverride;
        
        /// <summary>
        /// Reset the currently cached default value to a new value.
        /// </summary>
        /// <param name="newDefaultValue"> The new default value to be cached. </param>
        public void ResetDefaultValue(Gradient newDefaultValue) => defaultValue = newDefaultValue;

        /// <summary>
        /// A struct that optionally overrides a given Gradient value with a specified override value.
        /// </summary>
        public GradientOverride(bool doOverride, Gradient defaultValue, Gradient overrideValue)
        {
            this.doOverride = doOverride;
            this.defaultValue = defaultValue;
            this.overrideValue = overrideValue;
        }
        
        public GradientOverride(bool doOverride, GradientOverride value)
        {
            this.doOverride = doOverride;
            this.defaultValue = value.defaultValue;
            this.overrideValue = value.overrideValue;
        }
        
        /// <summary>
        /// Implicit conversion from the value override type to a value type according to its current override state. If being overriden, the override value is returned, otherwise the default value is returned.
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        public static implicit operator Gradient(GradientOverride valueOverride) => valueOverride.Value;
        
        /// <summary>
        /// Implicit conversion from a value type to the value override type. The value will be assigned as the default and override values, and the override state will be set to false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator GradientOverride(Gradient value) => new (false, value, value);
        
        /// <summary>
        /// Returns an equivalent value override with the override state switched/negated, thereby changing the current value (via the Value property). 
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        /// <remarks>
        /// This leverages the override value type's ability to operate as a binary value, in which the two states are the default value and override value.
        /// </remarks>
        public static GradientOverride operator !(GradientOverride valueOverride) => new(!valueOverride.doOverride, valueOverride);
    }
    
    /// <summary>
    /// Represents a structure that conditionally overrides a RenderingLayerMask value.
    /// </summary>
    /// <remarks>
    /// This struct contains an override flag and a replacement RenderingLayerMask value.
    /// The replacement value is returned when the override is enabled;
    /// otherwise, the original value is used.
    /// </remarks>
    [Serializable]
    public struct RenderingLayerMaskOverride
    {
        /// <summary>
        /// Indicates whether the override value should be applied in the computations
        /// or if the original value should be retained.
        /// </summary>
        public bool doOverride;

        /// <summary>
        /// Represents an optional override value for a RenderingLayerMask.
        /// If the override is active, this value will replace the original RenderingLayerMask value during evaluation.
        /// </summary>
        [SerializeField] private RenderingLayerMask overrideValue;

        /// <summary>
        /// Represents the retained value for the override that is used when the override is not active.
        /// </summary>
        [SerializeField] private RenderingLayerMask defaultValue;
        
        /// <summary>
        /// Retrieves the current value determined by whether the override is active or not.
        /// In the case of override, the override value is returned; otherwise, the original value is returned.
        /// </summary>
        public RenderingLayerMask Value => (doOverride) ? overrideValue : defaultValue;

        /// <summary>
        /// Retrieves the currently set default value for this value override.
        /// </summary>
        public RenderingLayerMask Default => defaultValue;
        
        /// <summary>
        /// The override value for this value override.
        /// </summary>
        public RenderingLayerMask Override
        {
            get => overrideValue; 
            set => overrideValue = value;
        }
        
        /// <summary>
        /// Switch between the default and override states, affecting the returned value.
        /// </summary>
        public void Toggle() => doOverride = !doOverride;
        
        /// <summary>
        /// Reset the currently cached default value to a new value.
        /// </summary>
        /// <param name="newDefaultValue"> The new default value to be cached. </param>
        public void ResetDefaultValue(RenderingLayerMask newDefaultValue) => defaultValue = newDefaultValue;

        /// <summary>
        /// A struct that optionally overrides a given RenderingLayerMask value with a specified override value.
        /// </summary>
        public RenderingLayerMaskOverride(bool doOverride, RenderingLayerMask defaultValue, RenderingLayerMask overrideValue)
        {
            this.doOverride = doOverride;
            this.defaultValue = defaultValue;
            this.overrideValue = overrideValue;
        }
        
        public RenderingLayerMaskOverride(bool doOverride, RenderingLayerMaskOverride value)
        {
            this.doOverride = doOverride;
            this.defaultValue = value.defaultValue;
            this.overrideValue = value.overrideValue;
        }
        
        /// <summary>
        /// Implicit conversion from the value override type to a value type according to its current override state. If being overriden, the override value is returned, otherwise the default value is returned.
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        public static implicit operator RenderingLayerMask(RenderingLayerMaskOverride valueOverride) => valueOverride.Value;
        
        /// <summary>
        /// Implicit conversion from a value type to the value override type. The value will be assigned as the default and override values, and the override state will be set to false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator RenderingLayerMaskOverride(RenderingLayerMask value) => new (false, value, value);
        
        /// <summary>
        /// Returns an equivalent value override with the override state switched/negated, thereby changing the current value (via the Value property). 
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        /// <remarks>
        /// This leverages the override value type's ability to operate as a binary value, in which the two states are the default value and override value.
        /// </remarks>
        public static RenderingLayerMaskOverride operator !(RenderingLayerMaskOverride valueOverride) => new(!valueOverride.doOverride, valueOverride);
    }
        
    /// <summary>
    /// Represents a structure that conditionally overrides a floating-point value.
    /// </summary>
    /// <remarks>
    /// This struct is designed to store an override flag and a replacement float value.
    /// The replacement value is used when the override flag is set; otherwise, the original value is retained.
    /// </remarks>
    [Serializable]
    public struct FloatOverride
    {
        /// <summary>
        /// Indicates whether the override value should be applied in the computations
        /// or if the original value should be retained.
        /// </summary>
        public bool doOverride;

        /// <summary>
        /// Represents an optional override value for a float.
        /// If the override is active, this value will replace the original float value during evaluation.
        /// </summary>
        [SerializeField] private float overrideValue;

        /// <summary>
        /// Represents the retained value for the override that is used when the override is not active.
        /// </summary>
        [SerializeField] private float defaultValue;
        
        /// <summary>
        /// Retrieves the current value determined by whether the override is active or not.
        /// In the case of override, the override value is returned; otherwise, the original value is returned.
        /// </summary>
        public float Value => (doOverride) ? overrideValue : defaultValue;

        /// <summary>
        /// Retrieves the currently set default value for this value override.
        /// </summary>
        public float Default => defaultValue;
        
        /// <summary>
        /// The override value for this value override.
        /// </summary>
        public float Override
        {
            get => overrideValue; 
            set => overrideValue = value;
        }
        
        /// <summary>
        /// Switch between the default and override states, affecting the returned value.
        /// </summary>
        public void Toggle() => doOverride = !doOverride;
        
        /// <summary>
        /// Reset the currently cached default value to a new value.
        /// </summary>
        /// <param name="newDefaultValue"> The new default value to be cached. </param>
        public void ResetDefaultValue(float newDefaultValue) => defaultValue = newDefaultValue;

        /// <summary>
        /// A struct that optionally overrides a given float value with a specified override value.
        /// </summary>
        public FloatOverride(bool doOverride, float defaultValue, float overrideValue)
        {
            this.doOverride = doOverride;
            this.defaultValue = defaultValue;
            this.overrideValue = overrideValue;
        }
        
        public FloatOverride(bool doOverride, FloatOverride value)
        {
            this.doOverride = doOverride;
            this.defaultValue = value.defaultValue;
            this.overrideValue = value.overrideValue;
        }
        
        /// <summary>
        /// Implicit conversion from the value override type to a value type according to its current override state. If being overriden, the override value is returned, otherwise the default value is returned.
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        public static implicit operator float(FloatOverride valueOverride) => valueOverride.Value;
        
        /// <summary>
        /// Implicit conversion from a value type to the value override type. The value will be assigned as the default and override values, and the override state will be set to false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator FloatOverride(float value) => new (false, value, value);
        
        /// <summary>
        /// Returns an equivalent value override with the override state switched/negated, thereby changing the current value (via the Value property). 
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        /// <remarks>
        /// This leverages the override value type's ability to operate as a binary value, in which the two states are the default value and override value.
        /// </remarks>
        public static FloatOverride operator !(FloatOverride valueOverride) => new(!valueOverride.doOverride, valueOverride);
    }
    
    /// <summary>
    /// Represents a structure that conditionally overrides a String value.
    /// </summary>
    /// <remarks>
    /// This struct contains an override flag and a replacement String value.
    /// The replacement value is returned when the override is enabled;
    /// otherwise, the original value is used.
    /// </remarks>
    [Serializable]
    public struct StringOverride
    {
        /// <summary>
        /// Indicates whether the override value should be applied in the computations
        /// or if the original value should be retained.
        /// </summary>
        public bool doOverride;

        /// <summary>
        /// Represents an optional override value for a String.
        /// If the override is active, this value will replace the original String value during evaluation.
        /// </summary>
        [SerializeField] private string overrideValue;

        /// <summary>
        /// Represents the retained value for the override that is used when the override is not active.
        /// </summary>
        [SerializeField] private string defaultValue;
        
        /// <summary>
        /// Retrieves the current value determined by whether the override is active or not.
        /// In the case of override, the override value is returned; otherwise, the original value is returned.
        /// </summary>
        public string Value => (doOverride) ? overrideValue : defaultValue;

        /// <summary>
        /// Retrieves the currently set default value for this value override.
        /// </summary>
        public string Default => defaultValue;
        
        /// <summary>
        /// The override value for this value override.
        /// </summary>
        public string Override
        {
            get => overrideValue; 
            set => overrideValue = value;
        }
        
        /// <summary>
        /// Switch between the default and override states, affecting the returned value.
        /// </summary>
        public void Toggle() => doOverride = !doOverride;
        
        /// <summary>
        /// Reset the currently cached default value to a new value.
        /// </summary>
        /// <param name="newDefaultValue"> The new default value to be cached. </param>
        public void ResetDefaultValue(string newDefaultValue) => defaultValue = newDefaultValue;

        /// <summary>
        /// A struct that optionally overrides a given String value with a specified override value.
        /// </summary>
        public StringOverride(bool doOverride, string defaultValue, string overrideValue)
        {
            this.doOverride = doOverride;
            this.defaultValue = defaultValue;
            this.overrideValue = overrideValue;
        }
        
        public StringOverride(bool doOverride, StringOverride value)
        {
            this.doOverride = doOverride;
            this.defaultValue = value.defaultValue;
            this.overrideValue = value.overrideValue;
        }
        
        /// <summary>
        /// Implicit conversion from the value override type to a value type according to its current override state. If being overriden, the override value is returned, otherwise the default value is returned.
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        public static implicit operator string(StringOverride valueOverride) => valueOverride.Value;
        
        /// <summary>
        /// Implicit conversion from a value type to the value override type. The value will be assigned as the default and override values, and the override state will be set to false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator StringOverride(string value) => new (false, value, value);
        
        /// <summary>
        /// Returns an equivalent value override with the override state switched/negated, thereby changing the current value (via the Value property). 
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        /// <remarks>
        /// This leverages the override value type's ability to operate as a binary value, in which the two states are the default value and override value.
        /// </remarks>
        public static StringOverride operator !(StringOverride valueOverride) => new(!valueOverride.doOverride, valueOverride);
    }
    
    /// <summary>
    /// Represents a structure that conditionally overrides an integer value.
    /// </summary>
    /// <remarks>
    /// This struct is designed to store an override flag and a replacement int value.
    /// The replacement value is used when the override flag is set; otherwise, the original value is retained.
    /// </remarks>
    [Serializable]
    public struct IntOverride
    {
        /// <summary>
        /// Indicates whether the override value should be applied in the computations
        /// or if the original value should be retained.
        /// </summary>
        public bool doOverride;

        /// <summary>
        /// Represents an optional override value for an int.
        /// If the override is active, this value will replace the original int value during evaluation.
        /// </summary>
        [SerializeField] private int overrideValue;

        /// <summary>
        /// Represents the retained value for the override that is used when the override is not active.
        /// </summary>
        [SerializeField] private int defaultValue;
        
        /// <summary>
        /// Retrieves the current value determined by whether the override is active or not.
        /// In the case of override, the override value is returned; otherwise, the original value is returned.
        /// </summary>
        public int Value => (doOverride) ? overrideValue : defaultValue;

        /// <summary>
        /// Retrieves the currently set default value for this value override.
        /// </summary>
        public int Default => defaultValue;
        
        /// <summary>
        /// The override value for this value override.
        /// </summary>
        public int Override
        {
            get => overrideValue; 
            set => overrideValue = value;
        }
        
        /// <summary>
        /// Switch between the default and override states, affecting the returned value.
        /// </summary>
        public void Toggle() => doOverride = !doOverride;
        
        /// <summary>
        /// Reset the currently cached default value to a new value.
        /// </summary>
        /// <param name="newDefaultValue"> The new default value to be cached. </param>
        public void ResetDefaultValue(int newDefaultValue) => defaultValue = newDefaultValue;

        /// <summary>
        /// A struct that optionally overrides a given int value with a specified override value.
        /// </summary>
        public IntOverride(bool doOverride, int defaultValue, int overrideValue)
        {
            this.doOverride = doOverride;
            this.defaultValue = defaultValue;
            this.overrideValue = overrideValue;
        }
        
        public IntOverride(bool doOverride, IntOverride value)
        {
            this.doOverride = doOverride;
            this.defaultValue = value.defaultValue;
            this.overrideValue = value.overrideValue;
        }
        
        /// <summary>
        /// Implicit conversion from the value override type to a value type according to its current override state. If being overriden, the override value is returned, otherwise the default value is returned.
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        public static implicit operator int(IntOverride valueOverride) => valueOverride.Value;
        
        /// <summary>
        /// Implicit conversion from a value type to the value override type. The value will be assigned as the default and override values, and the override state will be set to false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator IntOverride(int value) => new (false, value, value);
        
        /// <summary>
        /// Returns an equivalent value override with the override state switched/negated, thereby changing the current value (via the Value property). 
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        /// <remarks>
        /// This leverages the override value type's ability to operate as a binary value, in which the two states are the default value and override value.
        /// </remarks>
        public static IntOverride operator !(IntOverride valueOverride) => new(!valueOverride.doOverride, valueOverride);
    }
    
    /// <summary>
    /// Represents a structure that conditionally overrides a Hash128 value.
    /// </summary>
    /// <remarks>
    /// This struct contains an override flag and a replacement Hash128 value.
    /// The replacement value is returned when the override is enabled;
    /// otherwise, the original value is used.
    /// </remarks>
    [Serializable]
    public struct Hash128Override
    {
        /// <summary>
        /// Indicates whether the override value should be applied in the computations
        /// or if the original value should be retained.
        /// </summary>
        public bool doOverride;

        /// <summary>
        /// Represents an optional override value for a Hash128.
        /// If the override is active, this value will replace the original Hash128 value during evaluation.
        /// </summary>
        [SerializeField] private Hash128 overrideValue;

        /// <summary>
        /// Represents the retained value for the override that is used when the override is not active.
        /// </summary>
        [SerializeField] private Hash128 defaultValue;
        
        /// <summary>
        /// Retrieves the current value determined by whether the override is active or not.
        /// In the case of override, the override value is returned; otherwise, the original value is returned.
        /// </summary>
        public Hash128 Value => (doOverride) ? overrideValue : defaultValue;

        /// <summary>
        /// Retrieves the currently set default value for this value override.
        /// </summary>
        public Hash128 Default => defaultValue;
        
        /// <summary>
        /// The override value for this value override.
        /// </summary>
        public Hash128 Override
        {
            get => overrideValue; 
            set => overrideValue = value;
        }
        
        /// <summary>
        /// Switch between the default and override states, affecting the returned value.
        /// </summary>
        public void Toggle() => doOverride = !doOverride;
        
        /// <summary>
        /// Reset the currently cached default value to a new value.
        /// </summary>
        /// <param name="newDefaultValue"> The new default value to be cached. </param>
        public void ResetDefaultValue(Hash128 newDefaultValue) => defaultValue = newDefaultValue;

        /// <summary>
        /// A struct that optionally overrides a given Hash128 value with a specified override value.
        /// </summary>
        public Hash128Override(bool doOverride, Hash128 defaultValue, Hash128 overrideValue)
        {
            this.doOverride = doOverride;
            this.defaultValue = defaultValue;
            this.overrideValue = overrideValue;
        }
        
        public Hash128Override(bool doOverride, Hash128Override value)
        {
            this.doOverride = doOverride;
            this.defaultValue = value.defaultValue;
            this.overrideValue = value.overrideValue;
        }
        
        /// <summary>
        /// Implicit conversion from the value override type to a value type according to its current override state. If being overriden, the override value is returned, otherwise the default value is returned.
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        public static implicit operator Hash128(Hash128Override valueOverride) => valueOverride.Value;
        
        /// <summary>
        /// Implicit conversion from a value type to the value override type. The value will be assigned as the default and override values, and the override state will be set to false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator Hash128Override(Hash128 value) => new (false, value, value);
        
        /// <summary>
        /// Returns an equivalent value override with the override state switched/negated, thereby changing the current value (via the Value property). 
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        /// <remarks>
        /// This leverages the override value type's ability to operate as a binary value, in which the two states are the default value and override value.
        /// </remarks>
        public static Hash128Override operator !(Hash128Override valueOverride) => new(!valueOverride.doOverride, valueOverride);
    }
    
    /// <summary>
    /// Represents a structure that conditionally overrides a char value.
    /// </summary>
    /// <remarks>
    /// This struct contains an override flag and a replacement char value.
    /// The replacement value is returned when the override is enabled;
    /// otherwise, the original value is used.
    /// </remarks>
    [Serializable]
    public struct CharOverride
    {
        /// <summary>
        /// Indicates whether the override value should be applied in the computations
        /// or if the original value should be retained.
        /// </summary>
        public bool doOverride;

        /// <summary>
        /// Represents an optional override value for a char.
        /// If the override is active, this value will replace the original char value during evaluation.
        /// </summary>
        [SerializeField] private char overrideValue;

        /// <summary>
        /// Represents the retained value for the override that is used when the override is not active.
        /// </summary>
        [SerializeField] private char defaultValue;
        
        /// <summary>
        /// Retrieves the current value determined by whether the override is active or not.
        /// In the case of override, the override value is returned; otherwise, the original value is returned.
        /// </summary>
        public char Value => (doOverride) ? overrideValue : defaultValue;

        /// <summary>
        /// Retrieves the currently set default value for this value override.
        /// </summary>
        public char Default => defaultValue;
        
        /// <summary>
        /// The override value for this value override.
        /// </summary>
        public char Override
        {
            get => overrideValue; 
            set => overrideValue = value;
        }
        
        /// <summary>
        /// Switch between the default and override states, affecting the returned value.
        /// </summary>
        public void Toggle() => doOverride = !doOverride;
        
        /// <summary>
        /// Reset the currently cached default value to a new value.
        /// </summary>
        /// <param name="newDefaultValue"> The new default value to be cached. </param>
        public void ResetDefaultValue(char newDefaultValue) => defaultValue = newDefaultValue;

        /// <summary>
        /// A struct that optionally overrides a given char value with a specified override value.
        /// </summary>
        public CharOverride(bool doOverride, char defaultValue, char overrideValue)
        {
            this.doOverride = doOverride;
            this.defaultValue = defaultValue;
            this.overrideValue = overrideValue;
        }
        
        public CharOverride(bool doOverride, CharOverride value)
        {
            this.doOverride = doOverride;
            this.defaultValue = value.defaultValue;
            this.overrideValue = value.overrideValue;
        }
        /// <summary>
        /// Implicit conversion from the value override type to a value type according to its current override state. If being overriden, the override value is returned, otherwise the default value is returned.
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        public static implicit operator char(CharOverride valueOverride) => valueOverride.Value;
        
        /// <summary>
        /// Implicit conversion from a value type to the value override type. The value will be assigned as the default and override values, and the override state will be set to false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator CharOverride(char value) => new (false, value, value);
        
        /// <summary>
        /// Returns an equivalent value override with the override state switched/negated, thereby changing the current value (via the Value property). 
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        /// <remarks>
        /// This leverages the override value type's ability to operate as a binary value, in which the two states are the default value and override value.
        /// </remarks>
        public static CharOverride operator !(CharOverride valueOverride) => new(!valueOverride.doOverride, valueOverride);
    }
    
    #if UNITY_6000_2_OR_NEWER
    
    /// <summary>
    /// Represents a structure that conditionally overrides a EntityId value.
    /// </summary>
    /// <remarks>
    /// This struct contains an override flag and a replacement EntityId value.
    /// The replacement value is returned when the override is enabled;
    /// otherwise, the original value is used.
    /// </remarks>
    [Serializable]
    public struct EntityIdOverride
    {
        /// <summary>
        /// Indicates whether the override value should be applied in the computations
        /// or if the original value should be retained.
        /// </summary>
        public bool doOverride;

        /// <summary>
        /// Represents an optional override value for a EntityId.
        /// If the override is active, this value will replace the original EntityId value during evaluation.
        /// </summary>
        [SerializeField] private EntityId overrideValue;

        /// <summary>
        /// Represents the retained value for the override that is used when the override is not active.
        /// </summary>
        [SerializeField] private EntityId defaultValue;
        
        /// <summary>
        /// Retrieves the current value determined by whether the override is active or not.
        /// In the case of override, the override value is returned; otherwise, the original value is returned.
        /// </summary>
        public EntityId Value => (doOverride) ? overrideValue : defaultValue;

        /// <summary>
        /// Retrieves the currently set default value for this value override.
        /// </summary>
        public EntityId Default => defaultValue;
        
        /// <summary>
        /// The override value for this value override.
        /// </summary>
        public EntityId Override
        {
            get => overrideValue; 
            set => overrideValue = value;
        }
        
        /// <summary>
        /// Switch between the default and override states, affecting the returned value.
        /// </summary>
        public void Toggle() => doOverride = !doOverride;
        
        /// <summary>
        /// Reset the currently cached default value to a new value.
        /// </summary>
        /// <param name="newDefaultValue"> The new default value to be cached. </param>
        public void ResetDefaultValue(EntityId newDefaultValue) => defaultValue = newDefaultValue;

        /// <summary>
        /// A struct that optionally overrides a given EntityId value with a specified override value.
        /// </summary>
        public EntityIdOverride(bool doOverride, EntityId defaultValue, EntityId overrideValue)
        {
            this.doOverride = doOverride;
            this.defaultValue = defaultValue;
            this.overrideValue = overrideValue;
        }
        
        public EntityIdOverride(bool doOverride, EntityIdOverride value)
        {
            this.doOverride = doOverride;
            this.defaultValue = value.defaultValue;
            this.overrideValue = value.overrideValue;
        }
        /// <summary>
        /// Implicit conversion from the value override type to a value type according to its current override state. If being overriden, the override value is returned, otherwise the default value is returned.
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        public static implicit operator EntityId(EntityIdOverride valueOverride) => valueOverride.Value;
        
        /// <summary>
        /// Implicit conversion from a value type to the value override type. The value will be assigned as the default and override values, and the override state will be set to false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator EntityIdOverride(EntityId value) => new (false, value, value);
        
        /// <summary>
        /// Returns an equivalent value override with the override state switched/negated, thereby changing the current value (via the Value property). 
        /// </summary>
        /// <param name="valueOverride"></param>
        /// <returns></returns>
        /// <remarks>
        /// This leverages the override value type's ability to operate as a binary value, in which the two states are the default value and override value.
        /// </remarks>
        public static EntityIdOverride operator !(EntityIdOverride valueOverride) => new(!valueOverride.doOverride, valueOverride);
    }
    
    #endif
}
