using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Outline))]

public class FinalDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private string _endScene = "End";
    private int _keys = 0;
    public UnityEvent _doorOpen;
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
        if (_keys >= 1)
        {
            _doorOpen.Invoke();
        }
    }
    public void OnKeyCollecr()
    {
        _keys++;
    }
    public void ToEndScene()
    {
        SceneManager.LoadScene(_endScene);
    }
}
