using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankBattle
{
    public abstract class Ability : ScriptableObject
    {
        [HideInInspector] public PlayerHandler m_PlayerHandler;

        public virtual void AbilityButtonPress() { }

        public virtual void AbilityButtonHold() { }

        public virtual void AbilityButtonRelease() { }

        public virtual void RemoveAbility() { }

        public virtual void OnImpact(AbilityProjectile abilityProjectile, Collider[] other) { }
    }
}