using UnityEngine;

namespace Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class AbilityMoveByInput : MonoBehaviour, IActorAbility, IResettable
    {
        [SerializeField] private float _movementSpeed = 12f;
        [SerializeField] private float _turnSpeed = 180f;

        [SerializeField] private string _movementAxisName;
        [SerializeField] private string _turnAxisName;

        private float _movementInputValue;
        private float _turnInputValue;

        private Rigidbody _rigidbody;

        private bool _inputEnable;
        
        public IActor Actor { get; set; }

        public void Initialize(IActor actor)
        {
            Actor = actor;
        }

        public void Execute()
        {
            Move();
            Turn();
        }
        
        public void PrewarmSetup()
        {
            _inputEnable = true;
        }

        public void Reset()
        {
            _inputEnable = false;
            _rigidbody.velocity = Vector3.zero;
        }
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (!_inputEnable) return;
            
            _movementInputValue = Input.GetAxis(_movementAxisName);
            _turnInputValue = Input.GetAxis(_turnAxisName);
        }
        
        private void FixedUpdate()
        {
            Execute();
        }

        private void Move()
        {
            var movement = transform.forward * (_movementInputValue * _movementSpeed * Time.deltaTime);
            
            _rigidbody.MovePosition(_rigidbody.position + movement);
        }
        
        private void Turn()
        {
            var turn = _turnInputValue * _turnSpeed * Time.deltaTime;
            var turnRotation = Quaternion.Euler(0f, turn, 0f);
            
            _rigidbody.MoveRotation(_rigidbody.rotation * turnRotation);
        }
    }
}