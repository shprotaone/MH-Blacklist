using System.Collections.Generic;
using Cell;
using UnityEngine;
using UnityEngine.UI;

namespace Systems
{
    public class ProgressSeekerView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        private List<MonsterCell> _cells = new();
        public void Initialize(List<MonsterCell> cells)
        {
            _slider.maxValue = cells.Count;
            _cells.Clear();
            _cells.AddRange(cells);
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
}
