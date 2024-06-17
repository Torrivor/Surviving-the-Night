using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    //Ruch naszej postaci
    public float Speed;
    Rigidbody2D rb;
    Vector2 wstrone;





    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

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

    }
    
    void Ruch()
    {
        //pr�dko�� poruszania
        rb.velocity = new Vector2 (wstrone.x * Speed, wstrone.y * Speed);

    }

}
