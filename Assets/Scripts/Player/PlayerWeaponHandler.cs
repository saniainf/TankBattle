using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankBattle
{
    public class PlayerWeaponHandler : MonoBehaviour
    {
        [HideInInspector] public Weapon m_CurrentWeapon;
        [HideInInspector] public PlayerHandler m_PlayerHandler;

        public void WeaponButtonPress() { m_CurrentWeapon.WeaponButtonPress(); }

        public void WeaponButtonHold() { m_CurrentWeapon.WeaponButtonHold(); }

        public void WeaponButtonRelease() { m_CurrentWeapon.WeaponButtonRelease(); }

        public void SetWeapon(Weapon weapon)
        {
            m_CurrentWeapon = weapon;
            m_CurrentWeapon.m_PlayerHandler = m_PlayerHandler;
            m_CurrentWeapon.SetupWeapon(m_PlayerHandler.m_WeaponTransform);
        }
    }
}