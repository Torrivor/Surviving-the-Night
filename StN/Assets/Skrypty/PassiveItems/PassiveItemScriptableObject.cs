using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PassiveItemScriptableObject", menuName ="ScriptableObjects/Passive Item")]
public class PassiveItemScriptableObject : ScriptableObject
{
    [SerializeField]
    float multipler;
    public float Multipler { get => multipler; private set => multipler = value; }

    [SerializeField]
    int level;  //nie powinno byc zmieniane w grze (tylok w edytorze)
    public int Level { get => level; private set => level = value; }

    [SerializeField]
    GameObject nextLevelPrefab;  //prefab nastepnego poziomu- czym sie obiekt staje po lvlupie          nie mylic z prefabem do spawnowania w kolejnym lvl
    public GameObject NextLevelPrefab { get => nextLevelPrefab; private set => nextLevelPrefab = value; }

    [SerializeField]
    Sprite icon;  //nie modyfikowac w grze tylko w editorze
    public Sprite Icon { get => icon; private set => icon = value; }
}
