using UnityEngine;
using UnityEngine.Events;

public class DanceController : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _onActivateSelectDance;
    [SerializeField]
    private UnityEvent _onSelectDance;
    [SerializeField]
    private Animator _characterAnimator;
    [SerializeField]
    private NotesManager _notesManager;
    private SoundData _currentSoundData;
    public void ActivateSelectDance()
    {
        _onActivateSelectDance?.Invoke();
    }

    public void SelectDance(SoundData soundData)
    {
        _currentSoundData = soundData;
        _onSelectDance?.Invoke();
    }
    public void StartDance()
    {
        _characterAnimator.Play(_currentSoundData.danceName);
        SoundManager.instance.PlayMusic(_currentSoundData.musicName);
        _notesManager.StartNotes(_currentSoundData.notesConfig, _currentSoundData.speed);
    }
}
