using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {

    public GameObject model;

    public Rigidbody rg;

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


        model.transform.rotation = Quaternion.Lerp(model.transform.rotation, desRotation, rotSpeed);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rg.AddForce(Vector3.up * jumpForce);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
        }
    }
}
