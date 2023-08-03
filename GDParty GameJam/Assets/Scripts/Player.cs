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
   }

   void FixedUpdate()
   {
    Move();

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
    // ChangeAnimationState("Player_Jump");
   }

   void Update()
   {
        Pausou();
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
        
        if(Input.GetAxis("Horizontal") > 0f)
        {
            ChangeAnimationState("Player_Walk");
            transform.eulerAngles = new Vector3(0f,0f,0f);
        }
        else if(Input.GetAxis("Horizontal") < 0f)
        {
            ChangeAnimationState("Player_Walk");
            transform.eulerAngles = new Vector3(0f,180f,0f);
        }
        else
        {
            ChangeAnimationState("Player_Idle");
        }
    }
    // --------------------------------------------------------
    // Quando tiver as outras skills implementadas, colocar os states pras animações
    //ChangeAnimationState("Player_Dash");
    // ChangeAnimationState("Player_DoubleJump");

    public void Pausou()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Paused = !Paused;
        }
    }
}
