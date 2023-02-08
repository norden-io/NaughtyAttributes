using System;
using System.Reflection;

namespace NaughtyAttributes
{
    public class SortingLayerAttribute : BaseLayerAttribute
    {
#if UNITY_EDITOR
        public override string[] GetLayers()
        {

            Type internalEditorUtilityType = typeof(UnityEditorInternal.InternalEditorUtility);
            PropertyInfo sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);
            return (string[])sortingLayersProperty.GetValue(null, new object[0]);
        }
#endif
    }
}
