using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ModifierWrap : Image{

    public Modifier modifier;

    /// <summary>
    /// The bug this modifier has been associated to
    /// </summary>
    private Bug bug;

    public void SetBug(Bug b) {
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
        
        SerializedProperty modifierProperty = serializedObject.FindProperty("modifier");

        ModifierWrap wrap = (ModifierWrap)target;
        //wrap.modifier = EditorGUILayout.PropertyField("Modifier", wrap.modifier);
        EditorGUILayout.PropertyField(modifierProperty, new GUIContent("Modifier"),true);
        serializedObject.ApplyModifiedProperties();
    }
}