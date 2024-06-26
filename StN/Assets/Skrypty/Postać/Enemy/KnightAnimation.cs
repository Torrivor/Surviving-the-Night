using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAnimation : MonoBehaviour
{

    //Odnoœniki
    Animator A;
    EnemyMovement EM;
    SpriteRenderer SR;

    void Start()
    {
        A = GetComponent<Animator>();
        EM = GetComponent<EnemyMovement>();
        SR = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        /* //Animacja chodzenia postaci
         if (EM.wstrone.x != 0 || EM.wstrone.y != 0)
         {
             A.SetBool("Move", true);
         }
         else
         {
             A.SetBool("Move", false);
         }
        */
    }
}

