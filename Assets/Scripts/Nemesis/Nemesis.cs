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
    private PatrolController _patrolController;
    private GameObject _nape;
    private Transform _player;
    private NavMeshAgent _agent;
    private EnemyState _currentState;
    [SerializeField][Range(0.5f, 5)] private float _waitTime = 2f;
    private float _jumpscareDistance = 3f;
    [SerializeField] private UnityEvent _jumpscareUI;
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
        SetState(EnemyState.Patrolling); //Só para testar
    }

    // Update is called once per frame
    void Update()
    {
        Vision();

        CheckJumpscare();
    }
    public void Vision()
    {
        bool playerInSight = Physics.Linecast(transform.position, _player.position, out RaycastHit hit);
        if (playerInSight)
        {
            if (_currentState.Equals(EnemyState.Chasing))
            {
                SetState(EnemyState.Idle);
            }
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

            //if (_jumpscareSound != null)
            //    _jumpscareSound.Play();
        }
    }
    public void SetState(EnemyState newState)
    {
        Vector3 lastPlaterPos = _player.position;
        switch (_currentState)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Chasing:
                _agent.SetDestination(lastPlaterPos);
                _nape.SetActive(true);
                break;
            case EnemyState.Patrolling:
                print("Parou de patrulhar");
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
                print("patrulhando");
                _agent.SetDestination(_patrolController.MoveToNextPoint());
                StartCoroutine(Patrolling());
                break;
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitUntil(() => _agent.remainingDistance <= _agent.stoppingDistance);
        yield return new WaitForSeconds(_waitTime); //Solução temporaria em wait
        SetState(EnemyState.Patrolling);
    }
    IEnumerator Patrolling()
    {
        yield return new WaitUntil(() => _agent.remainingDistance <= _agent.stoppingDistance);
        SetState(EnemyState.Idle);
    }
}
