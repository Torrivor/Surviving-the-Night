using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endless : MonoBehaviour
{
    public List<GameObject> Chunki;
    public GameObject Skeleton;
    public float checkerRadius;
    Vector3 PozycjaTerenu;
    public LayerMask Maskaterenu;
    public GameObject currentChunk;
    SkeletonMovement SM;

    [Header("Optymalizacja")]
    public List<GameObject> spawnedChunks;
    GameObject ostatniChunk;
    public float Max;
    float opDist;
    float optimizercooldown;
    public float czascooldown;
    void Start()
    {
        SM = FindObjectOfType<SkeletonMovement>();
    }

   
    void Update()
    {
        ChunkCheck();
        Optimizer();
    }

    void ChunkCheck()
    {
        if(!currentChunk)
        {
            return;
        }
        //Proces generowania mapy
        if(SM.wstrone.x > 0 && SM.wstrone.y == 0)    //prawa strona
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Prawo").position, checkerRadius, Maskaterenu))
            {
                PozycjaTerenu = currentChunk.transform.Find("Prawo").position;
                ChunkSpawn();
            }
        }
        else if (SM.wstrone.x < 0 && SM.wstrone.y == 0) //lewa strona
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Lewo").position, checkerRadius, Maskaterenu))
            {
                PozycjaTerenu = currentChunk.transform.Find("Lewo").position;
                ChunkSpawn();
            }
        }
        else if (SM.wstrone.x == 0 && SM.wstrone.y > 0) // góra
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Góra").position, checkerRadius, Maskaterenu))
            {
                PozycjaTerenu = currentChunk.transform.Find("Góra").position;
                ChunkSpawn();
            }
        }
        else if (SM.wstrone.x > 0 == SM.wstrone.y < 0)  //dó³
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Dó³").position, checkerRadius, Maskaterenu))
            {
                PozycjaTerenu = currentChunk.transform.Find("Dó³").position;
                ChunkSpawn();
            }
        }
        else if (SM.wstrone.x > 0 && SM.wstrone.y > 0) //prawo-góra
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("PG").position, checkerRadius, Maskaterenu))
            {
                PozycjaTerenu = currentChunk.transform.Find("PG").position;
                ChunkSpawn();
            }
        }
        if (SM.wstrone.x > 0 && SM.wstrone.y < 0)  //prawo-dó³
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("PD").position, checkerRadius, Maskaterenu))
            {
                PozycjaTerenu = currentChunk.transform.Find("PD").position;
                ChunkSpawn();
            }
        } 
        if (SM.wstrone.x < 0 && SM.wstrone.y > 0)  //lewo-góra
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("LG").position, checkerRadius, Maskaterenu))
            {
                PozycjaTerenu = currentChunk.transform.Find("LG").position;
                ChunkSpawn();
            }
        }
        if (SM.wstrone.x < 0 && SM.wstrone.y < 0)  //lewo-dó³
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("LD").position, checkerRadius, Maskaterenu))
            {
                PozycjaTerenu = currentChunk.transform.Find("LD").position;
                ChunkSpawn();
            }
        }
    }

    void ChunkSpawn()
    {
        int rand = Random.Range(0, Chunki.Count);
        ostatniChunk = Instantiate(Chunki[rand], PozycjaTerenu, Quaternion.identity);
        spawnedChunks.Add(ostatniChunk);
    }
    void Optimizer()
    {
        optimizercooldown -= Time.deltaTime;
        if (optimizercooldown <= 0f)
        {
            optimizercooldown = czascooldown;
        }
        else
        {
            return;
        }

        foreach (GameObject chunk in spawnedChunks)
        {
            opDist = Vector3.Distance(Skeleton.transform.position, chunk.transform.position);
            if(opDist > Max)
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }
        }
    }
}
