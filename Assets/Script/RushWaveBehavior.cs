using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushWaveBehavior : MonoBehaviour
{
    public GameObject ReushPrefab;

    private int CountMax;
    private int Count;
    private float MoveTimer;

    void Awake()
    {
        CountMax = 4;
        Count = 0;
    }

    void setup(int countMax)
    {
        CountMax = countMax;
    }

    // Update is called once per frame
    void Update()
    {
        if(Count < CountMax)
        {
            if (MoveTimer > 0.3f)
            {
                GameObject rush = Instantiate(ReushPrefab,transform.position,Quaternion.identity,GameObject.Find("Stage00").transform);
                rush.GetComponent<RushBehavior>().setup(GameObject.FindGameObjectWithTag("Player").transform.position.y);
                Count++;
                MoveTimer = 0.0f;
            }
        }
        MoveTimer += Time.deltaTime;
    }
}
