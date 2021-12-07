using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager
{
    int _order = 10;

    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    UI_Scene _sceneUI = null;

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
            {
                root = new GameObject { name = "@UI_Root" };

            }
            return root;
        }
    }

    public void SetCanvas(GameObject go,bool sort=true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay; 
        //이 렌더링 모드는 씬에 렌더링 된 UI 요소를 화면에 배치합니다. 화면 크기와 해상도가 변경된 경우, Canvas는 스크린에 일치하도록 자동으로 크기를 변경합니다.
        canvas.overrideSorting = true;
        //캔버스 중첩되어있을경우 나만의 sortingorder를 가지게함

        if(sort)
        {
            canvas.sortingOrder = _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    public T MakeWorldSpaceUI<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Manager.Resource.Instantiate($"UI/WorldSpace/{name}");

        if (parent != null)
            go.transform.SetParent(parent);

        Canvas canvas = go.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;

        return Util.GetOrAddComponent<T>(go);
    }

    public T MakeSubItem<T>(Transform parent=null,string name=null)where T:UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Manager.Resource.Instantiate($"UI/SubItem/{name}");

        if (parent != null)
            go.transform.SetParent(parent);

        return Util.GetOrAddComponent<T>(go);
    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Manager.Resource.Instantiate($"UI/Scene/{name}");
        T sceneUI = Util.GetOrAddComponent<T>(go);
        _sceneUI = sceneUI;

       
        go.transform.SetParent(Root.transform);

        return sceneUI;
    }
    public T ShowPopupUI<T>(string name=null)where T:UI_Popup //name prefab때문에 T는 script와 연관
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go= Manager.Resource.Instantiate($"UI/Popup/{name}");
        T popup= Util.GetOrAddComponent<T>(go);
        _popupStack.Push(popup);

       
        go.transform.SetParent(Root.transform);

            return popup;
    }

    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0)
            return;

        if(_popupStack.Peek()!=popup)
        {
            Debug.Log("Close Popup Failed");
            return;
        }

        ClosePopupUI();

    }

    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0)
            return;

       UI_Popup popup= _popupStack.Pop();
       Manager.Resource.Destroy(popup.gameObject);
       popup = null;
       _order--;
    }

    public void CloseAllPopupUI()
    {
        while (_popupStack.Count > 0)
            ClosePopupUI();
    }

    public void Clear()
    {
        CloseAllPopupUI();
        _sceneUI = null;
    }
}
