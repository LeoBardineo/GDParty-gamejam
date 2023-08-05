using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed, jump;
    public bool isJumping, Paused;
    // private float Move;
    public Rigidbody2D rb;
    private Animator anim;
    private string currentState;
    //DoubleJump
    public bool doubleJumpUnlocked = false;
    public bool doubleJump = false;
    //Dash
    public int dashCount = 0;
    public bool dashUnlocked = false, jumpDashUnlocked = false;
    private Vector3 myVector;
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 7200f;
    private float dashingTime = 0.09f;
    private float dashingCooldown = 0.2f;
    public bool facingRight = true, facingLeft;
    private RigidbodyConstraints2D originalConstraints;
    [SerializeField] private TrailRenderer tr;
    private bool teste;

    //Raycast
    //Tentativa de corrigir bordas
    private CapsuleCollider2D capsuleCollider;
    void Start()
    {
        jump = 500;
        anim = GetComponent<Animator>();
        isJumping = false;
        originalConstraints = rb.constraints;
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        //capsuleCollider.sharedMaterial.friction = 2;
    }

    void FixedUpdate()
    {
        Move();
        if (isDashing)
        {
            return;
        }
        // ChangeAnimationState("Player_Jump");

    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }
        Dash();
        Jump();
        Pausou();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        /*         if (other.gameObject.CompareTag("edge"))
                {
                    Debug.Log("paredig");
                    capsuleCollider.sharedMaterial.friction = 0;
                } */
        /*                 if (other.gameObject.CompareTag("ground"))
                        {
                            isJumping = false;
                            dashCount=0;
                        }

                        if (other.gameObject.CompareTag("obstacle"))
                        {
                            //morre
                            //ChangeAnimationState("Player_Death");
                        } */
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("edge"))
        {
            Debug.Log("paredig");
            //capsuleCollider.sharedMaterial.friction = 2;
        }
        /*                 if(other.gameObject.CompareTag("ground"))
                        {
                            isJumping = true;
                        } */
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        /*         if (other.gameObject.CompareTag("edge"))
                {
                    Debug.Log("paredig");
                    capsuleCollider.sharedMaterial.friction = 0;
                } */
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            isJumping = false;
            dashCount = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            isJumping = true;
        }
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }

    void Move()
    {
        rb.velocity = new Vector3(speed * Input.GetAxis("Horizontal"), rb.velocity.y, 0f);

        if (Input.GetAxis("Horizontal") > 0f && isJumping == false)
        {
            ChangeAnimationState("Player_Walk");
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            facingLeft = false;
            facingRight = true;

        }
        else if (Input.GetAxis("Horizontal") < 0f && isJumping == false)
        {
            ChangeAnimationState("Player_Walk");
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            facingRight = false;
            facingLeft = true;
        }
        else if (Input.GetAxis("Horizontal") == 0f && isJumping == false)
        {
            ChangeAnimationState("Player_Idle");
        }
    }

    public void Jump()
    {
        if (Input.GetButtonDown("Jump") && isJumping == false)
        {
            rb.velocity = new Vector2(rb.velocity.y, 0f);
            rb.AddForce(new Vector2(rb.velocity.x, jump));
            doubleJump = true;
        }
        if (Input.GetButtonDown("Jump") && isJumping == true && doubleJump == true && doubleJumpUnlocked == true)
        {
            rb.velocity = new Vector2(rb.velocity.y, 0f);
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(new Vector2(rb.velocity.x, jump));
            doubleJump = false;
        }
        if (Input.GetAxis("Horizontal") > 0f)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            facingLeft = false;
            facingRight = true;
        }
        if (Input.GetAxis("Horizontal") < 0f)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            facingRight = false;
            facingLeft = true;
        }
        if (rb.velocity.y > 0 && isJumping == true)
        {
            ChangeAnimationState("Player_JumpUp");
        }
        if (rb.velocity.y < 0 && isJumping == true)
        {
            ChangeAnimationState("Player_JumpDown");
        }
    }
    // --------------------------------------------------------
    // Quando tiver as outras skills implementadas, colocar os states pras animações
    // ChangeAnimationState("Player_DoubleJump");

    public void Pausou()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Paused = !Paused;
        }
    }

    public void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            if (dashUnlocked && !isJumping)
            {
                StartCoroutine(DashCode());
            }
            if (dashUnlocked && jumpDashUnlocked && isJumping)
            {
                if (dashCount == 0)
                {
                    float originalGravity = rb.gravityScale;
                    StartCoroutine(DashCode());
                    rb.gravityScale = originalGravity;
                    dashCount += 1;
                }
            }
        }
    }
    private IEnumerator DashCode()
    {
        if (facingRight == true)
        {
            canDash = false;
            isDashing = true;
            float originalGravity = rb.gravityScale;
            //rb.gravityScale = 0f;
            rb.velocity = new Vector2(0f, 0f);
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            rb.AddForce(new Vector2(dashingPower, 0f));
            tr.emitting = true;
            yield return new WaitForSeconds(dashingTime);
            rb.constraints = originalConstraints;
            tr.emitting = false;
            rb.gravityScale = originalGravity;
            isDashing = false;
            yield return new WaitForSeconds(dashingCooldown);
            canDash = true;
        }

        else if (facingLeft == true)
        {

            canDash = false;
            isDashing = true;
            float originalGravity = rb.gravityScale;
            //rb.gravityScale = 0f;
            rb.velocity = new Vector2(0f, 0f);
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            rb.AddForce(new Vector2(-dashingPower, 0f));
            tr.emitting = true;
            yield return new WaitForSeconds(dashingTime);
            rb.constraints = originalConstraints;
            tr.emitting = false;
            rb.gravityScale = originalGravity;
            isDashing = false;
            yield return new WaitForSeconds(dashingCooldown);
            canDash = true;
        }
    }
}
