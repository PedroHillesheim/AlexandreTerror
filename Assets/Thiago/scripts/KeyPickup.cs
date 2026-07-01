using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    [Header("ID da Chave")]
    public string keyID;

    [Header("Som")]
    public AudioClip pickupSound;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                if (hit.transform == transform)
                {
                    if (PlayerInventory.Instance != null)
                    {
                        PlayerInventory.Instance.AddKey(keyID);
                    }

                    // Toca o som antes de destruir a chave
                    if (pickupSound != null)
                    {
                        AudioSource.PlayClipAtPoint(pickupSound, transform.position);
                    }

                    Destroy(gameObject);
                }
            }
        }
    }
}