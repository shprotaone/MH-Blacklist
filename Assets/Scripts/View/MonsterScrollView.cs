using System;
using UnityEngine;
using UnityEngine.UI;

public class MonsterScrollView : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup _gridLayoutGroup;
    [SerializeField] private Transform _contentContainer;
    public event Action<string> OnChangeSize;
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
    }

    public void Clear()
    {
        for (int i = 0; i < _contentContainer.childCount; i++)
        {
            Destroy(_contentContainer.GetChild(i).gameObject);
        }
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
}
