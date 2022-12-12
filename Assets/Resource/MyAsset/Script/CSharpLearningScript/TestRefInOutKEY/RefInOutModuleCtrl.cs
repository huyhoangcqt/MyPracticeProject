using System;
using UnityEngine;

public class RefInOutModuleCtrl : TestSingleton<RefInOutModuleCtrl>{
    SampleData data;
    SampleData data2;

    public void InitDataTest(){
        data =  new SampleData();
        data2 = new SampleData();
    }

    public void RemoveAllData(){
        data = null;
        data2 = null;
    }

    public SampleData SumData(SampleData data, SampleData add_data){
        SampleData result = new SampleData();
        result.id = Int16.Parse(data.id.ToString() + add_data.id.ToString());
        result.name += add_data.name;
        result.height = (data.height + add_data.height) / 2;
        result.height_ratio = (data.height_ratio + add_data.height_ratio) / 2f;
        return result;
    }

    public void SumSampleDataRef(ref SampleData data, SampleData add_data){
        Debug.Log("OriginData: ");
        Debug.Log("Data1: " + JsonUtility.ToJson(data));
        Debug.Log("Data2: " + JsonUtility.ToJson(add_data));
        data = SumData(data, add_data);
        Debug.Log("FunctionResult: " + JsonUtility.ToJson(data));
    }

    public void SumSampleDataOut(out SampleData data, SampleData add_data){
        data = new SampleData();
        Debug.Log("OriginData: ");
        Debug.Log("Data1: " + JsonUtility.ToJson(data));
        Debug.Log("Data2: " + JsonUtility.ToJson(add_data));
        data = SumData(data, add_data);
        Debug.Log("FunctionResult: " + JsonUtility.ToJson(data));
    }

    public void SumSampleDataIn(in SampleData data, SampleData add_data){
        Debug.Log("OriginData: ");
        Debug.Log("Data1: " + JsonUtility.ToJson(data));
        Debug.Log("Data2: " + JsonUtility.ToJson(add_data));
        data.id = Int16.Parse(data.id.ToString() + add_data.id.ToString());
        // data = new SampleData();
        // data = SumData(data, add_data);
        Debug.Log("FunctionResult: " + JsonUtility.ToJson(data));
    }

    public void Handle(){
        InitDataTest();
        SumSampleDataRef(ref data, data2);
        Debug.Log("ResultData: ");
        Debug.Log("Data1: " + JsonUtility.ToJson(data));
        Debug.Log("Data2: " + JsonUtility.ToJson(data2));

        RemoveAllData();
        data2 = new SampleData();
        SumSampleDataOut(out data, data2);
        Debug.Log("ResultData: ");
        Debug.Log("Data1: " + JsonUtility.ToJson(data));
        Debug.Log("Data2: " + JsonUtility.ToJson(data2));

        RemoveAllData();
        data = new SampleData();
        data2 = new SampleData();
        SumSampleDataIn(in data, data2);
        Debug.Log("ResultData: ");
        Debug.Log("Data1: " + JsonUtility.ToJson(data));
        Debug.Log("Data2: " + JsonUtility.ToJson(data2));
    }
}