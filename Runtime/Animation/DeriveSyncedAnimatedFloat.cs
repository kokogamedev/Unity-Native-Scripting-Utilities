using System;
using UnityEngine;
using PsigenVision.Utilities.Math;

namespace PsigenVision.Animation
{
    /// <summary>
    /// The DeriveSyncedAnimatedFloat class is a MonoBehaviour
    /// used for synchronizing one animated float property with another.
    /// It provides functionality to normalize and synchronize properties
    /// based on defined ranges or animation curves.
    /// </summary>
    public class DeriveSyncedAnimatedFloat : MonoBehaviour
    {
        /// <summary>
        /// Represents the animated float property whose animation parameters will be used
        /// to synchronize another animated float property. This property serves as the source
        /// data for normalization and synchronization. Its minimum and maximum values define the
        /// range of the normalized level used in synchronization calculations.
        /// </summary>
        [Tooltip("The animation parameters for animated float to which we are syncing")] [SerializeField]
        public AnimatedPropertyData animatedFloatToSyncWith = new AnimatedPropertyData(0f, 1f);

        /// <summary>
        /// Represents the animated float whose properties (e.g., range and optional animation curve)
        /// are synchronized based on the normalized parameters of another animated property. This
        /// synchronization ensures that changes in the source property are reflected proportionally
        /// in this property, according to its defined range or animation curve.
        /// </summary>
        [Tooltip("The animation parameters for animated float we bringing into sync")] [SerializeField]
        public AnimatedPropertyData animatedFloatToSync = new AnimatedPropertyData(0f, 1f);
        
        private float lastValueToSyncWith = Mathf.Infinity;
        private bool isInitialized = false;

        // Start is called before the first frame update
        void Awake()
        {
            if (animatedFloatToSyncWith.maxValue <= animatedFloatToSyncWith.minValue)
            {
                isInitialized = false;
                Debug.LogError(
                    "The minimum minimum value of the float with which we are syncing cannot be equal to or greater than the maximum material global alpha");
            }

            if (animatedFloatToSync.maxValue <= animatedFloatToSync.minValue)
            {
                isInitialized = false;
                Debug.LogError(
                    "The minimum value of the float being synced cannot be equal to or greater than its maximum value");
            }
        }

        /// <summary>
        /// Retrieves a synchronized float value based on the provided input value.
        /// </summary>
        /// <param name="valueToSyncWith">The input float value to synchronize with.</param>
        /// <param name="syncedValue">The resulting synchronized float value.</param>
        /// <returns>True if the synchronized value was updated, otherwise false.</returns>
        public bool GetSyncedFloatValue(float valueToSyncWith, out float syncedValue)
        {
            //If the value to sync with has not changed, do not derive synced float data
            if (Mathf.Approximately(valueToSyncWith, lastValueToSyncWith))
            {
                syncedValue = valueToSyncWith;
                return false;
            }
            //Get the normalized value of the animated float value with which we are syncing given its maximum and minmum values
            float level = animatedFloatToSyncWith.NormalizePropertyValue(valueToSyncWith);
            //Debug.Log($"`float value to sync with is at normalized level `{level}`");
            //Get the synced animated true float value relative to its minimum and maximum values
            syncedValue = animatedFloatToSync.GetAbsolutePropertyValue(level);
            //Retain the current valueToSyncWith for next call (to check for level sameness)
            lastValueToSyncWith = valueToSyncWith;
            return true;
        }


        [Serializable]
        public struct AnimatedPropertyData
        {
            public AnimatedPropertyData(float minValue, float maxValue, bool useAnimationCurve = false, AnimationCurve animatedPropertyCurve = null)
            {
                this.minValue = minValue;
                this.maxValue = maxValue;
                if (this.useAnimationCurve = useAnimationCurve)
                {
                    this.animatedPropertyCurve = animatedPropertyCurve;
                    this.inverseAnimatedPropertyCurve = (AnimationCurveHelper.IsCurveApproximatelyLinear(animatedPropertyCurve)) 
                        ? this.animatedPropertyCurve 
                        : AnimationCurveHelper.InvertCurve(animatedPropertyCurve);
                }
                else
                {
                    this.animatedPropertyCurve = null;
                    this.inverseAnimatedPropertyCurve = null;
                }
            }

            [Tooltip("The maximum value permitted")]
            public float maxValue;

            [Tooltip("The minimum value permitted")]
            public float minValue;

            public bool useAnimationCurve;
            
            [Tooltip("The normalized global alpha over the range of the normalized particle system speed")] [SerializeField]
            public AnimationCurve animatedPropertyCurve;

            private AnimationCurve inverseAnimatedPropertyCurve;

            /// <summary>
            /// Return the normalized value of the passed in property given the minimum and maximum values within this data
            /// </summary>
            /// <param name="propertyValue"></param>
            /// <returns></returns>
            public float NormalizePropertyValue(float propertyValue)
            {
                if (useAnimationCurve)
                {
                    return Mathf.InverseLerp(minValue, maxValue, inverseAnimatedPropertyCurve.Evaluate(propertyValue));
                }
                return Mathf.InverseLerp(minValue, maxValue,
                    propertyValue);  //(propertyValue - minValue) / (maxValue - minValue);
            }

            /// <summary>
            /// Return the true value of the property given the passed in normalized property value given the minimum and maximum values within this data
            /// </summary>
            /// <param name="normalizedLevel"></param>
            /// <returns></returns>
            public float GetAbsolutePropertyValue(float normalizedLevel)
            {
                if (useAnimationCurve)
                {
                    return Mathf.Lerp(minValue, maxValue, animatedPropertyCurve.Evaluate(normalizedLevel));
                }
                return Mathf.Lerp(minValue, maxValue, normalizedLevel);  //normalizedLevel * (maxValue - minValue) + minValue;
            }
        }
    }
}