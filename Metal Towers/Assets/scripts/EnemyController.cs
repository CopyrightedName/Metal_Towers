using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    PlayerMotor player;

    float rotation = 0;
    public float maxCountdown = 100;
    [SerializeField]
    float countdown;

    bool facingRight = true;
    bool facingLeft = false;

    void Start () {
        player = FindObjectOfType<PlayerMotor>();
        countdown = maxCountdown;
	}
	
	void Update () {
        StartCoroutine(Turn());
	}

    IEnumerator Turn()
    {
        if (countdown > 0)
        {
            if(countdown >= maxCountdown) { 
                countdown = maxCountdown;
                rotation += 180;
                transform.rotation = Quaternion.Euler(0, rotation, 0);
            }

            countdown = countdown - 1;

        }
        else if (countdown <= 0)
        {
            rotation += 180;
            transform.rotation = Quaternion.Euler(0, rotation, 0);

            countdown = countdown + 1;
        }

        return null;
    }
}
