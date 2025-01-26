using UnityEngine;
using UnityEngine.UI;

public class ProgressSeeker : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private Transform _contentContainer;
    public void Initialize(Transform contentContainer)
    {
        _slider.maxValue = contentContainer.childCount;
        _contentContainer = contentContainer;
    }


    public void UpdateSlider()
    {
        int value = 0;
        foreach (var cell in _contentContainer.GetComponentsInChildren<MonsterCell>(true))
        {
            if (cell.IsDefeated) value++;
        }

        _slider.value = value;
    }
}
