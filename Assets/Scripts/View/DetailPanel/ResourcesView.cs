using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace View.DetailPanel
{
    public class ResourcesView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        public void Fill(List<string> resourcesList)
        {
            string result = "";

            foreach (var resource in resourcesList)
            {
                result += resource;
            }
            _text.text = result;
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