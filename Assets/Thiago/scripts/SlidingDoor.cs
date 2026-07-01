using UnityEngine;
using UnityEngine.SceneManagement;

public class SlidingDoor : MonoBehaviour
{
    [Header("Movimento")]
    public Vector3 moveDirection = new Vector3(0, 3, 0);
    public float speed = 3f;

    [Header("Troca de Cena")]
    public bool changeSceneAfterOpen = false;
    public string sceneName;

    [Header("Itens Necessários")]
    public bool requiresItems = false;

    public string requiredKey = "Key";
    public string requiredFlashlight = "Flashlight";
    public string requiredCamera = "Camera";

    [Header("Sons")]
    public AudioSource audioSource;
    public AudioClip openSound;
    public AudioClip closeSound;
    public AudioClip lockedSound;

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
    private bool sceneChanged;

    private float shakeTimer;
    private float bounceTimer;

    private Vector3 closedPosition;
    private Vector3 openPosition;

    void Start()
    {
        closedPosition = transform.localPosition;
        openPosition = closedPosition + moveDirection;

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
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
                    if (requiresItems && !isOpen)
                    {
                        if (PlayerInventory.Instance == null)
                        {
                            Debug.LogWarning("PlayerInventory não encontrado!");
                            return;
                        }

                        bool hasEverything =
                            PlayerInventory.Instance.HasKey(requiredKey) &&
                            PlayerInventory.Instance.HasKey(requiredFlashlight) &&
                            PlayerInventory.Instance.HasKey(requiredCamera);

                        if (!hasEverything)
                        {
                            shakingLocked = true;
                            shakeTimer = 0f;

                            if (audioSource != null && lockedSound != null)
                                audioSource.PlayOneShot(lockedSound);

                            Debug.Log("Você precisa da chave, lanterna e câmera.");
                            return;
                        }
                    }

                    // Alterna entre abrir e fechar
                    isOpen = !isOpen;
                    hasBounced = false;
                    bouncing = false;

                    if (audioSource != null)
                    {
                        if (isOpen)
                        {
                            if (openSound != null)
                                audioSource.PlayOneShot(openSound);
                        }
                        else
                        {
                            if (closeSound != null)
                                audioSource.PlayOneShot(closeSound);
                        }
                    }
                }
            }
        }

        // Tremida quando trancada
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

        // Movimento da porta
        Vector3 targetPosition = isOpen ? openPosition : closedPosition;

        if (!bouncing)
        {
            transform.localPosition = Vector3.MoveTowards(
                transform.localPosition,
                targetPosition,
                speed * Time.deltaTime
            );

            if (Vector3.Distance(transform.localPosition, targetPosition) < 0.01f)
            {
                transform.localPosition = targetPosition;

                if (isOpen && !hasBounced)
                {
                    bouncing = true;
                    bounceTimer = 0f;
                }
            }
        }
        else
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

                if (changeSceneAfterOpen && !sceneChanged)
                {
                    sceneChanged = true;
                    SceneManager.LoadScene(sceneName);
                }
            }
        }
    }
}