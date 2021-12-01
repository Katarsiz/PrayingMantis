using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ModifierWrap))]
public class ModifierWrapEditor : UnityEditor.UI.ImageEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();//Draw inspector UI of ImageEditor
        
        SerializedProperty applyModifierProperty = serializedObject.FindProperty("applyModifier");
        SerializedProperty resetModifierProperty = serializedObject.FindProperty("resetModifier");
        EditorGUILayout.PropertyField(applyModifierProperty, new GUIContent("Apply Modifier"),true);
        EditorGUILayout.PropertyField(resetModifierProperty, new GUIContent("Reset Modifier"),true);
        serializedObject.ApplyModifiedProperties();
    }
}