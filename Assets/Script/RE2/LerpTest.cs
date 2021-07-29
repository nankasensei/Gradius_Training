using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTest : MonoBehaviour
{
    private const float AttackLerpTime = 5.0f;
    private float AttackLerpTimer;
    private Vector3 AttackStartPos;
    private Vector3 AttackEndPos;
    // Start is called before the first frame update
    void Start()
    {
        AttackLerpTimer = 0.0f;
        AttackStartPos = transform.position;
        AttackEndPos = AttackStartPos + new Vector3(5.0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        AttackLerpTimer += Time.deltaTime;
        transform.position = Vector3.Lerp(AttackStartPos, AttackEndPos, AttackLerpTimer/ AttackLerpTime);
    }
}
