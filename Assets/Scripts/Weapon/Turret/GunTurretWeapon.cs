using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankBattle
{
    public class GunTurretWeapon : MonoBehaviour, IWeapon
    {
        [HideInInspector]public PlayerHandler playerHandler { get; set; }

        public Rigidbody Projectile;
        public Transform FireTransform;

        public float AtackSpeed = 0.2f;
        public float EnergyCost = 3f;
        public float LaunchForce = 30f;

        private bool reload;
        private float reloadTime;

        public void FireButtonHold()
        {
            if (!reload && playerHandler.GetEnergy() >= EnergyCost)
            {
                playerHandler.SetEnergy(-EnergyCost);
                Fire();
            }
        }

        public void FireButtonPress()
        {
            
        }

        public void FireButtonRelease()
        {

        }

        void Start()
        {
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

        private void Fire()
        {
            Rigidbody shellInstance = Instantiate(Projectile, FireTransform.position, FireTransform.rotation) as Rigidbody;
            shellInstance.velocity = LaunchForce * FireTransform.forward;

            reload = true;
        }
    }
}