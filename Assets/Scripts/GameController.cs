using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; } //Singleton
    public Transform PlayerTransform { get => _playerTransform; }
    public PatrolController PatrolController { get => _patrolController; }
    public UnityEvent JumpscareUI { get => _jumpscareUI; }

    [Header("Scene Reference")]
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private PatrolController _patrolController;
    [SerializeField] private UnityEvent _jumpscareUI;
    //[Space]
    //[Header("Events")]
    //public UnityEvent OnUseBattery;
    //public UnityEvent OnUseFlashlight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
    }
}
