using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankBattle
{
    public class GunTurretProjectile : MonoBehaviour
    {
        public LayerMask PlayerLayer;
        public ParticleSystem ExplosionParticles;
        public AudioSource ExplosionAudio;
        public float MinDamage = 70f;
        public float MaxDamage = 100f;
        public float ExplosionForce = 200f;
        public float ExplosionRadius = 1f;
        public float MaxLifeTime = 3f;

        private Transform projectileTransform;

        private void Start()
        {
            projectileTransform = gameObject.GetComponent<Transform>();
            Destroy(gameObject, MaxLifeTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            Rigidbody targetRigitbody;
            //TankHealth targetHealth;
            PlayerHandler playerHandler;

            targetRigitbody = other.gameObject.GetComponent<Rigidbody>();
            if (targetRigitbody)
            {
                targetRigitbody.AddExplosionForce(ExplosionForce, projectileTransform.position, ExplosionRadius);
            }

            playerHandler = other.gameObject.GetComponent<PlayerHandler>();
            if (playerHandler)
            {
                float damage = Random.Range(MinDamage, MaxDamage);
                playerHandler.TakeDamage(damage);
            }

            ExplosionParticles.gameObject.transform.parent = null;
            ExplosionParticles.Play();
            ExplosionAudio.Play();
            Destroy(ExplosionParticles.gameObject, ExplosionParticles.main.duration);
            Destroy(gameObject);
        }
    }
}
