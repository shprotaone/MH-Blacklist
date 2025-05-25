using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Systems
{
    public class InputSystemHandler
    {
        public readonly float dragTime = 0.4f;
        private readonly float dragDeltaThreshold = 20;

        private Action _currentAction;
        private CustomInputActions _actions;
        private float _dragDelta;
        private float _lastPosition;

        public void SetDoubleClickAction(Action action)
        {
            if (_currentAction == action)
            {
                Debug.Log("SameAction");
                return;
            }
            _currentAction = null;
            _currentAction = action;
            Debug.Log("SetAction");
        }
        
        

        public void StartDragging(float positionSqrMagnitude)
        {
            _dragDelta = positionSqrMagnitude;
            _lastPosition = positionSqrMagnitude;
        }

        public void EndDragging(float positionMagnitude, float time)
        {
            _dragDelta -= positionMagnitude;
            if (time > dragTime && Mathf.Abs(_dragDelta) < dragDeltaThreshold)
            {
                _currentAction?.Invoke();
            }
        }
        
        public void EndDragging()
        {
            _dragDelta -= _lastPosition;
            if (Mathf.Abs(_dragDelta) < dragDeltaThreshold)
            {
                Handheld.Vibrate();
                _currentAction?.Invoke();    
            }
        }

        public void SetLastPoint(float positionMagnitude)
        {
            _lastPosition = positionMagnitude;
        }
    }
}