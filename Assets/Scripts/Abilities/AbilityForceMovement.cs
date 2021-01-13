using UnityEngine;

namespace Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class AbilityForceMovement : MonoBehaviour, IActorAbility, IResettable
    {
        [SerializeField] private float _forceToApply;
        public IActor Actor { get; set; }

        private Rigidbody _rigidbody;
        private bool _forceApplied;

        public void Initialize(IActor actor)
        {
            Actor = actor;
        }

        public void Reset()
        {
            _rigidbody.velocity = Vector3.zero;
            _forceApplied = false;
        }
        
        public void Execute()
        {
        }

        public void PrewarmSetup()
        {
        }
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            Execute();
        }
        
        private void FixedUpdate()
        {
            if (Actor.Spawner == null || _forceApplied)
            {
                return;
            }
            
            _rigidbody.AddForce(_forceToApply * Actor.Spawner.Transform.forward);
            _forceApplied = true;
        }
    }
}