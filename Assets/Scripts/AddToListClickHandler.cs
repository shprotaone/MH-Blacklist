using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AddToListClickHandler : MonoBehaviour
{
    private MonsterCell _cell;

    public void Initialize(MonsterCell cell)
    {
        _cell = cell;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _cell.AddToKillList();
    }
}
