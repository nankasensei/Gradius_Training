using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public Transform child1;
    public Transform child2;
    public Transform child3;
    private Queue<MyNode> nodes;
    private float timer;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    // Start is called before the first frame update
    void Start()
    {
        nodes = new Queue<MyNode>();
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0f || Mathf.Abs(Input.GetAxis("Vertical")) > 0f)
        {
            MyNode node = new MyNode();
            node.pos = transform.position;
            node.time = timer;
            nodes.Enqueue(node);
            while((timer - nodes.ToArray()[0].time) > 0.8f)
            {
                nodes.Dequeue();
            }
            foreach (var i in nodes)
            {
                if ((timer - i.time) <= 0.267f)
                {
                    child1.position = i.pos;
                    break;
                }
            }
            foreach (var i in nodes)
            {
                if ((timer - i.time) <= 0.533f)
                {
                    child2.position = i.pos;
                    break;
                }
            }
            child3.position = nodes.ToArray()[0].pos;

            timer += Time.deltaTime;
            Debug.Log(nodes.ToArray()[0].pos);
        }
    }
}

struct MyNode
{
    public Vector3 pos;
    public float time;
};