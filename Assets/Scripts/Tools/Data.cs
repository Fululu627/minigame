using System.Collections;
using System.Collections.Generic;
using QxFramework.Utilities;
using UnityEngine;

public class Data : Singleton<Data>
{
    public override void Initialize()
    {
        base.Initialize();

    }

    public float NormalSpeed = 1.2f;
    public float HighSpeed = 1.5f;
    public float LowSpeed = 1.05f;

    /// <summary>
    /// 添加CSV表.
    /// </summary>
    public void LoadTable()
    {
        string beltGenerateText = Resources.Load<TextAsset>("Text/BeltGenerate").text;
        string propText = Resources.Load<TextAsset>("Text/Prop").text;
        Debug.Log(beltGenerateText);
        List<string> tableList = new List<string>();
        tableList.Add(beltGenerateText);
        tableList.Add(propText);
        TableAgent.Instance.Add(tableList);
    }
}
