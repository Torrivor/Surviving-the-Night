using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public CharacterScriptableObject characterData;

    //obecne staty
    float currentHP;
    float currentRecovery;
    float currentMoveSpeed;
    float currentMight;
    float currentProjectileSpeed;
    float currentMagnet;


    #region Current Stats Properties
    public float CurrentHP
    {
        get { return currentHP; }
        set
        {
            //sprawdza czy wartosc sie zmienila
            if (currentHP != value)
            {
                currentHP = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentHPDisplay.text = "Hp: " + currentHP;
                }

            }
        }
    }

    public float CurrentRecovery
    {
        get { return currentRecovery; }
        set
        {
            //sprawdza czy wartosc sie zmienila
            if (currentRecovery != value)
            {
                currentRecovery = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentRecoveryDisplay.text = "Recovery: " + currentRecovery;
                }
            }
        }
    }
    public float CurrentMoveSpeed
    {
        get { return currentMoveSpeed; }
        set
        {
            //sprawdza czy wartosc sie zmienila
            if (currentMoveSpeed != value)
            {
                currentMoveSpeed = value;
                //Debug.Log("zmiana wartosci");
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentMoveSpeedDisplay.text = "Move Speed: " + currentMoveSpeed;
                }
            }
        }
    }
    public float CurrentMight
    {
        get { return currentMight; }
        set
        {
            //sprawdza czy wartosc sie zmienila
            if (currentMight != value)
            {
                currentMight = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentMightDisplay.text = "Might: " + currentMight;
                }
            }
        }
    }
    public float CurrentProjectileSpeed
    {
        get { return currentProjectileSpeed; }
        set
        {
            //sprawdza czy wartosc sie zmienila
            if (currentProjectileSpeed != value)
            {
                currentProjectileSpeed = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentProjectileSpeedDisplay.text = "Projectile Speed: " + currentProjectileSpeed;
                }
            }
        }
    }
    public float CurrentMagnet
    {
        get { return currentMagnet; }
        set
        {
            //sprawdza czy wartosc sie zmienila
            if (currentMagnet != value)
            {
                currentMagnet = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentMagnetDisplay.text = "Magnet: " + currentMagnet;
                }
            }
        }
    }
    #endregion

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


    public GameObject secondWeaponTest;
    public GameObject firstPassiveItemTest, secondPassiveItemTest;

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
        CurrentHP = characterData.MaxHP;
        CurrentRecovery = characterData.Recovery;
        CurrentMoveSpeed = characterData.MoveSpeed;
        CurrentMight = characterData.Might;
        CurrentProjectileSpeed = characterData.ProjectileSpeed;
        CurrentMagnet = characterData.Magnet;

        //Spawn broni
        SpawnWeapon(characterData.StartingWeapon);
        //SpawnWeapon(secondWeaponTest);
        //SpawnPassiveItem(firstPassiveItemTest);
        //SpawnPassiveItem(secondPassiveItemTest);

        playerAnimator = GetComponent<SkeletonAnimator>();
        if (characterData.controller)
        {
            
            playerAnimator.SetAnimatorController(characterData.controller);
        }
        
    }

    void Start()
    {
        experienceCap = levelRanges[0].experienceCapIncrease;

        //ustawia obecny display statow
        GameManager.instance.currentHPDisplay.text = "Hp: " + currentHP;
        GameManager.instance.currentRecoveryDisplay.text = "Recovery: " + currentRecovery;
        GameManager.instance.currentMoveSpeedDisplay.text = "Move Speed: " + currentMoveSpeed;
        GameManager.instance.currentMightDisplay.text = "Might: " + currentMight;
        GameManager.instance.currentProjectileSpeedDisplay.text = "Projectile Speed: " + currentProjectileSpeed;
        GameManager.instance.currentMagnetDisplay.text = "Magnet: " + currentMagnet;

        GameManager.instance.AssignChosenCharacterUI(characterData);
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

            GameManager.instance.StartLevelUp();
        }
    }

    public void TakeDamage(float dmg)
    {
        //jesli mozna bic - zadaj bmg i rozpocznij invi
        if(!isInvincible)
        {
            CurrentHP -= dmg;

            invincibilityTimer = invincibilityDuration;
            isInvincible = true;

            if (CurrentHP <= 0)
            {
                Kill();
            }
        }
       
    }

    public void Kill()
    {
        //Debug.Log("Player is Dead");
        if(!GameManager.instance.isKoniec_Gry)
        {
            GameManager.instance.Koniec_Gry();
            GameManager.instance.AssignLevelReachedUI(level);
            GameManager.instance.AssignChosenWeaponAndPassiveItemUI(inventory.weaponUISlots, inventory.passiveItemUISlots);
        }
    }

    public void RestoreHealth(float amount)
    {
        //leczy jesli hp mniej niz 100%
        if (CurrentHP < characterData.MaxHP)
        {
            CurrentHP += amount;

            //upewnienie ze hp nie jest powyzej 100%
            if(CurrentHP > characterData.MaxHP)
            {
                CurrentHP = characterData.MaxHP;
            }
        }
    }

    //pasywna regeneneracja zdrowia
    void Recover()
    {
        if(CurrentHP < characterData.MaxHP)
        {
            CurrentHP += CurrentRecovery * Time.deltaTime;

            //upewnai sie czy hp nie przekracza 100%
            if(CurrentHP > characterData.MaxHP )
            {
                CurrentHP = characterData.MaxHP;
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

        //respi poczatkowa bron
        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform);
        inventory.AddWeapon(weaponIndex, spawnedWeapon.GetComponent<Weapons>());  //dodaje bron do iventory slota

        weaponIndex++;
    }

    public void SpawnPassiveItem(GameObject passiveItem)
    {
        //sprawdza czy eq iest pelne
        if (passiveItemIndex >= inventory.passiveItemsSlots.Count - 1)
        {
            Debug.LogError("Inventory zape³nione");
            return;
        }


        GameObject spawnedPassiveItem = Instantiate(passiveItem, transform.position, Quaternion.identity);
        spawnedPassiveItem.transform.SetParent(transform);
        inventory.AddPassiveItem(passiveItemIndex, spawnedPassiveItem.GetComponent<PassiveItem>());  //dodaje passive do iventory slota

        passiveItemIndex++;
    }
}
