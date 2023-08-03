using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   public float speed;
   public float jump;
   public bool isJumping;
   private float Move;
   public Rigidbody2D rb;
   //DoubleJump
   public bool doubleJump = false;
   void Start()
   {

   }

   void Update()
   {
    Move = Input.GetAxis("Horizontal");
    rb.velocity = new Vector2(speed * Move, rb.velocity.y);

    if (Input.GetButtonDown("Jump") && isJumping == false)
    {
        rb.AddForce(new Vector2(rb.velocity.x, jump));
        doubleJump = true;
    }
    if (Input.GetButtonDown("Jump") && isJumping == true && doubleJump == true)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(new Vector2(rb.velocity.x, jump));
        doubleJump = false;
    }

   }

   private void OnCollisionEnter2D(Collision2D other)
   {
        if (other.gameObject.CompareTag("ground"))
        {
            isJumping = false;
        }
   }

   private void OnCollisionExit2D(Collision2D other)
   {
        if(other.gameObject.CompareTag("ground"))
        {
            isJumping = true;
        }
   }
}
