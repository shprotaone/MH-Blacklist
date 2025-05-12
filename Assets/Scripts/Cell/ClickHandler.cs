using UnityEngine;
using UnityEngine.EventSystems;

public class ClickHandler : MonoBehaviour, IPointerClickHandler
{
    private MonsterCell _cell;
    private float _time;
    private bool _isPressed;
    private int _holdTime = 1;

    public void Initialize(MonsterCell cell)
    {
        _cell = cell;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _cell.ChangeState();
    }

    /*public void OnPointerDown(PointerEventData eventData)
    {
        _isPressed = true;
        StartCoroutine(Timer());
        Debug.Log(_time);

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPressed = false;
        _time = 0;
    }

    private IEnumerator Timer()
    {
        while (_isPressed)
        {
            _time += Time.deltaTime;

            if (_holdTime < _time)
            {
                _isPressed = false;
                _time = 0;
                _cell.CallDetailInfo();
            }

            yield return new WaitForEndOfFrame();
        }
    }*/
}