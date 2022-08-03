using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 40f;

    public float horizontalMove = 0f;
    public bool jump = false;

    public bool IsJumping { get; set; }
    public bool crouch = false;

    Vector2 position;

    private void Start()
    {
        //position = ;
    }


    // Update is called once per frame
    void Update()
    {
        Debug.Log(position.y);
        if (GetComponent<Transform>().transform.position.y <= -100f)
        {
            controller.Die();
        }

        if (Application.isMobilePlatform)
        {
            animator.SetFloat("Speed", Mathf.Abs(horizontalMoveMobile * runSpeed));

            if (Input.GetButtonDown("Jump"))
            {
                IsJumping = true;
                jump = true;
                animator.SetBool("IsJumping", true);
            }

            if (Input.GetButtonDown("Crouch"))
            {
                crouch = true;
            }
            else if (Input.GetButtonUp("Crouch"))
            {
                crouch = false;
            }
        }
        else
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("test");
                Debug.Log("Jump!");
                IsJumping = true;
                jump = true;
                animator.SetBool("IsJumping", true);
            }

            if (Input.GetButtonDown("Crouch"))
            {
                crouch = true;
            }
            else if (Input.GetButtonUp("Crouch"))
            {
                crouch = false;
            }
        }
    }

    public void Jump()
    {
        IsJumping = true;
        jump = true;
        animator.SetBool("IsJumping", true);
    }

    public void Move()
    {

    }

    public void OnLanding()
    {
        
        //IsJumping = false;
        //animator.SetBool("IsJumping", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("lending");
        if (collision.gameObject.tag == "Ground") {
            
            IsJumping = false;
            animator.SetBool("IsJumping", false);
        }
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }

    public float horizontalMoveMobile;

    void FixedUpdate()
    {
        if (Application.isMobilePlatform)
        {
            // Move our character
            Debug.Log("horizontalMoveMobile  " + horizontalMoveMobile);
            controller.Move(horizontalMoveMobile * runSpeed * Time.fixedDeltaTime, crouch, jump);
            jump = false;
        }
        else
        {
            // Move our character
            controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
            jump = false;
        }

    }
}
