using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Outline))]

public class FinalKey : MonoBehaviour, ICollectable
{
    public UnityEvent _finalKeyCollected;
    private Outline _outline;

    private void Start()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
    }
    public void Collect()
    {
        _finalKeyCollected.Invoke();
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
}
