using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    
    #region Pool
    class Pool
    {
       
        public GameObject Original { get; private set; }
        public Transform Root { get; set; }

        Stack<Poolable> _poolStack = new Stack<Poolable>();
        public void Init(GameObject original,int count=5)
        {
            Original = original;
            Root = new GameObject().transform;
            Root.name = $"{original.name}_Root";

            for(int i=0;i<count;i++)
            {
                Push(Create());
                
            }
        }

        Poolable Create()
        {
            GameObject go = Object.Instantiate<GameObject>(Original);
            go.name = Original.name;
            return go.GetOrAddComponent<Poolable>();
        }

        public void Push(Poolable poolable)
        {
            if (poolable == null)
                return;
            poolable.transform.parent = Root;//정리하기 위해 empty object 산하로 넣는다 ex)unity_chan_root>unity_chan*5
            poolable.gameObject.SetActive(false);
            poolable.IsUsing = false;

            _poolStack.Push(poolable);
        }

        public Poolable Pop( Transform parent = null)
        {
            Poolable poolable;
            if (_poolStack.Count > 0)
                poolable = _poolStack.Pop();
            else
                poolable = Create();

            poolable.gameObject.SetActive(true);
            //dontdestroyonload 해제 용도
            if (parent == null)
                poolable.transform.parent = Manager.Scene.CurrentScene.transform;
            poolable.transform.parent = parent;
            poolable.IsUsing = true;


            return poolable;
        }
    }
    #endregion
    Dictionary<string, Pool> _pool = new Dictionary<string, Pool>(); //name을 key로 사용

    Transform _root;
    public void Init()
    {
        if(_root==null)
        {
            _root = new GameObject { name = "@Pool_Root" }.transform;
            Object.DontDestroyOnLoad(_root);
        }
    }

    public void CreatePool(GameObject original,int count=5)
    {
        Pool pool = new Pool();
        pool.Init(original, count);
        pool.Root.parent = _root;
          
        _pool.Add(original.name, pool);
    }

    public void Push(Poolable poolable)
    {
        string name = poolable.gameObject.name;
        if(_pool.ContainsKey(name)==false)
        {
            GameObject.Destroy(poolable.gameObject);
            return;
        }
        _pool[name].Push(poolable);
    }



    public Poolable Pop(GameObject original,Transform parent=null)
    {
        if (_pool.ContainsKey(original.name) == false)
            CreatePool(original);

        return _pool[original.name].Pop(parent);
    }

    public GameObject GetOriginal(string name)
    {
        if (_pool.ContainsKey(name) == false)
            return null;

            return _pool[name].Original;
    }

    public void Clear()
    {
        foreach (Transform child in _root)
            GameObject.Destroy(child.gameObject);

        _pool.Clear();
    }
}
