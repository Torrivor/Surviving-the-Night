using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookControl : MonoBehaviour
{
    public WeaponsScriptableObject weaponData;

    public float destroyAfterSeconds;

    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownofweapon;
    protected int currentWytrzymalocs;

    private void Awake()
    {
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCooldownofweapon = weaponData.Cooldownofweapon;
        currentWytrzymalocs = weaponData.Wytrzymalosc;
    }


    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }


    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        //gdy kolizja zadaj obrazenia
        if (col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);

        }
    }
}
