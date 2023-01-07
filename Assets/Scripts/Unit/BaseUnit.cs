using UnityEngine;
using UnityEngine.AI;

namespace Unit
{
    public class BaseUnit : MonoBehaviour
    {
        public GameObject MainTarget { get; set; }

        [SerializeField] private Animator _unitAnimator;
        [SerializeField] private NavMeshAgent _navMeshAgent;

        private EntitiesHealth _entitiesHealth;
        private GameObject _target;
        private float _delayTime;
        private bool _isAttack;
        private const float PowerAttack = 20;
        private const float TimeForAttack = 1.1f;
        private const float RotationSpeed = 10;
        private static readonly int AnimStatus = Animator.StringToHash("AnimStatus");

        private void Start()
        {
            ChangeAnimationState(UnitAnimationType.Idle);
            SetTarget(MainTarget);
        }

        private void Update()
        {
            if (_target != null)
            {
                if (_isAttack)
                {
                    GoToTarget();
                }

                Fight();
            }
            else
            {
                ChangeAnimationState(_isAttack ? UnitAnimationType.Walk : UnitAnimationType.Idle);
                _target = MainTarget;
            }
        }

        private void Fight()
        {
            float distanceToEnemy = Vector3.Distance(_target.transform.position, transform.position);
            if (distanceToEnemy <= _navMeshAgent.stoppingDistance)
            {
                ChangeAnimationState(UnitAnimationType.Attack);

                _delayTime += Time.deltaTime;
                if (_delayTime >= TimeForAttack)
                {
                    _entitiesHealth.TakeDamage(PowerAttack);
                    _delayTime = 0;
                }
            }

            LookAtEnemy();
        }


        private void LookAtEnemy()
        {
            var direction = _target.transform.position - transform.position;
            direction.y = 0;
            var rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, RotationSpeed * Time.deltaTime);
        }

        private void GoToTarget()
        {
            _navMeshAgent.destination = _target.transform.position;
        }

        public void GoAttack(bool isAttack)
        {
            _isAttack = isAttack;
            ChangeAnimationState(UnitAnimationType.Walk);
        }

        public void SetTarget(GameObject target)
        {
            _target = target;
            _entitiesHealth = _target.GetComponent<EntitiesHealth>();
        }

        private void ChangeAnimationState(UnitAnimationType animationType)
        {
            switch (animationType)
            {
                case UnitAnimationType.Idle:
                    _unitAnimator.SetInteger(AnimStatus, 0);
                    break;
                case UnitAnimationType.Walk:
                    _unitAnimator.SetInteger(AnimStatus, 1);
                    break;

                case UnitAnimationType.Attack:
                    _unitAnimator.SetInteger(AnimStatus, 2);
                    break;

                case UnitAnimationType.Die:
                    _unitAnimator.SetInteger(AnimStatus, 3);
                    break;
                default:
                    _unitAnimator.SetInteger(AnimStatus, 0);
                    break;
            }
        }
    }
}