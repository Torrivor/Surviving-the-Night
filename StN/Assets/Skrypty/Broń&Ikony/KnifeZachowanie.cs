using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeZachowanie : ZachowaniePociskow
{
    KnifeControl KC;
    protected override void Start()
    {
        base.Start();
        KC = FindObjectOfType<KnifeControl>();
    }

    
    void Update()
    {
        transform.position += kierunki * KC.speed * Time.deltaTime;  //Ustawianie ruchu knife
    }
}
