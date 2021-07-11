using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraBehaviour : MonoBehaviour
{
    public const float MoveSpeed = 1.0f;
    private Vector3 Speed;
    // Start is called before the first frame update
    void Start()
    {
        Speed = new Vector3(1.0f, 0f, 0f) * MoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Speed * Time.deltaTime;
    }
}
