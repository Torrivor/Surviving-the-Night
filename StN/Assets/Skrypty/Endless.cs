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
    SkeletonMovement SM;
    void Start()
    {
        SM = FindObjectOfType<SkeletonMovement>();
    }

   
    void Update()
    {
        ChunkCheck();
    }

    void ChunkCheck()
    {
        //Proces generowania mapy
        if(SM.wstrone.x > 0 && SM.wstrone.y == 0)    //prawa strona
        {
            if(!Physics2D.OverlapCircle(Skeleton.transform.position + new Vector3(20, 0, 0), checkerRadius, Maskaterenu))
            {
                PozycjaTerenu = Skeleton.transform.position + new Vector3(20, 0, 0);
                ChunkSpawn();
            }
        }
        else if (SM.wstrone.x < 0 && SM.wstrone.y == 0) //lewa strona
        {
            if (!Physics2D.OverlapCircle(Skeleton.transform.position + new Vector3(-20, 0, 0), checkerRadius, Maskaterenu))
            {
                PozycjaTerenu = Skeleton.transform.position + new Vector3(-20, 0, 0);
                ChunkSpawn();
            }
        }
        else if (SM.wstrone.x == 0 && SM.wstrone.y > 0) // góra
        {
            if (!Physics2D.OverlapCircle(Skeleton.transform.position + new Vector3(0, 20, 0), checkerRadius, Maskaterenu))
            {
                PozycjaTerenu = Skeleton.transform.position + new Vector3(0, 20, 0);
                ChunkSpawn();
            }
        }
        else if (SM.wstrone.x > 0 == SM.wstrone.y < 0)  //dó³
        {
            if (!Physics2D.OverlapCircle(Skeleton.transform.position + new Vector3(0, -20, 0), checkerRadius, Maskaterenu))
            {
                PozycjaTerenu = Skeleton.transform.position + new Vector3(0, -20, 0);
                ChunkSpawn();
            }
        }
        else if (SM.wstrone.x > 0 && SM.wstrone.y > 0) //prawo-góra
        {
            if (!Physics2D.OverlapCircle(Skeleton.transform.position + new Vector3(20, 20, 0), checkerRadius, Maskaterenu))
            {
                PozycjaTerenu = Skeleton.transform.position + new Vector3(20, 20, 0);
                ChunkSpawn();
            }
        }
        if (SM.wstrone.x > 0 && SM.wstrone.y < 0)  //prawo-dó³
        {
            if (!Physics2D.OverlapCircle(Skeleton.transform.position + new Vector3(20, -20, 0), checkerRadius, Maskaterenu))
            {
                PozycjaTerenu = Skeleton.transform.position + new Vector3(20, -20, 0);
                ChunkSpawn();
            }
        } 
        if (SM.wstrone.x < 0 && SM.wstrone.y > 0)  //lewo-góra
        {
            if (!Physics2D.OverlapCircle(Skeleton.transform.position + new Vector3(-20, 20, 0), checkerRadius, Maskaterenu))
            {
                PozycjaTerenu = Skeleton.transform.position + new Vector3(-20, 20, 0);
                ChunkSpawn();
            }
        }
        if (SM.wstrone.x < 0 && SM.wstrone.y < 0)  //lewo-dó³
        {
            if (!Physics2D.OverlapCircle(Skeleton.transform.position + new Vector3(-20, -20, 0), checkerRadius, Maskaterenu))
            {
                PozycjaTerenu = Skeleton.transform.position + new Vector3(-20, -20, 0);
                ChunkSpawn();
            }
        }
    }

    void ChunkSpawn()
    {
        int rand = Random.Range(0, Chunki.Count);
        Instantiate(Chunki[rand], PozycjaTerenu, Quaternion.identity);
    }
}
