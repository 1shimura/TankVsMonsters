using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Scripts
{
    public class EnemySpawnManager
    {
        private readonly List<IActor> _enemiesList;
        private readonly List<ActorSpawnPoint> _enemiesSpawnPoints;
        private readonly int _requiredEnemiesQuantity;

        private Pool<IActor> _actorsPool;
        private IActor _target;

        private Dictionary<IActor, AbilityActorPlayer> _existingEnemiesDict =
            new Dictionary<IActor, AbilityActorPlayer>();

        public EnemySpawnManager(List<IActor> enemiesList, List<ActorSpawnPoint> enemiesSpawnPoints,
            int requiredEnemiesQuantity, IActor target)
        {
            _enemiesList = enemiesList;
            _enemiesSpawnPoints = enemiesSpawnPoints;
            _requiredEnemiesQuantity = requiredEnemiesQuantity;
            _target = target;
        }

        public void Maintain()
        {
            _actorsPool = new Pool<IActor>(new RandomActorFactory(_enemiesList), _requiredEnemiesQuantity);

            for (var i = 0; i < _requiredEnemiesQuantity; i++)
            {
                GetNewEnemy();
            }
        }

        public void StopMaintain()
        {
            foreach (var abilityActorPlayer in _existingEnemiesDict.Values.Where(ability => ability != null))
            {
                abilityActorPlayer.OnDeath = null;
            }

            _existingEnemiesDict.Clear();
        }

        private void GetNewEnemy()
        {
            var newEnemy = _actorsPool.Allocate();

            _enemiesSpawnPoints.ForEach(point => point.TryRelease());
            var availableSpawnPoints = _enemiesSpawnPoints.Where(spawnPoint => !spawnPoint.IsBusy).ToList();

            var spawnPoints = availableSpawnPoints.Any() ? availableSpawnPoints : _enemiesSpawnPoints;

            var randSpawnPointIndex = Random.Range(0, spawnPoints.Count);
            var randSpawnPoint = spawnPoints[randSpawnPointIndex];

            newEnemy.Transform.position = randSpawnPoint.PointTransform.position;
            randSpawnPoint.OccupyPoint(newEnemy);

            var actorPlayerAbility =
                newEnemy.Abilities.FirstOrDefault(ability => ability is AbilityActorPlayer) as AbilityActorPlayer;

            _existingEnemiesDict.Add(newEnemy, actorPlayerAbility);

            if (actorPlayerAbility != null)
            {
                actorPlayerAbility.OnDeath += DeadActorHandler;
            }

            foreach (var ability in newEnemy.Abilities.Where(ability => ability is IActorAbilityTarget))
            {
                ((IActorAbilityTarget) ability).TargetActor = _target;
            }
        }

        private void DeadActorHandler(IActor actor)
        {
            _actorsPool.Release(actor);

            if (_existingEnemiesDict.ContainsKey(actor))
            {
                if (_existingEnemiesDict[actor] != null)
                {
                    _existingEnemiesDict[actor].OnDeath -= DeadActorHandler;
                }

                _existingEnemiesDict.Remove(actor);
            }

            GetNewEnemy();
        }
    }
}