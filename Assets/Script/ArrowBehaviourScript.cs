using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviourScript : MonoBehaviour
{
    private const float RotateAngleMax = 0.2f;
    private const float LifeTime = 20.0f;

    public float MoveSpeed;

    private Vector3 Speed;
    private int TimeFlag;
    private float MainTimer;
    private float MoveTimer;

    // Start is called before the first frame update
    void Start()
    {
        //向き初期化
        Vector3 axisZ = new Vector3(0, 0, 1);
        transform.Rotate(axisZ, 180.0f);
        //速度初期化
        MoveSpeed = 1.0f;
        Speed = new Vector3(-1.0f, 0f, 0f) * MoveSpeed;
        //タイマー初期化
        MainTimer = 0f;
        MoveTimer = 0f;
        TimeFlag = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Speed * Time.deltaTime;
    }

    private void LateUpdate()
    {
        Vector3 myPos = transform.position;
        if (myPos.y > 7.0f)
            transform.position = new Vector3(myPos.x, myPos.y - 7.0f, 0f);
        if (myPos.y < 0)
            transform.position = new Vector3(myPos.x, 7.0f - myPos.y, 0f);

        switch (TimeFlag)
        {
            case 0:
                {
                    if(MainTimer > LifeTime)
                    {
                        TimeFlag++;
                    }
                    else
                    {
                        if (MoveTimer > 0.25f)
                        {
                            //自分の向き
                            float myAngle = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
                            if (myAngle > Mathf.PI)
                                myAngle -= Mathf.PI * 2;

                            Vector3 myDir = new Vector3(Mathf.Cos(myAngle), Mathf.Sin(myAngle), 0f);

                            //ターゲットへの向き
                            Transform target = GameObject.Find("Player").transform;
                            Vector3 targetDir = target.position - transform.position;

                            //一フレームの回転量を計算
                            float deltaAngleVolume = Vector3.Angle(myDir, targetDir);
                            float pole = Vector3.Cross(myDir, targetDir).z / Mathf.Abs(Vector3.Cross(myDir, targetDir).z);
                            float deltaAngle = deltaAngleVolume * pole;

                            float rotateAngle;
                            if (Mathf.Abs(deltaAngle) > RotateAngleMax)
                                rotateAngle = RotateAngleMax * (deltaAngle / Mathf.Abs(deltaAngle));
                            else
                                rotateAngle = deltaAngle;


                            //ターゲットへ向き変換
                            Vector3 axisZ = new Vector3(0, 0, 1);
                            transform.Rotate(axisZ, rotateAngle * 180.0f / Mathf.PI);

                            //変換後の向き
                            float dirAngle = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
                            Vector3 dir = new Vector3(Mathf.Cos(dirAngle), Mathf.Sin(dirAngle), 0.0f).normalized;

                            //向きに応じて速度方向計算
                            Speed = dir * MoveSpeed;

                            //タイマー更新
                            MoveTimer = 0.0f;
                        }
                    }
                }
                break;
            case 1:
                //if (!isInCamera())
                //    Destroy(gameObject);
                break;
        }

        //タイマー更新
        MainTimer += Time.deltaTime;
        MoveTimer += Time.deltaTime;
    }
}
