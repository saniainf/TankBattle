using System;

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

    [Flags]
    public enum ProjectileType
    {
        PROJECTILE_NONE = 1 << 0,
        PROJECTILE_COLLISON_ON_TRIGGER = 1 << 1,
        PROJECTILE_IMPACT_ON_TRIGGER = 1 << 2,
        PROJECTILE_COLLISION_ON_OVERLAPE_SPHERE = 1 << 3,
        PROJECTILE_IMPACT_ON_OVERLAPE_SPHERE = 1 << 4,
        PROJECTILE_COLLISION_NONE = 1 << 5,
        PROJECTILE_IMPACT_NONE = 1 << 6,
        PROJECTILE_CHECK_IMPACT_ALONG = 1 << 7
    }

    [Flags]
    public enum ProjectileCallback
    {
        PROJECTILE_CALLBACK_NONE = 1 << 0,
        PROJECTILE_CALLBACK_FIXED = 1 << 1,
        PROJECTILE_CALLBACK_ON_IMPACT = 1 << 2
    }

    [Flags]
    public enum ProjectileModifierProperty
    {
        PROPERTY_NONE = 1 << 0,
        PROPERTY_DAMAGE_MULTIPLIER = 1 << 1
    }
}