using UnityEngine;
using PsigenVision.Utilities;

namespace PsigenVision.Utilities.Prototyping
{
    [CreateAssetMenu(fileName = "TestOverride", menuName = "PsigenVision/Utilities/Prototyping/TestOverrideSO")]
    public class TestOverride: ScriptableObject
    {
        [SerializeField] private IntOverride intOverride;
        [SerializeField] private FloatOverride floatOverride;
        [SerializeField] private StringOverride stringOverride;
        [SerializeField] private Vector3Override vector3Override;
        [SerializeField] private Vector3IntOverride vector3IntOverride;
        [SerializeField] private Vector2Override vector2Override;
        [SerializeField] private Vector2IntOverride vector2IntOverride;
        [SerializeField] private Vector4Override vector4Override;
        [SerializeField] private QuaternionOverride quaternionOverride;
        [SerializeField] private ColorOverride colorOverride;
        [SerializeField] private LayerMaskOverride layerMaskOverride;
        [SerializeField] private RectOverride rectOverride;
        [SerializeField] private RectIntOverride rectIntOverride;
        [SerializeField] private BoundsOverride boundsOverride;
        [SerializeField] private BoundsIntOverride boundsIntOverride;
        [SerializeField] private GradientOverride gradientOverride;
        [SerializeField] private RenderingLayerMaskOverride renderingLayerMaskOverride;
        [SerializeField] private Hash128Override hash128Override;
        [SerializeField] private CharOverride charOverride;
        [SerializeField] private EntityIdOverride entityIdOverride;
        [SerializeField] private AnimationCurveOverride animationCurveOverride;
    }
}