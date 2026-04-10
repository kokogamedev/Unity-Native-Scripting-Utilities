using UnityEngine;

namespace PsigenVision.Utilities.Interpolation
{
    public static class Easing
    {
 //Precalculated constants
        private const float s = 1.70158f;
        private const float K = 6.9314718f; //10 * ln(2)

        /// <summary>
        /// Gradually moves a Vector3 towards a target using exponential decay.
        /// This is the Vector version of the "chase" logic. Instead of a linear Vector3.Lerp which requires a fixed duration,
        /// this "dampens" the distance between the current point and the target every frame.
        /// </summary>
        /// <param name="start">The starting Vector3 position.</param>
        /// <param name="end">The target Vector3 position to move towards.</param>
        /// <param name="decay">The decay rate, determining the speed of convergence.</param>
        /// <param name="deltaTime">The time step, typically Time.deltaTime in Unity.</param>
        /// <returns>The interpolated Vector3 position after applying the decay.</returns>
        /// <remarks>
        /// Use Cases:
        /// Smooth Camera Follow: If the player moves, the camera "decays" toward their new position. It feels incredibly "liquid" and never jitters, even if the frame rate fluctuates.
        /// Weapon Sway: Having a held item decay toward the center of the screen after the player moves the mouse.
        /// Cursor Smoothing: If you're building a game with a custom software cursor, this prevents "micro-stutters" from the mouse hardware.
        /// Performance Evaluation:
        /// Efficiency: Excellent. Because we calculate the Mathf.Exp once and then multiply it by the vector, it is nearly as fast as the float version.
        /// Comparison: It is faster than calling DecayTowards three separate times on .x, .y, and .z because it minimizes transcendental math calls. It is significantly faster than Vector3.SmoothDamp because it doesn't need to track a ref Vector3 currentVelocity state.
        /// </remarks>
        public static Vector3 DecayTowards(Vector3 start, Vector3 end, float decay, float deltaTime)
            // The multiplier is the same for all axes, so we only calculate Exp once.
            => Mathf.Exp(-decay * deltaTime) * (end + (start - end));
        

        /// <summary>
        /// Gradually moves a Vector2 towards a target using exponential decay.
        /// This is the Vector version of the "chase" logic. Instead of a linear Vector3.Lerp which requires a fixed duration,
        /// this "dampens" the distance between the current point and the target every frame.
        /// </summary>
        /// <param name="start">The starting Vector2 position.</param>
        /// <param name="end">The target Vector2 position to move towards.</param>
        /// <param name="decay">The decay rate, determining the speed of convergence.</param>
        /// <param name="deltaTime">The time step, typically Time.deltaTime in Unity.</param>
        /// <returns>The interpolated Vector2 position after applying the decay.</returns>
        /// <remarks>
        /// Use Cases:
        /// Smooth Camera Follow: If the player moves, the camera "decays" toward their new position. It feels incredibly "liquid" and never jitters, even if the frame rate fluctuates.
        /// Weapon Sway: Having a held item decay toward the center of the screen after the player moves the mouse.
        /// Cursor Smoothing: If you're building a game with a custom software cursor, this prevents "micro-stutters" from the mouse hardware.
        /// Performance Evaluation:
        /// Efficiency: Excellent. Because we calculate the Mathf.Exp once and then multiply it by the vector, it is nearly as fast as the float version.
        /// Comparison: It is faster than calling DecayTowards three separate times on .x, .y, and .z because it minimizes transcendental math calls. It is significantly faster than Vector3.SmoothDamp because it doesn't need to track a ref Vector3 currentVelocity state.
        /// </remarks>
        public static Vector2 DecayTowards(Vector2 start, Vector2 end, float decay, float deltaTime)
        => Mathf.Exp(-decay * deltaTime) * (end + (start - end));

        
        /// <summary>
        /// Gradually moves a value towards a target using exponential decay.
        /// Unlike standard Lerp, this provides a frame-rate independent "snapping" feel 
        /// that handles moving targets gracefully.
        /// </summary>
        /// <remarks>
        /// Use Case: Smooth camera following, UI snapping, and filtering noisy input.
        /// Description: A target-chasing function that mimics physical dampening. 
        /// Performance: High. Faster than Mathf.Pow because it avoids internal logarithm 
        /// calculations by using a fixed base (e).
        /// </remarks>
        /// <param name="start">The initial value from which the decay begins.</param>
        /// <param name="end">The target value towards which the decay progresses.</param>
        /// <param name="decay">The decay rate, controlling how quickly the value approaches the target.</param>
        /// <param name="deltaTime">The time step for the decay, typically the time elapsed since the last update.</param>
        /// <returns>The value decayed towards the target, calculated using an exponential decay formula.</returns>
        public static float DecayTowards(float start, float end, float decay, float deltaTime) =>
            end + (start - end) * Mathf.Exp(-decay * deltaTime);

