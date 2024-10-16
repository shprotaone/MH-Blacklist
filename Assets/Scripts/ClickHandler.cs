using UnityEngine;
using UnityEngine.EventSystems;

public class ClickHandler : MonoBehaviour, IPointerClickHandler
{
    private MonsterCell _cell;

    public void Initialize(MonsterCell cell)
    {
        _cell = cell;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _cell.ChangeState();
    }
}