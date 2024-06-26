using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        //sprawdza czy inny obiekt ma interface ICollect
        if(col.gameObject.TryGetComponent(out ICollectible collectible))
        {
            //jesli ma to zbiera
            collectible.Collect();
        }
    }
}
