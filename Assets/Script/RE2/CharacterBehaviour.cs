using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    CapsuleCollider SensorCollider;
    // Start is called before the first frame update
    void Start()
    {
        SensorCollider = GetComponentInChildren<CapsuleCollider>();
    }

    private void OnTriggerEnter(Collider colliInfo)
    {
        if(!colliInfo.isTrigger)
        {
            return;
        }
        Debug.Log("in");
    }

    private void OnTriggerStay(Collider colliInfo)
    {
        if (!colliInfo.isTrigger)
        {
            return;
        }
    }

    private void OnTriggerExit(Collider colliInfo)
    {
        if (!colliInfo.isTrigger)
        {
            return;
        }
        Debug.Log("out");
    }


    // Update is called once per frame
    void Update()
    {
    }
}
