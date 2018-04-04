using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class Modifier
{
    public bool ImDone;

    protected ModifierBehavior behavior;
    protected ModifierStates states;

    public void OnEnable(GameObject gameObject) { }
    public void Update() { }
    public void OnDisable() { }

    public ModifierStates GetModifierStates()
    {
        return ModifierStates.MODIFIER_STATE_NONE;
    }

    public ModifierBehavior GetModifierBehavior()
    {
        return ModifierBehavior.MODIFIER_BEHAVIOR_NONE;
    }
}
