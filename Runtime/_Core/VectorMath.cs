using System.Numerics;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace PsigenVision.Utilities
{
    public class VectorMath
	{
		#region Safe Methods

		/// <summary>
		/// Projects a given vector onto a specified direction vector.
		/// </summary>
		/// <param name="vector">The vector to be projected.</param>
		/// <param name="direction">The direction vector onto which the projection will occur. This vector does not need to be normalized; it will be normalized internally.</param>
		/// <returns>Returns the projected vector in the specified direction.</returns>
		public static Vector3 GetProjectedVector(Vector3 vector, Vector3 direction)
		{
			//Perform vector projection by multiplying our desired normalized direction vector by the magnitude of the vector when projected onto this direction, which is extracted via the dot product of our original vector with the normalized direction vector
			direction.Normalize(); 
			return Vector3.Dot(vector, direction) * direction;
		}
		
		/// <summary>
		/// Projects a given vector onto a specified direction vector.
		/// </summary>
		/// <param name="vector">The vector to be projected.</param>
		/// <param name="direction">The direction vector onto which the projection will occur. This vector does not need to be normalized; it will be normalized internally.</param>
		public static void Project(ref Vector3 vector, Vector3 direction)
		{
			//Perform vector projection by multiplying our desired normalized direction vector by the magnitude of the vector when projected onto this direction, which is extracted via the dot product of our original vector with the normalized direction vector
			direction.Normalize(); 
			vector = Vector3.Dot(vector, direction) * direction;
		}

		/// <summary>
		/// Removes the component of a vector that is projected onto a specified direction vector.
		/// </summary>
		/// <param name="vector">The original vector to adjust.</param>
		/// <param name="direction">
		/// The direction vector representing the direction of the projection. This vector does not need to be normalized; it will be normalized internally.
		/// </param>
		/// <returns>
		/// A `Vector3` that excludes any contribution of the original vector in the specified direction. The resulting vector is orthogonal to the specified direction.
		/// </returns>
		/// <remarks>
		/// This method is useful when you need to eliminate unwanted velocity or movement in the direction of a specified vector (e.g., removing planar momentum from a character controller's horizontal velocity).
		/// </remarks>
		public static Vector3 RemoveProjection(Vector3 vector, Vector3 direction)
		{
			direction.Normalize();
			return vector - Vector3.Dot(vector, direction) * direction;
		}
		
		/// <summary>
		/// Removes the component of a vector that is projected onto a specified direction vector.
		/// </summary>
		/// <param name="vector">The original vector to adjust.</param>
		/// <param name="direction">
		/// The direction vector representing the direction of the projection. This vector does not need to be normalized; it will be normalized internally.
		/// </param>
		/// <remarks>
		/// This method is useful when you need to eliminate unwanted velocity or movement in the direction of a specified vector (e.g., removing planar momentum from a character controller's horizontal velocity).
		/// </remarks>
		public static void RemoveProjection(ref Vector3 vector, Vector3 direction)
		{
			direction.Normalize();
			vector -= Vector3.Dot(vector, direction) * direction;
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
		public static float GetProjectedMagnitude(Vector3 vector, Vector3 direction)
		{
			// Calculate the scalar projection using the dot product of the vector and the normalized direction vector.
			// The sign of the result indicates the alignment (positive = same direction, negative = opposite direction).
			return Vector3.Dot(vector, direction.normalized);
		}

		/// <summary>
		/// Translates/maps a 2D vector (Vector2) into a 3D vector (Vector3) by projecting along two custom 3D directions (xDirection, yDirection).
		/// </summary>
		/// <param name="vector2">The 2D vector to be mapped.</param>
		/// <param name="xAxis3D">The 3D vector representing the X-axis direction in the 3D space.</param>
		/// <param name="yAxis3D">The 3D vector representing the Y-axis direction in the 3D space.</param>
		/// <returns>Returns a 3D vector resulting from the mapping of the 2D vector onto the specified 3D axes.</returns>
		public static Vector3 Map2DTo3D(Vector2 vector2, Vector3 xAxis3D, Vector3 yAxis3D)
			=> vector2.x * xAxis3D + vector2.y * yAxis3D; // Maps a 2D vector into 3D space using custom axes


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
		public static Vector3 Project2DToPlane(Vector2 vector2, Vector2 xAxis2D, Vector2 yAxis2D, Vector3 planeNormal) 
			=> vector2.x * Vector3.ProjectOnPlane(xAxis2D, planeNormal).normalized 
			   + vector2.y * Vector3.ProjectOnPlane(yAxis2D, planeNormal).normalized; // Projects a 2D vector onto a 3D plane along specified axes
		
		/// <summary>
		/// Changes the magnitude of the given vector to the specified value while maintaining its direction.
		/// </summary>
		/// <param name="vector3">The vector whose magnitude is to be changed. The direction of this vector remains unchanged.</param>
		/// <param name="magnitude">The new magnitude to assign to the vector.</param>
		/// <returns>Returns a new vector with the specified magnitude and the same direction as the input vector.</returns>
		public static Vector3 ChangeMagnitude(Vector3 vector3, float magnitude) => magnitude * vector3.normalized;
		
		/// <summary>
		/// Changes the magnitude of the given vector to the specified value while maintaining its direction.
		/// </summary>
		/// <param name="vector3">The vector whose magnitude is to be changed. The direction of this vector remains unchanged.</param>
		/// <param name="magnitude">The new magnitude to assign to the vector.</param>
		/// <returns>Returns a new vector with the specified magnitude and the same direction as the input vector.</returns>
		public static void ChangeMagnitude(ref Vector3 vector3, float magnitude) => vector3 = magnitude * vector3.normalized;

		/// <summary>
		/// Moves a vector closer to a target vector by a maximum distance delta.
		/// </summary>
		/// <param name="me">The vector that will be moved towards the target.</param>
		/// <param name="towards">The target vector to move towards.</param>
		/// <param name="maxDistanceDelta">The maximum distance the vector can move toward the target in this operation.</param>
		public static void MoveMeTowards(ref Vector3 me, Vector3 towards, float maxDistanceDelta) =>
			me = Vector3.MoveTowards(me, towards, maxDistanceDelta);

		/// <summary>
		/// Gradually moves a vector towards zero by a maximum delta distance.
		/// </summary>
		/// <param name="vector">The vector to be moved towards zero.</param>
		/// <param name="maxDistanceDelta">The maximum distance the vector can move in this step.</param>
		public static void MoveTowardsZero(ref Vector3 vector, float maxDistanceDelta) =>
			vector = Vector3.MoveTowards(vector, Vector3.zero, maxDistanceDelta);

		/// <summary>
		/// Gradually moves a vector toward zero by a specified maximum distance delta.
		/// </summary>
		/// <param name="vector">The vector to be moved toward zero.</param>
		/// <param name="maxDistanceDelta">The maximum distance the vector can move toward zero in this operation.</param>
		/// <returns>Returns the updated vector after moving toward zero by the specified distance delta.</returns>
		public static Vector3 MoveTowardsZero(Vector3 vector, float maxDistanceDelta) =>
			Vector3.MoveTowards(vector, Vector3.zero, maxDistanceDelta);
		
		#endregion

		#region Unsafe Methods

		
		/// <summary>
		/// Projects a given vector onto a specified direction vector.
		/// </summary>
		/// <param name="vector">The vector to be projected.</param>
		/// <param name="direction">The direction vector onto which the projection will occur. This vector MUST BE normalized before being passed into this method.</param>
		/// <returns>Returns the projected vector in the specified direction.</returns>
		public static Vector3 GetProjectedVectorUnsafe(Vector3 vector, Vector3 direction) =>
			//Perform vector projection by multiplying our desired normalized direction vector by the magnitude of the vector when projected onto this direction, which is extracted via the dot product of our original vector with the normalized direction vector
			Vector3.Dot(vector, direction) * direction;
		
		
		/// <summary>
		/// Projects a given vector onto a specified direction vector.
		/// </summary>
		/// <param name="vector">The vector to be projected.</param>
		/// <param name="direction">The direction vector onto which the projection will occur. This vector MUST BE normalized before being passed into this method.</param>
		public static void ProjectUnsafe(ref Vector3 vector, Vector3 direction) =>
			//Perform vector projection by multiplying our desired normalized direction vector by the magnitude of the vector when projected onto this direction, which is extracted via the dot product of our original vector with the normalized direction vector
			vector = Vector3.Dot(vector, direction) * direction;

		/// <summary>
		/// Removes the component of a vector that is projected onto a specified direction vector.
		/// </summary>
		/// <param name="vector">The original vector to adjust.</param>
		/// <param name="direction">
		/// The direction vector representing the direction of the projection. This vector MUST BE normalized before being passed into this method.
		/// </param>
		/// <returns>
		/// A `Vector3` that excludes any contribution of the original vector in the specified direction. The resulting vector is orthogonal to the specified direction.
		/// </returns>
		/// <remarks>
		/// This method is useful when you need to eliminate unwanted velocity or movement in the direction of a specified vector (e.g., removing planar momentum from a character controller's horizontal velocity).
		/// </remarks>
		public static Vector3 RemoveProjectionUnsafe(Vector3 vector, Vector3 direction) =>
			vector - Vector3.Dot(vector, direction) * direction;
		
		/// <summary>
		/// Removes the component of a vector that is projected onto a specified direction vector.
		/// </summary>
		/// <param name="vector">The original vector to adjust.</param>
		/// <param name="direction">
		/// The direction vector representing the direction of the projection. This vector MUST BE normalized before being passed into this method
		/// </param>
		/// <remarks>
		/// This method is useful when you need to eliminate unwanted velocity or movement in the direction of a specified vector (e.g., removing planar momentum from a character controller's horizontal velocity).
		/// </remarks>
		public static void RemoveProjectionUnsafe(ref Vector3 vector, Vector3 direction) =>
			vector -= Vector3.Dot(vector, direction) * direction;
		
		/// <summary>
		/// Calculates the signed magnitude (scalar projection) of a given NORMALIZED vector onto a specified direction vector.
		/// </summary>
		/// <param name="vector">The vector to be projected.</param>
		/// <param name="direction">
		/// The direction vector onto which the projection will occur. This vector MUST BE normalized before being passed into this method.
		/// </param>
		/// <returns>
		/// A float representing the signed scalar magnitude of the projection of the vector onto the direction. 
		/// A positive value indicates alignment with the direction, while a negative value indicates opposition.
		/// </returns>
		public static float GetProjectedMagnitudeUnsafe(Vector3 vector, Vector3 direction)
		{
			// Calculate the scalar projection using the dot product of the vector and the normalized direction vector.
			// The sign of the result indicates the alignment (positive = same direction, negative = opposite direction).
			return Vector3.Dot(vector, direction);
		}

		#endregion
    }
}
