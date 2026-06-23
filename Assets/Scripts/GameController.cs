using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; } //Singleton
    public Transform PlayerTransform { get => _playerTransform; }
    public PatrolController PatrolController { get => _patrolController; }
    public UnityEvent JumpscareUI { get => _jumpscareUI; }
    public Transform NemesisTransform { get => _nemesisTransform; }

    [Header("Scene Reference")]
    [Space]
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _nemesisTransform;
    [SerializeField] private PatrolController _patrolController;
    [Space]
    [Header("Events")]
    [SerializeField] private UnityEvent _jumpscareUI;
    //public UnityEvent OnUseBattery;
    //public UnityEvent OnUseFlashlight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
    }
}
