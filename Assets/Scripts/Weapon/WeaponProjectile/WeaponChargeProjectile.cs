using UnityEngine;
using System.Collections;

namespace TankBattle
{
    [CreateAssetMenu(fileName = "Weapon Charge Projectile", menuName = "Player/Weapon/Projectile/Charge")]
    public class WeaponChargeProjectile : WeaponProjectile
    {
        [Header("Attributes")]
        [SerializeField]
        private float startingSpeed = 30f;
        [SerializeField] private float minDamage = 20f;
        [SerializeField] private float maxDamage = 30f;
        [SerializeField] private float explosionForce = 500f;
        [SerializeField] private float explosionRadius = 5f;
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

            Collider[] colliders = Physics.OverlapSphere(projectileBehaviour.transform.position, explosionRadius, activeLayers);
            PlayerHandler otherPlayer;
            Rigidbody otherRigidBody;

            for (int i = 0; i < colliders.Length; i++)
            {
                otherRigidBody = null;
                otherPlayer = null;

                otherRigidBody = other.GetComponent<Rigidbody>();
                if (otherRigidBody)
                {
                    otherRigidBody.AddExplosionForce(explosionForce, projectileBehaviour.gameObject.transform.position, explosionRadius);
                }

                otherPlayer = other.GetComponent<PlayerHandler>();
                if (otherPlayer)
                {
                    if (otherPlayer.GetPlayerNumber() != playerNumber)
                    {
                        otherPlayer.TakeDamage(CalculateDamage(projectileBehaviour.gameObject.transform.position, colliders[i].gameObject.transform.position));
                    }
                }
            }
            ParticleSystem particle = Instantiate(explosionEffect, projectileBehaviour.gameObject.transform);
            particle.gameObject.transform.parent = null;
            particle.Play();
            Destroy(particle.gameObject, particle.main.duration);

            RemoveProjectile();
        }

        private float CalculateDamage(Vector3 projectilePosiotion, Vector3 targetPosition)
        {
            float explosionDistance = (targetPosition - projectilePosiotion).magnitude;
            float relativeDistance = (explosionRadius - explosionDistance) / explosionRadius;
            float damage = relativeDistance * maxDamage;
            if (projectileModificators.ContainsKey(ProjectileModifierProperty.PROPERTY_DAMAGE_MULTIPLIER))
            {
                damage = damage * projectileModificators[ProjectileModifierProperty.PROPERTY_DAMAGE_MULTIPLIER];
            }
            return Mathf.Max(0f, damage); ;
        }
    }
}