using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckerBehaviour : MonoBehaviour
{
    public const float MoveSpeed = 3.0f;
    public const float WaitTime = 0.4f;

    public GameObject BulletEnemyPrefab;

    private float MoveTimer;
    private float MainTimer;
    private Vector3 TargetShootPos;
    private Vector3 Speed;
    private int TimeFlag;

    private Vector3 OldPos;

    // Start is called before the first frame update
    void Awake()
    {
        MoveTimer = 0.0f;
        MainTimer = 0.0f;
        TimeFlag = 0;
        TargetShootPos = getTargetShootPos(GameObject.Find("Player").transform.position + new Vector3(-1.5f, 0, 0));
        OldPos = transform.position;
        Speed = new Vector3(1.0f, 0, 0) * MoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        OldPos = transform.position;
        transform.position += Speed * Time.deltaTime;
    }

    private void LateUpdate()
    {
        Vector3 playerPos = GameObject.Find("Player").transform.position;
        Vector3 myPos = transform.position;

        switch(TimeFlag)
        {
            case 0:
                {
                    var targetPos = playerPos + new Vector3(-1.0f, 0, 0);
                    walkAndShoot(targetPos);
                }
                break;
            case 1:
                if(MoveTimer > WaitTime)
                {
                    TimeFlag++;
                }
                break;
            case 2:
                {
                    var targetPos = playerPos + new Vector3(-1.0f, 0, 0);
                    walkAndShoot(targetPos);
                }
                break;
            case 3:
                if (MoveTimer > WaitTime)
                {
                    TimeFlag++;
                }
                break;
            case 4:
                {
                    var targetPos = playerPos + new Vector3(-1.0f, 0, 0);
                    walkAndShoot(targetPos);
                }
                break;
            case 5:
                if (MoveTimer > WaitTime)
                {
                    TimeFlag++;
                }
                break;
            //--------------------------------------
            case 6:
                {
                    var targetPos = playerPos;
                    walkAndShoot(targetPos);
                }
                break;
            case 7:
                if (MoveTimer > WaitTime)
                {
                    TimeFlag++;
                }
                break;
            case 8:
                {
                    var targetPos = playerPos + new Vector3(-0.5f, 0, 0);
                    walkAndShoot(targetPos);
                }
                break;
            case 9:
                if (MoveTimer > WaitTime)
                {
                    TimeFlag++;
                }
                break;
            //--------------------------------------
            case 10:
                {
                    var targetPos = playerPos + new Vector3(0f, 0, 0);
                    walkAndShoot(targetPos);
                }
                break;
            case 11:
                if (MoveTimer > WaitTime)
                {
                    TimeFlag++;
                }
                break;
            case 12:
                {
                    var targetPos = playerPos + new Vector3(0.5f, 0, 0);
                    walkAndShoot(targetPos);
                }
                break;
            case 13:
                if (MoveTimer > WaitTime)
                {
                    TimeFlag++;
                }
                break;
            case 14:
                {
                    var targetPos = Vector3.zero;
                    walkAndShoot(targetPos);
                }
                break;

        }

        MoveTimer += Time.deltaTime;
        MainTimer += Time.deltaTime;
    }

    private bool isReachTarget()
    {
        if ((OldPos.x < TargetShootPos.x && transform.position.x >= TargetShootPos.x) || (OldPos.x > TargetShootPos.x && transform.position.x <= TargetShootPos.x))
        {
            return true;
        }
        else
            return false;
    }

    private Vector3 getTargetShootPos(Vector3 targetPos)
    {
        float targetShootPosX = targetPos.x + Mathf.Abs(targetPos.y - transform.position.y) * Mathf.Tan(Mathf.PI / 4.0f);
        return new Vector3(targetShootPosX, transform.position.y,0.0f);
    }

    private void walkAndShoot(Vector3 targetPos)
    {
        Vector3 myPos = transform.position;

        TargetShootPos = getTargetShootPos(targetPos);

        if (isReachTarget())
        {
            GameObject bullet = Instantiate(BulletEnemyPrefab, transform.position, Quaternion.identity, GameObject.Find("Stage00").transform);
            Vector3 targetDir = (targetPos - transform.position).normalized;
            bullet.GetComponent<BulletEnemyBehaviour>().setup(targetDir);
            TimeFlag++;

            Speed = Vector3.zero;
            MoveTimer = 0.0f;
        }
        else
        {
            if (TargetShootPos.x > myPos.x)
                Speed = new Vector3(1.0f, 0, 0) * MoveSpeed;
            else
                Speed = new Vector3(-1.0f, 0, 0) * MoveSpeed;
        }
    }
}