        /*
         * Easing functions are mathematical equations that modify the rate of change for a linear interpolation (Lerp), transforming boring, constant-speed movement into natural, polished motion. In Unity, these are typically used by passing a normalized time value (0 to 1) into a formula to calculate a new, curved value
         */
        /// <summary>
        /// Calculates the Ease In Quadratic easing function, which starts slow and accelerates over time.
        /// This function modifies the value of a normalized time parameter to adjust its progression in a quadratic manner.
        /// </summary>
        /// <param name="t">The normalized time parameter (ranging from 0 to 1), where 0 represents the start value and 1 represents the end value.</param>
        /// <returns>The eased value, transformed using the Ease In Quadratic formula.</returns>
        /// <remarks>
        /// Description: The most basic "natural" curve. It mimics constant acceleration (like gravity).
        /// Use Cases:
        /// In: A character starting a sprint or a car pulling away from a light. Smooth starting/stopping UI elements.
        /// Performance: Lightest. Uses basic multiplication.
        /// </remarks>
        public static float EaseInQuad(float t) => t * t;

        /// <summary>
        /// Calculates the Ease Out Quadratic easing function, which starts fast and decelerates over time.
        /// This function adjusts the progression of a normalized time parameter in a quadratic manner, resulting in a slower finish.
        /// </summary>
        /// <param name="t">The normalized time parameter (ranging from 0 to 1), where 0 represents the start value and 1 represents the end value.</param>
        /// <returns>The eased value, transformed using the Ease Out Quadratic formula.</returns>
        /// <remarks>
        /// Description: The most basic "natural" curve. It mimics constant acceleration (like gravity).
        /// Use Cases:
        /// Out: A sliding UI panel that should feel like it has "friction" as it stops. Smooth deceleration for animations or transitions. Smooth starting/stopping UI elements
        /// Performance: Lightest. Uses basic multiplication.
        /// </remarks>
        public static float EaseOutQuad(float t) => 1 - (1 - t) * (1 - t);

        /// <summary>
        /// Calculates the Ease In-Out Quadratic easing function, which combines the characteristics of Ease In and Ease Out.
        /// This function starts slow, accelerates through the middle, and decelerates towards the end.
        /// </summary>
        /// <param name="t">The normalized time parameter (ranging from 0 to 1), where 0 represents the start value and 1 represents the end value.</param>
        /// <returns>The eased value, transformed using the Ease In-Out Quadratic formula.</returns>
        /// <remarks>
        /// Description: The most basic "natural" curve. It mimics constant acceleration (like gravity).
        /// Use Case: Transitions where both smooth starting and stopping are required. Smooth starting/stopping UI elements
        /// Performance: Lightest. Uses basic multiplication.
        /// </remarks>
        public static float EaseInOutQuad(float t) => t < 0.5f ? 2 * t * t : -1 + (4 - 2 * t) * t;

