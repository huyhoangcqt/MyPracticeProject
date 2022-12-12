// using Excel = Microsoft.Office.Interop.Excel; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using System;

#if UNITY_EDITOR
public class ExcelConverter : EditorWindow
{
    #region Attributes
    private bool import = true;
    private string filename;
    private string rootExcel = Application.dataPath + "/ExcelConverter/ExcelFiles/";
    private string rootConfig = Application.dataPath + "/ExcelConverter/ConfigBean/";
    private string srcPath;
    private string desPath;
    // private TextAsset
    #endregion


    #region  Main Methods
    [MenuItem("HoangTool/Excel Converter")]
    static public void Init()
    {
        AssetDatabase.Refresh();
        ExcelConverter window = (ExcelConverter)EditorWindow.GetWindow(typeof(ExcelConverter));
        window.minSize = new Vector2(430, 100);
        window.Show();
        if (!Directory.Exists(Application.dataPath + "/../ConfigBean"))
        {
            Directory.CreateDirectory(Application.dataPath + "/../ConfigBean");
        }
        if (!Directory.Exists(Application.dataPath + "/../ExcelFiles"))
        {
            Directory.CreateDirectory(Application.dataPath + "/../ExcelFiles");
        }
    }

    void OnGUI(){
        GUILayout.BeginVertical("Header");

        GUILayout.BeginHorizontal("box");

        import = GUILayout.Button("ExcelToBytes");
        import = !GUILayout.Button("BytesToExcel");

        GUILayout.EndHorizontal();

        GUILayout.Space(5);
        GUILayout.BeginVertical("Editor View");
        ContenView();
        GUILayout.EndVertical();

        GUILayout.EndVertical();
    }
    #endregion

    #region View Methods;
    void ContenView(){
        filename = GUILayout.TextField("FileName:", filename);
        if (GUILayout.Button("Convert 1 File")){
            ConvertFile("FileName");
        }
        if (GUILayout.Button("Convert Alls")){
            ConvertAllFiles();
        }
    }
    #endregion !View

    #region Process
    void ConvertFile(string filename){
        if (import){
            srcPath = rootExcel + filename + ".xlsx";
        }
        else {

        }
    }

    void ConvertAllFiles(){

    }

    #endregion !Process
}
#endif