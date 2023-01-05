using UnityEngine;
using UnityEngine.AI;

public enum UnitAnimationType
{
    Idle = 0,
    Walk = 1,
    Attack = 2,
    Die = 3,
}

public class BaseUnit : MonoBehaviour
{
    public GameObject MainTarget;
    [SerializeField] private Animator _unitAnimator;
    [SerializeField] private NavMeshAgent _navMeshAgent;

    private float _powerAttack = 10;

    private float _delayTime;
    private GameObject _target;
    private bool _isAttack;
    private const float TimeForAttack = 1.1f;
    private const float RotationSpeed = 10;
    private EntitiesHealth _entitiesHealth;
    private static readonly int AnimStatus = Animator.StringToHash("AnimStatus");

    private void Start()
    {
        ChangeAnimationState(UnitAnimationType.Idle);
        SetTarget(MainTarget);
    }

    private void Update()
    {
        if (_isAttack)
        {
            GoToTarget();
        }

        Fight();
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
                _entitiesHealth.TakeDamage(_powerAttack);
                _delayTime = 0;
            }
        }

        LookAtEnemy();
    }


    private void LookAtEnemy()
    {
        Vector3 direction = _target.transform.position - transform.position;
        direction.y = 0;
        Quaternion rotation = Quaternion.LookRotation(direction);
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

    public void ChangeAnimationState(UnitAnimationType animationType)
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