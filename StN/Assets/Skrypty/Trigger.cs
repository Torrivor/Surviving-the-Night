using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    Endless End;
    public GameObject targetMap;

    //Naprawa Chunków
    void Start()
    {
        End = FindObjectOfType<Endless>();
    }

    
    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.CompareTag("Gracz"))
        {
            End.currentChunk = targetMap;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Gracz"))
        {
            if(End.currentChunk == targetMap)
            {
                End.currentChunk = null;
            }
            
        }
    }
}
