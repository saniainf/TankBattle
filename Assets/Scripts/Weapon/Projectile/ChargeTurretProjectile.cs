using UnityEngine;

namespace TankBattle
{
    public class ChargeTurretProjectile : MonoBehaviour
    {
        public LayerMask m_ActiveLayers;
        public ParticleSystem m_ExplosionParticles;
        public AudioSource m_ExplosionAudio;
        public float m_MaxDamage = 100f;
        public float m_ExplosionForce = 500f;
        public float m_MaxLifeTime = 3f;
        public float m_ExplosionRadius = 5f;

        private Transform projectileTransform;

        private void Start()
        {
            projectileTransform = gameObject.GetComponent<Transform>();
            Destroy(gameObject, m_MaxLifeTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            Collider[] colliders = Physics.OverlapSphere(projectileTransform.position, m_ExplosionRadius, m_ActiveLayers);

            Rigidbody targetRigitbody;
            PlayerHandler playerHandler;

            for (int i = 0; i < colliders.Length; i++)
            {
                targetRigitbody = null;
                playerHandler = null;

                targetRigitbody = colliders[i].gameObject.GetComponent<Rigidbody>();
                if (!targetRigitbody)
                    continue;

                targetRigitbody.AddExplosionForce(m_ExplosionForce, projectileTransform.position, m_ExplosionRadius);

                playerHandler = targetRigitbody.gameObject.GetComponent<PlayerHandler>();
                if (!playerHandler)
                    continue;

                playerHandler.TakeDamage(CalculateDamage(targetRigitbody.position));
            }

            m_ExplosionParticles.gameObject.transform.parent = null;
            m_ExplosionParticles.Play();
            m_ExplosionAudio.Play();
            Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.main.duration);
            Destroy(gameObject);
        }

        private float CalculateDamage(Vector3 targetPosition)
        {
            float explosionDistance = (targetPosition - projectileTransform.position).magnitude;
            float relativeDistance = (m_ExplosionRadius - explosionDistance) / m_ExplosionRadius;
            float damage = relativeDistance * m_MaxDamage;
            return Mathf.Max(0f, damage); ;
        }
    }
}