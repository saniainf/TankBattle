using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TankBattle
{
    public class ChargeTurretWeapon : MonoBehaviour, IWeapon
    {
        public Rigidbody Projectile;
        public Transform FireTransform;
        public Slider AimSlider;

        public float MinLaunchForce = 15f;
        public float MaxLaunchForce = 30f;
        public float MaxChargeTime = 0.75f;
        public float AtackSpeed = 0.5f;
        public float EnergyCost = 10f;

        [HideInInspector] public PlayerHandler playerHandler { get; set; }

        private bool charge;
        private bool reload;
        private float currentLaunchForce;
        private float chargeSpeed;
        private float reloadTime;

        void Start()
        {
            chargeSpeed = (MaxLaunchForce - MinLaunchForce) / MaxChargeTime;

            currentLaunchForce = MinLaunchForce;
            AimSlider.value = AimSlider.minValue;

            charge = false;
            reload = false;
        }

        void Update()
        {
            if (reload)
                reloadTime -= Time.deltaTime;
            if (reloadTime < 0)
            {
                reloadTime = AtackSpeed;
                reload = false;
            }
        }

        public void FireButtonPress()
        {
            if (!charge && !reload && playerHandler.GetEnergy() >= EnergyCost)
            {
                charge = true;
                currentLaunchForce = MinLaunchForce;
                AimSlider.value = AimSlider.minValue;
                playerHandler.SetEnergy(-EnergyCost);
            }
        }

        public void FireButtonHold()
        {
            if (charge)
            {
                currentLaunchForce += chargeSpeed * Time.deltaTime;

                if (currentLaunchForce >= MaxLaunchForce)
                {
                    currentLaunchForce = MaxLaunchForce;
                    Fire();
                    return;
                }

                AimSlider.value = (currentLaunchForce - MinLaunchForce) / (MaxLaunchForce - MinLaunchForce);
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
            Rigidbody shellInstance = Instantiate(Projectile, FireTransform.position, FireTransform.rotation) as Rigidbody;
            shellInstance.velocity = currentLaunchForce * FireTransform.forward;

            currentLaunchForce = MinLaunchForce;
            AimSlider.value = AimSlider.minValue;
            charge = false;
            reload = true;
        }
    }
}
