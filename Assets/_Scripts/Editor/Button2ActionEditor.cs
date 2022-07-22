using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(Button2Action), true)]
[CanEditMultipleObjects]
public class Button2ActionEditor : ButtonEditor
{
    SerializedProperty m_OnDownProperty;

    protected override void OnEnable()
    {
        base.OnEnable();
        m_OnDownProperty = serializedObject.FindProperty("m_OnDown");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space();

        serializedObject.Update();
        EditorGUILayout.PropertyField(m_OnDownProperty);
        serializedObject.ApplyModifiedProperties();
    }
}
