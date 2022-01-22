using System.Collections.Generic;
using Source.Interfaces;
using Source.Mechanics;
using UnityEngine;

namespace Source
{
    public class EnemyAttack: IAttack
    {
        private PlayerMechanics _playerMechanics;
        private EnemyBaseMechanics _currentEnemy;
        private Weapon.Weapon _weapon;
        private Rigidbody _currentEnemyRigidbody;
        private IEnemy _enemy;
        
        public void Initialize(List<Transform> enemy, PlayerMechanics player, Weapon.Weapon weapon)
        {
            throw new System.NotImplementedException();
        }

        public void Initialize(PlayerMechanics player, EnemyBaseMechanics enemy, Weapon.Weapon weapon)
        {
            _playerMechanics = player;
            _currentEnemy = enemy;
            _weapon = weapon;
            _enemy = enemy.GetComponent<EnemyBase>();
            _currentEnemyRigidbody = enemy.GetComponent<Rigidbody>();
        }

        public bool HasEnemy()
        {
            return _playerMechanics.transform;
        }

        public void RotateToEnemy(Transform from, Transform to)
        {
            throw new System.NotImplementedException();
        }

        public void RotateToEnemy()
        {
            {
                if (_playerMechanics.transform != null)
                {
                    var rot = new Vector3(_playerMechanics.transform.position.x, _currentEnemy.transform.position.y,
                        _playerMechanics.transform.position.z);
                    _currentEnemy.transform.LookAt(rot);
                }
            }
        }

        public void TryOpenFire()
        {
            if (_currentEnemy.CanAtkState() && HasEnemy())
            {
                _currentEnemyRigidbody.velocity = Vector3.zero;
                RotateToEnemy();
                _weapon.Attack(_playerMechanics.transform.position, _enemy);
            }
        }
    }
}