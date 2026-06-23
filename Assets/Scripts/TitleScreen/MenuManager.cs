using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Nome da Cena do Jogo")]
    [SerializeField] private string gameScene;

    [Header("Canvas de Créditos")]
    [SerializeField] private GameObject creditsCanvas;

    private void Start()
    {
        if (creditsCanvas != null)
            creditsCanvas.SetActive(false);
    }

    // Botão Play
    public void PlayGame()
    {
        SceneManager.LoadScene(gameScene);
    }

    // Botão Créditos
    public void OpenCredits()
    {
        if (creditsCanvas != null)
            creditsCanvas.SetActive(true);
    }

    // Botão Voltar dos Créditos
    public void CloseCredits()
    {
        if (creditsCanvas != null)
            creditsCanvas.SetActive(false);
    }

    // Botão Sair
    public void QuitGame()
    {
        Debug.Log("Fechando jogo...");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}