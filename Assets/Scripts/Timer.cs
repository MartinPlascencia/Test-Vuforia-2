using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _onTimerComplete;
    [SerializeField]
    private UnityEvent<string> _onSecondPassed;
    private Coroutine _timerCoroutine;
    public void StartTimer(float duration)
    {
        _timerCoroutine = StartCoroutine(RunTimer(duration));
    }

    private IEnumerator RunTimer(float duration)
    {
        _onSecondPassed?.Invoke(""+(int)duration);
        yield return new WaitForSeconds(1f);
        if (duration == 0)
        {
            _onTimerComplete?.Invoke();
            _timerCoroutine = null;
        }
        else
        {
            _timerCoroutine = StartCoroutine(RunTimer(duration - 1));
        }
    }

    public void StopTimer()
    {
        if (_timerCoroutine != null)
        {
            StopCoroutine(_timerCoroutine);
            _timerCoroutine = null;
        }
    }
}
