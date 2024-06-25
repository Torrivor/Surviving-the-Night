using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EnemyScriptableObject", menuName ="ScriptableObject/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    //Staty przeciwnikow
    [SerializeField]
    float moveSpeed;
    public float MoveSpeed { get => moveSpeed; private set =>moveSpeed = value; }
    [SerializeField]
    float maxHP;
    public float MaxHP { get => maxHP; private set => maxHP = value; }
    [SerializeField]
    float damage;
    public float Damage { get => damage; private set => damage = value; }

}
