using UnityEngine;

namespace TankBattle
{
    public class ChargeTurretProjectile : MonoBehaviour
    {
        public LayerMask m_TankMask;
        public ParticleSystem m_ExplosionParticles;
        public AudioSource m_ExplosionAudio;
        public float m_MaxDamage = 100f;
        public float m_ExplosionForce = 1000f;
        public float m_MaxLifeTime = 2f;
        public float m_ExplosionRadius = 5f;

        private Transform m_transform;

        private void Start()
        {
            m_transform = gameObject.GetComponent<Transform>();
            Destroy(gameObject, m_MaxLifeTime);
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 8 || other.gameObject.layer == 9)
            {
                // Find all the tanks in an area around the shell and damage them.
                Collider[] colliders = Physics.OverlapSphere(m_transform.position, m_ExplosionRadius, m_TankMask);
                Rigidbody targetRigitbody;
                //TankHealth targetHealth;
                PlayerHandler playerHandler;

                for (int i = 0; i < colliders.Length; i++)
                {
                    targetRigitbody = null;
                    playerHandler = null;

                    targetRigitbody = colliders[i].gameObject.GetComponent<Rigidbody>();
                    if (!targetRigitbody)
                        continue;

                    targetRigitbody.AddExplosionForce(m_ExplosionForce, m_transform.position, m_ExplosionRadius);

                    playerHandler = targetRigitbody.gameObject.GetComponent<PlayerHandler>();
                    if (!playerHandler)
                        continue;

                    float damage = CalculateDamage(targetRigitbody.position);
                    playerHandler.TakeDamage(damage);
                }

                m_ExplosionParticles.gameObject.transform.parent = null;
                m_ExplosionParticles.Play();
                m_ExplosionAudio.Play();
                Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.main.duration);
                Destroy(gameObject);
            }
        }


        private float CalculateDamage(Vector3 targetPosition)
        {
            // Calculate the amount of damage a target should take based on it's position.
            Vector3 explosionToTarget = targetPosition - m_transform.position;

            float explosionDistance = explosionToTarget.magnitude;
            float relativeDistance = (m_ExplosionRadius - explosionDistance) / m_ExplosionRadius;
            float damage = relativeDistance * m_MaxDamage;
            damage = Mathf.Max(0f, damage);
            return damage;
        }
    }
}