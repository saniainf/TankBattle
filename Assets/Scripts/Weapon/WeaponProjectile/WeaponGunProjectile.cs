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
        private ParticleSystem particleSystem;

        public override void SetupProjectile(int playerNumber, float playerVelocity, Transform fireTransform, ProjectileModificators projectileModificators)
        {
            base.SetupProjectile(playerNumber, playerVelocity, fireTransform, projectileModificators);

            if (weaponProjectile != null)
            {
                Rigidbody rigidbody = weaponProjectile.GetComponent<Rigidbody>();
                ProjectileBehaviour projectileBehaviour = weaponProjectile.GetComponent<ProjectileBehaviour>();
                projectileBehaviour.m_WeaponProjectile = this;
                projectileBehaviour.m_ActiveLayers = activeLayers;
                projectileBehaviour.m_ProjectileType = ProjectileType.PROJECTILE_COLLISON_ON_TRIGGER;
                if (playerVelocity > 0)
                    rigidbody.velocity = (startingSpeed + playerVelocity) * fireTransform.forward;
                else
                    rigidbody.velocity = startingSpeed * fireTransform.forward;
            }
        }

        public override void OnCollide(ProjectileBehaviour projectileBehaviour, Collider[] other)
        {
            PlayerHandler otherPlayer;
            Rigidbody otherRigidBody;

            for (int i = 0; i < other.Length; i++)
            {
                otherRigidBody = other[i].GetComponent<Rigidbody>();
                if (otherRigidBody)
                {
                    otherRigidBody.AddForce(projectileBehaviour.gameObject.transform.forward * explosionForce);
                }

                otherPlayer = other[i].GetComponent<PlayerHandler>();
                if (otherPlayer)
                {
                    if (otherPlayer.GetPlayerNumber() != playerNumber)
                    {
                        Debug.Log("impact" + otherPlayer.GetPlayerNumber());
                        otherPlayer.TakeDamage(Random.Range(minDamage, maxDamage));
                    }
                }
            }

            particleSystem = Instantiate(particleSystem, projectileBehaviour.gameObject.transform);
            particleSystem.gameObject.transform.parent = null;
            particleSystem.Play();
            Destroy(particleSystem.gameObject, particleSystem.main.duration);

            RemoveProjectile();
        }
    }
}