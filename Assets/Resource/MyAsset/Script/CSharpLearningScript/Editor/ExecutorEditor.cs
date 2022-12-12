using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Executor))]
[CanEditMultipleObjects]
public class ExecutorEditor : Editor{
    SerializedProperty module;
    Executor _target;

    void OnEnable(){
        module = serializedObject.FindProperty("module");
    }

    public override void OnInspectorGUI(){
        _target = (Executor)target;

        serializedObject.Update();
        EditorGUILayout.PropertyField(module);
        if (GUILayout.Button("Run Example")){
            _target.Execute();
        }
        serializedObject.ApplyModifiedProperties();
    }
}