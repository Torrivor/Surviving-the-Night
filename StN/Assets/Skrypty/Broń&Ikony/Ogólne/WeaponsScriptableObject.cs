using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponsScriptableObject : ScriptableObject
{
    public GameObject prefab;
    //podstawowe staty dla broni
    public float dmg;
    public float speed;
    public float Cooldownofweapon;
    public int wytrzymalosc;
}
