using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public float Speed, JumpForce;
    private Rigidbody2D rig;
    //Inicia double jump como falso
    public bool DoubleJumpBool = false;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
    }

    void FixedUpdate()
    {
        Move();
    }
        void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"),0f,0f);
        transform.position += movement * Time.deltaTime * Speed;

        if(Input.GetAxis("Horizontal") > 0f)
        {
            //anim.SetBool("Walk",true);
            transform.eulerAngles = new Vector3(0f,0f,0f);
        }
        else if(Input.GetAxis("Horizontal") < 0f)
        {
            //anim.SetBool("Walk",true);
            transform.eulerAngles = new Vector3(0f,180f,0f);
        }
        else
        {
            //anim.SetBool("Walk",false);
        }
    }

        void Jump()
    {
/*         if(IsJumping && rig.velocity.y > 0)
        {
            anim.SetBool("Jump_UP",true);
        }
        else if(IsJumping && rig.velocity.y < 0)
        {
            anim.SetBool("Jump_UP",false);
            anim.SetBool("Jump_DOWN",true);
        } */
        if(Input.GetButtonDown("Jump"))
        {
            rig.AddForce(new Vector2(0f,JumpForce),ForceMode2D.Impulse);
            DoubleJumpBool = true;
            //Som_Pulo.Play();
        }
        else if (Input.GetButtonDown("Jump") && DoubleJumpBool == true)
        {
            DoubleJump();
            DoubleJumpBool = false;
        }
    }

        void DoubleJump()
    {
            rig.velocity = new Vector2 (rig.velocity.x, 0);
            rig.AddForce(new Vector2(0f,JumpForce),ForceMode2D.Impulse);
            //anim.SetBool("DoubleJump",true);
        
    }

}
