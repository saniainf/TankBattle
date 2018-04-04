using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusShield : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        TankModifiers m_TankModifier;

        if (m_TankModifier = other.gameObject.GetComponent<TankModifiers>())
        {
            m_TankModifier.AddModifier(new ModifierShield());
        }
    }
}
