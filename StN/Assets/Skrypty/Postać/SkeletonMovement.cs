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
        //Poruszanie w boki i góra/do³
        float ruchX = Input.GetAxisRaw("Horizontal");
        float ruchY = Input.GetAxisRaw("Vertical");

        wstrone = new Vector2(ruchX, ruchY).normalized;

        //If do zaoamiêtania pozycji w jak¹ patrzy postaæ
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
            lastVector = new Vector2(OstatniaPozycjaHoryzontalna, OstatniaPozycjaVertykalna);  //Kiedy siê rusza
        }

    }
    
    void Ruch()
    {
        //prêdkoœæ poruszania
        rb.velocity = new Vector2 (wstrone.x * player.currentMoveSpeed, wstrone.y * player.currentMoveSpeed);

    }

}
