// using System.Collections.Generic;
// using UnityEngine;
// using System.Linq;
// using UnityEditor;
// using System.IO;
// using TableTool;
// using System.Text.RegularExpressions;
// using System;
// using System.Reflection;
// using System.Text;
// using NGTools;

// #if UNITY_EDITOR
// public class ConvertToBytes : EditorWindow
// {
//     string exportPath = "";
//     string sourcePath = "";
//     DefaultAsset source = null;
//     DefaultAsset export = null;

//     TextAsset textAsset = null;

//     bool pathEnabled = false;
//     bool selectedEnabled = true;

//     byte[] raws;
//     int position;

//     [MenuItem("GH Tools/Convert to Bytes")]
//     static public void Init()
//     {
//         AssetDatabase.Refresh();
//         ConvertToBytes window = (ConvertToBytes)EditorWindow.GetWindow(typeof(ConvertToBytes));
//         window.minSize = new Vector2(400, 175);
//         window.Show();
//     }

//     void OnGUI()
//     {
//         //sourcePath = EditorGUILayout.TextField("Export from", sourcePath);
//         //EditorGUILayout.BeginHorizontal(new GUIStyle(GUI.skin.box));
//         //if (GUILayout.Button("Browse"))
//         //{
//         //    sourcePath = EditorUtility.OpenFolderPanel("Export from", "", "");
//         //    source = null;
//         //}
//         //GUILayout.Label("or drag folder here");
//         //source = (DefaultAsset)EditorGUILayout.ObjectField(source, typeof(DefaultAsset), false);
//         //if (source != null)
//         //{
//         //    sourcePath = Application.dataPath.Replace("Assets", string.Empty) + AssetDatabase.GetAssetPath(source);
//         //}
//         //EditorGUILayout.EndHorizontal();

//         //GUILayout.Label("");
//         //exportPath = EditorGUILayout.TextField("Export to", exportPath);
//         //EditorGUILayout.BeginHorizontal(new GUIStyle(GUI.skin.box));
//         //if (GUILayout.Button("Browse"))
//         //{
//         //    exportPath = EditorUtility.OpenFolderPanel("Export to", "", "");
//         //    export = null;
//         //}
//         //GUILayout.Label("or drag folder here");
//         //export = (DefaultAsset)EditorGUILayout.ObjectField(export, typeof(DefaultAsset), false);
//         //if (export != null)
//         //{
//         //    exportPath = Application.dataPath.Replace("Assets", string.Empty) + AssetDatabase.GetAssetPath(export);
//         //}
//         //EditorGUILayout.EndHorizontal();

//         GUILayout.Label("");
//         EditorGUILayout.BeginHorizontal(new GUIStyle(GUI.skin.box));
//         GUILayout.Label("or drag folder here");
//         textAsset = (TextAsset)EditorGUILayout.ObjectField(textAsset, typeof(TextAsset), false);
//              EditorGUILayout.EndHorizontal();

//         GUILayout.Label("");
//         if (GUILayout.Button("Execute"))
//         {
//             ReadFromFile();
//         }

//         GUILayout.Label("");
//         if (GUILayout.Button("Write"))
//         {
//             WriteFromFile();
//         }
//     }

//     private void WriteFromFile()
//     {
//         WriteBytesFromCsv();
//     }
//     private void WriteBytesFromCsv()
//     {
//         Regex CsvParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
//         string dataString = textAsset.text;
//         string[] splitedString = dataString.Split('\n');
//         int numLines = splitedString.Length;
//         short dataLength = 0;
//         byte[] rawBytes = BytesCombine(BitConverter.GetBytes(0), BitConverter.GetBytes(dataLength));

//         // Get data types
//         string[] typeString = splitedString[1].Split(',');
//         int numCols = typeString.Length;
//         for (int i = 0; i < numCols; i++)
//         {
//             if (typeString[i].Contains('"') || typeString[i].Contains('\n') || typeString[i].Contains('\r'))
//             {
//                 typeString[i] = typeString[i].Replace("\"", string.Empty);
//                 typeString[i] = typeString[i].Replace("\r", string.Empty);
//                 typeString[i] = typeString[i].Replace("\n", string.Empty);
//             }
//         }
        

