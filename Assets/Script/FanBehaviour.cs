using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanBehaviour : MonoBehaviour
{
    public float Timer;
    private int Flag;
    private Vector3 OldPos;
    private Vector3 SpeedDir;
    private float SpeedScale;
    // Start is called before the first frame update
    void Start()
    {
        Timer = 0.0f;
        Flag = 0;
        SpeedDir = new Vector3(-1.0f, 0, 0);
        SpeedScale = 15f;
    }

    // Update is called once per frame
    void Update()
    {
        OldPos = transform.position;

    }

    void LateUpdate()
    {
        transform.position += Time.deltaTime * SpeedDir * SpeedScale;

        Vector3 playerPos = GameObject.Find("Player").transform.position;
        switch (Flag)
        {
            case 0:
                if (Timer > 1.2f)
                {
                    if (transform.position.z > playerPos.z)
                        SpeedDir = new Vector3(1.0f, 0, -1.0f).normalized;
                    else
                        SpeedDir = new Vector3(1.0f, 0, 1.0f).normalized;
                    Flag++;
                }
                break;
            case 1:
                if (Timer > 1.4f)
                {
                    SpeedDir = new Vector3(-1.0f, 0, 0f).normalized;
                    Flag++;
                }
                break;
            case 2:
                if (Timer > 2.0f)
                {
                    if (transform.position.z > playerPos.z)
                        SpeedDir = new Vector3(1.0f, 0, -1.0f).normalized;
                    else
                        SpeedDir = new Vector3(1.0f, 0, 1.0f).normalized;
                    Flag++;
                }
                break;
            case 3:
                if ((OldPos.z > playerPos.z && transform.position.z < playerPos.z) || (OldPos.z < playerPos.z && transform.position.z > playerPos.z) || transform.position.z == playerPos.z)
                {
                    Debug.Log("TRUE!!");
                    SpeedDir = new Vector3(1.0f, 0, 0).normalized;
                    Flag++;
                }
                break;
        }

        Timer += Time.deltaTime;
    }
}
