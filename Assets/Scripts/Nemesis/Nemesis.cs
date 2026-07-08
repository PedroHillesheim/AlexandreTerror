using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public enum EnemyState
{
    Idle,
    Chasing,
    Patrolling
}

public class Nemesis : MonoBehaviour
{
    private bool _isFirstTimePatrolling = true;
    private PatrolController _patrolController;
    private GameObject _nape;
    private Transform _player;
    private NavMeshAgent _agent;
    private EnemyState _currentState;
    [SerializeField][Range(0.5f, 5)] private float _waitTime = 2f;
    private float _jumpscareDistance = 3f;
    private UnityEvent _jumpscareUI;
    private Animator _animator;
    //[SerializeField] private AudioSource _jumpscareSound;

    private bool _hasJumpScared = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _nape = transform.GetChild(0).gameObject;
        _patrolController = GameController.Instance.PatrolController;
        _player = GameController.Instance.PlayerTransform;
        _agent = GetComponent<NavMeshAgent>();
        _jumpscareUI = GameController.Instance.JumpscareUI;
        SetState(EnemyState.Idle);
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vision();

        CheckJumpscare();

        //AnimationChanger();
    }
    public void Vision()
    {
        bool playerInSight = Physics.Linecast(transform.position, _player.position, out RaycastHit hit);
        if (playerInSight)
        {
            if (!_currentState.Equals(EnemyState.Chasing))
                return;
            StopAllCoroutines();
            SetState(EnemyState.Idle);
        }
        else
        {
            if (_currentState.Equals(EnemyState.Chasing))
                return;
            StopAllCoroutines();
            SetState(EnemyState.Chasing);
        }
    }
    void CheckJumpscare()
    {
        if (_hasJumpScared) return;
        if (_currentState != EnemyState.Chasing) return;

        float distance = Vector3.Distance(transform.position, _player.position);

        if (distance <= _jumpscareDistance)
        {
            print("JumpScare");
            _hasJumpScared = true;

            gameObject.SetActive(false);

            _jumpscareUI.Invoke();
        }
    }
    public void SetState(EnemyState newState)
    {
        Vector3 lastPlaterPos = _player.position;
        switch (_currentState)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Chasing:;
                _agent.SetDestination(lastPlaterPos);
                _isFirstTimePatrolling = true;
                _nape.SetActive(true);
                break;
            case EnemyState.Patrolling:
                break;
        }
        _currentState = newState;
        switch (_currentState)
        {
            case EnemyState.Idle:
                StopAllCoroutines();
                StartCoroutine(Wait());
                break;
            case EnemyState.Chasing:
                _agent.SetDestination(_player.position);
                _nape.SetActive(false);
                break;
            case EnemyState.Patrolling:
                if (_isFirstTimePatrolling == true)
                {
                    _agent.SetDestination(_patrolController.GetClosestPoint());
                }
                else
                {
                    _agent.SetDestination(_patrolController.GetRandomPoint());
                }
                StartCoroutine(Patrolling());
                break;
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitUntil(() => _agent.remainingDistance <= _agent.stoppingDistance);
        yield return new WaitForSeconds(_waitTime);
        SetState(EnemyState.Patrolling);
    }
    IEnumerator Patrolling()
    {
        yield return new WaitUntil(() => _agent.remainingDistance <= _agent.stoppingDistance);
        yield return new WaitForSeconds(_waitTime);
        _isFirstTimePatrolling = false;
        SetState(EnemyState.Idle);
    }
}
