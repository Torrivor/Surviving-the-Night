using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public CharacterScriptableObject characterData;

    //obecne staty
    //[HideInInspector]
    public float currentHP;
    //[HideInInspector]
    public float currentRecovery;
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentMight;
    [HideInInspector]
    public float currentProjectileSpeed;
    //[HideInInspector]
    public float currentMagnet;



    //exp i lvl gracza
    [Header("Experience/Level")]
    public int experience = 0;  //exp ktore masz
    public int level = 1;   //obecny lvl
    public int experienceCap;   //max exp dla danego poziomu

    //klasa do definiowania przedzialow leveli i wzrostu potrzebnego expa dla przedzialu
    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }

    //I-Frames
    [Header("I-Frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;

    public List<LevelRange> levelRanges;

    InventoryManager inventory;
    public int weaponIndex;
    public int passiveItemIndex;


    SkeletonAnimator playerAnimator;

    void Awake()
    {
        characterData = CharacterSelector.getData();
        /*if(CharacterSelector.instance)
        {
            CharacterSelector.instance.destroySingleton();
        }*/

        inventory = GetComponent<InventoryManager>();

        //przypisane wartosci
        currentHP = characterData.MaxHP;
        currentRecovery = characterData.Recovery;
        currentMoveSpeed = characterData.MoveSpeed;
        currentMight = characterData.Might;
        currentProjectileSpeed = characterData.ProjectileSpeed;
        currentMagnet = characterData.Magnet;

        //Spawn broni
        SpawnWeapon(characterData.StartingWeapon);

        playerAnimator = GetComponent<SkeletonAnimator>();
        if (characterData.controller)
        {
            
            playerAnimator.SetAnimatorController(characterData.controller);
        }
        
    }

    void Start()
    {
        experienceCap = levelRanges[0].experienceCapIncrease;
    }

    void Update()
    {
        if(invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        else if (isInvincible)
        {
            isInvincible = false;
        }
        Recover();
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;

        LevelUpChecker();
    }

    void LevelUpChecker()
    {
        if (experience >= experienceCap)
        {
            level++;
            experience -= experienceCap;

            int experienceCapIncrease = 0;
            foreach (LevelRange range in levelRanges)
            {
                if (level >= range.startLevel && level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }
            experienceCap += experienceCapIncrease;
        }
    }

    public void TakeDamage(float dmg)
    {
        //jesli mozna bic - zadaj bmg i rozpocznij invi
        if(!isInvincible)
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
        Debug.Log("Player is Dead");
    }

    public void RestoreHealth(float amount)
    {
        //leczy jesli hp mniej niz 100%
        if (currentHP < characterData.MaxHP)
        {
            currentHP += amount;

            //upewnienie ze hp nie jest powyzej 100%
            if(currentHP > characterData.MaxHP)
            {
                currentHP = characterData.MaxHP;
            }
        }
    }

    //pasywna regeneneracja zdrowia
    void Recover()
    {
        if(currentHP < characterData.MaxHP)
        {
            currentHP += currentRecovery * Time.deltaTime;

            //upewnai sie czy hp nie przekracza 100%
            if(currentHP > characterData.MaxHP )
            {
                currentHP = characterData.MaxHP;
            }
        }
    }

    public void SpawnWeapon(GameObject weapon)
    {
        //sprawdza czy eq iest pelne
        if(weaponIndex >= inventory.weaponSlots.Count - 1)
        {
            Debug.LogError("Inventory zape³nione");
            return;
        }


        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform);
        inventory.AddWeapon(weaponIndex, spawnedWeapon.GetComponent<Weapons>());  //dodaje bron do iventory slota

        weaponIndex++;
    }
}
