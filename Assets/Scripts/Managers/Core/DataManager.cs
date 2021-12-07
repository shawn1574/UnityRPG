using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//json []:�迭 {}:����ü
public interface ILoader<Key,Value>
{
    Dictionary<Key, Value> MakeDict();
}
public class DataManager
{
    public Dictionary<int, Data.Stat> StatDict { get; private set; } = new Dictionary<int, Data.Stat>();
    
    public void Init()
    {
        StatDict = LoadJson<Data.StatData, int, Data.Stat>("StatData").MakeDict();

       
    }
  //again
    Loader LoadJson<Loader,Key,Value>(string path)where Loader:ILoader<Key, Value>
    {
        TextAsset textAsset = Manager.Resource.Load<TextAsset>($"Data/{path}");

        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
