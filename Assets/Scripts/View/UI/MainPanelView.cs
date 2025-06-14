using UnityEngine;
using UnityEngine.UI;

namespace View.UI
{
    public class MainPanelView : MonoBehaviour
    {
        [SerializeField] private Transform _viewPort;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private Button _settingsButton;

        public ScrollRect ScrollRect => _scrollRect;
        public Transform ViewPort => _viewPort;

        public Button SettingsButton => _settingsButton;
    }
}