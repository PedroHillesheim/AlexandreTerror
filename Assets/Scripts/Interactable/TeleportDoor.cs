using UnityEngine;

public enum DoorFloor
{
    Low,
    High
}
[RequireComponent(typeof(Outline))]
public class TeleportDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private DoorFloor _currentState;
    private Transform _lowFloorDoor;
    private Transform _highFloorDoor;
    private Transform _playerTransform;
    private Outline _outline;
    private CharacterController characterController;

    public void ShowOutline()
    {
        if (_outline != null)
        {
            _outline.enabled = true;
        }
    }

    public void HideOutline()
    {
        if (_outline != null)
        {
            _outline.enabled = false;
        }
    }

    public void Interact()
    {
        if( _currentState == DoorFloor.Low )
        {
            characterController.enabled = false;
            _playerTransform.position = _highFloorDoor.position;
            characterController.enabled = true;
        }
        else if ( _currentState == DoorFloor.High)
        {
            characterController.enabled = false;
            _playerTransform.position = _lowFloorDoor.position;
            characterController.enabled = true;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GameController.Instance.Charactercontroller;
        _highFloorDoor = GameController.Instance.DoorHighFloorTransform;
        _lowFloorDoor = GameController.Instance.DoorLowFloorTransform;
        _playerTransform = GameController.Instance.PlayerTransform;
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
    }
}