        /// <summary>
        /// Calculates the Ease In Cubic easing function, which starts slowly and accelerates at a cubic rate.
        /// This function modifies the value of a normalized time parameter to adjust its progression with cubic scaling.
        /// </summary>
        /// <param name="t">The normalized time parameter (ranging from 0 to 1), where 0 represents the start value and 1 represents the end value.</param>
        /// <returns>The eased value, transformed using the Ease In Cubic formula.</returns>
        /// <remarks>
        /// Description: A steeper curve than Quadratic. It stays "slow" for longer and finishes much faster.
        /// Use Cases:
        /// In: Heavy objects falling or "dropping" into the scene. Objects with some weight needing a noticeable but smooth transition.
        /// Performance: Very Light. Just one extra multiplication compared to Quad.
        /// Logic Tip: Use Cubic when Quadratic feels too "linear" or "lazy."
        /// </remarks>
        public static float EaseInCubic(float t) => t * t * t;

        /// <summary>
        /// Calculates the Ease Out Cubic easing function, which starts fast and decelerates smoothly towards the end.
        /// This function modifies the progression of a normalized time parameter in a cubic manner, providing a gradual and smooth stop.
        /// </summary>
        /// <param name="t">The normalized time parameter (ranging from 0 to 1), where 0 represents the start value and 1 represents the end value.</param>
        /// <returns>The eased value, transformed using the Ease Out Cubic formula.</returns>
        /// <remarks>
        /// Description: A steeper curve than Quadratic. It stays "slow" for longer and finishes much faster.
        /// Use Cases:
        /// Out: Snappy transitions where you want the object to spend very little time in the middle of the screen. Objects with some weight needing a noticeable but smooth transition.
        /// Performance: Very Light. Just one extra multiplication compared to Quad.
        /// Logic Tip: Use Cubic when Quadratic feels too "linear" or "lazy."
        /// </remarks>
        public static float EaseOutCubic(float t) => 1 - Mathf.Pow(1 - t, 3);

        /// <summary>
        /// Calculates the Ease In Out Cubic easing function, which combines both acceleration and deceleration phases.
        /// This function starts slowly, accelerates through the middle of the progression, and then decelerates towards the end.
        /// </summary>
        /// <param name="t">The normalized time parameter (ranging from 0 to 1), where 0 represents the starting value and 1 represents the ending value.</param>
        /// <remarks>
        /// Description: A steeper curve than Quadratic. It stays "slow" for longer and finishes much faster.
        /// Use Cases:
        /// In: Heavy objects falling or "dropping" into the scene. Objects with some weight needing a noticeable but smooth transition.
        /// Out: Snappy transitions where you want the object to spend very little time in the middle of the screen. Objects with some weight needing a noticeable but smooth transition.
        /// Performance: Very Light. Just one extra multiplication compared to Quad.
        /// Logic Tip: Use Cubic when Quadratic feels too "linear" or "lazy."
        /// </remarks>
        public static float EaseInOutCubic(float t) => t < 0.5f ? 4 * t * t * t : 1 - Mathf.Pow(-2 * t + 2, 3) / 2;
        
        /// <summary>
        /// Calculates the Ease In Exponential easing function, where the rate of change starts slow and accelerates rapidly.
        /// This function adjusts the progression of the normalized time parameter in an exponential manner.
        /// </summary>
        /// <param name="t">The normalized time parameter (ranging from 0 to 1), where 0 represents the start value and 1 represents the end value.</param>
        /// <returns>The eased value, transformed using the Ease In Exponential formula.</returns>
        /// <remarks>
        /// Description: This is an "aggressive" curve. It starts nearly invisible and then explodes into motion.
        /// Use Cases:
        /// In: "Warp speed" effects or flashes of light. Fading out/in very quickly, items "zooming" off-screen.
        /// Out: A UI menu that needs to feel "locked in" or "magnetic" as it hits the final position. Fading out/in very quickly, items "zooming" off-screen.
        /// Performance: Moderate. Uses Mathf.Pow, which is more expensive than multiplication. Use EaseInExp for better performance.
        /// Logic Tip: Avoid using this for long durations (e.g., 2+ seconds), as the object will appear to stand still for the first second before teleporting.
        /// </remarks>
        public static float EaseInPow2(float t) => Mathf.Approximately(t, 0) ? 0 : Mathf.Pow(2, 10 * t - 10);

