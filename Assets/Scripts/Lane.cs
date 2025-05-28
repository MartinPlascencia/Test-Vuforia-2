using UnityEngine;

public class Lane : MonoBehaviour
{
    [SerializeField]
    private GameObject _notePrefab;
    public GameObject NotePrefab
    {
        get => _notePrefab;
    }
    [SerializeField]
    private Transform _notesOrigin;
    public Transform NotesOrigin
    {
        get => _notesOrigin;
    }
}
