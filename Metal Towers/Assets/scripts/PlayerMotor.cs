using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {

    public GameObject model;

    public Rigidbody rg;
    public Animator animModel;
    public Animator animPlayer;

    Quaternion desRotation;
    Vector3 wallSidePos;

    public float moveSpeed;
    public float jumpForce;
    public float rotSpeed;
    public float climbSpeed;
    float moveX;

    bool isGrounded = true;


    void Start () {
		
	}
	
	void Update () {
        Movement();
	}

    void Movement()
    {
        moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        rg.velocity = new Vector3(moveX, rg.velocity.y, 0);

        if(moveX < 0)
        {
            desRotation = Quaternion.Euler(0, 180, 0);
        }

        if (moveX > 0)
        {
            desRotation = Quaternion.Euler(0, 0, 0);
        }

        if(moveX != 0)
        {
            animModel.SetBool("walking", true);
        }
        else
        {
            animModel.SetBool("walking", false);
        }


        model.transform.rotation = desRotation;

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            animModel.SetBool("jumping", true);
            rg.AddForce(Vector3.up * jumpForce);
            isGrounded = false;
        }

        if(isGrounded && Input.GetKey(KeyCode.LeftControl))
        {
            animPlayer.SetBool("crouching", true);
            animModel.SetBool("crouching", true);
        }

        if (isGrounded && Input.GetKeyUp(KeyCode.LeftControl))
        {
            animPlayer.SetBool("crouching", false);
            animModel.SetBool("crouching", false);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
            animModel.SetBool("jumping", false);
        }
    }
}
