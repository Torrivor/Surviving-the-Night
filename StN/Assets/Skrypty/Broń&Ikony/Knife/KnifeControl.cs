using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeControl : Weapons
{
    
    protected override void Start()
    {
        base.Start();
    }


    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedKnife = Instantiate(prefab);
        spawnedKnife.transform.position = transform.position; //Ustawianie pozycji spawn knife 
        spawnedKnife.GetComponent<KnifeZachowanie>().KierunekChecker(SM.lastVector); //Ustawianie pozycji knife po ruchu
    }
}
