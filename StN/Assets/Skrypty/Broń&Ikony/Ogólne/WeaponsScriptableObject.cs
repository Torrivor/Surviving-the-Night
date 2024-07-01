using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="WeaponScriptableObjesc", menuName ="ScriptableObjects/Weapons")]
public class WeaponsScriptableObject : ScriptableObject
{
    [SerializeField]
    GameObject prefab;
    public GameObject Prefab { get => prefab; private set => prefab = value; }
    
    //podstawowe staty dla broni
    [SerializeField]
    float damage;
    public float Damage { get => damage; private set => damage = value; }
    
    [SerializeField]
    float speed;
    public float Speed { get => speed; private set => speed = value; }
    
    [SerializeField]
    float cooldownofweapon;
    public float Cooldownofweapon { get => cooldownofweapon; private set => cooldownofweapon = value; }
    
    [SerializeField]
    int wytrzymalosc;
    public int Wytrzymalosc { get => wytrzymalosc; private set => wytrzymalosc = value; }

    [SerializeField]
    int level;  //nie powinno byc zmieniane w grze (tylok w edytorze)
    public int Level { get => level; private set => level = value; }

    [SerializeField]
    GameObject nextLevelPrefab;  //prefab nastepnego poziomu- czym sie obiekt staje po lvlupie          nie mylic z prefabem do spawnowania w kolejnym lvl
    public GameObject NextLevelPrefab { get => nextLevelPrefab; private set => nextLevelPrefab = value; }
}
