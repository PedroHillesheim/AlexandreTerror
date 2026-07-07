using UnityEngine;
using UnityEngine.Events;

public class Note : MonoBehaviour, IInteractable
{
    [SerializeField] private bool _isOn;
    private bool _isNoteVisible = false;
    [Header("Event")]
    [SerializeField] private UnityEvent OnTurnOn;
    [SerializeField] private UnityEvent OnTurnOff;
    private Outline _outline;

    private void Start()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
    }
    private void Update()
    {
        if(!_isNoteVisible == true)
            return;
        if (Input.GetButtonDown("Jump"))
        {
            OnTurnOff.Invoke();
            _isNoteVisible = false;
            _isOn = false;
        }
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
        if (_isOn)
        {
            OnTurnOff.Invoke();
            _isNoteVisible = false;
        }
        else
        {
            OnTurnOn.Invoke();
            _isNoteVisible = true;
        }
        _isOn = !_isOn;
    }
}
