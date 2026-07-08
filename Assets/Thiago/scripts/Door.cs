using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Configuração")]
    public float openAngle = 90f;
    public float speed = 180f;

    [Header("Tranca")]
    public bool requiresKey = false;

    [Header("ID da Chave Necessária")]
    public string keyID;

    [Header("Tremida da Porta Trancada")]
    public float lockedShakeAngle = 10f;
    public float lockedShakeSpeed = 35f;
    public float lockedShakeDuration = 0.2f;

    [Header("Efeito de Batida")]
    public float bounceAmount = 6f;
    public float bounceSpeed = 8f;
    public float damping = 3.5f;

    private bool isOpen = false;
    private bool bouncing = false;
    private bool hasBounced = false;

    private bool shakingLocked = false;
    private float shakeTimer = 0f;

    private float bounceTimer = 0f;

    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        closedRotation = transform.rotation;
        openRotation = closedRotation * Quaternion.Euler(0, 0, openAngle);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(
                new Vector3(Screen.width / 2f, Screen.height / 2f)
            );

            if (Physics.Raycast(ray, out RaycastHit hit, 3f))
            {
                if (hit.transform == transform)
                {
                    if (requiresKey)
                    {
                        if (PlayerInventory.Instance == null)
                        {
                            Debug.LogWarning("PlayerInventory não encontrado!");
                            return;
                        }

                        if (!PlayerInventory.Instance.HasKey(keyID))
                        {
                            Debug.Log("Você precisa da chave: " + keyID);

                            shakingLocked = true;
                            shakeTimer = 0f;

                            return;
                        }
                    }

                    isOpen = !isOpen;

                    if (isOpen)
                    {
                        hasBounced = false;
                    }
                    else
                    {
                        bouncing = false;
                    }
                }
            }
        }

        if (shakingLocked)
        {
            shakeTimer += Time.deltaTime;

            float offset =
                Mathf.Sin(shakeTimer * lockedShakeSpeed) *
                lockedShakeAngle *
                (1f - (shakeTimer / lockedShakeDuration));

            transform.rotation =
                closedRotation *
                Quaternion.Euler(0, 0, offset);

            if (shakeTimer >= lockedShakeDuration)
            {
                shakingLocked = false;
                transform.rotation = closedRotation;
            }

            return;
        }

        if (!isOpen)
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                closedRotation,
                speed * Time.deltaTime
            );
        }
        else
        {
            if (!bouncing && !hasBounced)
            {
                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation,
                    openRotation,
                    speed * Time.deltaTime
                );

                if (Quaternion.Angle(transform.rotation, openRotation) < 0.1f)
                {
                    bouncing = true;
                    bounceTimer = 0f;
                }
            }
            else if (bouncing)
            {
                bounceTimer += Time.deltaTime;

                float offset =
                    Mathf.Sin(bounceTimer * bounceSpeed) *
                    bounceAmount *
                    Mathf.Exp(-damping * bounceTimer);

                transform.rotation =
                    openRotation *
                    Quaternion.Euler(0, 0, offset);

                if (Mathf.Abs(offset) < 0.05f)
                {
                    bouncing = false;
                    hasBounced = true;
                    transform.rotation = openRotation;
                }
            }
            else
            {
                transform.rotation = openRotation;
            }
        }
    }
}