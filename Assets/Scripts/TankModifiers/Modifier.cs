using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankBattle
{
    abstract public class Modifier
    {
        public bool ImDone;

        protected ModifierAttribute attribute;
        protected ModifierStates states;

        virtual public void OnEnable(GameObject gameObject) { }
        virtual public void Update() { }
        virtual public void OnDisable() { }

        virtual public ModifierStates GetModifierStates()
        {
            return states;
        }

        virtual public ModifierAttribute GetModifierBehavior()
        {
            return attribute;
        }
    }
}