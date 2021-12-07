using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager 
{
   public T Load<T>(string path)where T:Object 
    {
        if(typeof(T)==typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf('/');
            if (index >= 0)
                name = name.Substring(index + 1);

            GameObject go = Manager.Pool.GetOriginal(name);
            if (go != null)
                return go as T;

        }

        return Resources.Load<T>(path);
    }
   public GameObject Instantiate(string path, Transform parent=null)
    {
       
        GameObject original = Load<GameObject>($"Prefabs/{path}");//메모리에서 들고있음
        if(original == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }
        //2.혹시 풀링된 친구 있을까?
        if (original.GetComponent<Poolable>() != null)
            return Manager.Pool.Pop(original, parent).gameObject;

        GameObject go = Object.Instantiate(original, parent);//씬에 배치까지
        go.name = original.name;
        return go;
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        //만약에 풀링이 필요한 아이라면 ->풀링 매니저한테 위탁
        Poolable poolable = go.GetComponent<Poolable>();
        if(poolable!=null)
        {
            Manager.Pool.Push(poolable);
            return;
        }
        Object.Destroy(go);
    }
}
