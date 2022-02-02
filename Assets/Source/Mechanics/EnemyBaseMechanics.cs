using System;
using System.Collections.Generic;
using Source.Interfaces;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Source.Mechanics
{
    public class EnemyBaseMechanics : BaseMechanics
    {
        [SerializeField] private NavMeshAgent _meshAgent;
        [SerializeField] private float _playerRealizeRange;
        [SerializeField] private float _collisionDamage;
        [Range(1f,20f)] [SerializeField] private float _randomPointRadius;
        private float _distance;
        private Vector3 _randomPoint;
        private Transform _playerTransform;
        private NavMeshPath _navMeshPath;

        public override void Initialize(PlayerMechanics player, float pauseTime, Weapon.Weapon weaponGameObject)
        {
            base.Initialize(player, pauseTime, weaponGameObject);
            _navMeshPath = new NavMeshPath();
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
            Attack = new EnemyAttack();
            Attack.Initialize(player, this, weapon);
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
            Debug.DrawRay(_weaponPlace.position, (targetDir) * _attackRange, Color.red);
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
                var dir  = transform.position - transform.forward;;
                transform.LookAt(_playerTransform.position);
                _distance = Vector3.Distance(_playerTransform.position, transform.position);
                if (_distance > _playerRealizeRange)
                    _meshAgent.SetDestination(dir) ;
                else
                {
                    _meshAgent.SetDestination(GetNearPos());
                }

            }
        }

        private Vector3 GetNearPos()
        {
            var getCorrectPoint = false;
            Debug.Log("here");
            while (getCorrectPoint == false)
            {
                NavMeshHit navMeshHit;
                var sourcePosition = Random.insideUnitSphere * _randomPointRadius + _playerTransform.position;
                NavMesh.SamplePosition(sourcePosition,
                    out navMeshHit, _randomPointRadius, NavMesh.AllAreas);
                _randomPoint = navMeshHit.position;
                
                if (_randomPoint.y > -10000 && _randomPoint.y < 10000)
                {
                    _meshAgent.CalculatePath(_randomPoint, _navMeshPath);
                    if (_navMeshPath.status == NavMeshPathStatus.PathComplete && !NavMesh.Raycast(_playerTransform.position, _randomPoint, out navMeshHit,NavMesh.AllAreas))
                    {
                        getCorrectPoint = true;
                    }
                }
            }
            return _randomPoint;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _attackRange);
        }
    }
}