using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushBehavior : MonoBehaviour
{
    private float MainTimer;
    private float TargetY;
    private int TimeFlag;
    private float MoveSpeed;
    private Vector3 Speed;
    private Vector3 OldPos;

    // Start is called before the first frame update
    void Awake()
    {
        MoveSpeed = 2.0f;
        TimeFlag = 0;
        TargetY = 0;
        Speed = new Vector3(0f, 1.0f, 0f);
    }

    public void setup(float targetY)
    {
        TargetY = targetY;
    }

    // Update is called once per frame
    void Update()
    {
        OldPos = transform.position;

        transform.position += Speed * Time.deltaTime * MoveSpeed;

        switch (TimeFlag)
        {
            case 0:
                if((OldPos.y < TargetY && transform.position.y >= TargetY)|| (OldPos.y > TargetY && transform.position.y <= TargetY))
                {
                    Speed = new Vector3(-1.0f, 0.0f, 0f); ;
                    TimeFlag++;
                }
                break;
            case 1:
                break;
        }
    }
}
