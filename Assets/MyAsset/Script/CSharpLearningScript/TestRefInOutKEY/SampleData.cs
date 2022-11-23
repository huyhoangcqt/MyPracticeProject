using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleData
{
    private string[] NAME_LIB = {"Nguyen", "Van", "Huy", "Anh", "Ngoc", "Thi", "Huyen", "Bich", "Nam"};
    private int count;
    private static int ID = 1;
    public int id;
    public string name;
    public float height_ratio;
    public int height;

    public SampleData(){
        count = NAME_LIB.Length;
        id = ID;
        ID++;
        UnityEngine.Random random = new UnityEngine.Random();
        name = NAME_LIB[UnityEngine.Random.Range(0, count-1)];
        height_ratio = Random.Range(0.0f, 1.0f);
        height = Random.Range(150, 185);
    }
}
