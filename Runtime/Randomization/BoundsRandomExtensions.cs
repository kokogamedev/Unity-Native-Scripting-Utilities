using UnityEngine;
using Random = UnityEngine.Random;

namespace PsigenVision.Utilities.Randomization
{
    public static class BoundsRandomExtensions {
        public static Vector3 GetRandomPointWithin(this Bounds input)
        {
            return new Vector3(
                Random.Range(-input.extents.x, input.extents.x) + input.center.x,
                Random.Range(-input.extents.y, input.extents.y) + input.center.y,
                Random.Range(-input.extents.z, input.extents.z) + input.center.z
            );
        }
    }
}