using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inven : UI_Scene
{
    enum GameObjects
    {
        GridPanel
    }
   
    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);

        Debug.Log(gridPanel.transform);
        foreach (Transform child in gridPanel.transform)
            Manager.Resource.Destroy(child.gameObject);

        

        for(int i=0;i<8;i++)
        {
            GameObject Item = Manager.UI.MakeSubItem<UI_Inven_Item>(parent:gridPanel.transform).gameObject;
            
            UI_Inven_Item invenItem = Item.GetOrAddComponent<UI_Inven_Item>();
            invenItem.SetInfo($"æ∆¿Ã≈€{i}");
        }

    }
    void Update()
    {
        
    }
}
