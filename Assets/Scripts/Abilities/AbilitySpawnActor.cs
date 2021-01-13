using UnityEngine;

namespace Scripts
{
    public class AbilitySpawnActor : MonoBehaviour, IActorAbility
    {
        [Header("Actor To Spawn MonoBehaviour must have a component derived from IActor")]
        [SerializeField] private MonoBehaviour _actorBehaviour;
        [SerializeField] private Vector3 _spawnPoint;
        [SerializeField] private bool _executeOnStart;
        
        public IActor Actor { get; set; }

        public void Initialize(IActor actor)
        {
            Actor = actor;
        }

        public void Execute()
        {
            var spawnedActor = Instantiate(_actorBehaviour, _spawnPoint, Quaternion.identity).GetComponent<IActor>();
            
            if (spawnedActor != null)
            {
                spawnedActor.Spawner = Actor;
            }
        }
        
        private void Start()
        {
            if (_executeOnStart)
            {
                Execute();
            }
        }
    }
}