using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.View
{
    public class MainPanelView : MonoBehaviour
    {
        [SerializeField] private ButtonBehaviour _riseButton;
        [SerializeField] private ButtonBehaviour _worldButton;
        [SerializeField] private ButtonBehaviour _wildsButton;
        [SerializeField] private Transform _viewPort;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private Button _settingsButton;

        public ScrollRect ScrollRect => _scrollRect;
        public Transform ViewPort => _viewPort;
        public ButtonBehaviour RiseButton => _riseButton;
        public ButtonBehaviour WorldButton => _worldButton;
        public ButtonBehaviour WildsButton => _wildsButton;
        public Button SettingsButton => _settingsButton;
    }
}