using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TankBattle
{
    public class ChargeTurretWeapon : MonoBehaviour, IWeapon
    {
        public Transform m_FireTransform;
        public Rigidbody m_ProjectileRigidbody;
        public Slider m_ChargeSlider;

        public float m_MinLaunchForce = 15f;
        public float m_MaxLaunchForce = 30f;
        public float m_MaxChargeTime = 0.75f;
        public float m_AtackSpeed = 0.5f;
        public float m_EnergyCost = 10f;

        [HideInInspector] public PlayerHandler m_PlayerHandler { get; set; }

        private bool charge = false;
        private bool reload = false;
        private float currentLaunchForce;
        private float chargeSpeed;
        private float reloadTime = 0f;

        void Start()
        {
            chargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;

            currentLaunchForce = m_MinLaunchForce;
            m_ChargeSlider.value = m_ChargeSlider.minValue;
        }

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
            if (!charge && !reload && m_PlayerHandler.GetEnergy() >= m_EnergyCost)
            {
                charge = true;
                currentLaunchForce = m_MinLaunchForce;
                m_ChargeSlider.value = m_ChargeSlider.minValue;
                m_PlayerHandler.SetEnergy(-m_EnergyCost);
            }
        }

        public void FireButtonHold()
        {
            if (charge)
            {
                currentLaunchForce += chargeSpeed * Time.deltaTime;

                if (currentLaunchForce >= m_MaxLaunchForce)
                {
                    currentLaunchForce = m_MaxLaunchForce;
                    Fire();
                    return;
                }

                m_ChargeSlider.value = (currentLaunchForce - m_MinLaunchForce) / (m_MaxLaunchForce - m_MinLaunchForce);
            }
        }

        public void FireButtonRelease()
        {
            if (charge)
            {
                Fire();
            }
        }

        private void Fire()
        {
            Rigidbody shellInstance = Instantiate(m_ProjectileRigidbody, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;
            shellInstance.velocity = currentLaunchForce * m_FireTransform.forward;

            currentLaunchForce = m_MinLaunchForce;
            m_ChargeSlider.value = m_ChargeSlider.minValue;
            charge = false;
            reload = true;
        }
    }
}
