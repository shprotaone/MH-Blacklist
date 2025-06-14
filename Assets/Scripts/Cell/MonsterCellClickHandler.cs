using UnityEngine;
using UnityEngine.EventSystems;

namespace Cell
{
    public class MonsterCellClickHandler : MonoBehaviour, IPointerClickHandler
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
            _cell.CallDetailInfo();
        }
    }
}