using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    //Skrypt dla wszystkich broni
    [Header("Statystyki Broni")]
    public WeaponsScriptableObject weaponData;
    float currentCooldown;

    protected SkeletonMovement SM;

    protected virtual void Start()
    {
        SM = FindObjectOfType<SkeletonMovement>();
        currentCooldown = weaponData.Cooldownofweapon; 
    }

   
    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if(currentCooldown <= 0f) //Kiedy cooldown zostanie 0, broñ atakuje
        {
            Attack();
        }
    }
    protected virtual void Attack()
    {
        currentCooldown = weaponData.Cooldownofweapon;
    }
}
