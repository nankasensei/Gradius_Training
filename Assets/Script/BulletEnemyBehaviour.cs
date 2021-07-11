using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyBehaviour : MonoBehaviour
{
    public const float MoveSpeed = 3.0f;
    private Vector3 targetDir;
    private Vector3 Speed;

    private void Awake()
    {
        Speed = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 speedWorld = Speed + new Vector3(MainCameraBehaviour.MoveSpeed, 0f, 0f);
        transform.position += speedWorld * Time.deltaTime;
    }

    public void setup(Vector3 targetDir)
    {
        Speed = targetDir * MoveSpeed;
    }
}
