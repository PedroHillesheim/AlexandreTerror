using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainDoor : MonoBehaviour, IInteractable
{
    private int _keys = 0;
    [SerializeField] private int _maxKeys = 3;
    [Header("Event")]
    [SerializeField] private UnityEvent OnDoorOpen;
    private Outline _outline;

    private void Start()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
    }

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
        if (_keys < _maxKeys)
        {
            Debug.LogError("Mecânica a ser finalizada");
        }
        else
        {
            OnDoorOpen.Invoke();
        }
    }
}
