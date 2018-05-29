using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {

    public GameObject crosshair;
    public GameObject model;
    public GameObject bullet;
    GameObject bulletInstance;


    public Transform bulletPos;

    public Rigidbody rg;
    public Animator animModel;
    public Animator animPlayer;

    Quaternion desRotation;
    Vector3 wallSidePos;
    Vector3 crosshairPos;

    public float moveSpeed;
    public float jumpForce;
    public float rotSpeed;
    public float climbSpeed;
    float moveX;
    public float bulletSpeed;
    public float minY;
    public float maxY;

    bool isGrounded = true;
    bool isMoving;


    void Start () {
		
	}
	
	void Update () {
        Movement();

        if(Input.GetMouseButton(1) && !isMoving)
        {
            if (isMoving)
            {
                crosshair.SetActive(false);
                return;
            }
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("cursorPos"))
                {
                    crosshair.transform.position = hit.point;
                }
            }

            crosshair.SetActive(true);
            if (Input.GetMouseButtonDown(0))
            {
                bulletInstance = Instantiate(bullet, bulletPos.position, Quaternion.identity);
                bulletInstance.transform.position = Vector3.Lerp(bulletPos.position, hit.point, bulletSpeed * Time.deltaTime);
                crosshairPos = crosshair.transform.position;
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            crosshair.SetActive(false);
        }

        Cursor.visible = false;

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
            isMoving = true;
            animModel.SetBool("walking", true);
        }
        else
        {
            isMoving = false;
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

    void Attack()
    {



       
    }
}
