using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Idle,
    Chasing,
    Patrolling
}

public class Enemy : MonoBehaviour
{
    //private PatrolController _patrolController;
    private GameObject _nape;
    private Transform _player;
    private NavMeshAgent _agent;
    private EnemyState _currentState;
    [SerializeField][Range(0.5f, 5)] private float _waitTime = 2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _nape = transform.GetChild(0).gameObject;
        //_patrolController = GameController.Instance.PatrolController;
        _player = GameController.Instance.PlayerTransform;
        _agent = GetComponent<NavMeshAgent>();
        SetState(EnemyState.Patrolling); //Só para testar
    }

    // Update is called once per frame
    void Update()
    {
        Vision();
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
                //_agent.SetDestination(_patrolController.MoveToNextPoint());
                StartCoroutine(Patrilling());
                break;
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitUntil(() => _agent.remainingDistance <= _agent.stoppingDistance);
        yield return new WaitForSeconds(_waitTime); //Solução temporaria em wait
        SetState(EnemyState.Patrolling);
    }
    IEnumerator Patrilling()
    {
        yield return new WaitUntil(() => _agent.remainingDistance <= _agent.stoppingDistance);
        SetState(EnemyState.Idle);
    }
}