        /// <summary>
        /// Eases a value smoothly from 0 to 1 using an exponential curve with acceleration at the beginning.
        /// Produces a gradual, smooth start that quickly accelerates as the input progresses.
        /// </summary>
        /// <remarks>
        /// Description: Replaces Mathf.Pow with Mathf.Exp to bypass expensive internal log calculations.
        /// Use Case: Rapidly accelerating objects or "warp" effects.
        /// In: "Warp speed" effects or flashes of light. Fading out/in very quickly, items "zooming" off-screen.
        /// Out: A UI menu that needs to feel "locked in" or "magnetic" as it hits the final position. Fading out/in very quickly, items "zooming" off-screen.
        /// Performance: High. Significantly faster than base-2 Power functions.Optimized for minimal runtime overhead by using a precomputed constant and Mathf.Exp instead of Mathf.Pow
        /// High-performance Ease In Exponential.
        /// Logic Tip: Avoid using this for long durations (e.g., 2+ seconds), as the object will appear to stand still for the first second before teleporting.
        /// </remarks>
        /// <param name="t">A normalized time value (0 to 1) where 0 represents the start and 1 represents the end of the easing.</param>
        /// <returns>The eased value calculated based on an exponential function, ranging from 0 to 1.</returns>
        public static float EaseInExp(float t) => 
            t <= 0 ? 0 : Mathf.Exp(K * (t - 1));
        
        /// <summary>
        /// Calculates the Ease Out Exponential easing function, which starts fast and decelerates exponentially towards the end.
        /// This function adjusts the progression of a normalized time parameter to create a smooth finish.
        /// </summary>
        /// <param name="t">The normalized time parameter (ranging from 0 to 1), where 0 represents the start value and 1 represents the end value.</param>
        /// <returns>The eased value, transformed using the Ease Out Exponential formula.</returns>
        /// <remarks>
        /// Description: This is an "aggressive" curve. It starts nearly invisible and then explodes into motion.
        /// Use Cases:
        /// In: "Warp speed" effects or flashes of light. Fading out/in very quickly, items "zooming" off-screen.
        /// Out: A UI menu that needs to feel "locked in" or "magnetic" as it hits the final position. Fading out/in very quickly, items "zooming" off-screen.
        /// Performance: Moderate. Uses Mathf.Pow, which is more expensive than multiplication. Use EaseOutExp for better performance.
        /// Logic Tip: Avoid using this for long durations (e.g., 2+ seconds), as the object will appear to stand still for the first second before teleporting.
        /// </remarks>
        public static float EaseOutPow2(float t) => Mathf.Approximately(t, 1) ? 1 : 1 - Mathf.Pow(2, -10 * t);

        /// <summary>
        /// Maps a value from the range [0, 1] to an exponential ease-out curve.
        /// Produces a smooth decreasing transition where the change starts quickly and slows down towards the end.
        /// </summary>
        /// <param name="t">The normalized time value in the range [0, 1].</param>
        /// <returns>The interpolated value after applying the exponential ease-out transformation. If <paramref name="t"/>
        /// is 1 or greater, the returned value will be 1.</returns>
        /// <remarks>
        /// Description: Uses Mathf.Exp for a fast "magnetic" snap to the target value.
        /// Use Case: UI elements popping into place with high initial velocity.
        /// In: "Warp speed" effects or flashes of light. Fading out/in very quickly, items "zooming" off-screen.
        /// Out: A UI menu that needs to feel "locked in" or "magnetic" as it hits the final position. Fading out/in very quickly, items "zooming" off-screen.
        /// Performance: Optimized for minimal runtime overhead by using a precomputed constant and Mathf.Exp instead of Mathf.Pow
        /// Logic Tip: Avoid using this for long durations (e.g., 2+ seconds), as the object will appear to stand still for the last second before teleporting.
        /// </remarks>
        public static float EaseOutExp(float t) =>
            t >= 1 ? 1 : 1 - Mathf.Exp(-K * t);

