using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Fale
    {
        public string NazwaFali;
        public List<GrupaEnemy> enemyGroups;
        public int FalaQuota;  //ilo�� przeciwnik�w co si� spawni�
        public float spawnIntereval; //czas pomi�dzy spawnami
        public int spawnCount; //ilo�� przeciwnik�w aktualnie na mapie
    }

    [System.Serializable]
    public class GrupaEnemy
    {
        public string EnemyName;
        public int EnemyCount;
        public int SpawnCount;
        public GameObject enemyPrefabs;
    }

    public List<Fale> waves; //Lista wszystkich fal gier
    public int obecnaFala;

    Transform Skeleton;

    void Start()
    {
        Skeleton = FindObjectOfType<SkeletonMovement>().transform;
        CalculateWaveQuota();
        SpawnEnemies();
    }

   
    void Update()
    {
        
    }

    void CalculateWaveQuota()
    {
        int obecnaFala = 0;
        foreach (var GrupaEnemy in waves[obecnaFala].enemyGroups)
        {
            obecnaFala += GrupaEnemy.EnemyCount;
        }

        waves[obecnaFala].FalaQuota = obecnaFala;
        Debug.LogWarning(obecnaFala);
    }
    void SpawnEnemies()
    {
        //Sprawdzanie czy minimalna ilo�� przeciwnik�w w fali si� zespawni�o
        if (waves[obecnaFala].spawnCount < waves[obecnaFala].FalaQuota)
        {
            //Spawn ka�dy typ przeciwnika dop�ki quota jest pe�na
            foreach (var GrupaEnemy in waves[obecnaFala].enemyGroups)
            {
                //Czy minimum przeciwnik�w si� zespawni�o?
                if (GrupaEnemy.SpawnCount < GrupaEnemy.EnemyCount)
                {
                    Vector2 SpawnPosition = new Vector2(Skeleton.transform.position.x + Random.Range(-10f, 10f), Skeleton.transform.position.y + Random.Range(-10f, 10f));
                    Instantiate(GrupaEnemy.enemyPrefabs, SpawnPosition, Quaternion.identity);

                    GrupaEnemy.SpawnCount++;
                    waves[obecnaFala].spawnCount++;
                }
            }
        }
    }
}
