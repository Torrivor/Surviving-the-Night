using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropRadnom : MonoBehaviour
{
    public List<GameObject> propspawn;
    public List<GameObject> propprefabs;


    void Start()
    {
        SpawnPoints();
    }

    
    void Update()
    {
        
    }

    void SpawnPoints()
    {
        foreach (GameObject sp in propspawn)
        {
            int rand = Random.Range(0, propprefabs.Count);
            GameObject prop = Instantiate(propprefabs[rand], sp.transform.position, Quaternion.identity);
            prop.transform.parent = sp.transform;
        }
    }
}
