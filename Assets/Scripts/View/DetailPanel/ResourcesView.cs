using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View.DetailPanel
{
    public class ResourcesView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private RectTransform _rect;

        private void OnEnable()
        {
            _rect = GetComponent<RectTransform>();
        }

        public void Fill(List<string> resourcesList)
        {
            string result = "";
            if (resourcesList != null)
            {
                foreach (var resource in resourcesList)
                {
                    result += resource;
                }                
            }
            
            _text.text = result;

            LayoutRebuilder.ForceRebuildLayoutImmediate(_rect);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}