using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;

    //Obecne staty
    float currentMoveSpeed;
    float currentHP;
    float currentDamage;

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

}
