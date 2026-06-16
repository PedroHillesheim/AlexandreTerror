using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Botão esquerdo
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                if (hit.transform == transform)
                {
                    PlayerInventory.Instance.hasKey = true;

                    Debug.Log("Chave obtida!");

                    Destroy(gameObject);
                }
            }
        }
    }
}