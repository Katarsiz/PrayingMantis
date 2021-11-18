using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ModifierWrap : Image{

    /// <summary>
    /// Modifier applied by the bug
    /// </summary>
    public Modifier applyModifier;
    
    /// <summary>
    /// ;Modifier applied when the bug is corrected
    /// </summary>
    public Modifier resetModifier;

    /// <summary>
    /// The bug this modifier has been associated to
    /// </summary>
    private Bug bug;

    public ScrollManager originalContainer;

    public void SetBug(Bug b) {
        Debug.Log(b);
        bug = b;
    }

    /// <summary>
    /// Determines if this modifier wrap has been associated with a bug
    /// </summary>
    /// <returns></returns>
    public Bug GetBug() {
        return bug;
    }
}

[CustomEditor(typeof(ModifierWrap))]
public class ModifierWrapEditor : UnityEditor.UI.ImageEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();//Draw inspector UI of ImageEditor
        
        SerializedProperty applyModifierProperty = serializedObject.FindProperty("applyModifier");
        SerializedProperty resetModifierProperty = serializedObject.FindProperty("applyModifier");
        EditorGUILayout.PropertyField(applyModifierProperty, new GUIContent("Apply Modifier"),true);
        EditorGUILayout.PropertyField(resetModifierProperty, new GUIContent("Reset Modifier"),true);
        serializedObject.ApplyModifiedProperties();
    }
}