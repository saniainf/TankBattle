using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusShield : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ModifiersManager m_TankModifier;

        if (m_TankModifier = other.gameObject.GetComponent<ModifiersManager>())
        {
            m_TankModifier.AddModifier(new ModifierShield());
            gameObject.SetActive(false);
        }
    }
}
