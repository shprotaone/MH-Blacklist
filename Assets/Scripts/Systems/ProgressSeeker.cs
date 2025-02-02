using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressSeeker : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private List<MonsterCell> _cells;
    public void Initialize(List<MonsterCell> cells)
    {
        _slider.maxValue = cells.Count;
        _cells = cells;
    }


    public void UpdateSlider()
    {
        int value = 0;
        foreach (var cell in _cells)
        {
            if (cell.IsDefeated) value++;
        }

        _slider.value = value;
    }
}
