using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estalactite : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    public bool quebrou = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rb.gravityScale = 0;
    }

    void Update()
    {
        RayCastPlayer();
    }

    public void RayCastPlayer()
    {
        if (quebrou) return;
        Debug.DrawRay(transform.position, -Vector2.up * 10, Color.white, 0);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 10, LayerMask.GetMask("Player"));
        if (hit.collider != null && hit.collider.gameObject.tag == "Player")
        {
            Debug.DrawRay(transform.position, -Vector2.up * 10, Color.red, 0);
            rb.gravityScale = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (quebrou) return;
        if (collision.gameObject.tag == "Player")
        {
            // Player morreu!
        }
        quebrou = true;
        rb.gravityScale = 0;
        anim.Play("Estalactite quebrando");
        Destroy(gameObject, 0.4f);
    }

}
