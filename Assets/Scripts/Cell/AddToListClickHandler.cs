using System.Collections;
using Systems;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cell
{
    public class AddToListClickHandler : MonoBehaviour,IPointerEnterHandler,IPointerDownHandler
    {
        private MonsterCell _cell;
        private InputSystemHandler _inputHandler;
        private bool _isStart;
        private float _dragTime;
        
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

        public void OnDrag(PointerEventData eventData)
        {
            _inputHandler.SetLastPoint(eventData.position.magnitude);
            Debug.Log("SetNewPoint");
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("Start " + eventData.position.magnitude);
            _inputHandler.StartDragging(eventData.position.magnitude);
            StartCoroutine(StartTimer());
        }

        private IEnumerator StartTimer()
        {
            _isStart = true;
            _dragTime = 0;
            
            while (_isStart)
            {
                _dragTime += 0.1f;
                yield return new WaitForSeconds(0.1f);
                
                if (_dragTime > 0.4f)
                {
                    _inputHandler.EndDragging();
                    _isStart = false;
                }
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            //Debug.Log("End " + eventData.position.magnitude);
            _isStart = false;
            //_inputSystemHandler.EndDragging(eventData.position.magnitude,_dragTime);
        }
    }
}
