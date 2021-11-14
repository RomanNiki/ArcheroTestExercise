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
        private IEnemy _thisEnemy;
        private GameObject _enemyGameObject;

        public override void Initialize(List<GameObject> enemies, float pauseTime, GameObject weaponGameObject)
        {
            _thisEnemy = transform.GetComponent<IEnemy>();
            base.Initialize(enemies, pauseTime, weaponGameObject);
            if (_meshAgent != null)
            {
                _meshAgent.speed = _speed;
                _meshAgent.stoppingDistance = _attackRange;
            }

            _enemyGameObject = _enemies[0];
        }

        public override void TryOpenFire()
        {
            if (CanAtkState() && HaveEnemy())
            {
                RotateToEnemy();
                _weaponClass.Attack(_enemyGameObject.transform.position, _thisEnemy);
            }
        }

        public override void RotateToEnemy()
        {
            if (_enemyGameObject != null)
            {
                var rot = new Vector3(_enemyGameObject.transform.position.x, transform.position.y,
                    _enemyGameObject.transform.position.z);
                transform.LookAt(rot);
            }
        }
        private void OnCollisionStay(Collision other)
        {
            var enemyCollision = other.transform.GetComponent<IPlayer>();
            if (enemyCollision != null) Player.Instance.ApplyDamage(_collisionDamage);
        }

        protected override bool CanAtkState()
        {
            if (_enemyGameObject == null) return false;
            var targetDir = _enemyGameObject.transform.position - _weaponPlace.position;
            Physics.Raycast(_weaponPlace.position, targetDir,
                out var hit, _attackRange, _layerMask);
            _distance = Vector3.Distance(_enemyGameObject.transform.position, transform.position);


            if (hit.transform == null) return false;

            if (Physics.SphereCast(_weaponPlace.position, 0.25f, targetDir, out var hitInfo, _attackRange, _layerMask))
                if (hitInfo.transform.GetComponent<IPlayer>() == null)
                    return false;
            return _distance <= _attackRange;
        }

        public override void Move(Vector3 direction)
        {
            if (_enemyGameObject == null) return;

            if (CanAtkState() == false)
            {
                if (_meshAgent == null) return;
                if (_distance > _playerRealizeRange)
                    _meshAgent.SetDestination(transform.position - Vector3.forward * 5f);
                else
                    _meshAgent.SetDestination(_enemyGameObject.transform.position);
            }
        }
    }
}