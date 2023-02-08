using System;
using System.Reflection;

namespace NaughtyAttributes
{
    public class SortingLayerAttribute : BaseLayerAttribute
    {
        public override string[] GetLayers()
        {
            Type internalEditorUtilityType = typeof(UnityEditorInternal.InternalEditorUtility);
            PropertyInfo sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);
            return (string[])sortingLayersProperty.GetValue(null, new object[0]);
        }
    }
}
