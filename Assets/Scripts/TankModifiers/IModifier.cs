using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModifier
{
    bool ImDone { get; set; }
    void OnEnable(GameObject tank);
    void Update();
    void OnDisable();
    ModifierStates GetModifierStates();
    ModifierBehavior GetModifierBehavior();
}
