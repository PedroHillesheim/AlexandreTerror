using System.Collections;
using UnityEngine;
using UnityEngine.Events;

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
}