//         // Encode datas
//         for (int i = 2; i < numLines; i++)
//         {
//             string[] dataInString = CsvParser.Split(splitedString[i]);
//             for (int col = 0; col < numCols; col++)
//             {
//                 if (dataInString[col].Contains('"') || dataInString[col].Contains('\n') || dataInString[col].Contains('\r'))
//                 {
//                     dataInString[col] = dataInString[col].Replace("\"", string.Empty);
//                     dataInString[col] = dataInString[col].Replace("\r", string.Empty);
//                     dataInString[col] = dataInString[col].Replace("\n", string.Empty);
//                 }                

//                 switch (typeString[col])
//                 {
//                     case "Int32":
//                         rawBytes = BytesCombine(rawBytes, writeInt(Int32.Parse(dataInString[col])));
//                         break;
//                     case "String":
//                         rawBytes = BytesCombine(rawBytes, writeString(dataInString[col]));
//                         break;
//                     case "Boolean":
//                         if (dataInString[col] == "True")
//                         {
//                             rawBytes = BytesCombine(rawBytes, writeShort((short)1));
//                         }
//                         else
//                         {
//                             rawBytes = BytesCombine(rawBytes, writeShort((short)0));
//                         }
//                         break;
//                     case "String[]":
//                         string[] stringArray = dataInString[col].Split(';');
//                         rawBytes = BytesCombine(rawBytes, writeArrayString(stringArray));
//                         break;

//                 }
//             }
//         }
        
//         File.WriteAllBytes("C:\\Users\\gihot\\Documents\\Git\\m10_client\\PGame\\Assets\\Resources\\csvdata" + textAsset.name+ ".bytes", rawBytes);
//     }

//     protected byte[] writeShort(short s)
//     {
//         return BitConverter.GetBytes(s).Reverse().ToArray();
//     }

//     protected byte[] writeInt(int i)
//     {
//         return BitConverter.GetBytes(i).Reverse().ToArray();
//     }
//     protected byte[] writeString(string s)
//     {
//         byte[] encodedByte = Encoding.UTF8.GetBytes(s);
//         int numBytes = encodedByte.Length;
//         short num = Convert.ToInt16(numBytes+2);
//         return BytesCombine(BitConverter.GetBytes(num).Reverse().ToArray(), encodedByte);
//     }

//     protected byte[] writeArrayString(string[] s)
//     {
//         byte[] encodedByte = null;
//         for (int i = 0; i < s.Length; i++)
//         {
//             if(i == 0)
//             {
//                 encodedByte = writeString(s[0]);
//             }
//             else
//             {
//                 encodedByte = BytesCombine(encodedByte, writeString(s[i]));
//             }
//         }
//         short num = Convert.ToInt16(s.Length);
//         return BytesCombine(BitConverter.GetBytes(num).Reverse().ToArray(), encodedByte);
//     }


//     protected byte[] BytesCombine(byte[] first, byte[] second)
//     {
//         byte[] bytes = new byte[first.Length + second.Length];
//         Buffer.BlockCopy(first, 0, bytes, 0, first.Length);
//         Buffer.BlockCopy(second, 0, bytes, first.Length, second.Length);
//         return bytes;
//     }

//     private Dictionary<string, byte[]> SelectFilesFromPath()
//     {
//         string relativeSourcePath = "";
//         string[] files = Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories);
//         Dictionary<string, byte[]> byteFiles = new Dictionary<string, byte[]>();
//         foreach (string file in files)
//         {
//             if (file.EndsWith(".bytes"))
//             {

//                 if (sourcePath.StartsWith(Application.dataPath))
//                 {
//                     relativeSourcePath = "Assets" + sourcePath.Substring(Application.dataPath.Length);
//                 }
//                 // Trim the filepath
//                 string filename = file.Replace(sourcePath + @"\", string.Empty);
//                 byteFiles.Add(filename, File.ReadAllBytes(relativeSourcePath + "/" + filename));
//                 //Debug.Log(filename);
//             }
//         }
//         return byteFiles;
//         //return Selection.gameObjects.SelectMany(gameObjectList => gameObjectList.GetComponentsInChildren<Transform>(true)).Select(t => t.gameObject);
//     }

//     private void ConvertSelectedFilesToXlsx()
//     {
//         Dictionary<string, byte[]> files = SelectFilesFromPath();
//         foreach (string key in files.Keys)
//         {
//             File.WriteAllBytes(exportPath + "/" + key.Split('.')[0] + ".xlsx", files[key]);
//         }
//     }

//     private void ReadFromFile()
//     {
//         LocalModelManager localModelManager = LocalModelManager.Instance;
//         localModelManager.InitializeAll();
//     }

// }
// #endif