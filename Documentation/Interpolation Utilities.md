# PsigenVision Interpolation Utilities

## **`Easing` Extensions Class**

**CAUTION**: _This class is currently untested._

**NOTE**: _All methods below have non-extension counterparts with precisely the same implementations. The only difference lies in the signature (no `this` enforcing an extension method) and the location (defined in the `Easing` class rather than the `EasingExtensions` class)._

---

### **DecayTowards (Extension Method)**

#### Definition:
```csharp
public static Vector3 DecayTowards(this Vector3 start, Vector3 end, float decay, float deltaTime)
public static Vector2 DecayTowards(this Vector2 start, Vector2 end, float decay, float deltaTime)
public static float DecayTowards(this float start, float end, float decay, float deltaTime)
```

#### Summary:
Gradually decays the input value towards a target value, exponentially approaching the end with a decay rate. Works for both `Vector2`, `Vector3`, and `float`.

#### Parameters:
- `start`: The starting value.
- `end`: The target value to approach.
- `decay`: Decay factor determining the rate of approach.
- `deltaTime`: The elapsed time since the last update.

#### Returns:
- The updated value, decayed towards the target at the specified rate.

---

### **Easing Methods**

#### Definition:
```csharp
public static float EaseInQuad(this float t)
public static float EaseOutQuad(this float t)
public static float EaseInOutQuad(this float t)
public static float EaseInCubic(this float t)
public static float EaseOutCubic(this float t)
public static float EaseInOutCubic(this float t)
public static float EaseInPow2(this float t)
public static float EaseInExp(this float t)
public static float EaseOutPow2(this float t)
public static float EaseOutExp(this float t)
public static float EaseInBack(this float t)
public static float EaseOutBack(this float t)
public static float EaseOutBounce(this float t)
public static float InSine(this float t)
public static float OutSine(this float t)
public static float InOutSine(this float t)
```

#### Summary:
Provides a variety of easing animations for smooth transitions, transformations, and deceleration/acceleration patterns. Includes support for quadratic, cubic, exponential, sine wave functions, and bounce effects.

#### Parameters:
- `t`: The normalized time (typically, `t` should be in the range `[0, 1]`).

#### Returns:
- The eased value at the given normalized time `t`.

#### Example:
```csharp
using UnityEngine;
using PsigenVision.Utilities;

public class Example : MonoBehaviour
{
    void Update()
    {
        float time = Mathf.PingPong(Time.time, 1f); // Normalized time in the range [0, 1]
        float easedValue = time.EaseOutQuad(); // Ease out quad transformation
        Debug.Log($"Eased Value: {easedValue}");
    }
}
```

### DecayTowards (float)

#### Definition:
```csharp
public static float DecayTowards(this float start, float end, float decay, float deltaTime)
```

#### Description:
Gradually moves a single float value towards a target using exponential decay. Unlike `Mathf.Lerp`, this method adapts to frame rates and provides continuous, smooth motion, even with changing targets.

#### Use Cases:
1. **Smooth UI Transitions:** For sliders, health/energy bars, or other UI components that "snap" naturally to a target value.
2. **Camera Zoom:** Decay-based zooming feels more natural compared to instantaneous jumps.
3. **Noise Filtering:** Smoothens input data or animations to eliminate abrupt changes.

#### Performance:
- More efficient than `Mathf.Pow` due to fixed exponential base calculations (`Mathf.Exp`).
- Adapts to real-time scenarios with excellent performance.

#### Example:
```csharp
float currentZoom = 1f;
float targetZoom = 5f;
float decayRate = 4.0f;
float deltaTime = Time.deltaTime;

// Apply decay towards the target
float smoothZoom = currentZoom.DecayTowards(targetZoom, decayRate, deltaTime);

Debug.Log($"Smoothed Value: {smoothZoom}");
```
---

### DecayTowards (Vector2)

#### Definition:
```csharp
public static Vector2 DecayTowards(this Vector2 start, Vector2 end, float decay, float deltaTime)
```

#### Description:
Applies exponential decay to move a `Vector2` towards a target smoothly. Like its `Vector3` counterpart, this method ensures smooth, frame-rate independent transitions and is ideal for 2D movement or UI elements.

#### Use Cases:
1. **Camera Pan in 2D:** For 2D games, this method ensures the camera follows the player with fluidity and minimal jitter.
2. **UI Element Animation:** Smoothly transitions a UI widget into position without abrupt stops.
3. **Cursor Movement Smoothing:** Handles pointer smoothing for custom UI implementations.

