using UnityEditor;
using UnityEngine;
using System;
namespace CoonGames
{
    [CustomPropertyDrawer(typeof(PrefabAttribute))]
    public class PrefabPropertyDrawer : PropertyDrawer
    {
        //private Object _ObjectReference = null;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            PrefabAttribute prefabAttribute = (PrefabAttribute)attribute;
            if (property.propertyType == SerializedPropertyType.ObjectReference)
            {
                property.objectReferenceValue = EditorGUI.ObjectField(position, label, property.objectReferenceValue, GetType(property), prefabAttribute.AllowInstantiatedPrefabs);
                //EditorGUI.PropertyField(position, property, label);
                if (property.objectReferenceValue != null)
                {
                    if (PrefabUtility.GetPrefabType(property.objectReferenceValue) != PrefabType.Prefab )
                    {
                        property.objectReferenceValue = null;
                    }
                }
            }
            else
            {
                EditorGUI.LabelField(position, label.text, "Field can be an object reference");
            }
        }
        public Type GetType(SerializedProperty property)
        {
            Type parentType = property.serializedObject.targetObject.GetType();
            System.Reflection.FieldInfo fi = parentType.GetField(property.propertyPath);
            return fi.FieldType;
        }
    }
}