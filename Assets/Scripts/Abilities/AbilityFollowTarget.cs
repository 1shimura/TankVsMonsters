using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Transform))]
    public class AbilityFollowTarget : MonoBehaviour, IActorAbilityTarget, IResettable
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private string _walkAnimationParam;

        private Animator _animator;
        private Transform _transform;

        private bool _followingEnable;

        public IActor Actor { get; set; }
        public IActor TargetActor { get; set; }
        
        public void Initialize(IActor actor)
        {
            Actor = actor;
        }

        public void Execute()
        {
            if (TargetActor == null || !_followingEnable)
            {
                return;
            }

            var targetPlayerAbility =
                TargetActor.Abilities.FirstOrDefault(a => a is AbilityActorPlayer) as AbilityActorPlayer;

            if (targetPlayerAbility != null && !targetPlayerAbility.IsAlive)
            {
                _agent.ResetPath();
                Reset();
                return;
            }

            var distance = Vector3.Distance(_transform.position, TargetActor.Transform.position);
            var continueFollowing = (int) distance > (int) _agent.stoppingDistance;
            
            _animator.SetBool(_walkAnimationParam, continueFollowing);
            
            if (continueFollowing)
            {
                _agent.SetDestination(TargetActor.Transform.position);
            }
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _transform = GetComponent<Transform>();
        }

        private void Update()
        {
            Execute();
        }

        public void PrewarmSetup()
        {
            _followingEnable = true;
        }

        public void Reset()
        {
            _animator.SetBool(_walkAnimationParam, false);
            _followingEnable = false;
        }
    }
}