        /// <summary>
        /// Calculates the Ease In Back easing function, which starts by moving slightly backward before accelerating.
        /// This function creates an effect where the movement initially overshoots in the opposite direction and then progresses forward in a smooth motion.
        /// </summary>
        /// <param name="t">The normalized time parameter (ranging from 0 to 1), where 0 represents the start value and 1 represents the end value.</param>
        /// <returns>The eased value, transformed using the Ease In Back formula.</returns>
        /// <remarks>
        /// Description: The object slightly moves backward before launching forward, or goes past the target and settles back.
        /// Use Case: "Pop-up" UI menus or cartoony, bouncy actions. An archer pulling back a bowstring before firing. Items dropped into a container, pop-up notifications.
        /// Performance: Light/Moderate. A few extra multiplications.
        /// </remarks>
        public static float EaseInBack(float t) => t * t * ((s + 1) * t - s);

        /// <summary>
        /// Calculates the Ease Out Back easing function, which starts quickly and overshoots before settling into the final value.
        /// This function modifies the progression of a normalized time parameter, creating a dynamic and elastic motion effect.
        /// </summary>
        /// <param name="t">The normalized time parameter (ranging from 0 to 1), where 0 represents the start value and 1 represents the end value.</param>
        /// <returns>The eased value, transformed using the Ease Out Back formula.</returns>
        /// <remarks>
        /// Description: Pulls back before moving (In) or overshoots the target and settles (Out). The constant s (1.70158) determines how far it overshoots (~10%).
        /// Use Cases:
        /// In: An archer pulling back a bowstring before firing. Items dropped into a container, pop-up notifications.
        /// Out: The "Unity Standard" for UI. Buttons that "pop" slightly larger than their final size feel much more juice than standard fades. Items dropped into a container, pop-up notifications. 
        /// Performance: Light/Moderate. A few extra multiplications.
        /// </remarks>
        public static float EaseOutBack(float t) => 1 + (--t * t * ((s + 1) * t + s));

        /// <summary>
        /// Provides an ease-in bounce easing function that simulates the effect of a bounce as the value starts moving from 0.
        /// </summary>
        /// <param name="t">The normalized time (progress) value ranging from 0 to 1.</param>
        /// <returns>The eased value, producing a bounce effect as time progresses from 0 to 1.</returns>
        /// <remarks>
        /// Use Cases:
        /// Objects Leaving the Scene: It is useful for objects that need to appear to be "sucked" into a destination or launching off-screen with increasing speed and a slight jolt at the end.
        /// Building Anticipation: The slow, bouncy start can build suspense before a fast, final movement, such as a player being launched from a cannon.
        /// Specific Effects: Can be used to animate something being pulled into a confined space, like a vacuum effect, where the initial movement is hesitant/bouncy and the final movement is a quick snap into place.
        /// Performance: High complexity. Use sparingly for "hero" elements (main menus, player deaths).
        /// </remarks>
        public static float EaseInBounce(float t) => 1 - EaseOutBounce(1 - t);

        /// <summary>
        /// Calculates the Ease Out Bounce easing function, which creates a motion that decelerates with a bouncing effect towards the end.
        /// This function modifies the value of a normalized time parameter to simulate a bounce-like behavior as the motion completes.
        /// </summary>
        /// <param name="t">The normalized time parameter (ranging from 0 to 1), where 0 represents the start value and 1 represents the end value.</param>
        /// <returns>The eased value, transformed using the Ease Out Bounce formula.</returns>
        /// <remarks>
        /// Use Cases:
        /// UI Elements: A common use is for pop-up dialog boxes, notification windows, or buttons that fly into the screen and "land" with a subtle, attention-grabbing bounce.
        /// Physics Simulation: Use it for an object dropping onto a surface, where it should bounce realistically before settling at its final position.
        /// Game Effects: Animating a character jumping and landing, or items being collected and dropping into an inventory slot with a satisfying final movement
        /// Performance: Light/Moderate. A few extra multiplications.
        /// </remarks>
        public static float EaseOutBounce(float t)
        {
            if (t < (1 / 2.75f)) return 7.5625f * t * t;
            else if (t < (2 / 2.75f)) return 7.5625f * (t -= 1.5f / 2.75f) * t + 0.75f;
            else if (t < (2.5f / 2.75f)) return 7.5625f * (t -= 2.25f / 2.75f) * t + 0.9375f;
            else return 7.5625f * (t -= 2.625f / 2.75f) * t + 0.984375f;
        }

