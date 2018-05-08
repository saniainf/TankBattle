using UnityEngine;
using System.Collections;

namespace TankBattle
{
    [CreateAssetMenu(fileName = "Weapon Gun", menuName = "Player/Weapon/Gun")]
    public class WeponGun : Weapon
    {
        [SerializeField] private float atackSpeed = 0.2f;
        [SerializeField] private float launchForce = 30f;
        [SerializeField] private float energyCost = 25f;

        private bool reload = false;
        private float reloadTime = 0f;

        public override void WeaponButtonPress()
        {
            Fire();
        }

        private void Fire()
        {
            Rigidbody shellInstance = Instantiate(projectilePrefab, fireTransform.position, fireTransform.rotation).GetComponent<Rigidbody>();

            if (playerHandler.GetPlayerVelocity() > 0)
            {
                shellInstance.velocity = (launchForce + playerHandler.GetPlayerVelocity()) * fireTransform.forward;
            }
            else
            {
                shellInstance.velocity = launchForce * fireTransform.forward;
            }
        }
    }
}