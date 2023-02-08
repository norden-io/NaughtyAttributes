using UnityEngine.XR.Interaction.Toolkit;
using static System.Linq.Enumerable;

#nullable enable
namespace NaughtyAttributes.XRI
{
    public class XRILayerAttribute : BaseLayerAttribute
    {
        public override string[] GetLayers()
        {
            return Range(0, 32)
                .Select(i => InteractionLayerMask.LayerToName(i))
                .Where(name => !string.IsNullOrEmpty(name))
                .ToArray();
        }

        public override string LayerToName(int layer)
        {
            return InteractionLayerMask.LayerToName(layer);
        }

        public override int NameToLayer(string layerName)
        {
            return InteractionLayerMask.NameToLayer(layerName);
        }
    }
}
