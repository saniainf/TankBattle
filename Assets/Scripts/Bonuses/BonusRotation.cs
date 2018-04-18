using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusRotation : MonoBehaviour
{
    private Rigidbody m_Rigidbody;
    float turnSpeed = 1.0f;

    private void OnEnable()
    {
        m_Rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Quaternion turnRotation = Quaternion.Euler(0, turnSpeed, 0);
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }
}
