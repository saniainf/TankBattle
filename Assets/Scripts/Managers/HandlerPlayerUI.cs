using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlerPlayerUI : MonoBehaviour
{
    [HideInInspector] public float PlayerHealth;

    private TankHealth m_TankHealth;

    private void Start()
    {
        m_TankHealth = gameObject.GetComponent<TankHealth>();
    }

    private void Update()
    {
        PlayerHealth = m_TankHealth.m_CurrentHealth;
    }
}
