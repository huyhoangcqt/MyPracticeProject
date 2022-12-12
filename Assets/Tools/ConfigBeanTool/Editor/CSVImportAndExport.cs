using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;
using System.IO;
// using TableTool;
using System.Text.RegularExpressions;
using System;
using System.Reflection;
using System.Text;
// using NGTools;
// using Dxx.Util;
using System.Net;

#if UNITY_EDITOR
public class CSVImportAndExport : EditorWindow
{
    public static CSVImportAndExport Instance { get; private set; }
    void OnEnable()
    {
        Instance = this;
    }

    string fileName = "";
    string exportPath = "";
    string sourcePath = "";
    DefaultAsset source = null;
    DefaultAsset export = null;

    TextAsset sourceTextAsset = null;
    TextAsset exportTextAsset = null;

    bool isExporting = true;
    bool isOverwritten = false;

    [MenuItem("GH Tools/Convert to Bytes")]
    static public void Init()
    {
        AssetDatabase.Refresh();
        CSVImportAndExport window = (CSVImportAndExport)EditorWindow.GetWindow(typeof(CSVImportAndExport));
        window.minSize = new Vector2(430, 100);
        window.Show();
        if (Directory.Exists(Application.dataPath + "/../ConfigCSV") == false)
        {
            Directory.CreateDirectory(Application.dataPath + "/../ConfigCSV");
        }
    }

    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal(new GUIStyle(GUI.skin.box));
        isExporting = EditorGUILayout.Toggle("Export", isExporting);
        if (!isExporting)
        {
            isOverwritten = EditorGUILayout.Toggle("Overwrite Model/Bean", isOverwritten);
        }
        EditorGUILayout.EndHorizontal();
        if (isExporting)
        {
            EditorGUILayout.BeginHorizontal(new GUIStyle(GUI.skin.box));
            fileName = EditorGUILayout.TextField("Tên file", fileName);
            if (GUILayout.Button("Duyệt"))
            {
                fileName = Path.GetFileName(EditorUtility.OpenFilePanel("Export from", "", "")).Split('.')[0];
                sourceTextAsset = null;
            }
            EditorGUILayout.EndHorizontal();
            
            if (GUILayout.Button("Export một file"))
            {
                exportPath = Application.dataPath + "/../ConfigCSV";
                ReadSpecificFile();
            }
            if (GUILayout.Button("Export hết"))
            {
                exportPath = Application.dataPath + "/../ConfigCSV";
                ReadAllFile();
            }
                
        }

