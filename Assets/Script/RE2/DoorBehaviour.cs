using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    public enum DoorStatus
    {
        Closed,
        Closing,//加速過程
        Swing, //減速過程
        Colliding,
    }

    public const float RotateSpeedMax = Mathf.PI / 3.0f;
    public const float RotateAngleMax = Mathf.PI / 2.0f;
    public const float RotateAcc = Mathf.PI / 3.0f;
    public float RotateSpeed;
    public float MyOldAngle;
    public float MyAngle;
    private DoorStatus Status;

    // Start is called before the first frame update
    void Start()
    {
        RotateSpeed = 0.0f;
        MyOldAngle = 0.0f;
        MyAngle = 0.0f;
        Status = DoorStatus.Closed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 axisY = new Vector3(0, 1.0f, 0);

        //古い位置の計算
        MyOldAngle = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
        if (MyOldAngle > Mathf.PI)
            MyOldAngle -= Mathf.PI * 2;

        switch(Status)
        {
            case DoorStatus.Closed:
                break;
            case DoorStatus.Closing:
                {
                    if (MyOldAngle > 0.0f)
                        RotateSpeed += -RotateAcc * Time.deltaTime;
                    if (MyOldAngle < 0.0f)
                        RotateSpeed += RotateAcc * Time.deltaTime;
                }
                break;
            case DoorStatus.Swing:
                //一定距離離れたら自動閉める
                if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position) > 1.5f)
                {
                    Status = DoorStatus.Closing;
                }
                else
                {
                    if (RotateSpeed > 0.0f)
                        RotateSpeed += -RotateAcc * Time.deltaTime;
                    if (RotateSpeed < 0.0f)
                        RotateSpeed += RotateAcc * Time.deltaTime;
                }
                break;
            case DoorStatus.Colliding:
                break;
        }

        //回転
        transform.Rotate(axisY, RotateSpeed * Mathf.Rad2Deg * Time.deltaTime);
    }

    private void LateUpdate()
    {
        Vector3 axisY = new Vector3(0, 1.0f, 0);
        //新しい位置の計算
        MyAngle = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
        if (MyAngle > Mathf.PI)
            MyAngle -= Mathf.PI * 2;

        //閉めるチェック
        if ((MyOldAngle > 0.0f && MyAngle <= 0.0f) || (MyOldAngle < 0.0f && MyAngle >= 0.0f))
        {
            Status = DoorStatus.Closed;
            transform.rotation = Quaternion.AngleAxis(0.0f, axisY);
            RotateSpeed = 0.0f;
        }

        //回転制限
        if (MyAngle > RotateAngleMax)
            transform.rotation = Quaternion.AngleAxis(RotateAngleMax * Mathf.Rad2Deg, axisY);
        if (MyAngle < -RotateAngleMax)
            transform.rotation = Quaternion.AngleAxis(-RotateAngleMax * Mathf.Rad2Deg, axisY);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Status = DoorStatus.Colliding;
    }

    private void OnCollisionStay(Collision collision)
    {
        //現在位置の計算
        float myAngle = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
        if (myAngle > Mathf.PI)
            myAngle -= Mathf.PI * 2;

        Vector3 myDir = new Vector3(Mathf.Cos(myAngle), 0f, -Mathf.Sin(myAngle));

        //回転速度セット
        if (Vector3.Cross(collision.GetContact(0).normal, myDir).y > 0)
        {
            RotateSpeed = -RotateSpeedMax;
        }
        else
        {
            RotateSpeed = RotateSpeedMax;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Status = DoorStatus.Swing;
    }
}
