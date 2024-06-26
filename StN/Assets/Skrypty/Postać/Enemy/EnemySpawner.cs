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
        public int FalaQuota;  //iloœæ przeciwników co siê spawni¹
        public float spawnIntereval; //czas pomiêdzy spawnami
        public int spawnCount; //iloœæ przeciwników aktualnie na mapie
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

    [Header("Atrybuty Spawnera")]
    float SpawnTimer;
    public float waveInterval; //okres pomiêdzy falami
    public int enemiesAlive;
    public int maxEnemies;
    public bool maxenenmiesReached = false; //czy jest max?

    [Header("Spawner Positions")]
    public List<Transform> relativeSpawnPoints;

    Transform Skeleton;

    void Start()
    {
        Skeleton = FindObjectOfType<SkeletonMovement>().transform; 
        CalculateWaveQuota();
        PierwszaFala();
    }


    void Update()
    {

        if (obecnaFala < waves.Count && waves[obecnaFala].spawnCount == 0) //Sprawdzanie czy fala siê zakoñczy³a i  czy nastêpna ma siê rozpocz¹æ
        {
            StartCoroutine(NastepnaFala());
        }

        SpawnTimer += Time.deltaTime;

        if (SpawnTimer >= waves[obecnaFala].spawnIntereval)
        {
            SpawnTimer = 0f;
            SpawnEnemies();
        }
    }
    IEnumerator NastepnaFala()
    {
        //okres pomiêdzy falami
        yield return new WaitForSeconds(waveInterval);

        //je¿eli jest wiêcej fal po obecnej fali, przejdŸ do nastêpnej fali
        if (obecnaFala < waves.Count - 1)
        {
            obecnaFala++;
            CalculateWaveQuota();
        }
    }

    void CalculateWaveQuota()
    {
        int totalQuota = 0;
        foreach (var GrupaEnemy in waves[obecnaFala].enemyGroups)
        {
            totalQuota += GrupaEnemy.EnemyCount;
        }

        waves[obecnaFala].FalaQuota = totalQuota;
        Debug.LogWarning(totalQuota);
    }
    void SpawnEnemies()
    {

        //Sprawdzanie czy minimalna iloœæ przeciwników w fali siê zespawni³o
        if (maxenenmiesReached)
        {
            return;
        }

        bool enemiesSpawnedThisCycle = false;
        //Spawn ka¿dy typ przeciwnika dopóki quota jest pe³na
        foreach (var GrupaEnemy in waves[obecnaFala].enemyGroups)
        {
            //Czy minimum przeciwników siê zespawni³o?
            while (GrupaEnemy.SpawnCount < GrupaEnemy.EnemyCount && waves[obecnaFala].spawnCount < waves[obecnaFala].FalaQuota && !maxenenmiesReached)
            {
                Instantiate(GrupaEnemy.enemyPrefabs, Skeleton.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position, Quaternion.identity);
                GrupaEnemy.SpawnCount++;
                waves[obecnaFala].spawnCount++;
                enemiesAlive++;

                if (enemiesAlive >= maxEnemies)
                {
                    maxenenmiesReached = true;
                    return;
                }
            }
            if (maxenenmiesReached)
            {
                break;
            }
            if (!enemiesSpawnedThisCycle)
            {
                maxenenmiesReached = false;
            }
        }
    }


    public void OnEnemyKilled()
    {
        enemiesAlive--;
    }

    void PierwszaFala()
    {
        if (obecnaFala < waves.Count && waves[obecnaFala].spawnCount == 0)
        {
            StartCoroutine(NastepnaFala());
        }
        SpawnTimer = 0f;
        SpawnEnemies();
    }
}
