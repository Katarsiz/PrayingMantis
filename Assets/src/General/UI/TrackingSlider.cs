using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class TrackingSlider : Slider {

    public Transform target;

    void FixedUpdate() {
        transform.position = Camera.main.WorldToScreenPoint (target.position);
    }
}


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