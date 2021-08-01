using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject Enemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     if(Input.GetKeyDown(KeyCode.Q))
        {
            Player.GetComponent<PlayerBehaviourScript>().MyStatus = PlayerBehaviourScript.Status.Idle;
            Enemy.GetComponent<EnemyBehaviourScript>().MyStatus = EnemyBehaviourScript.Status.Idle;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Player.GetComponent<PlayerBehaviourScript>().MyStatus = PlayerBehaviourScript.Status.Moving;
            Enemy.GetComponent<EnemyBehaviourScript>().MyStatus = EnemyBehaviourScript.Status.Moving;
        }
    }
}
