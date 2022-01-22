using System.Collections.Generic;
using Source.Interfaces;
using Source.Mechanics;
using UnityEngine;

namespace Source
{
    public class PlayerAttack : IAttack
    {
        private List<Transform> _enemies;
        private PlayerMechanics _player;
        private Weapon.Weapon _weapon;
        private Rigidbody _rigidbody;


        public void Initialize(List<Transform> enemies, PlayerMechanics player, Weapon.Weapon weapon)
        {
            _enemies = enemies;
            _player = player;
            _weapon = weapon;
            _rigidbody = player.GetComponent<Rigidbody>();
        }

        public void Initialize(PlayerMechanics player, EnemyBaseMechanics enemy, Weapon.Weapon weapon)
        {
            
        }

        public bool HasEnemy()
        {
            return _enemies.Count > 0;
        }

        public void RotateToEnemy(Transform from, Transform to)
        {
            var direction = Vector3.forward;
            if (to != null) direction = to.position - from.position;
            direction.y = 0f;
            _rigidbody.MoveRotation(Quaternion.LookRotation(direction));
        }

        public void RotateToEnemy()
        {
            throw new System.NotImplementedException();
        }

        public void TryOpenFire()
        {
            if (_enemies.Count == 0) return;
            var closestEnemy = _player.FindClosestEnemy(out var canAttack, ref _enemies);

            if (canAttack && closestEnemy)
            {
                RotateToEnemy(_player.transform, closestEnemy);
                _weapon.Attack(closestEnemy.transform.position, _player);
            }
        }
    }
}