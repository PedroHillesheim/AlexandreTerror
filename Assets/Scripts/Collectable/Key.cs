using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Key : MonoBehaviour, ICollectable
{
    [SerializeField] private UnityEvent _OnKeyCollect;
    private Outline _outline;
    void Start()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
    }
    public void Collect()
    {
        _OnKeyCollect.Invoke();
        Destroy(gameObject);
    }

    public void HideOutline()
    {
        if (_outline != null)
        {
            _outline.enabled = false;
        }
    }

    public void ShowOutline()
    {
        if (_outline != null)
        {
            _outline.enabled = true;
        }
    }
}
