using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierShield : Modifier
{
    private TankHealth m_TankHealth;

    private float timer;

    public ModifierShield()
    {
        attribute = ModifierAttribute.MODIFIER_ATTRIBUTE_SINGLE_PRIMARY;
        states = ModifierStates.MODIFIER_STATE_ATTACK_IMMUNE;
    }

    public override void OnEnable(GameObject tank)
    {
        m_TankHealth = tank.GetComponent<TankHealth>();
        m_TankHealth.m_FillImage.color = Color.gray;
        timer = 3f;
    }

    public override void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            m_TankHealth.m_FillImage.color = Color.Lerp(m_TankHealth.m_ZeroHealthColor, m_TankHealth.m_FullHealthColor, m_TankHealth.m_CurrentHealth / m_TankHealth.m_StartingHealth);
            ImDone = true;
        }
    }
}
