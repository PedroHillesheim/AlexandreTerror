using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class JumpScareTimeinScreen : MonoBehaviour
{
    [SerializeField] float _jumpScareTime = 4.8f;
    [SerializeField] private AudioSource _jumpscareSound;
    [Space]
    [SerializeField] UnityEvent _jumpScareDesactivate;

    public void StartJumpScareTimeCoroutine()
    {
        StartCoroutine(JumpScareTime());
    }
    IEnumerator JumpScareTime()
    {
        _jumpscareSound.Play();
        yield return new WaitForSeconds(_jumpScareTime);
        _jumpScareDesactivate.Invoke();
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}