using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Button4Action), true)]
[CanEditMultipleObjects]
public class Button4ActionEditor : Button2ActionEditor
{
    SerializedProperty m_OnEnterProperty;
    SerializedProperty m_OnExitProperty;

    protected override void OnEnable()
    {
        base.OnEnable();
        m_OnExitProperty = serializedObject.FindProperty("m_OnExit");
        m_OnEnterProperty = serializedObject.FindProperty("m_OnEnter");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space();

        serializedObject.Update();
        EditorGUILayout.PropertyField(m_OnEnterProperty);
        EditorGUILayout.PropertyField(m_OnExitProperty);
        serializedObject.ApplyModifiedProperties();
    }
}
