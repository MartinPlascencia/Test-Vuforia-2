using UnityEngine;
using UnityEngine.Events;

public class DanceController : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _onActivateSelectDance;
    [SerializeField]
    private UnityEvent _onSelectDance;
    public void ActivateSelectDance(){
        _onActivateSelectDance?.Invoke();
    }

    public void SelectDance(){
        _onSelectDance?.Invoke();
    }
}
