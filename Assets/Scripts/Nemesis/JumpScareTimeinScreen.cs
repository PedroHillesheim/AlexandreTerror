using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class JumpScareTimeinScreen : MonoBehaviour
{
    [SerializeField] float _jumpScareTime = 1f;
    [Space]
    [SerializeField] UnityEvent _jumpScareDesactivate;

    public void StartJumpScareTimeCoroutine()
    {
        StartCoroutine(JumpScareTime());
    }
    IEnumerator JumpScareTime()
    {
        yield return new WaitForSeconds(_jumpScareTime);
        _jumpScareDesactivate.Invoke();
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}