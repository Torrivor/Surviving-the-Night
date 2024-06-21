using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimator : MonoBehaviour
{

    //Odnoœniki
    Animator A;
    SkeletonMovement SK;
    SpriteRenderer SR;
    
    void Start()
    {
        A = GetComponent<Animator>();
        SK = GetComponent<SkeletonMovement>();
        SR = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        //Animacja chodzenia postaci
        if (SK.wstrone.x != 0 || SK.wstrone.y != 0)
        {
            A.SetBool("Move", true);
            SkeletonGdziePatrzy();
        }
        else
        {
            A.SetBool("Move", false);
        }
    }
    void SkeletonGdziePatrzy()
        //Odwracanie modelu postaci podczas ruchu
    {
        if (SK.OstatniaPozycjaHoryzontalna < 0)
        {
            SR.flipX = true;
        }
        else
        {
            SR.flipX = false;
        }

    }
}
