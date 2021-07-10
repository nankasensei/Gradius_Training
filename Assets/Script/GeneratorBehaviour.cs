using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorBehaviour : MonoBehaviour
{
    public GameObject FanPrefab;
    private int Count;
    private float Timer;
    private List<GameObject> Fans;

    // Start is called before the first frame update
    void Start()
    {
        Count = 0;
        Timer = 0;
        Fans = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Count < 6)
        {
            if(Timer > 0.1f)
            {
                GameObject fan = GameObject.Instantiate(FanPrefab, transform);
                Fans.Add(fan);
                Count++;
                Timer = 0;
            }
        }

        Timer += Time.deltaTime;
    }
}