#### Performance:
- Optimized for real-time applications due to a single `Mathf.Exp` call.
- Functions significantly faster than using `Vector3.SmoothDamp` or component-by-component decay.

#### Example:
```csharp
Vector2 currentPosition = new Vector2(0f, 0f);
Vector2 targetPosition = new Vector2(5f, 5f);
float decayRate = 3.0f;
float deltaTime = Time.deltaTime;

// Apply decay towards the target
Vector2 smoothedPosition = currentPosition.DecayTowards(targetPosition, decayRate, deltaTime);

Debug.Log($"Smoothed Position (2D): {smoothedPosition}");
```
---

### DecayTowards (Vector3)

#### Definition:
```csharp
public static Vector3 DecayTowards(this Vector3 start, Vector3 end, float decay, float deltaTime)
```

#### Description:
Gradually moves a `Vector3` towards a target position using exponential decay. Unlike linear interpolation (`Vector3.Lerp`), this method provides smooth and natural motion unaffected by fluctuating frame rates, making it ideal for dynamic and physics-like interactions.

#### Use Cases:
1. **Smooth Camera Follow:** Causes the camera to “decay” toward a moving target smoothly without jitter, maintaining a liquid feel.
2. **Weapon Sway:** Smoothly brings a weapon back to its "resting" center position after movement.
3. **Custom Cursor Smoothing:** Great for UI implementations with custom mouse cursor smoothing.

#### Performance:
- **Efficiency:** Excellent. Only calculates `Mathf.Exp` once, optimizing performance, especially when compared to manually interpolating each axis.
- **Comparison:** Faster than `Vector3.SmoothDamp` and calling decay calculations on individual components (.x, .y, .z).

#### Example:
```csharp
Vector3 currentPosition = new Vector3(0f, 0f, 0f);
Vector3 targetPosition = new Vector3(10f, 10f, 10f);
float decayRate = 2.5f; 
float deltaTime = Time.deltaTime;

// Apply decay towards the target
Vector3 smoothedPosition = currentPosition.DecayTowards(targetPosition, decayRate, deltaTime);

Debug.Log($"Smoothed Position: {smoothedPosition}");
```
---

### EaseInQuad

#### Definition:
```csharp
public static float EaseInQuad(this float t)
```

#### Description:
The most basic "natural" curve. It mimics constant acceleration (like gravity). Produces a quadratic ease-in effect. The rate of change starts slowly and accelerates as `t` progresses, following a squared curve.

#### Use Case:
- A character starting a sprint or a car pulling away from a light. Smooth starting/stopping UI elements.

#### Performance:
- Lightweight: Involves a single multiplication; computationally inexpensive.
- Ideal for frequent updates in games or UI transitions.

#### Example:
```csharp
float normalizedTime = 0.5f; // 50% progress
float easedValue = normalizedTime.EaseInQuad(); 
Debug.Log($"EaseInQuad Value: {easedValue}");
```
---

### EaseOutQuad

#### Definition:
```csharp
public static float EaseOutQuad(this float t)
```

#### Description:
The most basic "natural" curve. It mimics constant acceleration (like gravity). Creates a quadratic ease-out effect. The rate of change starts quick and slows down as `t` approaches 1, following an inverted squared curve.

#### Use Case:
- A sliding UI panel that should feel like it has "friction" as it stops. Smooth deceleration for animations or transitions. Smooth starting/stopping UI elements

#### Performance:
- Lightest. Uses basic multiplication.
- Lightweight: Relies on efficient arithmetic operations (single subtraction and multiplication), ensuring smooth real-time calculations.

#### Example:
```csharp
float normalizedTime = 0.8f; // 80% progress
float easedValue = normalizedTime.EaseOutQuad(); 
Debug.Log($"EaseOutQuad Value: {easedValue}");
```
---

### EaseInOutQuad

#### Definition:
```csharp
public static float EaseInOutQuad(this float t)
```

#### Description:
The most basic "natural" curve. It mimics constant acceleration (like gravity). Creates a symmetric quadratic ease-in-out effect. The function starts slow, speeds up at the middle, and then slows down again near the end, providing a smooth acceleration followed by deceleration.

#### Use Case:
- Transitions where both smooth starting and stopping are required. Smooth starting/stopping UI elements
- Ideal for animations requiring natural fluidity, such as camera movements, object transitions, or UI element scaling where smooth start and end are necessary.

#### Performance:
- Lightest. Uses basic multiplication.
- Efficient: Combines simple arithmetic conditions for the in/out transitions. Lightweight and well-suited for real-time animations.

