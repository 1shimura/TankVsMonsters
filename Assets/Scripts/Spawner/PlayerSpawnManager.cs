using System.Linq;
using Scripts.UI;
using UnityEngine;

namespace Scripts
{
    public class PlayerSpawnManager
    {
        private MonoBehaviour _playerPrefab;
        private Vector3 _playerSpawnPoint;
        private RestartScreen _restartScreen;

        public PlayerSpawnManager(MonoBehaviour playerPrefab, Vector3 playerSpawnPoint, RestartScreen restartScreen)
        {
            _playerPrefab = playerPrefab;
            _playerSpawnPoint = playerSpawnPoint;
            _restartScreen = restartScreen;
        }

        public IActor SpawnActor()
        {
            var player = Object.Instantiate(_playerPrefab, _playerSpawnPoint, Quaternion.identity);
            var playerActor = player.GetComponent<IActor>();

            if (playerActor == null) return null;
            
            var actorPlayerAbility = playerActor.Abilities.FirstOrDefault(ability => ability is AbilityActorPlayer) as AbilityActorPlayer;
            
            if (actorPlayerAbility != null)
            {
                actorPlayerAbility.OnDeath += DeadActorHandler;
            }

            return playerActor;
        }

        private void DeadActorHandler(IActor actor)
        {
            foreach (var ability in actor.Abilities.Where(a => a is IResettable).ToList())
            {
                ((IResettable) ability).Reset();
                _restartScreen.SetScreenVisibility(true);
            }
        }
    }
}