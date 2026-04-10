using UnityEngine;

namespace PsigenVision.Utilities
{
    public static class VectorExtensions
    {
        #region Methods - Safe

        /// <summary>
        /// Projects a given vector onto a specified direction vector.
        /// </summary>
        /// <param name="vector">The vector to be projected. The original vector is modified in place.</param>
        /// <param name="direction">The direction vector onto which the projection will occur. This vector will be normalized internally.</param>
        /// <returns>Returns the projected vector in the specified direction.</returns>
        public static Vector3 Project(this Vector3 vector, Vector3 direction)
        {
            direction.Normalize();
            return Vector3.Dot(vector, direction) * direction;
        }

        /// <summary>
        /// Calculates the signed magnitude (scalar projection) of a given vector onto a specified direction vector.
        /// </summary>
        /// <param name="vector">The vector to be projected.</param>
        /// <param name="direction">
        /// The direction vector onto which the projection will occur. This vector does not need to be normalized, as it
        /// will be normalized automatically.
        /// </param>
        /// <returns>
        /// A float representing the signed scalar magnitude of the projection of the vector onto the direction. 
        /// A positive value indicates alignment with the direction, while a negative value indicates opposition.
        /// </returns>
        public static float ProjectMagnitude(this Vector3 vector, Vector3 direction)
        {
            direction.Normalize();
            return Vector3.Dot(vector, direction);
        }

        /// <summary>
        /// Removes the component of a vector that is projected onto a specified direction vector.
        /// </summary>
        /// <param name="vector">The original vector to adjust. The original vector is modified in place.</param>
        /// <param name="direction">
        /// The direction vector representing the direction of the projection. This vector does not need to be normalized; it will be normalized internally.
        /// </param>
        /// <remarks>
        /// This method is useful when you need to eliminate unwanted velocity or movement in the direction of a specified vector
        /// (e.g., removing planar momentum from a character controller's horizontal velocity).
        /// </remarks>
        /// <returns>
        /// A `Vector3` that excludes any contribution of the original vector in the specified direction. The resulting vector is orthogonal to the specified direction.
        /// </returns>
        public static Vector3 RemoveProjection(this Vector3 vector, Vector3 direction)
        {
            direction.Normalize();
            return vector - Vector3.Dot(vector, direction) * direction;
        }
        /// <summary>
        /// Translates/maps a 2D vector (Vector2) into a 3D vector (Vector3) by projecting along two custom 3D directions (xDirection, yDirection).
        /// </summary>
        /// <param name="vector2">The 2D vector to be mapped.</param>
        /// <param name="xAxis3D">The 3D vector representing the X-axis direction in the 3D space.</param>
        /// <param name="yAxis3D">The 3D vector representing the Y-axis direction in the 3D space.</param>
        /// <returns>Returns a 3D vector resulting from the mapping of the 2D vector onto the specified 3D axes.</returns>
        public static Vector3 MapTo3D(this Vector2 vector2, Vector3 xAxis3D, Vector3 yAxis3D)
            => vector2.x * xAxis3D + vector2.y * yAxis3D;
        
        /// <summary>
        /// Projects a 2D vector onto a 3D plane using specified 2D axes and the plane's normal vector.
        /// </summary>
        /// <param name="vector2">The 2D vector to be projected onto the plane.</param>
        /// <param name="xAxis2D">The 2D x-axis vector to define the plane alignment.</param>
        /// <param name="yAxis2D">The 2D y-axis vector to define the plane alignment.</param>
        /// <param name="planeNormal">The normal vector of the target plane for projection.</param>
        /// <returns>Returns the resulting 3D vector after projecting the 2D vector onto the plane.</returns>
        /// <remarks>
        /// Normalizes the resulting vectors from the projection before scaling and summing them based on the input Vector2
        /// </remarks>
        /*
         * it projects 2D directions (xDirection and yDirection) onto a 3D space along a plane defined by projectedPlaneNormal. It normalizes the resulting vectors from the projection before scaling and summing them based on the input Vector2.
         */
        public static Vector3 ProjectToPlane(this Vector2 vector2, Vector2 xAxis2D, Vector2 yAxis2D, Vector3 planeNormal) 
            => vector2.x * Vector3.ProjectOnPlane(xAxis2D, planeNormal).normalized 
               + vector2.y * Vector3.ProjectOnPlane(yAxis2D, planeNormal).normalized; // Projects a 2D vector onto a 3D plane along specified axes


        /// <summary>
        /// Changes the magnitude of the given vector to the specified value while maintaining its direction.
        /// </summary>
        /// <param name="vector3">The vector whose magnitude is to be changed. The direction of this vector remains unchanged.</param>
        /// <param name="magnitude">The new magnitude to assign to the vector.</param>
        /// <returns>Returns a new vector with the specified magnitude and the same direction as the input vector.</returns>
        public static Vector3 ChangeMagnitude(this Vector3 vector3, float magnitude) => magnitude * vector3.normalized;