#### Example:
```csharp
float normalizedTime = 0.5f; // Halfway through (50%)
float easedValue = normalizedTime.EaseInOutQuad();
Debug.Log($"EaseInOutQuad Value: {easedValue}");
```
---

### EaseInCubic

#### Definition:
```csharp
public static float EaseInCubic(this float t)
```

#### Description:
A steeper curve than Quadratic. It stays "slow" for longer and finishes much faster. Applies a cubic ease-in effect. The transition starts very slow and accelerates towards the end, following a cubic curve (t³). Use Cubic when Quadratic feels too "linear" or "lazy."

#### Use Case:
- Heavy objects falling or "dropping" into the scene. Objects with some weight needing a noticeable but smooth transition.
- Excellent for animations where a super-soft start is needed (e.g., an object scaling up slowly).

#### Performance:
- Very Light. Just one extra multiplication compared to Quad.
- Slightly heavier than quadratic methods due to the cubed calculation (`t * t * t`), but still performant for games and real-time usage.

#### Example:
```csharp
float normalizedTime = 0.3f; // 30% progress
float easedValue = normalizedTime.EaseInCubic();
Debug.Log($"EaseInCubic Value: {easedValue}");
```
---

### EaseOutCubic

#### Definition:
```csharp
public static float EaseOutCubic(this float t)
```

#### Description:
Provides a cubic easing-out effect. The transition starts fast and decelerates as it approaches its end, mimicking a smooth landing. Logic Tip: Use Cubic when Quadratic feels too "linear" or "lazy."

#### Use Case:
- Snappy transitions where you want the object to spend very little time in the middle of the screen. Objects with some weight needing a noticeable but smooth transition.
- Often used for deceleration-related animations, like objects "landing" visually or UI elements smoothly coming to rest.

#### Performance:
- Very Light. Just one extra multiplication compared to Quad.
- Similar to `EaseInCubic`: Slightly expensive compared to linear methods but optimized for real-time usage.

#### Example:
```csharp
float normalizedTime = 0.6f; // 60% progress
float easedValue = normalizedTime.EaseOutCubic();
Debug.Log($"EaseOutCubic Value: {easedValue}");
```
---



### EaseInExp

#### Definition:
```csharp
public static float EaseInExp(this float t)
```

#### Description:
This is an "aggressive" curve. It starts explosively and ends nearly invisibly. Generates an exponential ease-in effect, starting almost flat and rapidly accelerating as progress increases. Characterized by an exponentially increasing curve. Replaces Mathf.Pow with Mathf.Exp to bypass expensive internal log calculations.

#### Use Case:
- Suitable for animations that require a highly delayed start, such as an object suddenly "launching" after a pause or a gradual scale-up from zero.
- "Warp speed" effects or flashes of light. Fading out/in very quickly, items "zooming" off-screen.

#### Performance:
- Slightly more intensive than quadratic methods because of the exponential calculation, but modern hardware handles it well for real-time use.
- Significantly faster than base-2 Power functions.Optimized for minimal runtime overhead by using a precomputed constant and Mathf.Exp instead of Mathf.Pow

#### Example:
```csharp
float normalizedTime = 0.25f; // 25% progress
float easedValue = normalizedTime.EaseInExp();
Debug.Log($"EaseInExp Value: {easedValue}");
```
---

### EaseOutExp

#### Definition:
```csharp
public static float EaseOutExp(this float t)
```

#### Description:
This is an "aggressive" curve. It starts nearly invisible and then explodes into motion. Implements an exponential ease-out effect. The value starts rapidly and smoothly decelerates as progress approaches the end. Replaces Mathf.Pow with Mathf.Exp to bypass expensive internal log calculations.

#### Use Case:
- Often implemented for animations mimicking a sudden stop or de-escalation, like an object "breaking" quickly to a halt or reducing a UI element's opacity.
- A UI menu that needs to feel "locked in" or "magnetic" as it hits the final position. Fading out/in very quickly, items "zooming" off-screen.

#### Performance:
- Slightly heavier than quadratic methods due to exponential operations, but still performs well in most real-time scenarios.
- Significantly faster than base-2 Power functions.Optimized for minimal runtime overhead by using a precomputed constant and Mathf.Exp instead of Mathf.Pow

#### Example:
```csharp
float normalizedTime = 0.75f; // 75% progress
float easedValue = normalizedTime.EaseOutExp();
Debug.Log($"EaseOutExp Value: {easedValue}");
```
---

### EaseInPow2

#### Definition:
```csharp
public static float EaseInPow2(this float t)
```

