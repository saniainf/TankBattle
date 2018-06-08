using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankBattle
{
    /// <summary>
    /// Управление вооружением игрока
    /// </summary>
    public class PlayerWeaponHandler : MonoBehaviour
    {
        [HideInInspector] public Weapon m_CurrentWeapon;
        [HideInInspector] public PlayerHandler m_PlayerHandler;

        /// <summary>
        /// Поменять оружие игрока
        /// </summary>
        /// <param name="weapon">оружие</param>
        public void SetWeapon(Weapon weapon)
        {
            if (m_CurrentWeapon != null)
                m_CurrentWeapon.RemoveWeapon();

            m_CurrentWeapon = weapon;
            m_CurrentWeapon.SetupWeapon(m_PlayerHandler, m_PlayerHandler.m_WeaponTransform);
        }

        public void WeaponButtonPress() { m_CurrentWeapon.WeaponButtonPress(); }

        public void WeaponButtonHold() { m_CurrentWeapon.WeaponButtonHold(); }

        public void WeaponButtonRelease() { m_CurrentWeapon.WeaponButtonRelease(); }

        private void Update() { m_CurrentWeapon.Update(); }
    }
}