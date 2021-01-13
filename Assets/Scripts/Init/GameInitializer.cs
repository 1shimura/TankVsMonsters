using System.Linq;
using Scripts.UI;
using UnityEngine;

namespace Scripts
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private CameraFollow _camera;
        [SerializeField] private RestartScreen _restartScreen;

        private void Start()
        {
            var playerSpawnManager =
                new PlayerSpawnManager(_gameConfig.PlayerPrefab, _gameConfig.PlayerSpawnPoint, _restartScreen);
            var playerActor = playerSpawnManager.SpawnActor();

            if (playerActor == null) return;

            _camera.Target = playerActor.Transform;

            var enemyActors = _gameConfig.EnemyPrefabs
                .Select(enemy => enemy.GetComponent<IActor>())
                .Where(enemyActor => enemyActor != null)
                .ToList();

            var enemySpawnPoints = _gameConfig.EnemySpawnPointPrefabs
                .Select(spawnPoint => Instantiate(spawnPoint).GetComponent<ActorSpawnPoint>()).ToList();

            var enemySpawnManager =
                new EnemySpawnManager(enemyActors, enemySpawnPoints, _gameConfig.EnemyMaxCount, playerActor);
            enemySpawnManager.Maintain();
        }
    }
}