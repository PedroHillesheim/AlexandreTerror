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
    }

    public void HideOutline()
    {
        throw new System.NotImplementedException("Mecãnica não feita");
    }

    public void ShowOutline()
    {
        throw new System.NotImplementedException("Mecãnica não feita");
    }
}
