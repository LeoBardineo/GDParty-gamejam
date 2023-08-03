using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   public float speed,jump;
   public bool isJumping,Paused;
//    private float Move;
   public Rigidbody2D rb;
   private Animator anim;
   private string currentState;
   
   //DoubleJump
   public bool doubleJump = false;
   
   void Start()
   {
        anim = GetComponent<Animator>();
        isJumping = false;
   }

   void FixedUpdate()
   {
        Move();
        
        // ChangeAnimationState("Player_Jump");
   }

   void Update()
   {
        Pausou();
        if (Input.GetButtonDown("Jump") && isJumping == false)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump));
            doubleJump = true;
        }
        if (Input.GetButtonDown("Jump") && isJumping == true && doubleJump == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(new Vector2(rb.velocity.x, jump));
            doubleJump = false;
        }

        if(rb.velocity.y > 0 && isJumping == true)
        {
            ChangeAnimationState("Player_JumpUp");
        }
        if(rb.velocity.y < 0 && isJumping == true)
        {
            ChangeAnimationState("Player_JumpDown");
        }
   }

   private void OnCollisionEnter2D(Collision2D other)
   {
        if (other.gameObject.CompareTag("ground"))
        {
            isJumping = false;
        }

        if (other.gameObject.CompareTag("obstacle"))
        {
            //morre
            //ChangeAnimationState("Player_Death");
        }
   }

   private void OnCollisionExit2D(Collision2D other)
   {
        if(other.gameObject.CompareTag("ground"))
        {
            isJumping = true;
        }
   }

   void ChangeAnimationState( string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }

    void Move()
    {
        rb.velocity = new Vector3(speed * Input.GetAxis("Horizontal"), rb.velocity.y,0f);
        
        if(Input.GetAxis("Horizontal") > 0f && isJumping == false)
        {
            ChangeAnimationState("Player_Walk");
            transform.eulerAngles = new Vector3(0f,0f,0f);
        }
        else if(Input.GetAxis("Horizontal") < 0f && isJumping == false)
        {
            ChangeAnimationState("Player_Walk");
            transform.eulerAngles = new Vector3(0f,180f,0f);
        }
        else if(Input.GetAxis("Horizontal") == 0f && isJumping == false)
        {
            ChangeAnimationState("Player_Idle");
        }
    }
    // --------------------------------------------------------
    // Quando tiver as outras skills implementadas, colocar os states pras animações
    // ChangeAnimationState("Player_DoubleJump");

    public void Pausou()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Paused = !Paused;
        }
    }

    void Dash()
    {
        //ChangeAnimationState("Player_Dash");
    }
}
