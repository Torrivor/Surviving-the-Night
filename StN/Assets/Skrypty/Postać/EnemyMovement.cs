using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform Skeleton;
    public float Speed;
    void Start()
    {
        Skeleton = FindObjectOfType<SkeletonMovement>().transform;
    }

    
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Skeleton.transform.position, Speed * Time.deltaTime);
    }
}
