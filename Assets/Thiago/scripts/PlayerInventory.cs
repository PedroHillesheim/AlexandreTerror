using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    private HashSet<string> keys = new HashSet<string>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Debug.Log("PlayerInventory carregado");
    }

    public void AddKey(string keyID)
    {
        keys.Add(keyID);
        Debug.Log("Chave obtida: " + keyID);
    }

    public bool HasKey(string keyID)
    {
        return keys.Contains(keyID);
    }
}