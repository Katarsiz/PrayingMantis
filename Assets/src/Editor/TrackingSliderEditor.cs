using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TrackingSlider))]
public class TrackingSliderEditor : UnityEditor.UI.SliderEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();//Draw inspector UI of ImageEditor
        
        SerializedProperty modifierProperty = serializedObject.FindProperty("target");
        EditorGUILayout.PropertyField(modifierProperty, new GUIContent("Target"),true);
        serializedObject.ApplyModifiedProperties();
    }
}