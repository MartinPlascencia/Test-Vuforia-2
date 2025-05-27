using System.Collections.Generic;
using UnityEngine;

public class NotesChecker : MonoBehaviour
{
    private List<GameObject> _notes = new List<GameObject>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            _notes.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            _notes.Remove(collision.gameObject);
        }
    }
    public void DestroyNotes()
    {
        while (_notes.Count > 0)
        {
            GameObject note = _notes[0];
            _notes.RemoveAt(0);
            Destroy(note);
        }
        _notes.Clear();
    }
}
