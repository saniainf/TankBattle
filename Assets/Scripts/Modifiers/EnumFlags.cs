using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankBattle
{
    [Flags]
    public enum ModifierStates
    {
        MODIFIER_STATE_NONE = 1 << 0,
        MODIFIER_STATE_ATTACK_IMMUNE = 1 << 1
    }

    [Flags]
    public enum ModifierAttribute
    {
        MODIFIER_ATTRIBUTE_NONE = 1 << 0,
        MODIFIER_ATTRIBUTE_SINGLE_PRIMARY = 1 << 1,
        MODIFIER_ATTRIBUTE_SINGLE_SECONDARY = 1 << 2
    }
}