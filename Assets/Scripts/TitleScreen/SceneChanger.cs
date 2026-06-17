using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public GameObject creditosCanvas;

    public void AbrirCreditos()
    {
        creditosCanvas.SetActive(true);
    }

    public void FecharCreditos()
    {
        creditosCanvas.SetActive(false);
    }

    public void SairJogo()
    {
        Application.Quit();
    }
}