#### Description:
This is an "aggressive" curve. It starts explosively and ends nearly invisibly. Follows a power-of-two curve that starts very slow and rapidly accelerates near the end (quadratic). The result is slightly more "aggressive" than `EaseInQuad`.

#### Use Case:
- Works well with animations that require sharp, precise starting movement over time, such as a scale-up effect where the acceleration should become noticeable quickly.
- "Warp speed" effects or flashes of light. Fading out/in very quickly, items "zooming" off-screen.

#### Performance:
- Moderate. Uses Mathf.Pow, which is more expensive than multiplication. Use EaseInExp for better performance.

#### Example:
```csharp
float normalizedTime = 0.6f; // 60% progress
float easedValue = normalizedTime.EaseInPow2(); 
Debug.Log($"EaseInPow2 Value: {easedValue}");
```
---

### EaseOutPow2

#### Definition:
```csharp
public static float EaseOutPow2(this float t)
```

#### Description:
This is an "aggressive" curve. It starts nearly invisible and then explodes into motion. Exact opposite of `EaseInPow2`: Starts with rapid movement and slows down exponentially towards the end. Great for deceleration animations.

#### Use Case:
- Perfect for scenarios like easing out the opacity of an interface element or decelerating a falling object.
- A UI menu that needs to feel "locked in" or "magnetic" as it hits the final position. Fading out/in very quickly, items "zooming" off-screen.

#### Performance:
- Moderate. Uses Mathf.Pow, which is more expensive than multiplication. Use EaseOutExp for better performance.

#### Example:
```csharp
float normalizedTime = 0.8f; // 80% progress
float easedValue = normalizedTime.EaseOutPow2(); 
Debug.Log($"EaseOutPow2 Value: {easedValue}");
```
---

### EaseInBack

#### Definition:
```csharp
public static float EaseInBack(this float t)
```

#### Description:
Starts by slightly moving backward before advancing. Produces a "pull-back" effect that enhances the anticipation before motion begins.

#### Use Case:
- Useful for playful or dramatic animations like characters winding up before jumping or UI elements pulling back before appearing on screen.
- An archer pulling back a bowstring before firing. Items dropped into a container, pop-up notifications.
- "Pop-up" UI menus or cartoony, bouncy actions.

#### Performance:
- Light/Moderate. A few extra multiplications.
- Efficient: Based on basic operations and coefficients.

#### Example:
```csharp
float normalizedTime = 0.4f; // 40% progress
float easedValue = normalizedTime.EaseInBack();
Debug.Log($"EaseInBack Value: {easedValue}");
```
---

### EaseOutBack

#### Definition:
```csharp
public static float EaseOutBack(this float t)
```

#### Description:
Creates a "pulled-back landing" motion where the value overshoots its end slightly before returning, mimicking a dramatic deceleration.

#### Use Case:
- Often used when objects or UI elements need to animate past their final position slightly, providing a playful and dynamic feel.
- The "Unity Standard" for UI. Buttons that "pop" slightly larger than their final size feel much more juice than standard fades. Items dropped into a container, pop-up notifications.

#### Performance:
- Light/Moderate. A few extra multiplications.
- Efficient: Based on basic operations and coefficients.

#### Example:
```csharp
float normalizedTime = 0.9f; // 90% progress
float easedValue = normalizedTime.EaseOutBack();
Debug.Log($"EaseOutBack Value: {easedValue}");
```
---


### InSine

#### Definition:
```csharp
public static float InSine(this float t)
```

#### Description:
Generates a sinusoidal ease-in curve, starting very slowly and moving towards a steeper acceleration. The motion is smooth and elegant. It’s a very shallow curve that starts with a tiny bit of "drag" before reaching full speed. This function modifies the value of a normalized time parameter to create smooth, natural motion that starts gently.

#### Use Case:
- Best for things that should feel weightless or floaty. Think of a floating pick-up item starting to move toward the player, or a light fog fading in. It’s less "aggressive" than a Quadratic start.
- Often used for smooth, natural transitions, like fading in elements or a camera smoothly starting movement.

#### Performance:
- Slightly heavier than polynomial methods due to using a `Mathf.Sin` calculation. Still fast enough for most real-time applications.
- Moderate. It requires a Mathf.Cos calculation. In Unity, trig functions are slower than basic math (t*t), but unless you're running 5,000 of these per frame, you won’t notice.

#### Example:
```csharp
float normalizedTime = 0.25f; // 25% progress
float easedValue = normalizedTime.InSine();
Debug.Log($"InSine Value: {easedValue}");
```
---

### OutSine

#### Definition:
```csharp
public static float OutSine(this float t)
```

