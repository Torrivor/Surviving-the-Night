using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeZachowanie : ZachowaniePociskow
{
    protected override void Start()
    {
        base.Start();
    }

    
    void Update()
    {
        transform.position += kierunki * weaponData.speed * Time.deltaTime;  //Ustawianie ruchu knife
    }
}
