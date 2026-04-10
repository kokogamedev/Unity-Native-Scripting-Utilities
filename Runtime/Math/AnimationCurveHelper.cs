using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityRandom = UnityEngine.Random;

namespace PsigenVision.Utilities.Math
{
    public static class AnimationCurveHelper
    {
        /// <summary>
        /// Generates an approximate inverse of a given AnimationCurve. 
        /// The returned curve maps the original curve's y-values back to x-values,
        /// allowing you to "invert" curve-space progress.
        /// 
        /// For example, if you have an easing curve that maps linear 0..1 to eased 0..1,
        /// this lets you recover the corresponding linear t given an eased t.
        /// </summary>
        /// <param name="curve">The original AnimationCurve to invert. Must be monotonic for meaningful results.</param>
        /// <param name="resolution">
        /// Number of samples used to approximate the inverse mapping. Higher values
        /// give smoother results but increase memory and evaluation cost slightly.
        /// Typical values: 128–512.
        /// </param>
        /// <returns>
        /// A new AnimationCurve such that approximately:
        ///     invertedCurve.Evaluate(curve.Evaluate(t)) ≈ t
        /// </returns>
        public static AnimationCurve InvertCurve(AnimationCurve curve, int resolution = 256)
        {
            if (curve == null || curve.length == 0)
                return new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));

            resolution = Mathf.Max(2, resolution);
            Keyframe[] keys = new Keyframe[resolution];

            // Sample original curve at evenly spaced t in [0,1]
            for (int i = 0; i < resolution; i++)
            {
                float t = i / (float)(resolution - 1); // normalized 0..1
                float y = Mathf.Clamp01(curve.Evaluate(t)); // curve output (may not be strictly [0,1])

                // Inverse mapping: swapped (x,y)
                keys[i] = new Keyframe(y, t);
            }

            // Build inverse curve with linear tangents (approximate monotonicity)
            AnimationCurve inverse = new AnimationCurve(keys);
            for (int i = 0; i < keys.Length; i++)
            {
                AnimationUtility.SetKeyLeftTangentMode(inverse, i, AnimationUtility.TangentMode.Linear);
                AnimationUtility.SetKeyRightTangentMode(inverse, i, AnimationUtility.TangentMode.Linear);
            }

            return inverse;
        }

        /// <summary>
        /// Checks whether an AnimationCurve is approximately linear from (0,0) to (1,1).
        /// Useful for detecting when a curve does not meaningfully alter interpolation,
        /// so you can bypass it for performance.
        /// </summary>
        /// <param name="curve">The curve to test.</param>
        /// <param name="tolerance">
        /// Maximum allowed deviation from linear at any sample point.
        /// A smaller value is stricter (e.g. 0.001f), a larger value is looser (e.g. 0.05f).
        /// </param>
        /// <returns>True if the curve is essentially linear, false otherwise.</returns>
        public static bool IsCurveApproximatelyLinear(AnimationCurve curve, float tolerance = 0.01f)
        {
            if (curve == null || curve.length == 0)
                return true; // Treat null/empty as linear fallback

            int resolution = 32; // Number of points to check between 0..1
            for (int i = 0; i < resolution; i++)
            {
                float t = i / (float)(resolution - 1);
                float expected = t; // Perfect linear
                float actual = curve.Evaluate(t);

                if (Mathf.Abs(expected - actual) > tolerance)
                    return false;
            }

            return true;
        }
    }
}