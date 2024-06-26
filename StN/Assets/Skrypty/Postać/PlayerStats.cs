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

    void Awake()
    {
        //przypisane wartosci
        currentHP = characterData.MaxHP;
        currentRecovery = characterData.Recovery;
        currentMoveSpeed = characterData.MoveSpeed;
        currentMight = characterData.Might;
        currentProjectileSpeed = characterData.ProjectileSpeed;
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
}
