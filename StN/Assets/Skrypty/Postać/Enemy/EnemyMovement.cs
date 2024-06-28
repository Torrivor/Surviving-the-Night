using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    EnemyStats enemy;
    public GameObject Skeleton;
    private SpriteRenderer spriteRenderer;
    private Vector3 initialPosition;
    public bool facingRight = false;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialPosition = transform.position;
        enemy = GetComponent<EnemyStats>();
        Skeleton = FindObjectOfType<SkeletonMovement>().gameObject;  //Proces szkieleta/zamieniæ na wszystkich graczy
    }


    void Update()
    {
        Vector3 playerPosition = FindObjectOfType<SkeletonMovement>().transform.position;
        transform.position = Vector2.MoveTowards(transform.position, playerPosition, enemy.currentMoveSpeed * Time.deltaTime);

        if(Skeleton.transform.position.x < gameObject.transform.position.x && facingRight)
        {
            FlipPos();
        }
        if (Skeleton.transform.position.x > gameObject.transform.position.x && !facingRight)
        {
            FlipPos();
        }
           
    }

    void FlipPos()
    {
        facingRight = !facingRight;
        Vector3 tmpScale = gameObject.transform.localScale;
        tmpScale.x *= -1;
        gameObject.transform.localScale = tmpScale;
    }
}
