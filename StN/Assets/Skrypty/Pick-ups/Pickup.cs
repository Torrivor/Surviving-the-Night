using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Gracz"))         //jesli przedmiot bedzia zablisko gracza zostanie zniszczony
        {
            Destroy(gameObject);
        }
    }
}
