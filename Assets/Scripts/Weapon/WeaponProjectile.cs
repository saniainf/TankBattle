using UnityEngine;
using System.Collections;

namespace TankBattle
{
    public abstract class WeaponProjectile : ScriptableObject
    {
        [SerializeField] protected GameObject projectilePrefab;
        [SerializeField] private float maxLifeTime = 5f;
        protected int playerNumber;
        protected GameObject weaponProjectile;
        protected Transform fireTransform;

        public virtual void SetupProjectile(int playerNumber, float playerVelocity, Transform fireTransform)
        {
            this.playerNumber = playerNumber;
            this.fireTransform = fireTransform;
            if (projectilePrefab != null)
                weaponProjectile = Instantiate(projectilePrefab, fireTransform.position, fireTransform.rotation);
            Destroy(weaponProjectile, maxLifeTime);
            Destroy(this, maxLifeTime);
        }

        public virtual void OnImpact(ProjectileBehaviour projectileBehaviour, Collider[] other) { }

        public virtual void RemoveProjectile()
        {
            if (weaponProjectile != null)
                Destroy(weaponProjectile);
            Destroy(this);
        }
    }
}