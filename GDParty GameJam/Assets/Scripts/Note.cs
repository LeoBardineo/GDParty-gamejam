using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public bool VaiPraDireita;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Start()
    {
        if(VaiPraDireita == true)
        {
            rb.velocity = new Vector2(speed,0f);
        }
        else
        {
            rb.velocity = new Vector2(-speed,0f);
        }
    }
}
