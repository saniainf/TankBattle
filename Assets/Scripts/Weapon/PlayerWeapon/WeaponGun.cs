using UnityEngine;
using System.Collections;

namespace TankBattle
{
    [CreateAssetMenu(fileName = "Weapon Gun", menuName = "Player/Weapon/Gun")]
    public class WeaponGun : Weapon
    {
        [SerializeField] private float atackSpeed = 0.2f;
        [SerializeField] private float launchForce = 30f;
        [SerializeField] private float energyCost = 25f;

        [SerializeField] private float minDamage = 20f;
        [SerializeField] private float maxDamage = 30f;
        [SerializeField] private float explosionForce = 200f;
        [SerializeField] private LayerMask activeLayers;

        [SerializeField] private float maxLifeTime = 5f;

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
            Rigidbody shellInstance = Instantiate(projectilePrefab, fireTransform.position, fireTransform.rotation).GetComponent<Rigidbody>();

            WeaponProjectile weaponProjectile = shellInstance.GetComponent<WeaponProjectile>();
            weaponProjectile.m_ParentWeapon = this;
            weaponProjectile.m_ActiveLayers = activeLayers;

            Destroy(shellInstance.gameObject, maxLifeTime);

            if (playerHandler.GetPlayerVelocity() > 0)
            {
                shellInstance.velocity = (launchForce + playerHandler.GetPlayerVelocity()) * fireTransform.forward;
            }
            else
            {
                shellInstance.velocity = launchForce * fireTransform.forward;
            }
            reload = true;
        }

        public override void OnImpact(WeaponProjectile weaponProjectile, Collider[] other)
        {
            for (int i = 0; i < other.Length; i++)
            {
                PlayerHandler otherPlayer = other[i].GetComponent<PlayerHandler>();
                if (otherPlayer)
                {
                    if (otherPlayer.m_PlayerAttributes.PlayerNumber != playerHandler.m_PlayerAttributes.PlayerNumber)
                    {
                        Debug.Log("impact" + otherPlayer.m_PlayerAttributes.PlayerNumber);
                        otherPlayer.TakeDamage(10f);
                        //player.GetComponent<Rigidbody>().AddExplosionForce(1000f, abilityProjectile.transform.position, 20f);
                    }
                }
            }
            Destroy(weaponProjectile.gameObject);
        }
    }
}