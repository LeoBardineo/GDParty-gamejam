using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estalactite : MonoBehaviour
{
    Rigidbody2D rb;
    public bool grounded = false;
    private float initialY;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        initialY = transform.position.y;
    }
    
    void Update()
    {
        if(!grounded){
            RayCastPlayer();
        }
    }

    public void RayCastPlayer() {
        Debug.DrawRay(transform.position, -Vector2.up * 10, Color.white, 0);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 10, LayerMask.GetMask("Player"));
        if(hit.collider != null && hit.collider.gameObject.tag == "Player") {
            Debug.DrawRay(transform.position, -Vector2.up * 10, Color.red, 0);
            rb.gravityScale = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "ground"){
            grounded = true;
            Debug.LogError(grounded);
        } else if (collision.gameObject.tag == "Player" && !grounded){
            Debug.LogError("hitou player");
            Destroy(gameObject);
        }
    }
    
}
