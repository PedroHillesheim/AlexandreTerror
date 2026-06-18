using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public GameObject creditosCanvas;
    public string nomeCenaJogo = "Game";

    public void Jogar()
    {
        SceneManager.LoadScene(nomeCenaJogo);
    }

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
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}