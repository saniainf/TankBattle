using UnityEngine;
using System.Collections;

namespace TankBattle
{
    [CreateAssetMenu(fileName = "Weapon Gun Projectile", menuName = "Player/Weapon/Projectile/Gun")]
    public class WeaponGunProjectile : WeaponProjectile
    {
        [SerializeField] private float launchForce = 30f;
        [SerializeField] private float minDamage = 20f;
        [SerializeField] private float maxDamage = 30f;
        [SerializeField] private float explosionForce = 200f;
        [SerializeField] private LayerMask activeLayers;

        public override void SetupProjectile(int playerNumber, float playerVelocity, Transform fireTransform)
        {
            base.SetupProjectile(playerNumber, playerVelocity, fireTransform);

            if (weaponProjectile != null)
            {
                Rigidbody rigidbody = weaponProjectile.GetComponent<Rigidbody>();
                ProjectileBehaviour projectileBehaviour = weaponProjectile.GetComponent<ProjectileBehaviour>();
                projectileBehaviour.m_WeaponProjectile = this;
                projectileBehaviour.m_ActiveLayers = activeLayers;
                if (playerVelocity > 0)
                    rigidbody.velocity = (launchForce + playerVelocity) * fireTransform.forward;
                else
                    rigidbody.velocity = launchForce * fireTransform.forward;
            }
        }

        public override void OnImpact(ProjectileBehaviour projectileBehaviour, Collider[] other)
        {
            for (int i = 0; i < other.Length; i++)
            {
                PlayerHandler otherPlayer = other[i].GetComponent<PlayerHandler>();
                if (otherPlayer)
                {
                    if (otherPlayer.m_PlayerAttributes.PlayerNumber != playerNumber)
                    {
                        Debug.Log("impact" + otherPlayer.m_PlayerAttributes.PlayerNumber);
                        otherPlayer.TakeDamage(10f);
                        //player.GetComponent<Rigidbody>().AddExplosionForce(1000f, abilityProjectile.transform.position, 20f);
                    }
                }
            }
            RemoveProjectile();
        }
    }
}