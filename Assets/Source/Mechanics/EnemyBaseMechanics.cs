using System.Collections.Generic;
using Source.Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace Source.Mechanics
{
    public class EnemyBaseMechanics : BaseMechanics
    {
        [SerializeField] private NavMeshAgent _meshAgent;
        [SerializeField] private float _playerRealizeRange;
        [SerializeField] private float _collisionDamage;
        private float _distance;
        private Transform _playerTransform;

        public override void Initialize(PlayerMechanics player, float pauseTime, Weapon.Weapon weaponGameObject)
        {
           
            base.Initialize(player, pauseTime, weaponGameObject);
            if (_meshAgent != null)
            {
                _meshAgent.speed = _speed;
                _meshAgent.stoppingDistance = _attackRange;
            }

            _playerTransform = player.transform;
        }

        protected override void InitAttack(List<Transform> enemies, Weapon.Weapon weapon)
        {
            throw new System.NotImplementedException();
        }

        protected override void InitAttack(PlayerMechanics player, Weapon.Weapon weapon)
        {
            _attack = new EnemyAttack();
            _attack.Initialize(player, this, weapon);
        }


        private void OnCollisionStay(Collision other)
        {
            var enemyCollision = other.transform.GetComponent<IPlayer>();
            if (enemyCollision != null) Player.Instance.ApplyDamage(_collisionDamage);
        }

        public override bool CanAtkState()
        {
            if (_playerTransform == null) return false;
            var weaponPlacePosition = _weaponPlace.position;
            var targetDir = _playerTransform.position - weaponPlacePosition;
            Physics.Raycast(weaponPlacePosition, targetDir,
                out var hit, _attackRange, _layerMask);
            _distance = Vector3.Distance(_playerTransform.position, transform.position);

            if (hit.transform == null) return false;

            if (Physics.SphereCast(_weaponPlace.position, 0.25f, targetDir, out var hitInfo, _attackRange, _layerMask))
                if (hitInfo.transform.GetComponent<IPlayer>() == null)
                    return false;
            return _distance <= _attackRange;
        }

        public override void Move(Vector3 direction)
        {
            if (_playerTransform == null) return;

            if (CanAtkState() == false)
            {
                if (_meshAgent == null) return;
                var dir = transform.position - Vector3.forward;
                transform.LookAt(_playerTransform.position);
                if (_distance > _playerRealizeRange)
                    _meshAgent.SetDestination(dir * 5f);
                else
                    _meshAgent.SetDestination(_playerTransform.position);
            }
        }
    }
}