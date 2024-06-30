using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponBehaviour : MonoBehaviour
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
        // Sprawd�, czy kolizja jest z wrogiem
        if (col.CompareTag("Enemy"))
        {
            // Pobierz komponent EnemyStats z obiektu wroga
            EnemyStats enemy = col.GetComponent<EnemyStats>();

            // Sprawd�, czy uda�o si� pobra� komponent EnemyStats
            if (enemy != null)
            {
                // Zadaj obra�enia wrogowi
                enemy.TakeDamage(currentDamage);

                // Debugowanie - sprawd�, czy obra�enia s� zadawane
                Debug.Log("Obra�enia zadane wrogowi: " + currentDamage);
            }
            else
            {
                Debug.LogWarning("Obiekt wroga nie posiada komponentu EnemyStats.");
            }
        }
    }
}
