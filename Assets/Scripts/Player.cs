using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Cached component references
    CharacterController2D controller;
    Animator anim;
    CapsuleCollider2D myBody;
    GameSession myGameSession;


    [SerializeField] float moveSpeed = 10f;
    [SerializeField] Vector2 deathKick = new Vector2(0f, 15f);

    float horizontalMove = 0f;
    bool jump = false;
    bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController2D>();
        anim = GetComponent<Animator>();
        myBody = GetComponent<CapsuleCollider2D>();
        myGameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAlive) 
        {
            return;
        }

        horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
        anim.SetFloat("Speed", Mathf.Abs(horizontalMove));
        
        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
            anim.SetBool("isJumping", true);
        }
        Die();
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove*Time.fixedDeltaTime, false, jump);

        jump = false;
    }

    private void Die()
    {
        if (myBody.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            anim.SetTrigger("Die");
            isAlive = false;
            GetComponent<Rigidbody2D>().velocity = deathKick;
            myGameSession.ProcessPlayerDeath();

        }
    }

    public void JumpLanding()
    {
        anim.SetBool("isJumping", false);
    }
}
