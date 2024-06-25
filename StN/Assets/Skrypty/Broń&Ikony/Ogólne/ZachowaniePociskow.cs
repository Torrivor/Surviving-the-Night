using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZachowaniePociskow : MonoBehaviour
{
    public WeaponsScriptableObject weaponData;

    //Ustaw w prefabie broni pociskowej
    protected Vector3 kierunki;
    public float ZniszczPoSekundzie;
    
    protected virtual void Start()
    {
        Destroy(gameObject, ZniszczPoSekundzie);
    }

    public void KierunekChecker(Vector3 kier)
    {
        kierunki = kier;

        float kierx = kierunki.x;
        float kiery = kierunki.y;

        Vector3 skala = transform.localScale;
        Vector3 rotacja = transform.rotation.eulerAngles;

        if(kierx < 0 && kiery == 0 ) //lewo
        {
            skala.x = skala.x * -1;
            skala.y = skala.y * -1;
        }
        else if (kierx == 0 && kiery > 0) //g�ra
        {
            skala.y = skala.y * -1;
        }
        else if (kierx == 0 && kiery < 0) //d�
        {
            skala.x = skala.x * -1;
        }
        else if (kierx > 0 && kiery > 0) //prawo-g�ra
        {
            rotacja.z = -90f;
        }
        else if (kierx > 0 && kiery < 0) //prawo d�
        {
            rotacja.z = -180f;
        }
        else if (kierx < 0 && kiery > 0) //lewo g�ra
        {
            skala.x = skala.x * -1;
            skala.y = skala.y * -1;
            rotacja.z = -180f;
        }
        else if (kierx < 0 && kiery < 0) //lewo d�
        {
            skala.x = skala.x * -1;
            skala.y = skala.y * -1;
            rotacja.z = -90f;
        }
        transform.localScale = skala;
        transform.rotation = Quaternion.Euler(rotacja);
    }
}