#### Description:
This starts at maximum velocity and gently "coasts" to a stop. Creates a sinusoidal ease-out effect. The motion starts strong and decelerates smoothly towards the end for an elegant finish. This function modifies the value of a normalized time parameter, producing a sine wave-like decrease in speed.

#### Use Case:
- If you want a camera to move to a new target without a jarring "thud" at the end, OutSine is the most cinematic choice. It feels like a professional cameraman slowing down their rig.
- Ideal for exit animations, like smoothly fading or sliding UI elements off-screen.

#### Performance:
- Moderate. It requires a Mathf.Cos calculation. In Unity, trig functions are slower than basic math (t*t), but unless you're running 5,000 of these per frame, you won’t notice.
- Requires trigonometric computation (`Mathf.Sin`), which is slightly heavier than polynomial methods but optimized for modern hardware.

#### Example:
```csharp
float normalizedTime = 0.75f; // 75% progress
float easedValue = normalizedTime.OutSine();
Debug.Log($"OutSine Value: {easedValue}");
```
---

### InOutSine

#### Definition:
```csharp
public static float InOutSine(this float t)
```

#### Description:
Creates a sinusoidal ease-in-out effect. Starts slow, accelerates smoothly, and then decelerates towards the end. The motion is symmetrical and natural.

#### Use Case:
- The "Bread and Butter" of Loops. Use this for any repeating animation: a hovering platform, a pulsing "Press Start" text, or a character's breathing animation. Because the start and end velocities are both zero, it loops seamlessly without a "hiccup."
- Best suited for animations requiring smooth and seamless transitions, such as sliding panels or camera movements.

#### Performance:
- Moderate. Still just one trig call, but usually slightly more expensive than InOutQuad.
- Similar to `InSine` and `OutSine`: Uses trigonometric calculations for elegance but remains efficient for real-time usage.

#### Example:
```csharp
float normalizedTime = 0.5f; // Halfway through (50%)
float easedValue = normalizedTime.InOutSine();
Debug.Log($"InOutSine Value: {easedValue}");
```
---

### EaseOutBounce

#### Definition:
```csharp
public static float EaseOutBounce(this float t)
```

#### Description:
Pulls back before moving (In) or overshoots the target and settles (Out). The constant s (1.70158) determines how far it overshoots (~10%). Generates a bounce effect upon completion. The function mimics the physics of an object falling and rebounding.

#### Logic Explained:
- The function divides the timeline into segments to simulate the effect of bouncing at different decreasing heights.

#### Use Case:
- UI Elements: A common use is for pop-up dialog boxes, notification windows, or buttons that fly into the screen and "land" with a subtle, attention-grabbing bounce.
- Physics Simulation: Use it for an object dropping onto a surface, where it should bounce realistically before settling at its final position.
- Game Effects: Animating a character jumping and landing, or items being collected and dropping into an inventory slot with a satisfying final movement

#### Performance:
- High complexity. Use sparingly for "hero" elements (main menus, player deaths).

#### Example:
```csharp
float normalizedTime = 0.9f; // 90% progress
float easedValue = normalizedTime.EaseOutBounce(); 
Debug.Log($"EaseOutBounce Value: {easedValue}");
```
---

### EaseInBounce

#### Definition:
```csharp
public static float EaseInBounce(this float t)
{
    return 1 - EaseOutBounce(1 - t);
}
```

#### Description:
Simulates a reverse bouncing effect, where an object starts "bouncing" at the beginning and then smoothly converges to its target. It’s essentially the mirrored version of `EaseOutBounce`.

#### Use Cases:
- Objects Leaving the Scene: It is useful for objects that need to appear to be "sucked" into a destination or launching off-screen with increasing speed and a slight jolt at the end.
- Building Anticipation: The slow, bouncy start can build suspense before a fast, final movement, such as a player being launched from a cannon.
- Specific Effects: Can be used to animate something being pulled into a confined space, like a vacuum effect, where the initial movement is hesitant/bouncy and the final movement is a quick snap into place.
- **Reverse Bounce Entries:** Use for animations where objects enter the scene with a bouncing effect.
- **Creative Transitions:** Works well in playful objects such as UIs, balloons, or projectiles.

#### Performance:
- Leverages `EaseOutBounce` internally while reversing its effect, so it performs just as efficiently as its counterpart.
- High complexity. Use sparingly

#### Example:
```csharp
float normalizedTime = 0.3f; // 30% progress
float easedValue = normalizedTime.EaseInBounce();

Debug.Log($"EaseInBounce Value: {easedValue}");
```
---
