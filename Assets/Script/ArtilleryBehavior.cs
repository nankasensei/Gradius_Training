using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryBehavior : MonoBehaviour
{
    public GameObject BulletEnemyPrefab;
    private float MoveTimer;

    // Start is called before the first frame update
    void Start()
    {
        MoveTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = GameObject.Find("Player").transform.position;

        if (MoveTimer > 1.0f)
        {
            GameObject bullet = Instantiate(BulletEnemyPrefab, transform.position ,Quaternion.identity,GameObject.Find("Stage00").transform);

            Vector3 targetDir = (playerPos - transform.position).normalized;
            bullet.GetComponent<BulletEnemyBehaviour>().setup(targetDir);
            MoveTimer = 0.0f;
        }
    }

    private void LateUpdate()
    {
        MoveTimer += Time.deltaTime;
    }

    //”pŠü
    private Vector3 preTargetDir(Vector3 targetPos, Vector3 shooterPos, float moveSpeedTarget, float moveSpeedBullet)
    {
        float a = Mathf.Pow(moveSpeedTarget, 2) - Mathf.Pow(moveSpeedBullet, 2);
        float b = 2 * moveSpeedTarget * (targetPos.x - shooterPos.x);
        float c = Mathf.Pow(targetPos.y - shooterPos.y, 2) + Mathf.Pow(targetPos.x - shooterPos.x, 2);

        float t;
        if ( a !=0 )
        {
            float t1 = (-b + Mathf.Sqrt(b * b - 4.0f * a * c)) / (2.0f * a);
            float t2 = (-b - Mathf.Sqrt(b * b - 4.0f * a * c)) / (2.0f * a);
            t = t2;
        }
        else
        {
            t = -c / b;
        }

        float dirX = (targetPos.x - shooterPos.x + moveSpeedTarget * t) / (moveSpeedBullet * t);
        float dirY = (targetPos.y - shooterPos.y) / (moveSpeedBullet * t);

        Vector3 dir = new Vector3(dirX, dirY, 0);
        return dir;
    }
}
