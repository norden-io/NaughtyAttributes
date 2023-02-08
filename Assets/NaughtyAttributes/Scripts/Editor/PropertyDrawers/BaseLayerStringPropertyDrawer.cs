using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

namespace NaughtyAttributes.Editor
{
    [CustomPropertyDrawer(typeof(BaseLayerStringAttribute), true)]
    public class BaseStringLayerPropertyDrawer : PropertyDrawerBase
    {
        private const string TypeWarningMessage = "{0} must be a string";

        new BaseLayerStringAttribute attribute => (BaseLayerStringAttribute)base.attribute;

        protected override float GetPropertyHeight_Internal(SerializedProperty property, GUIContent label)
        {
            bool validPropertyType = property.propertyType == SerializedPropertyType.String;

            return validPropertyType
                ? GetPropertyHeight(property)
                : GetPropertyHeight(property) + GetHelpBoxHeight();
        }

        protected override void OnGUI_Internal(Rect rect, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(rect, label, property);

            switch (property.propertyType)
            {
                case SerializedPropertyType.String:
                    DrawPropertyForString(rect, property, label);
                    break;
                default:
                    string message = string.Format(TypeWarningMessage, property.name);
                    DrawDefaultPropertyAndHelpBox(rect, property, message, MessageType.Warning);
                    break;
            }

            EditorGUI.EndProperty();
        }


        protected void DrawPropertyForString(Rect rect, SerializedProperty property, GUIContent label)
        {
            string[] layers = attribute.GetLayers();
            int      index = IndexOf(layers, property.stringValue);
            
            int newIndex = EditorGUI.Popup(rect, label.text, index, layers);
            string newLayer = layers[newIndex];

            if (!property.stringValue.Equals(newLayer, StringComparison.Ordinal))
            {
                property.stringValue = layers[newIndex];
            }
        }

        protected int IndexOf<T>(T[] layers, T layer)
        {
            var index = Array.IndexOf(layers, layer);
            //if (index < 0) Debug.LogWarning($"Failed to find layer '{layer}' in [{String.Join(", ", layers)}\n{GetType()}");
            return Mathf.Clamp(index, 0, layers.Length - 1);
        }
    }
}
