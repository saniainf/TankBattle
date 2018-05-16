using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilsEnum
{
    public static bool HasFlag(this Enum mask, Enum flags)
    {
#if UNITY_EDITOR
        if (mask.GetType() != flags.GetType())
            Debug.Log("The argument type, '" + flags.GetType() + "', is not the same as the enum type '" + mask.GetType() + "'.");
#endif
        return ((int)(IConvertible)mask & (int)(IConvertible)flags) == (int)(IConvertible)flags;
    }

    public static bool HasLayer(this LayerMask mask, int layer)
    {
        return ((mask & (1 << layer)) == (1 << layer));
    }
}