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

    [Header("Atrybuty Spawnera")]
    float SpawnTimer;
    public float waveInterval; //okres pomi�dzy falami
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

        if (obecnaFala < waves.Count && waves[obecnaFala].spawnCount == 0) //Sprawdzanie czy fala si� zako�czy�a i  czy nast�pna ma si� rozpocz��
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
        //okres pomi�dzy falami
        yield return new WaitForSeconds(waveInterval);

        //je�eli jest wi�cej fal po obecnej fali, przejd� do nast�pnej fali
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

        //Sprawdzanie czy minimalna ilo�� przeciwnik�w w fali si� zespawni�o
        if (maxenenmiesReached)
        {
            return;
        }

        bool enemiesSpawnedThisCycle = false;
        //Spawn ka�dy typ przeciwnika dop�ki quota jest pe�na
        foreach (var GrupaEnemy in waves[obecnaFala].enemyGroups)
        {
            //Czy minimum przeciwnik�w si� zespawni�o?
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
