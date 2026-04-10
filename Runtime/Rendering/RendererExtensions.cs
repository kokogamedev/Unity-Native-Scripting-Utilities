using UnityEngine;

namespace PsigenVision.Utilities.Rendering
{
    public static class RendererExtensions
    {
        public static void SetColorViaPropertyBlock(this Renderer renderer, Color color, MaterialPropertyBlock propertyBlock)
        {
            //Get the current value of the material properties in the renderer
            renderer.GetPropertyBlock(propertyBlock);
            //Assign our new value
            propertyBlock.SetColor("_Color", color);
            //Apply the edited values to the renderer
            renderer.SetPropertyBlock(propertyBlock);
        }
    }
}