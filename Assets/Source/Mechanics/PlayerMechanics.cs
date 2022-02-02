using System;
using System.Collections.Generic;
using Source.Interfaces;
using UnityEngine;

namespace Source.Mechanics
{
    public class PlayerMechanics : BaseMechanics
    {
        [SerializeField] private DynamicJoystick _joystick;
        private Transform _lastClosestEnemy;
        private float _targetDistance = 100f;
        public event Action<bool> HaveNoEnemy;

        public sealed override Vector3 GetMoveDirection()
        {
            if (_joystick == null)
            {
                _joystick = FindObjectOfType<DynamicJoystick>();
            }

            return new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
        }

        protected override void InitAttack(List<Transform> enemies, Weapon.Weapon weapon)
        {
            Attack = new PlayerAttack();
            Attack.Initialize(enemies, this, weapon);
        }

        protected override void InitAttack(PlayerMechanics player, Weapon.Weapon weapon)
        {
            throw new NotImplementedException();
        }

        public Transform FindClosestEnemy(out bool canAttack, ref List<Transform> enemies)
        {
            canAttack = false;
            foreach (var enemy in enemies)
            {
                if (enemy == null)
                {
                    enemies.Remove(enemy);
                    if (!Attack.HasEnemy()) HaveNoEnemy?.Invoke(true);
                    return null;
                }

                var enemyPosition = enemy.transform.position;
                var weaponPlacePosition = _weaponPlace.position;
                var currentDistance = Vector3.Distance(transform.position, enemyPosition);
                var direction = enemyPosition - weaponPlacePosition;
                var ray = new Ray(weaponPlacePosition, direction);
                if (Physics.Raycast(ray, out var hitInfo, 100f, _layerMask) == false) continue;
                if (hitInfo.transform.TryGetComponent<IEnemy>(out _) == false) continue;
                if ((currentDistance <= _targetDistance) == false ||
                    (currentDistance <= _attackRange) == false) continue;
                _targetDistance = currentDistance;
                _lastClosestEnemy = enemy;
                canAttack = true;
            }

            _targetDistance = 100f;
            return _lastClosestEnemy;
        }
    }
}