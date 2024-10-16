using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.View
{
    public class MainPanelView : MonoBehaviour
    {
        [SerializeField] private Button _riseButton;
        [SerializeField] private Button _worldButton;
        [SerializeField] private Button _wildsButton;
        [SerializeField] private Transform _viewPort;
        [SerializeField] private ScrollRect _scrollRect;

        public ScrollRect ScrollRect => _scrollRect;
        public Transform ViewPort => _viewPort;
        public Button RiseButton => _riseButton;
        public Button WorldButton => _worldButton;
        public Button WildsButton => _wildsButton;
    }
}