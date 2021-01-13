using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    [CreateAssetMenu(menuName = "Game Config")]
    public class GameConfig : ScriptableObject
    {
        [Header("Player Settings")]
        [Header("Player Prefab must have a component derived from IActor")]
        [SerializeField] private MonoBehaviour _playerPrefab;
        [SerializeField] private Vector3 _playerSpawnPoint;
        
        [Header("Enemy Settings")]
        [SerializeField] private int _enemyMaxCount;
        [Header("Each enemy prefabs must have a component derived from IActor")]
        [SerializeField] private List<MonoBehaviour> _enemyPrefabs;
        [SerializeField] private List<ActorSpawnPoint> _enemySpawnPointPrefabs;
        
        public MonoBehaviour PlayerPrefab => _playerPrefab;
        public Vector3 PlayerSpawnPoint => _playerSpawnPoint;
        
        public int EnemyMaxCount => _enemyMaxCount;
        public List<MonoBehaviour> EnemyPrefabs => _enemyPrefabs;
        public List<ActorSpawnPoint> EnemySpawnPointPrefabs => _enemySpawnPointPrefabs;
    }
}