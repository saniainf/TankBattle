using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankBattle
{
    public class GunTurretWeapon : MonoBehaviour, IWeapon
    {
        [HideInInspector] public PlayerHandler m_PlayerHandler { get; set; }

        public Transform m_FireTransform;
        public Rigidbody m_ProjectileRigidbody;

        public float m_AtackSpeed = 0.2f;
        public float m_EnergyCost = 25f;
        public float m_LaunchForce = 30f;

        private bool reload = false;
        private float reloadTime = 0f;

        void Update()
        {
            if (reload)
                reloadTime += Time.deltaTime;

            if (reloadTime > m_AtackSpeed && reload)
            {
                reloadTime -= m_AtackSpeed;
                reload = false;
            }
        }

        public void FireButtonPress()
        {

        }

        public void FireButtonHold()
        {
            if (!reload && m_PlayerHandler.GetEnergy() >= m_EnergyCost)
            {
                m_PlayerHandler.SetEnergy(-m_EnergyCost);
                Fire();
            }
        }

        public void FireButtonRelease()
        {

        }

        private void Fire()
        {
            Rigidbody shellInstance = Instantiate(m_ProjectileRigidbody, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

            if (m_PlayerHandler.GetPlayerVelocity() > 0)
            {
                shellInstance.velocity = (m_LaunchForce + m_PlayerHandler.GetPlayerVelocity()) * m_FireTransform.forward;
            }
            else
            {
                shellInstance.velocity = m_LaunchForce * m_FireTransform.forward;
            }

            reload = true;
        }
    }
}