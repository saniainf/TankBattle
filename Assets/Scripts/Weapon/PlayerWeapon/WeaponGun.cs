using UnityEngine;
using System.Collections;

namespace TankBattle
{
    [CreateAssetMenu(fileName = "Weapon Gun", menuName = "Player/Weapon/Gun")]
    public class WeaponGun : Weapon
    {
        [Header("Attributes")]
        [SerializeField]
        private float atackSpeed = 0.2f;
        [SerializeField] private float energyCost = 25f;

        private bool reload = false;
        private float reloadTime = 0f;

        public override void Update()
        {
            if (reload)
                reloadTime += Time.deltaTime;

            if (reloadTime > atackSpeed && reload)
            {
                reloadTime -= atackSpeed;
                reload = false;
            }
        }

        public override void WeaponButtonHold()
        {
            if (!reload && playerHandler.GetEnergy() >= energyCost)
            {
                playerHandler.SetEnergy(-energyCost);
                Fire();
            }
        }

        private void Fire()
        {
            Instantiate(projectile).SetupProjectile(playerHandler.GetPlayerNumber(), playerHandler.GetPlayerVelocity(), fireTransform, new ProjectileModificators());
            reload = true;
        }
    }
}