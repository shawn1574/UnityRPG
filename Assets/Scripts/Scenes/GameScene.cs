using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene :BaseScene
{
   


    protected  override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        //temp
        //Manager.UI.ShowSceneUI<UI_Inven>();
        Dictionary<int, Data.Stat> dict= Manager.Data.StatDict;
        gameObject.GetOrAddComponent<CursorController>();

        GameObject player=  Manager.Game.Spawn(Define.WorldObject.Player, "UnityChan");
        Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);
        // GameObject monster= Manager.Game.Spawn(Define.WorldObject.Monster, "Knight");
        GameObject go = new GameObject { name = "SpawningPool" };
        SpawningPool pool=go.GetOrAddComponent<SpawningPool>();
        pool.SetKeepMonsterCount(5);

            

       
    }


    public override void Clear()
    {


    }


   

    
  
}
