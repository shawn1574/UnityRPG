using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollisionTest : MonoBehaviour
{

    int _mask;
    Stat playerStat;
    PlayerController playerController;
    private void Start()
    {
        _mask = (1 << (int)Define.Layer.Monster);
        playerStat = gameObject.GetComponentInParent<Stat>();
        playerController = gameObject.GetComponentInParent<PlayerController>();
    }

    

    private void OnTriggerEnter(Collider other)
    {
        GameObject HittedMonster = other.gameObject;
        Stat MonsterStat = HittedMonster.GetComponent<Stat>();
        if (MonsterStat == null)
            return;

        if(playerController.State==Define.State.Skill)
            MonsterStat.OnAttacked(playerStat);

        if (HittedMonster.layer== (int)Define.Layer.Monster)
            Debug.Log($"sword trigger: {HittedMonster.name}");
        

         
    }
}
