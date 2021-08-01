using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourScript : MonoBehaviour
{
    public enum Status
    {
        Idle,
        Moving,
        Jack,
    }

    public Status MyStatus;
    public Vector3 MoveSpeed;
    // Start is called before the first frame update
    void Awake()
    {
        MyStatus = Status.Idle;
        MoveSpeed = new Vector3(0, 0, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindWithTag("Player");

        switch(MyStatus)
        {
            case Status.Idle:
                break;

            case Status.Moving:
                //transform.position += MoveSpeed * Time.deltaTime;

                if(Vector3.Distance(transform.position, player.transform.position) <= 1.5f)
                {
                    onJack();
                }
                break;

            case Status.Jack:
                break;
        }
    }

    private void onJack()
    {
        MyStatus = Status.Jack;

        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerBehaviourScript>().onJack();
    }
}
