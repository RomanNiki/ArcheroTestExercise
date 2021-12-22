using System;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.Mechanics
{
    public class PlayerMechanics : BaseMechanics
    {
        [SerializeField] private DynamicJoystick _joystick;
        private bool _canAttack;
        private GameObject _lastClosestEnemy;
        private float _targetDistance = 100f;
        public event Action<bool> HaveNoEnemy;
        
        public sealed override Vector3 GetMoveDirection()
        {
            if ( _joystick == null)
            {
                _joystick = FindObjectOfType<DynamicJoystick>();
            }
            return new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
        }

        public override void TryOpenFire()
        {
            if (_enemies.Count == 0) return;
            FindClosestEnemy();

            if (_canAttack)
            {
                RotateToEnemy();
                _weaponClass.Attack(_lastClosestEnemy.transform.position, Player.Instance);
            }
        }

        public override void RotateToEnemy()
        {
            var direction = Vector3.forward;
            if (_lastClosestEnemy != null) direction = _lastClosestEnemy.transform.position - transform.position;
            direction.y = 0f;
            _rigidbody.MoveRotation(Quaternion.LookRotation(direction));
        }

        private void FindClosestEnemy()
        {
            _canAttack = false;
            foreach (var enemy in _enemies)
            {
                if (enemy == null)
                {
                    _enemies.Remove(enemy);
                    if (!HaveEnemy()) HaveNoEnemy?.Invoke(true);
                    return;
                }

                var currentDistance = Vector3.Distance(transform.position, enemy.transform.position);
                var ray = new Ray(_weaponPlace.position, enemy.transform.position - _weaponPlace.position);
                if (Physics.Raycast(ray, out var hitInfo, 100f, _layerMask))
                    if (hitInfo.transform.TryGetComponent<IEnemy>(out var component))
                        if (currentDistance <= _targetDistance && currentDistance <= _attackRange)
                        {
                            _targetDistance = currentDistance;
                            _lastClosestEnemy = enemy;
                            _canAttack = true;
                        }
            }

            _targetDistance = 100f;
        }
    }
}