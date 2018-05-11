﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TankBattle
{
    public class PlayerModifiersHandler : MonoBehaviour
    {
        [HideInInspector] public ModifierStates States = new ModifierStates();

        public List<Modifier> Modifiers;

        private void OnEnable()
        {
            Modifiers = new List<Modifier>(20);
        }

        public void AddModifier(Modifier modifier)
        {
            if (modifier.GetModifierBehavior().HasFlag(ModifierAttribute.MODIFIER_ATTRIBUTE_SINGLE_PRIMARY))
            {
                modifier.OnEnable(gameObject);
                Modifiers.RemoveAll(x => x.GetType() == modifier.GetType());
                Modifiers.Add(modifier);
            }
        }

        private void Update()
        {
            for (int i = 0; i < Modifiers.Count; i++)
            {
                Modifiers[i].Update();
            }
        }

        private void LateUpdate()
        {
            // удаление ImDone модификаторов с вызовом OnDisable
            Modifiers.RemoveAll(x => x.ImDone);

            // заполнение States для танка перебором всех модификаторов в Modifiers
            States = new ModifierStates();
            foreach (Modifier modifier in Modifiers)
            {
                if (modifier.GetModifierStates().HasFlag(ModifierStates.MODIFIER_STATE_ATTACK_IMMUNE) && !States.HasFlag(ModifierStates.MODIFIER_STATE_ATTACK_IMMUNE))
                    States |= ModifierStates.MODIFIER_STATE_ATTACK_IMMUNE;
            }
        }
    }
}