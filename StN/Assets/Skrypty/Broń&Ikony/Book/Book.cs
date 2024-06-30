using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public float rotateSpeed;
    public WeaponsScriptableObject weaponData;

    public Transform Skeleton;



    void Update()
    {
        Skeleton.rotation = Quaternion.Euler(0f, 0f, Skeleton.rotation.eulerAngles.z + (rotateSpeed + Time.deltaTime));
    }

}
