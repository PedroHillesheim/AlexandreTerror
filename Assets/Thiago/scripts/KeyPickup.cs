using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    [Header("ID da Chave")]
    public string keyID;

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

                    Destroy(gameObject);
                }
            }
        }
    }
}