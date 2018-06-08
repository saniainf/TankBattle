using UnityEngine;

namespace TankBattle
{
    [CreateAssetMenu(fileName = "Weapon Gun Projectile", menuName = "Player/Weapon/Projectile/Gun")]
    public class WeaponGunProjectile : WeaponProjectile
    {
        [Header("Attributes")]
        [SerializeField]
        private float startingSpeed = 30f;
        [SerializeField] private float minDamage = 20f;
        [SerializeField] private float maxDamage = 30f;
        [SerializeField] private float explosionForce = 200f;
        [SerializeField] private LayerMask activeLayers;
        [Header("Effects")]
        [SerializeField]
        private ParticleSystem explosionEffect;

        public override void SetupProjectile(int playerNumber, float playerVelocity, Transform fireTransform, ProjectileModificators projectileModificators = null)
        {
            base.SetupProjectile(playerNumber, playerVelocity, fireTransform, projectileModificators);

            if (projectileInstance != null)
            {
                Vector3 projectileVelocity;
                if (playerVelocity > 0)
                    projectileVelocity = (startingSpeed + playerVelocity) * fireTransform.forward;
                else
                    projectileVelocity = startingSpeed * fireTransform.forward;
                ProjectileBehaviour projectileBehaviour = projectileInstance.GetComponent<ProjectileBehaviour>();

                projectileBehaviour.SetupProjectileBehaviour(this, projectileVelocity, ProjectileCallback.PROJECTILE_CALLBACK_ON_IMPACT);
            }
        }

        public override void ProjectileOnImpact(ProjectileBehaviour projectileBehaviour, Collider other)
        {
            if (!activeLayers.HasLayer(other.gameObject.layer))
                return;

            PlayerHandler otherPlayer;
            Rigidbody otherRigidBody;

            otherRigidBody = other.GetComponent<Rigidbody>();
            if (otherRigidBody)
            {
                otherRigidBody.AddForce(projectileBehaviour.gameObject.transform.forward * explosionForce);
            }

            otherPlayer = other.GetComponent<PlayerHandler>();
            if (otherPlayer)
            {
                if (otherPlayer.GetPlayerNumber() != playerNumber)
                {
                    otherPlayer.TakeDamage(Random.Range(minDamage, maxDamage));
                }
            }

            ParticleSystem particle = Instantiate(explosionEffect, projectileBehaviour.gameObject.transform);
            particle.gameObject.transform.parent = null;
            particle.Play();
            Destroy(particle.gameObject, particle.main.duration);

            RemoveProjectile();
        }
    }
}