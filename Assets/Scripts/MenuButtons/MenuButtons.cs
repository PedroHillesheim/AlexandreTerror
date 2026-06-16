using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private string _scene;
    public void StartButton()
    {
        SceneManager.LoadScene(_scene);
    }
    public void QuitButton()
    {
        Application.Quit();
    } 
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