        if (!isExporting)
        {
            EditorGUILayout.BeginHorizontal(new GUIStyle(GUI.skin.box));
            fileName = EditorGUILayout.TextField("Tên file", fileName);
            
            if (GUILayout.Button("Duyệt"))
            {
                fileName = Path.GetFileName(EditorUtility.OpenFilePanel("Export from", "", "")).Split('.')[0];
                sourceTextAsset = null;
            }
            EditorGUILayout.EndHorizontal();

            

            if (GUILayout.Button("Import một file"))
            {
                try {
                    exportPath = Application.dataPath + "/../ConfigBeanTool/DataConfig";
                    sourcePath = Application.dataPath + "/AssetResources/ConfigBeanTool/Excel";
                    // Debug.Log("exportPath: " + exportPath);
                    // Debug.Log("sourcePath: " + sourcePath);
                    sourceTextAsset = new TextAsset(File.ReadAllText(sourcePath + "/"+ fileName + ".csv"));
                    sourceTextAsset.name = fileName;
                    WriteSpecificFile();
                }
                catch (Exception e){
                    Debug.LogError(e.Message);
                    Debug.LogError(e.StackTrace);
                }
            }
            if (GUILayout.Button("Import hết"))
            {
                exportPath = Application.dataPath + "/AssetResources/ConfigBeanTool/Excel";
                sourcePath = Application.dataPath + "/../ConfigBeanTool/DataConfig";
                WriteAllFile();
            }
        }
    }

    private void ReadSpecificFile()
    {
        // LocalModelManager.Instance = new LocalModelManager();
        PlayerPrefs.SetString("exportPath", exportPath);
        // LocalModelManager.Instance.ExportOne(fileName);
    }

    private void ReadAllFile()
    {
        // LocalModelManager.Instance = new LocalModelManager();
        PlayerPrefs.SetString("exportPath", exportPath);
        // LocalModelManager.Instance.InitializeAll();
    }

    private void WriteSpecificFile()
    {
        // LocalModelManager.Instance = new LocalModelManager();

        WriteBytesFromCsv(sourceTextAsset.text, sourceTextAsset.name);
    }

    private void WriteAllFile()
    {
        // LocalModelManager.Instance = new LocalModelManager();
        foreach (String fileName in SelectCsvsFilesFromPath().Keys)
        {
            Debug.Log(fileName + " exporting!");
            WriteBytesFromCsv(SelectCsvsFilesFromPath()[fileName], fileName);
        }
    }

    #region CREATE MODEL
    private bool AddToLocalModelManager(string modelName)
    {
        string managerRelativeFilePath = "Assets/ScriptsLogic/TableTool/LocalModelManager.cs";
        string managerFilePath = Application.dataPath + "/ScriptsLogic/TableTool/LocalModelManager.cs";

        TextAsset managerTextAsset = (TextAsset)AssetDatabase.LoadAssetAtPath(managerRelativeFilePath, typeof(TextAsset));
        List<string> linesInManager = managerTextAsset.text.Split('\n').ToList();
        //init property insert
        string newstring = string.Format("private {0}Model _{0}Model;",modelName);
        for (int i = 0; i < linesInManager.Count; i++)
        {
            if (linesInManager[i].Contains(newstring))
            {
                return false;
            }
        }
        linesInManager = TryInsertAfter(linesInManager, "//CSV IMPORTER - INIT PROPERTY (DO NOT DELETE THIS LINE)", newstring);
        //set property insert
        newstring = string.Format("public {0}Model {0}{{get{{if (_{0}Model == null){{ _{0}Model = new {0}Model();}}return _{0}Model;}}}}", modelName); ;
        linesInManager = TryInsertAfter(linesInManager, "//CSV IMPORTER - SET PROPERTY (DO NOT DELETE THIS LINE)", newstring);
        //init one insert
        newstring = string.Format("if(modelName==\"{0}\"){{{0}Model {1} = {0};}}", modelName, LowerCaseFirstChar(modelName));
        linesInManager = TryInsertAfter(linesInManager, "//CSV IMPORTER - INITIALIZE ONE PROPERTY (DO NOT DELETE THIS LINE)", newstring);
        //init all insert
        newstring = string.Format("{0}Model {1} = {0};", modelName, LowerCaseFirstChar(modelName));
        linesInManager = TryInsertAfter(linesInManager, "//CSV IMPORTER - INITIALIZE ALL PROPERTY (DO NOT DELETE THIS LINE)", newstring);
        File.WriteAllText(managerFilePath,string.Join("\n", linesInManager.ToArray()));
        Debug.Log(modelName + " added to LocalModelManager!");
        return true;
    }

    private void AddToTableTool(string modelName, string[] nameString, string[] typeString)
    {
        string modelRelativeFilePath = "Assets/ScriptsLogic/TableTool/" + modelName + ".cs";
        string modelPath = Application.dataPath + "/ScriptsLogic/TableTool/" + modelName + ".cs";
        string modelFileNameWithExtension = modelName + ".cs";
        string modelFileName = modelName;

        //localmodel
        string content = File.ReadAllText(Application.dataPath + "/Editor/CSVConfigurator/LocalModelTemplate.txt");
        content = content.Replace("{0}", modelName).Replace("{1}", GengerateTypeString(typeString[0]));
        File.WriteAllText(Application.dataPath + "/ScriptsLogic/TableTool/" +modelName+"Model.cs", content);

        //localbean
        content = File.ReadAllText(Application.dataPath + "/Editor/CSVConfigurator/LocalBeanTemplate.txt");
        content = content.Replace("{0}", modelName).Replace("{1}", LowerCaseFirstChar(modelName));
        string addedContent;
        List<string> initList = new List<string>();
        List<string> impleList = new List<string>();
        List<string> copyList = new List<string>();

        for (int i = 0; i < nameString.Length; i++)
        {
            initList.Add(GengerateInitString(nameString[i], typeString[i]));
            impleList.Add(GengerateReadImplString(nameString[i], typeString[i]));
            copyList.Add(GengerateCopyString(nameString[i], LowerCaseFirstChar(modelName)));
        }
        addedContent = string.Join("\n", initList.ToArray());
        content = content.Replace("//CSV IMPORTER - INIT PROPERTY", addedContent);

        addedContent = string.Join("\n", impleList.ToArray());
        content = content.Replace("//CSV IMPORTER - READ IMPLEMENT", addedContent);

        addedContent = string.Join("\n", copyList.ToArray());
        content = content.Replace("//CSV IMPORTER - COPY IMPLEMENT", addedContent);

        File.WriteAllText(Application.dataPath + "/ScriptsLogic/TableTool/" + modelName + ".cs", string.Join("\n", content));
        Debug.Log(modelName + " added LocalModel, LocalBean!");
    }

    private string GengerateTypeString(string type)
    {
        string newString = "";
        switch (type)
        {
            case "Int16":
                newString += "short";
                break;
            case "Int32":
                newString += "int";
                break;
            case "Int64":
                newString += "long";
                break;
            case "Single":
                newString += "float";
                break;
            case "Double":
                newString += "double";
                break;
            case "String":
                newString += "string";
                break;
            case "Int16[]":
                newString += "short[]";
                break;
            case "Int32[]":
                newString += "int[]";
                break;
            case "Int64[]":
                newString += "long[]";
                break;
            case "Single[]":
                newString += "float[]";
                break;
            case "Double[]":
                newString += "double[]";
                break;
            case "String[]":
                newString += "string[]";
                break;
        }
        return newString;
    }

    private string GengerateCopyString(string propertyName, string classname)
    {
        return classname + "." + propertyName + " = " + propertyName+ ";";
    }

    private string GengerateInitString(string name, string type) 
    {
        string newString = "public ";
        switch (type)
        {
            case "Int16":
                newString += "short";
                break;
            case "Int32":
                newString += "int";
                break;
            case "Int64":
                newString += "long";
                break;
            case "Single":
                newString += "float";
                break;
            case "Double":
                newString += "double";
                break;
            case "String":
                newString += "string";
                break;
            case "Int16[]":
                newString += "short[]";
                break;
            case "Int32[]":
                newString += "int[]";
                break;
            case "Int64[]":
                newString += "long[]";
                break;
            case "Single[]":
                newString += "float[]";
                break;
            case "Double[]":
                newString += "double[]";
                break;
            case "String[]":
                newString += "string[]";
                break;
        }
        newString += " " + name + " {get;private set;}";
        return newString;
    }
    
    private string GengerateReadImplString(string name, string type) 
    {
        string newString = name;
        newString += " = ";
        switch (type)
        {
            case "Int16":
                newString += "readShort();";
                break;
            case "Int32":
                newString += "readInt();";
                break;
            case "Int64":
                newString += "readLong();";
                break;
            case "Single":
                newString += "readFloat();";
                break;
            case "Double":
                newString += "readDouble();";
                break;
            case "String":
                newString += "readLocalString();";
                break;
            case "Int16[]":
                newString += "readArrayshort();";
                break;
            case "Int32[]":
                newString += "readArrayint();";
                break;
            case "Int64[]":
                newString += "readArraylong();";
                break;
            case "Single[]":
                newString += "readArrayfloat();";
                break;
            case "Double[]":
                newString += "readArraydouble();";
                break;
            case "String[]":
                newString += "readArraystring();";
                break;
        }
        return newString;
    }

    public List<string> TryInsertAfter(List<string> fileLines, String searchAfterText, String insertAfterText)
    {
        int index = -1;
        bool hasLine = false;
        for(int i = 0; i<fileLines.Count; i++)
        {
            index++;
            if (fileLines[i].Contains(searchAfterText)) 
            {
                hasLine = true;
                break;
            }
        }   
        if (hasLine)
        {
            fileLines.Insert(++index, insertAfterText);
        }
        return fileLines;
    }
    private string LowerCaseFirstChar (string inputString)
    {
        string firstChar = inputString.Substring(0, 1);
        string newString = inputString.Substring(1, inputString.Length-1);
        return firstChar.ToLower() + newString;
    }
    #endregion

    private void WriteBytesFromCsv(String dataString, String nameString)
    {
        Regex CsvParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

        string[] splitedString = dataString.Split('\n');
        int numLines = splitedString.Length;
        if (splitedString[numLines-1] == string.Empty)
        {
            numLines = numLines - 1;
        }
        short dataLength = 32767;
        byte[] rawBytes = BitConverter.GetBytes(numLines - 2).Reverse().ToArray();

        // Get data types
        string[] variableNameString = splitedString[0].Split(',');
        string[] typeString = splitedString[1].Split(',');
        int numCols = typeString.Length;
        for (int i = 0; i < numCols; i++)
        {
            if (typeString[i].Contains('"') || typeString[i].Contains('\n') || typeString[i].Contains('\r'))
            {
                typeString[i] = typeString[i].Replace("\"", string.Empty);
                typeString[i] = typeString[i].Replace("\r", string.Empty);
                typeString[i] = typeString[i].Replace("\n", string.Empty);
            }
        }

        Debug.Log("first line data: " + splitedString[2]);
        string[] dataInString = CsvParser.Split(splitedString[2]);
        for (int i = 0; i < dataInString.Length; i++){
            Debug.Log(string.Format("Col {0}: {1}", i+1, dataInString[i]));
        }
        // Encode datas
        // for (int i = 2; i < numLines; i++)
        // {
        //     rawBytes = BytesCombine(rawBytes, BitConverter.GetBytes(dataLength).Reverse().ToArray());
        //     string[] dataInString = CsvParser.Split(splitedString[i]);
        //     for (int col = 0; col < numCols; col++)
        //     {
        //         if (dataInString[col].Contains('"') || dataInString[col].Contains('\n') || dataInString[col].Contains('\r'))
        //         {
        //             dataInString[col] = dataInString[col].Replace("\"", string.Empty);
        //             dataInString[col] = dataInString[col].Replace("\r", string.Empty);
        //             dataInString[col] = dataInString[col].Replace("\n", string.Empty);
        //         }

        //         string[] array = dataInString[col].Split(';');
        //         bool isEmptyArray = false;
        //         if (dataInString[col] == String.Empty)
        //         {
        //             isEmptyArray = true;
        //         }

        //         switch (typeString[col])
        //         {
        //             case "Int16":
        //                 rawBytes = BytesCombine(rawBytes, writeShort(Int16.Parse(dataInString[col])));
        //                 break;
        //             case "Boolean":
        //                 rawBytes = BytesCombine(rawBytes, writeBool(dataInString[col]));
        //                 break;
        //             case "Int32":
        //                 rawBytes = BytesCombine(rawBytes, writeInt(Int32.Parse(dataInString[col])));
        //                 break;
        //             case "Int64":
        //                 rawBytes = BytesCombine(rawBytes, writeLong(Int64.Parse(dataInString[col])));
        //                 break;
        //             case "Single":
        //                 rawBytes = BytesCombine(rawBytes, writeFloat(Single.Parse(dataInString[col])));
        //                 break;
        //             case "Double":
        //                 rawBytes = BytesCombine(rawBytes, writeDouble(Double.Parse(dataInString[col])));
        //                 break;
        //             case "String":
        //                 rawBytes = BytesCombine(rawBytes, writeString(dataInString[col]));
        //                 break;
        //             case "Int16[]":
        //                 if (isEmptyArray)
        //                 {
        //                     rawBytes = BytesCombine(rawBytes, writeShort((short)0));
        //                     break;
        //                 }
        //                 short[] shortArray = array
        //                     .Select(Int16.Parse).ToArray(); ;
        //                 rawBytes = BytesCombine(rawBytes, writeArrayShort(shortArray));
        //                 break;
        //             case "Boolean[]":
        //                 if (isEmptyArray)
        //                 {
        //                     rawBytes = BytesCombine(rawBytes, writeShort((short)0));
        //                     break;
        //                 }
        //                 rawBytes = BytesCombine(rawBytes, writeArrayBool(array));
        //                 break;
        //             case "Int32[]":
        //                 if (isEmptyArray)
        //                 {
        //                     rawBytes = BytesCombine(rawBytes, writeShort((short)0));
        //                     break;
        //                 }
        //                 int[] intArray = array
        //                     .Select(Int32.Parse).ToArray(); ;
        //                 rawBytes = BytesCombine(rawBytes, writeArrayInt(intArray));
        //                 break;
        //             case "Int64[]":
        //                 if (isEmptyArray)
        //                 {
        //                     rawBytes = BytesCombine(rawBytes, writeShort((short)0));
        //                     break;
        //                 }
        //                 long[] longArray = array
        //                     .Select(Int64.Parse).ToArray(); ;
        //                 rawBytes = BytesCombine(rawBytes, writeArrayLong(longArray));
        //                 break;
        //             case "Single[]":
        //                 if (isEmptyArray)
        //                 {
        //                     rawBytes = BytesCombine(rawBytes, writeShort((short)0));
        //                     break;
        //                 }
        //                 float[] floatArray = array
        //                     .Select(Single.Parse).ToArray(); ;
        //                 rawBytes = BytesCombine(rawBytes, writeArrayFloat(floatArray)); break;
        //             case "Double[]":
        //                 if (isEmptyArray)
        //                 {
        //                     rawBytes = BytesCombine(rawBytes, writeShort((short)0));
        //                     break;
        //                 }
        //                 double[] doubleArray = array
        //                     .Select(Double.Parse).ToArray(); ;
        //                 rawBytes = BytesCombine(rawBytes, writeArrayDouble(doubleArray)); break;
        //             case "String[]":
        //                 if (isEmptyArray)
        //                 {
        //                     rawBytes = BytesCombine(rawBytes, writeShort((short)0));
        //                     break;
        //                 }
        //                 string[] stringArray = array;
        //                 rawBytes = BytesCombine(rawBytes, writeArrayString(stringArray));
        //                 //Array.Resize(ref rawBytes, rawBytes.Length - 2);
        //                 break;

        //         }
        //     }
        // }
        // File.WriteAllBytes(exportPath + "/" + nameString.Split('.')[0] + ".bytes", rawBytes);

        // if (AddToLocalModelManager(nameString.Split('.')[0]))
        // {
        //     AddToTableTool(nameString.Split('.')[0], variableNameString, typeString);
        //     AssetDatabase.Refresh();
        // }
        // else if (isOverwritten)
        // {
        //     AddToTableTool(nameString.Split('.')[0], variableNameString, typeString);
        //     AssetDatabase.Refresh();
        // }
        // else
        // {
        //     Debug.Log("Model " + nameString.Split('.')[0] + " exists, not gonna overwrite!");
        // }
    }

    #region WRITE
    protected byte[] writeShort(short s)
    {
        return BitConverter.GetBytes(s).Reverse().ToArray();
    }

    protected byte[] writeBool(string s)
    {
        if (s == "True" || s == "TRUE" || s == "true")
        {
            return writeShort((short)1);
        }
        else
        {
            return writeShort((short)0);
        }
    }

    protected byte[] writeInt(int i)
    {
        return BitConverter.GetBytes(i).Reverse().ToArray();
    }

    protected byte[] writeLong(long l)
    {
        return BitConverter.GetBytes(l).Reverse().ToArray();
    }

    protected byte[] writeFloat(float f)
    {
        return BitConverter.GetBytes(f).Reverse().ToArray();
    }

    protected byte[] writeDouble(double d)
    {
        return BitConverter.GetBytes(d).Reverse().ToArray();
    }

    protected byte[] writeString(string s)
    {
        byte[] encodedByte = Encoding.UTF8.GetBytes(s);
        int numBytes = encodedByte.Length;
        short num = Convert.ToInt16(numBytes + 2);
        return BytesCombine(BitConverter.GetBytes(num).Reverse().ToArray(), encodedByte);
    }

    protected byte[] writeArrayShort(short[] array)
    {
        byte[] encodedByte = null;
        short num = Convert.ToInt16(array.Length);
        if (num == 0)
        {
            return BitConverter.GetBytes(num).Reverse().ToArray();
        }
        for (int k = 0; k < num; k++)
        {
            if (k == 0)
            {
                encodedByte = writeShort(array[0]);
            }
            else
            {
                encodedByte = BytesCombine(encodedByte, writeShort(array[k]));
            }
        }

        return BytesCombine(BitConverter.GetBytes(num).Reverse().ToArray(), encodedByte);
    }

    protected byte[] writeArrayBool(string[] array)
    {
        byte[] encodedByte = null;
        short num = Convert.ToInt16(array.Length);
        if (num == 0)
        {
            return BitConverter.GetBytes(num).Reverse().ToArray();
        }
        for (int k = 0; k < num; k++)
        {
            if (k == 0)
            {
                encodedByte = writeBool(array[0]);
            }
            else
            {
                encodedByte = BytesCombine(encodedByte, writeBool(array[k]));
            }
        }

        return BytesCombine(BitConverter.GetBytes(num).Reverse().ToArray(), encodedByte);
    }

    protected byte[] writeArrayInt(int[] array)
    {
        byte[] encodedByte = null;
        short num = Convert.ToInt16(array.Length);
        if (num == 0)
        {
            return BitConverter.GetBytes(num).Reverse().ToArray();
        }
        for (int k = 0; k < num; k++)
        {
            if (k == 0)
            {
                encodedByte = writeInt(array[0]);
            }
            else
            {
                encodedByte = BytesCombine(encodedByte, writeInt(array[k]));
            }
        }

        return BytesCombine(BitConverter.GetBytes(num).Reverse().ToArray(), encodedByte);
    }

    protected byte[] writeArrayLong(long[] array)
    {
        byte[] encodedByte = null;
        short num = Convert.ToInt16(array.Length);
        if (num == 0)
        {
            return BitConverter.GetBytes(num).Reverse().ToArray();
        }
        for (int k = 0; k < num; k++)
        {
            if (k == 0)
            {
                encodedByte = writeLong(array[0]);
            }
            else
            {
                encodedByte = BytesCombine(encodedByte, writeLong(array[k]));
            }
        }

        return BytesCombine(BitConverter.GetBytes(num).Reverse().ToArray(), encodedByte);
    }

    protected byte[] writeArrayFloat(float[] array)
    {
        byte[] encodedByte = null;
        short num = Convert.ToInt16(array.Length);
        if (num == 0)
        {
            return BitConverter.GetBytes(num).Reverse().ToArray();
        }
        for (int k = 0; k < num; k++)
        {
            if (k == 0)
            {
                encodedByte = writeFloat(array[0]);
            }
            else
            {
                encodedByte = BytesCombine(encodedByte, writeFloat(array[k]));
            }
        }

        return BytesCombine(BitConverter.GetBytes(num).Reverse().ToArray(), encodedByte);
    }

    protected byte[] writeArrayDouble(double[] array)
    {
        byte[] encodedByte = null;
        short num = Convert.ToInt16(array.Length);
        if (num == 0)
        {
            return BitConverter.GetBytes(num).Reverse().ToArray();
        }
        for (int k = 0; k < num; k++)
        {
            if (k == 0)
            {
                encodedByte = writeDouble(array[0]);
            }
            else
            {
                encodedByte = BytesCombine(encodedByte, writeDouble(array[k]));
            }
        }

        return BytesCombine(BitConverter.GetBytes(num).Reverse().ToArray(), encodedByte);
    }

    protected byte[] writeArrayString(string[] array)
    {
        byte[] encodedByte = null;
        short num = Convert.ToInt16(array.Length);
        if (num == 0)
        {
            return BitConverter.GetBytes(num).Reverse().ToArray();
        }
        for (int i = 0; i < num; i++)
        {
            if (i == 0)
            {
                encodedByte = writeString(array[0]);
            }
            else
            {
                encodedByte = BytesCombine(encodedByte, writeString(array[i]));
            }
        }
        encodedByte = BytesCombine(BitConverter.GetBytes(num).Reverse().ToArray(), encodedByte);
        //Array.Resize(ref encodedByte, encodedByte.Length - 2);
        return encodedByte;
    }

    #endregion

    #region UTILS
    protected byte[] BytesCombine(byte[] first, byte[] second)
    {
        byte[] bytes = new byte[first.Length + second.Length];
        Buffer.BlockCopy(first, 0, bytes, 0, first.Length);
        Buffer.BlockCopy(second, 0, bytes, first.Length, second.Length);
        return bytes;
    }

    private Dictionary<string, string> SelectCsvsFilesFromPath()
    {
        string relativeSourcePath = "";
        string[] files = Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories);
        Dictionary<string, string> filesDictionary = new Dictionary<string, string>();
        foreach (string file in files)
        {
            if (file.EndsWith(".csv") || file.EndsWith(".CSV"))
            {

                if (sourcePath.StartsWith(Application.dataPath))
                {
                    relativeSourcePath = "Assets" + sourcePath.Substring(Application.dataPath.Length);
                }
                // Trim the filepath
                string filename = file.Replace(sourcePath + @"\", string.Empty);
                if (filename.Contains(sourcePath))
                {
                    filename = file.Replace(sourcePath, string.Empty);
                }
                Debug.Log(filename);
                filesDictionary.Add(filename, File.ReadAllText(relativeSourcePath + "/" + filename));
            }
        }
        return filesDictionary;
        //return Selection.gameObjects.SelectMany(gameObjectList => gameObjectList.GetComponentsInChildren<Transform>(true)).Select(t => t.gameObject);
    }
    #endregion

}
#endif