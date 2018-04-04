using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierShield : IModifier
{
    public bool ImDone { get; set; }

    private ModifierBehavior behavior = ModifierBehavior.MODIFIER_BEHAVIOR_SINGLE_PRIMARY;
    private ModifierStates states = ModifierStates.MODIFIER_STATE_ATTACK_IMMUNE;

    private TankHealth m_TankHealth;

    private float timer;

    public void OnEnable(GameObject tank)
    {
        m_TankHealth = tank.GetComponent<TankHealth>();
        m_TankHealth.m_FillImage.color = Color.gray;
        timer = 3f;
    }

    public void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            m_TankHealth.m_FillImage.color = Color.Lerp(m_TankHealth.m_ZeroHealthColor, m_TankHealth.m_FullHealthColor, m_TankHealth.m_CurrentHealth / m_TankHealth.m_StartingHealth);
            ImDone = true;
        }
    }

    public void OnDisable()
    {
        
    }

    public ModifierStates GetModifierStates()
    {
        return states;
    }

    public ModifierBehavior GetModifierBehavior()
    {
        return behavior;
    }
}
