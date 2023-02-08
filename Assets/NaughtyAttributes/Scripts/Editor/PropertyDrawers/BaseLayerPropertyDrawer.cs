using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

namespace NaughtyAttributes.Editor
{
    [CustomPropertyDrawer(typeof(BaseLayerAttribute), true)]
    public class BaseLayerPropertyDrawer : BaseStringLayerPropertyDrawer
    {
        private const string TypeWarningMessage = "{0} must be an int or a string";

        new BaseLayerAttribute attribute => (BaseLayerAttribute)base.attribute;

        protected override float GetPropertyHeight_Internal(SerializedProperty property, GUIContent label)
        {
            bool validPropertyType = property.propertyType == SerializedPropertyType.String || property.propertyType == SerializedPropertyType.Integer;

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
                case SerializedPropertyType.Integer:
                    if (attribute.mask) 
                    {
                        DrawPropertyForMask(rect, property, label);
                    } else {
                        DrawPropertyForInt(rect, property, label);
                    }
                    break;
                default:
                    string message = string.Format(TypeWarningMessage, property.name);
                    DrawDefaultPropertyAndHelpBox(rect, property, message, MessageType.Warning);
                    break;
            }
            
            EditorGUI.EndProperty();
        }
        
        private void DrawPropertyForInt(Rect rect, SerializedProperty property, GUIContent label)
        {
            string[] layers    = attribute.GetLayers();
            string   layerName = attribute.LayerToName(property.intValue);
            int      index     = IndexOf(layers, layerName);

            int newIndex = EditorGUI.Popup(rect, label.text, index, layers);
            string newLayerName = layers[newIndex];
            int newLayer = attribute.NameToLayer(newLayerName);

            if (property.intValue != newLayer)
            {
                property.intValue = newLayer;
            }
        }
        
        private void DrawPropertyForMask(Rect rect, SerializedProperty property, GUIContent label)
        {
            string[] layers    = attribute.GetLayers();
            int      fieldMask = attribute.FieldMaskFromLayerMask(property.intValue);
            
            int newFieldMask = EditorGUI.MaskField(rect, label.text, fieldMask, layers);
            int layerMask    = attribute.LayerMaskFromFieldMask(newFieldMask);
            
            if (property.intValue != layerMask)
            {
                //Debug.Log($"{property.intValue} -> {fieldMask} -> {newFieldMask} -> {layerMask}");
                property.intValue = layerMask;
            }
        }
    }
}
