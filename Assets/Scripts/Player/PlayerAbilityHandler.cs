using UnityEngine;
using System.Collections;

namespace TankBattle
{
    public class PlayerAbilityHandler : MonoBehaviour
    {
        [HideInInspector] public Ability m_CurrentAbility;
        [HideInInspector] public PlayerHandler m_PlayerHandler;

        public void AbilityButtonPress() { m_CurrentAbility.AbilityButtonPress(); }

        public void AbilityButtonHold() { m_CurrentAbility.AbilityButtonHold(); }

        public void AbilityButtonRelease() { m_CurrentAbility.AbilityButtonRelease(); }

        public void SetAbility(Ability ability)
        {
            m_CurrentAbility = ability;
            m_CurrentAbility.m_PlayerHandler = m_PlayerHandler;
        }
    }
}