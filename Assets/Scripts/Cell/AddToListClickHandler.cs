using Systems;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cell
{
    public class AddToListClickHandler : MonoBehaviour,IPointerEnterHandler
    {
        private MonsterCell _cell;
        private InputSystemHandler _inputHandler;
        
        public void Initialize(MonsterCell cell,InputSystemHandler inputHandler)
        {
            _cell = cell;
            _inputHandler = inputHandler;
        }

        private void AddToKillList()
        {
            _cell.AddToKillList();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(_inputHandler == null) return;
            _inputHandler.SetDoubleClickAction(AddToKillList);
        }
    }
}
