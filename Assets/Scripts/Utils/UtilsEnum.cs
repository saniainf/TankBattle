using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilsEnum
{
    public static bool HasFlag(this Enum mask, Enum flags)
    {
#if DEBUG
        if (mask.GetType() != flags.GetType())
            throw new System.ArgumentException(
                string.Format("The argument type, '{0}', is not the same as the enum type '{1}'.",
                flags.GetType(), mask.GetType()));
#endif
        return ((int)(IConvertible)mask & (int)(IConvertible)flags) == (int)(IConvertible)flags;
    }

    public static bool HasLayer(this LayerMask mask, int layer)
    {
        return ((mask & (1 << layer)) == (1 << layer));
    }
}