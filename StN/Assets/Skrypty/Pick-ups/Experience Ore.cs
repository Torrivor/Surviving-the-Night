using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceOre : MonoBehaviour, ICollectible
{
    public int experienceGranted;
    public void Collect()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.IncreaseExperience(experienceGranted);
        Destroy(gameObject);
    }
}