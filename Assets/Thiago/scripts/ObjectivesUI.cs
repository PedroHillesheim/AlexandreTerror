using TMPro;
using UnityEngine;

public class ObjectivesUI : MonoBehaviour
{
    public TextMeshProUGUI objectivesText;

    [Header("IDs dos itens")]
    public string keyID = "Key";
    public string flashlightID = "Flashlight";
    public string cameraID = "Camera";

    void Update()
    {
        if (PlayerInventory.Instance == null)
            return;

        string key =
            PlayerInventory.Instance.HasKey(keyID)
            ? "<s>Chave</s>"
            : "Chave";

        string flashlight =
            PlayerInventory.Instance.HasKey(flashlightID)
            ? "<s>Lanterna</s>"
            : "Lanterna";

        string cameraItem =
            PlayerInventory.Instance.HasKey(cameraID)
            ? "<s>Câmera</s>"
            : "Câmera";

        objectivesText.text =
            "Pegue os itens:\n\n" +
            key + "\n" +
            flashlight + "\n" +
            cameraItem;
    }
}