using UnityEngine;
using UnityEngine.Events;

public class Note : MonoBehaviour, IInteractable
{
    [SerializeField] private bool _isOn;
    [Header("Event")]
    [SerializeField] private UnityEvent OnTurnOn;
    [SerializeField] private UnityEvent OnTurnOff;
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
        if (_isOn)
        {
            OnTurnOff.Invoke();
        }
        else
        {
            OnTurnOn.Invoke();
        }
        _isOn = !_isOn;
        //Animação do interruptor mudando o botão
    }
}
