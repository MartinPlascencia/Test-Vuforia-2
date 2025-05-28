using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class NotesManager : MonoBehaviour
{
    [SerializeField]
    private List<Lane> _lanes = new List<Lane>();
    [SerializeField]
    private float _notesCheckInterval = 0.1f;
    [SerializeField]
    private UnityEvent _onNotesFinished;
    [SerializeField]
    private float _finishTime = 1.5f;
    private float _currentSpeed;
    private NoteChart _currentChart;
    private Coroutine _spawnCoroutine;
    private float _timer;

    public void StartNotes(TextAsset notesConfig, float speed)
    {
        _timer = 0f;
        _currentSpeed = speed;
        _currentChart = JsonUtility.FromJson<NoteChart>(notesConfig.text);
        _spawnCoroutine = StartCoroutine(SpawnNotes());
    }

    private IEnumerator SpawnNotes()
    {
        while (_currentChart.notes.Count > 0)
        {
            yield return new WaitForSeconds(_notesCheckInterval);
            _timer += _notesCheckInterval;
            List<NoteData> notesToSpawn = _currentChart.notes.FindAll(note => note.time <= _timer);
            foreach (NoteData noteData in notesToSpawn)
            {
                if (noteData.lane < 0 || noteData.lane >= _lanes.Count)
                {
                    Debug.LogWarning($"Invalid lane index {noteData.lane} for note at time {noteData.time}");
                    continue;
                }

                Lane lane = _lanes[noteData.lane];
                GameObject noteObject = Instantiate(lane.NotePrefab, lane.NotesOrigin.position, Quaternion.identity);
                Note note = noteObject.GetComponent<Note>();
                if (note != null)
                {
                    note.Speed = _currentSpeed;
                }
                noteObject.transform.SetParent(lane.transform);
                notesToSpawn.Remove(noteData);
            }
        }
        yield return new WaitForSeconds(_finishTime);
        _spawnCoroutine = null;
        _onNotesFinished?.Invoke();
    }

    public void StopNotes()
    {
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }
    }
}
