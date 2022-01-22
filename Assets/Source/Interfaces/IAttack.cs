using System.Collections.Generic;
using Source.Mechanics;
using UnityEngine;

namespace Source.Interfaces
{
    public interface IAttack
    {
        void Initialize(List<Transform> enemy, PlayerMechanics player, Weapon.Weapon weapon);
        void Initialize(PlayerMechanics player, EnemyBaseMechanics enemy, Weapon.Weapon weapon);
        bool HasEnemy();
        void RotateToEnemy(Transform from, Transform to);
        void RotateToEnemy();
        void TryOpenFire();
    }
}