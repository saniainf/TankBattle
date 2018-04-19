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

        private Rigidbody PlayerRigidbody;
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
            PlayerRigidbody = playerHandler.gameObject.GetComponent<Rigidbody>();
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

            var fwdSpeed = Vector3.Dot(PlayerRigidbody.velocity, transform.forward);
            if (fwdSpeed > 0)
            {
                shellInstance.velocity = (LaunchForce * FireTransform.forward) + PlayerRigidbody.velocity;
            }
            else
            {
                shellInstance.velocity = (LaunchForce * FireTransform.forward);
            }
            reload = true;
        }
    }
}