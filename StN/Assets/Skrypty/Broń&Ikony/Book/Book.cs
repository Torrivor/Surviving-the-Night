using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public float rotateSpeed;
    public WeaponsScriptableObject weaponData;

    //Ustaw w prefabie broni pociskowej
    protected Vector3 kierunki;
    public float ZniszczPoSekundzie;

    public Transform Skeleton;

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
        Destroy(gameObject, ZniszczPoSekundzie);
    }


    void Update()
    {
        Skeleton.rotation = Quaternion.Euler(0f, 0f, Skeleton.rotation.eulerAngles.z + (rotateSpeed + Time.deltaTime));
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        //gdy kolizja zadaj obrazenia
        if (col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);
            ZmniejszWytrzymalosc();
        }
        else if (col.CompareTag("Prop"))
        {
            if (col.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage(currentDamage);
                ZmniejszWytrzymalosc();
            }
        }
    }

    void ZmniejszWytrzymalosc() //niszczy pocisk gdy wytrzymalosc spada do 0
    {
        currentWytrzymalocs--;
        if (currentWytrzymalocs <= 0)
        {
            Destroy(gameObject);
        }
    }
}
