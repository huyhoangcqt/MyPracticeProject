//C# Example (LookAtPointEditor.cs)
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ConvertCsv2Binary))]
[CanEditMultipleObjects]
public class ConvertCsv2BinaryEditor : Editor 
{
    SerializedProperty pathIn;
    SerializedProperty pathOut;
    SerializedProperty rawText;
    
    void OnEnable()
    {
        pathIn = serializedObject.FindProperty("path_in");
        pathOut = serializedObject.FindProperty("path_out");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(pathIn);
        EditorGUILayout.PropertyField(pathOut);
        if (GUILayout.Button("Generate")){
            GenerateFile();
        }
        serializedObject.ApplyModifiedProperties();
    }

    public void GenerateFile(){
        ConvertCsv2Binary _target = (ConvertCsv2Binary)target; 
        _target.Generate();
    }
}