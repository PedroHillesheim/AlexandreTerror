using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    public bool hasKey = false;

    private void Awake()
    {
        Instance = this;
        Debug.Log("PlayerInventory carregado");
    }
}