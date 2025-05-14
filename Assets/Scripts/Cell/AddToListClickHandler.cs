using UnityEngine;
using UnityEngine.EventSystems;

namespace Cell
{
    public class AddToListClickHandler : MonoBehaviour,IPointerClickHandler
    {
        private MonsterCell _cell;

        public void Initialize(MonsterCell cell)
        {
            _cell = cell;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.clickCount == 2)
            {
                _cell.AddToKillList();
            }
        }
    }
}
