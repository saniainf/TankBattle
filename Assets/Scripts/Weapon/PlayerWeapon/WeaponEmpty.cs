using UnityEngine;
using System.Collections;

namespace TankBattle
{
    [CreateAssetMenu(fileName = "Weapon Empty", menuName = "Player/Weapon/Empty")]
    public class WeaponEmpty : Weapon
    {
        public override void WeaponButtonPress()
        {
            Debug.Log("puf puf");
        }
    }
}