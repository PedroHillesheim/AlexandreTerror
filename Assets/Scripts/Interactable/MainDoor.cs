using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MainDoor : MonoBehaviour, IInteractable
{
    [Header("Text")]
    private TMP_Text _warningText;
    private GameObject _warningTextGameObject;
    [Header("Keys")]
    private int _keys = 0;
    [SerializeField] private int _maxKeys = 3;
    [Space]
    [Header("Event")]
    [SerializeField] private UnityEvent OnDoorOpen;
    private Outline _outline;

    private void Start()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
        _warningTextGameObject = GameController.Instance.WarningTextGameObject;
        _warningText = GameController.Instance.WarningText;
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
            _warningTextGameObject.SetActive(true);
            _warningText.text = _keys + "/" + _maxKeys;
            StartCoroutine(TextAppear());
        }
        else
        {
            OnDoorOpen.Invoke();
        }
    }
    public void KeyCollect()
    {
        _keys++;
    }
    IEnumerator TextAppear()
    {
        yield return new WaitForSeconds(3f);
        _warningTextGameObject.SetActive(false);
    }
}
