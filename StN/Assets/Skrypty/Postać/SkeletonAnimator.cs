using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimator : MonoBehaviour
{

    //Odnoœniki
    Animator A;
    SkeletonMovement SK;
    SpriteRenderer SR;

    Transform attackPoint;
    Vector3 attackPointRightPosition;
    Vector3 attackPointLeftPosition;

    void Start()
    {
        A = GetComponent<Animator>();
        SK = GetComponent<SkeletonMovement>();
        SR = GetComponent<SpriteRenderer>();

        attackPoint = transform.Find("Attack_Point");
        attackPointRightPosition = attackPoint.localPosition;
        attackPointLeftPosition = new Vector3(-attackPointRightPosition.x, attackPointRightPosition.y, attackPointRightPosition.z);
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
            attackPoint.localPosition = attackPointLeftPosition;
        }
        else
        {
            SR.flipX = false;
            this.GetComponentInChildren<CircleCollider2D>().offset = new Vector2(-0.0700f, 0.019052f);
            attackPoint.localPosition = attackPointRightPosition;
        }

    }
    public void SetAnimatorController(RuntimeAnimatorController c)
    {
        if (!A) A = GetComponent<Animator>();
        A.runtimeAnimatorController = c;
    }
}
