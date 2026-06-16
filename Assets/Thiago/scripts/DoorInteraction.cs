using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Configuração")]
    public float openAngle = 90f;
    public float speed = 2f;

    private bool isOpen = false;

    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        closedRotation = transform.rotation;
        openRotation = closedRotation * Quaternion.Euler(0, openAngle, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = Camera.main.ScreenPointToRay(
                new Vector3(Screen.width / 2, Screen.height / 2)
            );

            if (Physics.Raycast(ray, out RaycastHit hit, 3f))
            {
                if (hit.transform == transform)
                {
                    isOpen = !isOpen;
                }
            }
        }

        Quaternion targetRotation = isOpen ? openRotation : closedRotation;

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            speed * Time.deltaTime
        );
    }
}