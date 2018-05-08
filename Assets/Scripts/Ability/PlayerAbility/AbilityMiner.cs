using UnityEngine;
using System.Collections;

namespace TankBattle
{
    [CreateAssetMenu(fileName = "Ability Miner", menuName = "Player/Ability/Miner")]
    public class AbilityMiner : Ability
    {
        [SerializeField] private float mineDamage = 30f;
        [SerializeField] private Rigidbody minePrefab;

        public override void AbilityButtonPress()
        {
            Vector3 force = new Vector3(Random.Range(-1f, 1f), 1f, Random.Range(-1f, 1f));
            Rigidbody mineInstance = Instantiate(minePrefab, m_PlayerHandler.transform.position, m_PlayerHandler.transform.rotation) as Rigidbody;
            mineInstance.AddForce(force * 500f); 

            mineInstance.GetComponent<AbilityProjectile>().m_ParentAbility = this;
        }

        public override void OnImpact(AbilityProjectile abilityProjectile, Collider[] other)
        {
            for (int i = 0; i < other.Length; i++)
            {
                PlayerHandler otherPlayer = other[i].GetComponent<PlayerHandler>();
                if (otherPlayer)
                {
                    if (otherPlayer.m_PlayerAttributes.PlayerNumber != m_PlayerHandler.m_PlayerAttributes.PlayerNumber)
                    {
                        Debug.Log("impact" + otherPlayer.m_PlayerAttributes.PlayerNumber);
                        otherPlayer.TakeDamage(10f);
                        //player.GetComponent<Rigidbody>().AddExplosionForce(1000f, abilityProjectile.transform.position, 20f);
                        Destroy(abilityProjectile.gameObject);
                    }
                }
            }
        }
    }
}