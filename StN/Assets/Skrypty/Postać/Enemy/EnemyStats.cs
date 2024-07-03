using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;

    //Obecne staty
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentHP;
    [HideInInspector]
    public float currentDamage;

    //I-Frames
    [Header("I-Frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;

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

        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        else if (isInvincible)
        {
            isInvincible = false;
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
        if (!isInvincible)
        {
            currentHP -= dmg;

            invincibilityTimer = invincibilityDuration;
            isInvincible = true;

            if (currentHP <= 0)
            {
                Kill();
            }
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
        if(col.gameObject.CompareTag("Gracz"))
        {
            PlayerStats player = col.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage);
        }
    }

    void ReturnEnemy()
    {
        EnemySpawner ES = FindObjectOfType<EnemySpawner>();
        transform.position = Skeleton.position + ES.relativeSpawnPoints[Random.Range(0, ES.relativeSpawnPoints.Count)].position;
    }
}
