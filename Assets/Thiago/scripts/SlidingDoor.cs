using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    [Header("Movimento")]
    public Vector3 moveDirection = new Vector3(0, 3, 0);
    public float speed = 3f;

    [Header("Tranca")]
    public bool requiresKey = false;
    public string keyID = "Key1";

    [Header("Tremida da Porta Trancada")]
    public float lockedShakeAmount = 0.08f;
    public float lockedShakeSpeed = 40f;
    public float lockedShakeDuration = 0.2f;

    [Header("Batida Final")]
    public float bounceAmount = 0.08f;
    public float bounceSpeed = 12f;
    public float damping = 4f;

    private bool isOpen;
    private bool bouncing;
    private bool hasBounced;

    private bool shakingLocked;
    private float shakeTimer;
    private float bounceTimer;

    private Vector3 closedPosition;
    private Vector3 openPosition;

    void Start()
    {
        closedPosition = transform.localPosition;
        openPosition = closedPosition + moveDirection;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
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
                            shakingLocked = true;
                            shakeTimer = 0f;

                            Debug.Log("Você precisa da chave: " + keyID);
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

        // Tremida da porta trancada
        if (shakingLocked)
        {
            shakeTimer += Time.deltaTime;

            float offset =
                Mathf.Sin(shakeTimer * lockedShakeSpeed) *
                lockedShakeAmount *
                (1f - (shakeTimer / lockedShakeDuration));

            transform.localPosition =
                closedPosition +
                moveDirection.normalized * offset;

            if (shakeTimer >= lockedShakeDuration)
            {
                shakingLocked = false;
                transform.localPosition = closedPosition;
            }

            return;
        }

        // Fechar porta
        if (!isOpen)
        {
            transform.localPosition = Vector3.MoveTowards(
                transform.localPosition,
                closedPosition,
                speed * Time.deltaTime
            );
        }
        // Abrir porta
        else
        {
            if (!bouncing && !hasBounced)
            {
                transform.localPosition = Vector3.MoveTowards(
                    transform.localPosition,
                    openPosition,
                    speed * Time.deltaTime
                );

                if (Vector3.Distance(transform.localPosition, openPosition) < 0.01f)
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

                transform.localPosition =
                    openPosition +
                    moveDirection.normalized * offset;

                if (Mathf.Abs(offset) < 0.005f)
                {
                    bouncing = false;
                    hasBounced = true;
                    transform.localPosition = openPosition;
                }
            }
            else
            {
                transform.localPosition = openPosition;
            }
        }
    }
}