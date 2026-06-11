using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    [SerializeField] private float _interactionRange = 3f;
    private Camera _mainCam;
    private ICollectable _target; //Objeto alvo do raycast
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(_mainCam.transform.position, _mainCam.transform.forward, out RaycastHit hit, _interactionRange))
        {
            if (hit.collider.TryGetComponent(out ICollectable collectable))
            {
                if (_target == collectable)
                    return;
                _target?.HideOutline();
                _target = collectable;
                _target.ShowOutline();
            }
            else
            {
                _target?.HideOutline();
                _target = null;
            }
        }
        else
        {
            _target?.HideOutline();
            _target = null;
        }
    }
    public void OnInteract(InputValue value)
    {
        if (_target == null) //nullCheck
            return;

        _target.Collect();
    }
}
