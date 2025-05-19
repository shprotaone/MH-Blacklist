using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Systems
{
    public class InputSystemHandler
    {
        private readonly float dragDeltaThreshold = 20;
        
        private Action _currentAction;
        private CustomInputActions _actions;
        private float _dragDelta;

        public InputSystemHandler()
        {
        }

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
        }

        public void EndDragging(float positionMagnitude, float time)
        {
            _dragDelta -= positionMagnitude;
            if (time > 0.4f && Mathf.Abs(_dragDelta) < dragDeltaThreshold)
            {
                _currentAction?.Invoke();
            }

            Debug.Log("Drag delta " + Mathf.Abs(_dragDelta));
        }
    }
}