using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    PlayerMotor player;

    float rotation = 0;

    void Start () {
        player = FindObjectOfType<PlayerMotor>();
	}
	
	void Update () {
        StartCoroutine(Turn());
	}

    IEnumerator Turn()
    {
        yield return new WaitForSeconds(3);
        while (true)
        {
            rotation += 180;
            transform.rotation = Quaternion.Euler(0, rotation, 0);
        }
    }
}
