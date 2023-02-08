using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditorInternal;
#endif

namespace NaughtyAttributes
{
    public class LayerAttribute : BaseLayerAttribute
    {
        public override string[] GetLayers() =>
#if UNITY_EDITOR
            InternalEditorUtility.layers;
#else
			new string[]{};
#endif

        public override string LayerToName(int layer) => 
            LayerMask.LayerToName(layer);

        public override int NameToLayer(string layerName) =>
            LayerMask.NameToLayer(layerName);
    }
}
