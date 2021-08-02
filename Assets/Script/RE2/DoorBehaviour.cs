using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    public enum DoorStatus
    {
        Closed,
        Closing,//�����ߒ�
        Swing, //�����ߒ�
        Colliding,
    }

    public const float RotateSpeedMax = Mathf.PI / 6.0f;
    public const float RotateAngleMax = Mathf.PI / 2.0f;
    public float RotateSpeed;
    public float MyOldAngle;
    public float MyAngle;
    private bool IsColliding;

    // Start is called before the first frame update
    void Start()
    {
        RotateSpeed = 0.0f;
        IsColliding = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 axisY = new Vector3(0, 1.0f, 0);

        //�Â��ʒu�̌v�Z
        MyOldAngle = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
        if (MyOldAngle > Mathf.PI)
            MyOldAngle -= Mathf.PI * 2;

        //��苗�����ꂽ�玩���߂�
        if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position) > 1.5f)
        {
            if (MyOldAngle > 0.0f)
                RotateSpeed = -RotateSpeedMax;
            if (MyOldAngle < 0.0f)
                RotateSpeed = RotateSpeedMax;
        }

        //��]
        transform.Rotate(axisY, RotateSpeed * Mathf.Rad2Deg * Time.deltaTime);
    }

    private void LateUpdate()
    {
        Vector3 axisY = new Vector3(0, 1.0f, 0);
        //�V�����ʒu�̌v�Z
        MyAngle = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
        if (MyAngle > Mathf.PI)
            MyAngle -= Mathf.PI * 2;

        //�߂�
        if ((MyOldAngle > 0.0f && MyAngle <= 0.0f) || (MyOldAngle < 0.0f && MyAngle >= 0.0f))
        {
            transform.rotation = Quaternion.AngleAxis(0.0f, axisY);
            RotateSpeed = 0.0f;
        }

        //��]����
        if (MyAngle > RotateAngleMax)
            transform.rotation = Quaternion.AngleAxis(RotateAngleMax * Mathf.Rad2Deg, axisY);
        if (MyAngle < -RotateAngleMax)
            transform.rotation = Quaternion.AngleAxis(-RotateAngleMax * Mathf.Rad2Deg, axisY);
    }

    private void OnCollisionEnter(Collision collision)
    {
        IsColliding = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        //���݈ʒu�̌v�Z
        float myAngle = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
        if (myAngle > Mathf.PI)
            myAngle -= Mathf.PI * 2;

        Vector3 myDir = new Vector3(Mathf.Cos(myAngle), 0f, -Mathf.Sin(myAngle));

        //��]���x�Z�b�g
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
        IsColliding = false;
        RotateSpeed = 0.0f;
    }
}
