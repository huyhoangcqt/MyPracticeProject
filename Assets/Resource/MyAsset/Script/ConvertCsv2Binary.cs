using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;
using UnityEditor;

public class ConvertCsv2Binary : MonoBehaviour
{
    public string path_in = "";
    public string path_out = "";
    public string rawText = "Vi ta yeu nhau nhu con song vo, quan quanh bao nam khong buong mat ho";
    private static Encoding CODE = Encoding.UTF8;
 
    // public byte[] ReadByteArrayFromFile(string fileName)
    // {
    //     byte[] buff = null;
    //     FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
    //     BinaryReader br = new BinaryReader(fs);
    //     long numBytes = new FileInfo(fileName).Length;
    //     buff = br.ReadBytes((int)numBytes);
    //     return buff;
    // }

    public string ReadTextAsset(string fileNamme){
        var file = Resources.Load<TextAsset>(path_in);
        var txt = file.text;
        // return txt.Replace(",","");
        return txt;
    }

    protected byte[] ConvertString(string s){
        byte[] bytes = CODE.GetBytes(s);
        short num = (short)bytes.Length;
        return BytesCombine(BitConverter.GetBytes(num), bytes);
    }

    protected byte[] BytesCombine(byte[] first, byte[] second)
    {
        byte[] bytes = new byte[first.Length + second.Length];
        Buffer.BlockCopy(first, 0, bytes, 0, first.Length);
        Buffer.BlockCopy(second, 0, bytes, first.Length, second.Length);
        return bytes;
    }

    public byte[] ReadByteArrayFromFile(string fileName)
    {
        var reader = new StreamReader(@"C:\test.csv");
        var stringBuilder = new StringBuilder();

        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            stringBuilder.Append(line);
        }
        string s = stringBuilder.ToString();
        return CODE.GetBytes(s.Replace(";",""));
    }

    public byte[] ReadByteArrayFromFile2(){
        var file = Resources.Load<TextAsset>(path_in);
        var txt = file.text;
        if (txt == null){
            Debug.Log("text null");
            return null;
        }
        Debug.Log("Text: " + txt);
        return CODE.GetBytes(txt.Replace(",",""));
    }

    // public string ByteArrayToString(byte[] byteArray, string s)
    // {       
    //     byte[] result = Encoding.Unicode.GetBytes(s.Replace(";",""));
    //     // for (int i = 0; i < byteArray.Length; i++)
    //     // {
    //     //     s += byteArray[i].ToString() + ";";
    //     // }
    //     // s = s.Substring(0, s.Length - 1);
    //     return s;
    // }
        
    public bool WriteByteArrayToFile(byte[] bytes)
    {
        bool response = false;
        try
        {
            FileStream fs = new FileStream(path_out, FileMode.Create, FileAccess.ReadWrite);
            BinaryWriter bw = new BinaryWriter(fs);
            string s = BitConverter.ToString(bytes);
            // string s = Convert.To

            // byte[] result = CODE.GetBytes(s);
            // string s3 = Convert.ToBase64String(bytes);
            int s3 = Convert.ToInt32(bytes);
            Debug.Log("s3: " + s3);
            // bw.Write(Encoding.Unicode.GetBytes(s3));
            bw.Write(s3);
            // bw.Write(s);
            // byte[] newByte = Encoding.Unicode.GetBytes(BitConverter.ToString(buff));
            // bw.Write(newByte);
            bw.Close();
            response = true;
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
            response = false;
        }
        return response;
    }

    private void WriteText(string filePath, string text)  
    {
        byte[] encodedText = Encoding.Unicode.GetBytes(text + Environment.NewLine);
        using (FileStream sourceStream = new FileStream(filePath,
        FileMode.Append, FileAccess.Write, FileShare.Write,
        bufferSize: 1024, useAsync: true))
        {
            sourceStream.Write(encodedText, 0, encodedText.Length);
        };
    }

    public void Generate(){
        // string text = ReadTextAsset(path_in);
        // WriteText(path_out, text);

        // Debug.Log("Generate: " + path_in);
        byte[] buff = ReadByteArrayFromFile2();
        WriteByteArrayToFile(buff);
    }

    void Start()
    {
        
    }
}
