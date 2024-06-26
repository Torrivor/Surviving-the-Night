using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;

    //Obecne staty
    [HideInInspector]
    float currentMoveSpeed;
    [HideInInspector]
    float currentHP;
    [HideInInspector]
    float currentDamage;

    public float despawnDistance = 20f;
    Transform Skeleton;

    private void Start()
    {
        Skeleton = FindObjectOfType<PlayerStats>().transform;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, Skeleton.position) >= despawnDistance)
        {
            ReturnEnemy();
        }
    }
    void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHP = enemyData.MaxHP;
        currentDamage = enemyData.Damage;
    }

    public void TakeDamage(float dmg)
    {
        currentHP -= dmg;
        if (currentHP <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        EnemySpawner ES = FindObjectOfType<EnemySpawner>();
        ES.OnEnemyKilled();
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        //odniesienie do skryptu z kolizjami i dostaje dmg -TakeDamage()
        if(col.gameObject.CompareTag("Player"))
        {
            PlayerStats player = col.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentMoveSpeed);
        }
    }

    void ReturnEnemy()
    {
        EnemySpawner ES = FindObjectOfType<EnemySpawner>();
        transform.position = Skeleton.position + ES.relativeSpawnPoints[Random.Range(0, ES.relativeSpawnPoints.Count)].position;
    }
}
