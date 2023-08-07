using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Geral")]
    public GameObject interaction;
    public int puloCheckAudio;
    public float speed;
    public static float jump;
    public bool isJumping;
    public static bool Paused;
    public static bool isDead = false;
    public static bool inDialogo = false;
    public Rigidbody2D rb;
    public static Vector3 respawnPoint;
    public static int almasColetadas;
    public Estresse estresse;
    public bool isMeditando;
    private Animator anim;
    private string currentState;

    [Header("Double Jump")]
    public static bool doubleJumpUnlocked = false;
    public static bool doubleJump = false;

    [Header("Dash")]
    public int dashCount = 0;
    public static bool dashUnlocked = false, jumpDashUnlocked = false;
    private Vector3 myVector;
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 7200f;
    private float dashingTime = 0.09f;
    private float dashingCooldown = 0.2f;

    [Header("Outros")]
    public bool facingRight = true;
    public bool facingLeft;
    private RigidbodyConstraints2D originalConstraints;
    [SerializeField] private TrailRenderer tr;
    public GameObject gameOver;
    private bool teste;

    [Header("Audio")]
    public AudioClip pulo;
    public AudioClip puloDuplo;
    public AudioClip dash;
    public AudioClip aterrissar;
    //Raycast
    //Tentativa de corrigir bordas
    private CapsuleCollider2D capsuleCollider;
    void Start()
    {
        isJumping = true;
        jump = 500;
        anim = GetComponent<Animator>();
        isJumping = false;
        inDialogo = false;
        isMeditando = false;
        originalConstraints = rb.constraints;
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        estresse = GetComponent<Estresse>();
        respawnPoint = transform.position;
        //capsuleCollider.sharedMaterial.friction = 2;
    }

    void FixedUpdate()
    {
        if (isMeditando) return;
        if (inDialogo)
        {
            ChangeAnimationState("Player_Idle");
            return;
        }
        Move();
        if (isDashing) return;
        // ChangeAnimationState("Player_Jump");

    }

    void Update()
    {
        Pausou();
        if (isDashing || inDialogo || isMeditando) return;
        Dash();
        Jump();
        if (facingLeft == true)
        {
            interaction.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (facingLeft == false)
        {
            interaction.GetComponent<SpriteRenderer>().flipX = false;
        }
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
            if (puloCheckAudio == 1)
            {
                AudioSource.PlayClipAtPoint(aterrissar, this.transform.position, 100f);
                puloCheckAudio = 0;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            isJumping = true;
        }
    }

    public void ChangeAnimationState(string newState)
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
            AudioSource.PlayClipAtPoint(pulo, this.transform.position, 100f);
            rb.velocity = new Vector2(rb.velocity.y, 0f);
            rb.AddForce(new Vector2(rb.velocity.x, jump));
            doubleJump = true;
            puloCheckAudio = 1;
        }
        if (Input.GetButtonDown("Jump") && isJumping == true && doubleJump == true && doubleJumpUnlocked == true)
        {
            AudioSource.PlayClipAtPoint(puloDuplo, this.transform.position, 100f);
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
        AudioSource.PlayClipAtPoint(dash, this.transform.position, 100f);
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

    public static Player GetPlayer()
    {
        return GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void Morrer()
    {
        isDead = true;
        gameOver.SetActive(true);
        Estresse.lockEstresse = true;
        Time.timeScale = 0;
    }

    public void Respawnar()
    {
        isDead = false;
        gameOver.SetActive(false);
        Estresse.lockEstresse = false;
        estresse.ResetarOEstresse();
        Time.timeScale = 1;
        transform.position = respawnPoint;
    }
}
