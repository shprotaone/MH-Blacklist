using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterScrollView : MonoBehaviour
{
    public event Action<string> OnChangeSize;

    [SerializeField] private GridLayoutGroup _gridLayoutGroup;
    [SerializeField] private Transform _contentContainer;

    private List<MonsterCell> _cells;
    private float _startX;
    private float _startY;
    private int _currentIndex = 0;

    public Transform ContentContainer => _contentContainer;

    public void Initialize(int savedIndexSize)
    {
        _startX = _gridLayoutGroup.cellSize.x;
        _startY = _gridLayoutGroup.cellSize.y;
        _currentIndex = savedIndexSize;
        ChangeSize();
        OnChangeSize?.Invoke(_currentIndex.ToString());
        _cells = new List<MonsterCell>();
    }

    public void Clear()
    {
        for (int i = 0; i < _contentContainer.childCount; i++)
        {
            _cells[i].Disable();
        }

        _cells.Clear();
    }

    public void DecreaseCellSize()
    {
        _currentIndex--;
        ChangeSize();
        OnChangeSize?.Invoke(_currentIndex.ToString());
    }

    public void IncreaseCellSize()
    {
        _currentIndex++;
        ChangeSize();
        OnChangeSize?.Invoke(_currentIndex.ToString());
    }

    private void ChangeSize()
    {
        var changeVal = _currentIndex * 20;

        _gridLayoutGroup.cellSize = new Vector2(_startX + changeVal, _startY + changeVal);
    }

    public void AddToList(MonsterCell monsterCell)
    {
        _cells.Add(monsterCell);
    }
}
