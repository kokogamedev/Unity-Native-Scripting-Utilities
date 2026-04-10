using UnityEngine;

namespace PsigenVision.Utilities.Math
{
    public static class AngleHelper
    {
        /// <summary>
        /// Wraps an angle to the range of -180 to 180 degrees.
        /// </summary>
        /// <param name="angle">The angle in degrees to normalize.</param>
        /// <returns>The normalized angle in degrees.</returns>
        public static float WrapAngle(float angle)
        {
            // If the angle is already within the range -180 to 180, return it directly.
            if (angle is >= -180f and <= 180f)
                return angle;
            // 1. Shift the range from [-180, 180] to [0, 360]
            // 2. Use double modulo to handle large positive/negative values -> This "double modulo" trick is essential in C# because the % operator can return negative values for negative inputs. This ensures the result is always a positive number between 0 and 360.
            // 3. Shift back to the [-180, 180] range
            return ((angle + 180) % 360 + 360) % 360 - 180;
        }

        /// <summary>
        /// Normalizes each component of the Vector3 representing Euler angles to the range of -180 to 180 degrees.
        /// </summary>
        /// <param name="eulerAngles">The Vector3 representing Euler angles.</param>
        /// <returns>The normalized Vector3 representing Euler angles.</returns>
        public static Vector3 NormalizeEulerAngles(this Vector3 eulerAngles)
        {
            eulerAngles.x = WrapAngle(eulerAngles.x);
            eulerAngles.y = WrapAngle(eulerAngles.y);
            eulerAngles.z = WrapAngle(eulerAngles.z);
            return eulerAngles;
        }
    }
}