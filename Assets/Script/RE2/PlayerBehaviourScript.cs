using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviourScript : MonoBehaviour
{
    public enum Status
    {
        Idle,
        Moving,
        Jack,
    }

    public Status MyStatus;
    public const float AttackLerpTime = 0.5f;
    public float AttackLerpTimer;

    private Vector3 AttackJackPosOffset;
    private float AttackJackRotOffset;
    // Start is called before the first frame update
    void Awake()
    {
        MyStatus = Status.Idle;
        AttackLerpTimer = 0.0f;

        AttackJackPosOffset = Vector3.zero;
        AttackJackRotOffset = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        switch (MyStatus)
        {
            case Status.Idle:
                break;
            case Status.Moving:
                break;
            case Status.Jack:
                //following is supposed to be an Action in MotionJackFSM of the player
                if (AttackLerpTimer > 0.0f)
                {
                    Vector3 axisY = new Vector3(0, 1, 0);
                    transform.Rotate(axisY, AttackJackRotOffset * Time.deltaTime / AttackLerpTime);
                    transform.position += AttackJackPosOffset * Time.deltaTime / AttackLerpTime;

                    AttackLerpTimer -= Time.deltaTime;
                }
                else
                    AttackLerpTimer = 0.0f;
                break;
        }
    }

    public void onJack()
    {
        MyStatus = Status.Jack;
        AttackLerpTimer = AttackLerpTime;

        //offset angleåvéZ-----------------------------------------------------------
        //é©ï™ÇÃå¸Ç´
        float myAngle = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
        if (myAngle > Mathf.PI)
            myAngle -= Mathf.PI * 2;

        Vector3 myDir = new Vector3(Mathf.Cos(myAngle), 0f, -Mathf.Sin(myAngle));

        //É^Å[ÉQÉbÉgÇÃå¸Ç´
        Transform enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
        float enemyAngle = enemy.rotation.eulerAngles.y * Mathf.Deg2Rad;
        if (enemyAngle > Mathf.PI)
            enemyAngle -= Mathf.PI * 2;

        Vector3 enemyDir = new Vector3(Mathf.Cos(enemyAngle), 0f, -Mathf.Sin(enemyAngle));

        //offset angleåvéZ
        float deltaAngleVolume = Vector3.Angle(myDir, -enemyDir);
        float pole = Vector3.Cross(myDir, -enemyDir).y / Mathf.Abs(Vector3.Cross(myDir, -enemyDir).y);
        float deltaAngle = deltaAngleVolume * pole;

        AttackJackRotOffset = deltaAngle;

        //offset posåvéZ-----------------------------------------------------------
        Vector3 targetPos = enemy.position + enemyDir.normalized * 1.0f;
        AttackJackPosOffset = targetPos - transform.position;
    }
}
