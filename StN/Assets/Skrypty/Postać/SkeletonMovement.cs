using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMovement : MonoBehaviour
{

    //Ruch naszej postaci
    [HideInInspector]
    public float OstatniaPozycjaHoryzontalna;
    [HideInInspector]
    public float OstatniaPozycjaVertykalna;
    [HideInInspector]
    public Vector2 wstrone;
    [HideInInspector]
    public Vector2 lastVector;


    Rigidbody2D rb;
    PlayerStats player;


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
        lastVector = new Vector2(1, 0f); //Utworzenie momentum broni przy starcie gry

    }

    // Update is called once per frame
    void Update()
    {
        InputManagement();
    }

    void FixedUpdate()
    {
        Ruch();
    }

    void InputManagement()
    {
        //Poruszanie w boki i g�ra/do�
        float ruchX = Input.GetAxisRaw("Horizontal");
        float ruchY = Input.GetAxisRaw("Vertical");

        wstrone = new Vector2(ruchX, ruchY).normalized;

        //If do zaoami�tania pozycji w jak� patrzy posta�
        if (wstrone.x != 0)
        {
            OstatniaPozycjaHoryzontalna = wstrone.x;
            lastVector = new Vector2(OstatniaPozycjaHoryzontalna, 0f); //ostatni ruch x
        }
        if (wstrone.y != 0)
        {
            OstatniaPozycjaVertykalna = wstrone.y;
            lastVector = new Vector2(0f,OstatniaPozycjaVertykalna); //ostatni ruch y
        }
        if (wstrone.x != 0 && wstrone.y != 0)
        {
            lastVector = new Vector2(OstatniaPozycjaHoryzontalna, OstatniaPozycjaVertykalna);  //Kiedy si� rusza
        }

    }
    
    void Ruch()
    {
        //pr�dko�� poruszania
        rb.velocity = new Vector2 (wstrone.x * player.currentMoveSpeed, wstrone.y * player.currentMoveSpeed);

    }

}
