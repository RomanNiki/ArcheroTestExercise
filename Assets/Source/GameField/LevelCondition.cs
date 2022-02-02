using System.Collections.Generic;
using Source.Interfaces;
using Source.Mechanics;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Source.GameField
{
    public class LevelCondition : MonoBehaviour
    {
        [SerializeField] private int _enemyCount;
        [SerializeField] private PlayerMechanics _player;
        [SerializeField] private List<EnemyBaseMechanics> _enemyPrefabs;
        [SerializeField] private List<Transform> _spawnPoints;
        [SerializeField] private float _pauseTime;
        [SerializeField] private Weapon.Weapon[] _weapons;
        [SerializeField] private Vector3 _volume;
        [SerializeField] private GameObject _uI;
        [SerializeField] private NavMeshSurface _surface;
        [SerializeField] private bool _ignoreNavmeshAgent;
        private List<Transform> _enemies;
        private PlayerMechanics _playerMechanics;
        public bool LevelClear { get; private set; }

        private void Awake()
        {
            _surface.ignoreNavMeshAgent = _ignoreNavmeshAgent;
            _enemies = new List<Transform>(_enemyCount);
            _playerMechanics = Instantiate(_player, null);
            SpawnEnemies(_playerMechanics);
            _playerMechanics.Initialize(_enemies, _pauseTime, _weapons[0]);
            Instantiate(_uI);
        }

        private void OnEnable()
        {
            _playerMechanics.HaveNoEnemy += ChangeLevelStatus;
        }

        private void OnDisable()
        {
            _playerMechanics.HaveNoEnemy -= ChangeLevelStatus;
        }
        
        private void ChangeLevelStatus(bool value)
        {
            LevelClear = value;
        }
        
        private void SpawnEnemies(PlayerMechanics player)
        {
            for (var i = 0; i < _enemyCount; i++)
            {
                var enemyPrefab = _enemyPrefabs[Random.Range(0, _enemyPrefabs.Count)];
                var randomSpawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
                var spawnPosition = randomSpawnPoint.position;
                var position = TakeRandomPosition(spawnPosition, enemyPrefab.transform.position.y);
                var enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
                enemy.Initialize(player, _pauseTime, _weapons[0]);
                _enemies.Add(enemy.transform);
            }
        }

        private Vector3 TakeRandomPosition(Vector3 spawnPosition, float yHeight)
        {
            var x = Random.Range(spawnPosition.x - _volume.x,
                spawnPosition.x + _volume.x);
            var y = Random.Range(spawnPosition.y - _volume.y + yHeight,
                spawnPosition.y + _volume.y + yHeight);
            var z = Random.Range(spawnPosition.z - _volume.z,
                spawnPosition.z + _volume.z);
            var position = new Vector3(x, y, z);
            return position;
        }
    }
}