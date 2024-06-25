using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    public GameObject Skeleton;
    private SpriteRenderer spriteRenderer;
    private Vector3 initialPosition;
    public bool facingRight = false;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialPosition = transform.position;
    }


    void Update()
    {
        Vector3 playerPosition = FindObjectOfType<SkeletonMovement>().transform.position;
        transform.position = Vector2.MoveTowards(transform.position, playerPosition, enemyData.MoveSpeed * Time.deltaTime);

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
