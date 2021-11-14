using System.Collections;
using System.Collections.Generic;
using Source.Interfaces;
using Source.Mechanics;
using UnityEngine;

namespace Source.GameField
{
    public class LevelCondition : MonoBehaviour
    {
        [SerializeField] private int _enemyCount;
        [SerializeField] private GameObject _player;
        [SerializeField] private List<GameObject> _enemyPrefabs;
        [SerializeField] private List<Transform> _spawnPoints;
        [SerializeField] private float _pauseTime;
        [SerializeField] private GameObject[] _weapons;
        [SerializeField] private Vector3 _volume;
        [SerializeField] private GameObject _uI;
        private List<GameObject> _enemies;
        private PlayerMechanics _playerMechanics;
        private bool levelClear;

        private void Awake()
        {
            _enemies = new List<GameObject>(_enemyCount);
            var player = Instantiate(_player, null);
            SpawnEnemies(player);
            _playerMechanics = player.GetComponent<PlayerMechanics>();
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

        private void OnCollisionEnter(Collision other)
        {
            if (levelClear && other.gameObject.GetComponent<IPlayer>() != null) 
                Quite();
        }

        private void ChangeLevelStatus(bool value)
        {
            levelClear = value;
        }
        
        private static void Quite()
        {
            if (UnityEditor.EditorApplication.isPlaying)
            {
                UnityEditor.EditorApplication.isPlaying = false;
            }
            Application.Quit();
        }

        private void SpawnEnemies(GameObject player)
        {
            var listEnemy = new List<GameObject> {player};
            for (var i = 0; i < _enemyCount; i++)
            {
                var randomSpawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
                var position1 = randomSpawnPoint.position;
                var enemyPrefab = _enemyPrefabs[Random.Range(0, _enemyPrefabs.Count)];
                var x = Random.Range(position1.x - _volume.x,
                    position1.x + _volume.x);
                var yHeight = enemyPrefab.transform.position.y;
                var y = Random.Range(position1.y - _volume.y + yHeight,
                    position1.y + _volume.y + yHeight);
                var z = Random.Range(position1.z - _volume.z,
                    position1.z + _volume.z);
                var position = new Vector3(x, y, z);
                var enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
                enemy.GetComponent<BaseMechanics>().Initialize(listEnemy, _pauseTime, _weapons[0]);
                _enemies.Add(enemy);
            }
        }
    }
}