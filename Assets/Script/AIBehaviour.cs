using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour : MonoBehaviour
{
    //public Transform Target;
    public PlayerBehavior pb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(pb.LastPos() != Vector3.zero)
            //transform.position = pb.LastPos();
        //if((Target.position-transform.position).magnitude > 2.0f)
        //{
        //    Vector3 dir = (Target.position - transform.position).normalized;
        //    float magnitude = (Target.position - transform.position).magnitude - 2.0f;
        //    transform.position += dir * magnitude;
        //}
    }
}