        /// <summary>
        /// Moves a vector towards a specified target position by a maximum distance delta.
        /// </summary>
        /// <param name="me">The current vector position to be moved.</param>
        /// <param name="towards">The target vector position to move towards.</param>
        /// <param name="maxDistanceDelta">The maximum distance the vector can move in this step.</param>
        /// <returns>Returns the new vector position after moving towards the target position.</returns>
        public static Vector3 MoveMeTowards(this Vector3 me, Vector3 towards, float maxDistanceDelta) =>
            Vector3.MoveTowards(me, towards, maxDistanceDelta);

        /// <summary>
        /// Moves the vector closer to the origin (zero) by a maximum distance specified.
        /// </summary>
        /// <param name="vector">The starting vector to move towards the origin.</param>
        /// <param name="maxDistanceDelta">The maximum distance the vector can move towards the origin.</param>
        /// <returns>Returns the updated vector, closer to the origin within the specified distance.</returns>
        public static Vector3 MoveTowardsZero(this Vector3 vector, float maxDistanceDelta) =>
            Vector3.MoveTowards(vector, Vector3.zero, maxDistanceDelta);

        #endregion

        #region Methods - Unsafe

                /// <summary>
        /// Projects a given vector onto a specified direction vector.
        /// </summary>
        /// <param name="vector">The vector to be projected. The original vector is modified in place.</param>
        /// <param name="direction">The direction vector onto which the projection will occur. This vector MUST BE normalized before being passed into this method.</param>
        /// <returns>Returns the projected vector in the specified direction.</returns>
        public static Vector3 ProjectUnsafe(this Vector3 vector, Vector3 direction) =>
            Vector3.Dot(vector, direction) * direction;

        /// <summary>
        /// Calculates the signed magnitude (scalar projection) of a given vector onto a specified direction vector.
        /// </summary>
        /// <param name="vector">The vector to be projected.</param>
        /// <param name="direction">
        /// The direction vector onto which the projection will occur. This vector MUST BE normalized before being passed into this method.
        /// </param>
        /// <returns>
        /// A float representing the signed scalar magnitude of the projection of the vector onto the direction. 
        /// A positive value indicates alignment with the direction, while a negative value indicates opposition.
        /// </returns>
        public static float ProjectMagnitudeUnsafe(this Vector3 vector, Vector3 direction) => Vector3.Dot(vector, direction);

        /// <summary>
        /// Removes the component of a vector that is projected onto a specified direction vector.
        /// </summary>
        /// <param name="vector">The original vector to adjust. The original vector is modified in place.</param>
        /// <param name="direction">
        /// The direction vector representing the direction of the projection. This vector MUST BE normalized before being passed into this method.
        /// </param>
        /// <remarks>
        /// This method is useful when you need to eliminate unwanted velocity or movement in the direction of a specified vector
        /// (e.g., removing planar momentum from a character controller's horizontal velocity).
        /// </remarks>
        /// <returns>
        /// A `Vector3` that excludes any contribution of the original vector in the specified direction. The resulting vector is orthogonal to the specified direction.
        /// </returns>
        public static Vector3 RemoveProjectionUnsafe(this Vector3 vector, Vector3 direction)
        {
            direction.Normalize();
            return vector - Vector3.Dot(vector, direction) * direction;
        }
        /// <summary>
        /// Translates/maps a 2D vector (Vector2) into a 3D vector (Vector3) by projecting along two custom 3D directions (xDirection, yDirection).
        /// </summary>
        /// <param name="vector2">The 2D vector to be mapped.</param>
        /// <param name="xAxis3D">The 3D vector representing the X-axis direction in the 3D space.</param>
        /// <param name="yAxis3D">The 3D vector representing the Y-axis direction in the 3D space.</param>
        /// <returns>Returns a 3D vector resulting from the mapping of the 2D vector onto the specified 3D axes.</returns>
        public static Vector3 MapTo3DUnsafe(this Vector2 vector2, Vector3 xAxis3D, Vector3 yAxis3D)
            => vector2.x * xAxis3D + vector2.y * yAxis3D;
        
        /// <summary>
        /// Projects a 2D vector onto a 3D plane using specified 2D axes and the plane's normal vector.
        /// </summary>
        /// <param name="vector2">The 2D vector to be projected onto the plane.</param>
        /// <param name="xAxis2D">The 2D x-axis vector to define the plane alignment.</param>
        /// <param name="yAxis2D">The 2D y-axis vector to define the plane alignment.</param>
        /// <param name="planeNormal">The normal vector of the target plane for projection.</param>
        /// <returns>Returns the resulting 3D vector after projecting the 2D vector onto the plane.</returns>
        /// <remarks>
        /// Normalizes the resulting vectors from the projection before scaling and summing them based on the input Vector2
        /// </remarks>
        /*
         * it projects 2D directions (xDirection and yDirection) onto a 3D space along a plane defined by projectedPlaneNormal. It normalizes the resulting vectors from the projection before scaling and summing them based on the input Vector2.
         */
        public static Vector3 ProjectToPlaneUnsafe(this Vector2 vector2, Vector2 xAxis2D, Vector2 yAxis2D, Vector3 planeNormal) 
            => vector2.x * Vector3.ProjectOnPlane(xAxis2D, planeNormal).normalized 
               + vector2.y * Vector3.ProjectOnPlane(yAxis2D, planeNormal).normalized; // Projects a 2D vector onto a 3D plane along specified axes

        #endregion
    }
}