        /// <summary>
        /// Calculates the Ease In Sine easing function, which starts slow and accelerates following a sinusoidal curve.
        /// This function modifies the value of a normalized time parameter to create smooth, natural motion that starts gently.
        /// </summary>
        /// <param name="t">The normalized time parameter (ranging from 0 to 1), where 0 represents the start value and 1 represents the end value.</param>
        /// <returns>The eased value, transformed using the Ease In Sine formula.</returns>
        /// <remarks>
        /// Description: It’s a very shallow curve that starts with a tiny bit of "drag" before reaching full speed.
        /// Use Cases:
        /// In: Best for things that should feel weightless or floaty. Think of a floating pick-up item starting to move toward the player,
        /// or a light fog fading in. It’s less "aggressive" than a Quadratic start.
        /// Performance: High complexity. Use sparingly for "hero" elements (main menus, player deaths).
        /// </remarks>
        // Starts slow, ends at full speed (1/4 of a sine wave)
        public static float InSine(float t)
        {
            return 1 - Mathf.Cos((t * Mathf.PI) / 2f);
        }

        /// <summary>
        /// Calculates the Ease Out Sine easing function, which starts fast and decelerates smoothly towards the end.
        /// This function modifies the value of a normalized time parameter, producing a sine wave-like decrease in speed.
        /// </summary>
        /// <param name="t">The normalized time parameter (ranging from 0 to 1), where 0 represents the start value and 1 represents the end value.</param>
        /// <returns>The eased value, transformed using the Ease Out Sine formula.</returns>
        /// <remarks>
        /// Description: This starts at maximum velocity and gently "coasts" to a stop.
        /// Use Cases:
        /// If you want a camera to move to a new target without a jarring "thud" at the end, OutSine is the most cinematic choice.
        /// It feels like a professional cameraman slowing down their rig.
        /// Performance: Moderate. Uses Mathf.Sin.
        /// </remarks>
        // Starts at full speed, decelerates to a stop
        public static float OutSine(float t)
        {
            return Mathf.Sin((t * Mathf.PI) / 2f);
        }

        /// <summary>
        /// Calculates the Ease In Out Sine easing function, which provides a smooth sinusoidal transition.
        /// The motion begins slowly, accelerates through the middle, and slows down toward the end.
        /// </summary>
        /// <param name="t">The normalized time parameter (ranging from 0 to 1), where 0 represents the start value and 1 represents the end value.</param>
        /// <returns>The eased value, transformed using the Ease In Out Sine formula.</returns>
        /// <remarks>
        /// Description: A full "S" curve. This is the only easing function that is perfectly symmetrical and smooth at both ends..
        /// Use Cases:
        /// The "Bread and Butter" of Loops. Use this for any repeating animation: a hovering platform, a pulsing "Press Start" text, or a character's breathing animation.
        /// Because the start and end velocities are both zero, it loops seamlessly without a "hiccup."
        /// Performance: Moderate. Still just one trig call, but usually slightly more expensive than InOutQuad.
        /// </remarks>
        // Accelerates then decelerates (A half sine wave cycle)
        // Moves from -1 to 1 on the Cosine wave, then remapped to 0 to 1
        public static float InOutSine(float t)
        {
            return -(Mathf.Cos(Mathf.PI * t) - 1) / 2f;
        }
    }
}
